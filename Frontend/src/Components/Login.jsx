import React from "react";

function Login() {
  const handleSubmit = async (event) => {};

  return (
    <div>
      <h1>Login</h1>
      <label>Email: </label>
      <input type="email" />
      <br /> <br />
      <label>Passwort: </label>
      <input type="password" />
      <br /> <br />
      <label for="rememberMe">Remember Me</label>
      <input type="checkbox" id="rememberMe" name="rememberMe" />
      <br /> <br />
      <button type="submit" onClick={handleSubmit}>
        Login
      </button>
    </div>
  );
}

export default Login;
