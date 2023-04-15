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
import { useLogin } from "../api";

export interface AuthContextValue {
  user?: User;
  token?: string;
  login: (token: string) => void;
  logout: () => void;
  loggedIn: boolean;
  getOnboardingCompleted: () => boolean;
  setOnboardingCompleted: (value: boolean) => void;
}

const AuthContext = createContext<AuthContextValue>({
  login: () => {},
  logout: () => {},
  loggedIn: false,
  getOnboardingCompleted: () => false,
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
        completed: decoded.completed === "true",
      };
      localStorage.setItem("token", token);
      if (!getOnboardingCompleted())
        setOnboardingCompleted(decoded.completed === "true");
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

  const setOnboardingCompleted = (value: boolean) => {
    localStorage.setItem("onboarding-completed", value ? "true" : "false");
    setUser((user) =>
      user
        ? {
            ...user,
            completed: value,
          }
        : undefined
    );
  };

  const getOnboardingCompleted = () => {
    return localStorage.getItem("onboarding-completed") === "true";
  };

  const logout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("onboarding-completed");
    setToken(undefined);
    setUser(undefined);
    setLoggedIn(false);
  };

  // Called when the user refreshes/revisits the page
  // Since all our state is gone, we need to load from localstorage
  useEffect(() => {
    if (!loggedIn) {
      // Get the stored token in local storage
      const storedToken = localStorage.getItem("token");

      if (storedToken) {
        const storedOnboardingCompleted = localStorage.getItem(
          "onboarding-completed"
        );
        const user = login(storedToken);

        if (user) {
          setOnboardingCompleted(storedOnboardingCompleted == "true");
          toast.info(
            "Welcome back " + user.firstName + " " + user.lastName + "!"
          );
        }
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
        getOnboardingCompleted: getOnboardingCompleted,
        setOnboardingCompleted: setOnboardingCompleted,
      }}
    >
      {props.children}
    </AuthContext.Provider>
  );
}
