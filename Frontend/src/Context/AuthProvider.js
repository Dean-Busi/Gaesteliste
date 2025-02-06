import { createContext, useState, useEffect } from "react";
import { useLocation } from "react-router-dom";
import axios from "axios";

const AuthContext = createContext({});

const api = axios.create({ baseURL: "http://localhost:5177" });

export const AuthProvider = ({ children }) => {
  const [auth, setAuth] = useState({ isAuthenticated: false, user: null });
  const [loading, setLoading] = useState(true);
  const location = useLocation();

  useEffect(() => {
    const verifyTokens = async () => {
      setLoading(true);

      const sessionStorageToken = sessionStorage.getItem("accessToken");

      if (sessionStorageToken) {
        setAuth({ isAuthenticated: true, user: true });
        setLoading(false);
        return;
      }

      try {
        const response = await api.post("/api/account/refreshToken",
        {}, 
        { withCredentials: true });

        if (response.data.accessToken) {
          setAuth({ isAuthenticated: true, user: response.data.user });

        } else {
          setAuth({ isAuthenticated: false, user: null });
        }

      } catch (error) {
        console.error("Error during token verification:", error);
        setAuth({ isAuthenticated: false, user: null });
        
      } finally {
        setLoading(false);
      }
    };

    verifyTokens();
  }, [location]);

  const logout = async () => {
    try {
      await api.post("/api/account/logout", {}, { withCredentials: true });
      setAuth({ isAuthenticated: false, user: null });
      sessionStorage.removeItem("accessToken");

    } catch (error) {
      console.log("Logout error:", error);
    }
  };

  return (
    <AuthContext.Provider value={{ auth, setAuth, loading, logout }}>
      {loading 
      ? (<div>Loading...</div>) 
      : (children)}
    </AuthContext.Provider>
  );
};

export default AuthContext;
