import React from "react";
import { useLocation } from "react-router-dom";

function Login() {
  const handleSubmit = async (event) => {};

  const location = useLocation();
  const message = location.state || {}; // Fallback to empty object if no data

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
      {message}
    </div>
  );
}

export default Login;
