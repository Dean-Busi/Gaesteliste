// import "../App.css";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { NavLink } from "react-router";
import napoleon from "../Assets/Bilder/napoleon.webp"

function SignUp(props) {
  const [email, setEmail] = useState();
  const [password, setPassword] = useState();
  const [confirmPassword, setConfirmPassword] = useState();
  const [errorMessage, setErrorMessage] = useState();

  const navigate = useNavigate();

  const api = axios.create({ baseURL: "http://localhost:5068" });

  const handleSubmit = async (event) => {
    event.preventDefault();
    const newUser = { email, password, confirmPassword };

    try {
      const response = await api.post("/api/account/register", newUser);
      console.log(response.data.message);
      setErrorMessage("");
      navigate("/login");
    } catch (error) {
      console.log(error);
      setErrorMessage(error.response.data);
    }
  };

  return (
    <div className="App">
      <h1>Registrieren</h1>

      <img src={napoleon} alt="Napoleon" id="napoleon"/>
      
      <form>
        <br />
        <label>Email: </label>
        <input
          type="email"
          value={email}
          name="email"
          onChange={(e) => setEmail(e.target.value)}
        />
        <br /> <br />
        <label>Passwort: </label>
        <input
          type="password"
          value={password}
          name="password"
          onChange={(e) => setPassword(e.target.value)}
        />
        <br /> <br />
        <label>Passwort bestätigen: </label>
        <input
          type="password"
          value={confirmPassword}
          name="confirmPassword"
          onChange={(e) => setConfirmPassword(e.target.value)}
        />
        <br /> <br />
        <button type="submit" onClick={handleSubmit}>
          Registrieren
        </button>
        <br /> <br />
        <p style={{ color: "red" }}>{errorMessage}</p>
      </form>

      <h2>Schon registriert?</h2>

      <br />

      <NavLink to="/login">
        <h2>Melde dich an</h2>
      </NavLink>

      <br /> <br />

      <h2>Vorteile beim Registrieren:</h2>
    
      • Kontaktiere den Schöpfer (mich) <br />
      • Bewerte und kommentiere die Gemälder

      <br /> <br />
      
    </div>
  );
}

export default SignUp;

