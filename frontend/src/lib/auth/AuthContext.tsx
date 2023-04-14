import {
  createContext,
  PropsWithChildren,
  useContext,
  useEffect,
  useState,
} from "react";
import { User, UserToken } from "../api/types";
import { decodeToken, isExpired } from "react-jwt";
import { toast } from "react-toastify";
import { useNavigate } from "react-router";

export interface AuthContextValue {
  user?: User;
  token?: string;
  login: (token: string) => void;
  logout: () => void;
  loggedIn: boolean;
  setOnboardingCompleted: () => void;
}

const AuthContext = createContext<AuthContextValue>({
  login: () => {},
  logout: () => {},
  loggedIn: false,
  setOnboardingCompleted: () => {},
});

export const useAuth = () => {
  return useContext(AuthContext);
};

export function AuthProvider(props: PropsWithChildren) {
  const [user, setUser] = useState<User>();
  const [token, setToken] = useState<string>();
  const [loggedIn, setLoggedIn] = useState<boolean>(false);

  const login = (token: string): User | undefined => {
    console.log(token);
    const decoded: UserToken | null = decodeToken(token) as UserToken | null;

    const expired = isExpired(token);
    if (decoded && !expired) {
      const user: User = {
        firstName: decoded.firstName,
        id: decoded.id,
        lastName: decoded.lastName,
        username: decoded.username,
        completed: decoded?.completed === "true",
      };
      localStorage.setItem("token", token);
      setUser(user);
      setToken(token);
      setLoggedIn(true);
      return user;
    } else {
      toast.error("Invalid token, please log in again.");
      logout();
      return undefined;
    }
  };

  const setOnboardingCompleted = () => {
    // Need to find another way to update our token with a newer one, but this prevents the old token from being loaded with complete=false
    // Unfortunately this means the user has to login again after leaving/refreshing the page
    localStorage.removeItem("token");
    setUser((user) => {
      if (user)
        return {
          ...user,
          completed: true,
        };
    });
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
        const user = login(storedToken);
        if (user)
          toast.info(
            "Welcome back " + user.firstName + " " + user.lastName + "!"
          );
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
        setOnboardingCompleted: setOnboardingCompleted,
      }}
    >
      {props.children}
    </AuthContext.Provider>
  );
}
