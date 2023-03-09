import { createContext, PropsWithChildren, useContext, useEffect, useState } from "react";
import { User } from "../api/types";
import jwt, { TokenExpiredError } from "jsonwebtoken"
import { toast } from "react-toastify";

export interface AuthContextValue {
    user?: User
    token?: string
    login: (token: string) => void
    logout: () => void
    loggedIn: boolean
}

const AuthContext = createContext<AuthContextValue>({login: () => {}, logout: () => {}, loggedIn: false});

export const useAuth = () => {
    return useContext(AuthContext);
}

export function AuthProvider(props: PropsWithChildren) {

  const [user, setUser] = useState<User>();
  const [token, setToken] = useState<string>();
  const [loggedIn, setLoggedIn] = useState<boolean>(false);

  const login = (token: string) => {
    try {
        const decoded = jwt.decode(token);

        setUser(decoded as User);
        setToken(token);
        setLoggedIn(true);

        toast.success("Logged in.");
    } catch (err) {
        toast.error("Invalid token, please log in again.");
        logout();
    }
  }

  const logout = () => {
    localStorage.removeItem("token");
    setToken(undefined);
    setUser(undefined);
    setLoggedIn(false);    

  }
  
  useEffect(() => {
    if (!loggedIn) {
        const storedToken = localStorage.getItem("token");
        if (storedToken) {
            login(storedToken)
        }
    }
  }, [])

  return (
    <AuthContext.Provider value={{
        loggedIn: loggedIn,
        logout: logout,
        token: token,
        login: login,
        user: user
    }}>
        {props.children}
    </AuthContext.Provider>);
}

