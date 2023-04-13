import {
  createBrowserRouter,
  Navigate,
  RouterProvider,
} from "react-router-dom";
import App from "./components/App/App";
import CreateAccount from "./components/CreateAccount/CreateAccount";
import Landing from "./components/Landing/Landing";
import SelectMajor from "./components/RegisteringForms/SelectMajor";
import SignIn from "./components/SignIn/SignIn";
import RegisteringForm from "./components/RegisteringForms/RegisteringForms";

// Contains all the routes and their associated components
export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      // Every route gets nested inside the App component
      {
        path: "/",
        element: <Landing />,
      },
      { path: "/register", element: <CreateAccount /> },
      { path: "/onboarding", element: <RegisteringForm /> },
      { path: "/login", element: <SignIn /> },
      { path: "/register", element: <CreateAccount /> },
      {
        path: "*",
        element: <Navigate to="/" />, // Redirect to home
      },
    ],
  },
]);
