import { createContext, useContext, useEffect, useState, type ReactNode } from "react";
import { Login, Logout } from "../services/AuthApiServices";
import { useNavigate } from "react-router";
import type { AuthContextType } from "../types/auth/AuthContextType";
import type { User } from "../types/auth/UserType";
import { jwtDecode, type JwtDecodeOptions } from "jwt-decode";

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: { children: ReactNode }) {
  const navigate = useNavigate();

  const [user, setUser] = useState<User | null>(null);
  const [token, setToken] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    console.log("triggered");

    const storedToken = localStorage.getItem("accessToken");

    if (storedToken) {
      setToken(storedToken);

      const decodedUser = jwtDecode<User>(storedToken);
      setUser(decodedUser);

      navigate("home", { replace: false });
    }

    setIsLoading(false);
  }, []);

  //TODO: continue working with the login context
  async function login(username: string, password: string) {
    // Guard for existing jwt
    // if (user) {
    //   navigate("home");
    //   return;
    // }
    const res = await Login(username, password);
    if (!res.IsSuccess || !res.Value) throw new Error(res.Message);
    const data = res.Value;

    localStorage.setItem("accessToken", data);
    setToken(data);

    const decodedUser = jwtDecode<User>(data);
    setUser(decodedUser);

    console.log(res.Message);

    navigate("home");
  }

  async function logout() {
    try {
      const res = await Logout();
      if (!res.IsSuccess) console.log(res.Message);

      localStorage.removeItem("accessToken");
      setToken(null);

      navigate("auth/login");
    } catch (err) {
      console.log(err);
    }
  }

  return (
    <AuthContext.Provider
      value={{
        user,
        token,
        isAuthenticated: !!token,
        isLoading,
        login,
        logout,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  const context = useContext(AuthContext);

  if (!context) {
    throw new Error("useAuth must be used inside an AuthProvider");
  }

  return context;
}
