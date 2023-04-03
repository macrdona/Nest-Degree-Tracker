import {
  createContext,
  PropsWithChildren,
  useContext,
  useEffect,
  useState,
} from "react";
import { User } from "../api/types";
import { decodeToken, isExpired } from "react-jwt";
import { toast } from "react-toastify";

export interface AuthContextValue {
  user?: User;
  token?: string;
  login: (token: string) => void;
  logout: () => void;
  loggedIn: boolean;
}

const AuthContext = createContext<AuthContextValue>({
  login: () => {},
  logout: () => {},
  loggedIn: false,
});

export const useAuth = () => {
  return useContext(AuthContext);
};

export function AuthProvider(props: PropsWithChildren) {
  const [user, setUser] = useState<User>();
  const [token, setToken] = useState<string>();
  const [loggedIn, setLoggedIn] = useState<boolean>(false);

  const login = (token: string) => {
    const decoded: User | null = decodeToken(token) as User | null;
    const expired = isExpired(token);
    if (decoded && !expired) {
      localStorage.setItem("token", token);
      setUser(decoded);
      setToken(token);
      setLoggedIn(true);
      console.log(decoded);
      toast.success("Logged in.");
    } else {
      toast.error("Invalid token, please log in again.");
      logout();
    }
  };

  const logout = () => {
    localStorage.removeItem("token");
    setToken(undefined);
    setUser(undefined);
    setLoggedIn(false);
  };

  useEffect(() => {
    if (!loggedIn) {
      const storedToken = localStorage.getItem("token");
      if (storedToken) {
        login(storedToken);
      }
    }
  }, []);

  return (
    <AuthContext.Provider
      value={{
        loggedIn: loggedIn,
        logout: logout,
        token: token,
        login: login,
        user: user,
      }}
    >
      {props.children}
    </AuthContext.Provider>
  );
}
