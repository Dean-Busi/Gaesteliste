using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Dtos.Authentication;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly TokenGenerator _tokenGenerator;

        public AuthController(ApplicationDbContext context, UserManager<User> userManager, SignInManager<User> signInManager,
        TokenGenerator tokenGenerator)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenGenerator = tokenGenerator;
        }

        //////////////////////////////////////////////////////////////

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var emailExists = await _context.Users.
                FirstOrDefaultAsync(x => x.Email == registerDto.Email);

                if (emailExists != null)
                {
                    return BadRequest("Diese Email wurde bereits registriert.");
                }

                var user = new User
                {
                    UserName = registerDto.Email,
                    Email = registerDto.Email,
                    RefreshToken = null,
                    RefreshTokenExpiryTime = null
                };

                var createdUser = await _userManager.CreateAsync(user, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "User");

                    if (roleResult.Succeeded)
                    {
                        return Ok(new
                        {
                            Message = "Du hast dich erfolgreich registriert.",

                            User = new RegisterResponseDto
                            {
                                Email = user.Email,
                            }
                        });
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync
            (x => x.Email == loginDto.Email.ToLower());

            if (user == null) return Unauthorized("Login-Eingaben waren inkorrekt, bitte erneut versuchen.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Login-Eingaben waren inkorrekt, bitte erneut versuchen.");

            var accessToken = _tokenGenerator.CreateAccessToken(user);
            var refreshToken = _tokenGenerator.CreateRefreshToken();
            var refreshTokenExpiryTime = DateTime.Now.AddDays(30);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = refreshTokenExpiryTime;
            await _userManager.UpdateAsync(user);

            Response.Cookies.Append("accessToken", accessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.Now.AddMinutes(30)
            });

            Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = refreshTokenExpiryTime
            });

            return Ok(new
            {
                Message = "Du wurdest erfolgreich eingeloggt.",

                User = new LoginResponseDto
                {
                    Email = user.Email,
                    AccessToken = accessToken
                }
            });
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized("Kein Refresh Token vorhanden.");

            var userToken = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);

            if (userToken == null || userToken.RefreshTokenExpiryTime <= DateTime.Now)
                return Unauthorized("Ungültiger oder abgelaufener Refresh Token.");

            var newAccessToken = _tokenGenerator.CreateAccessToken(userToken);
            var newRefreshToken = _tokenGenerator.CreateRefreshToken();
            var newRefreshTokenExpiryTime = DateTime.Now.AddDays(30);

            userToken.RefreshToken = newRefreshToken;
            userToken.RefreshTokenExpiryTime = newRefreshTokenExpiryTime;
            await _userManager.UpdateAsync(userToken);

            Response.Cookies.Append("accessToken", newAccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.Now.AddMinutes(30)
            });

            Response.Cookies.Append("refreshToken", newRefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = newRefreshTokenExpiryTime
            });

            return Ok(new
            {
                Message = "Tokens erfolgreich aktualisiert.",
                AccessToken = newAccessToken
            });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("accessToken");
            Response.Cookies.Delete("refreshToken");

            return Ok("Du hast dich erfolgreich ausgeloggt.");
        }
    }
}

