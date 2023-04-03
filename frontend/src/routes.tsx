import {
  createBrowserRouter,
  Navigate,
  RouterProvider,
} from "react-router-dom";
import App from "./components/App/App";
import CreateAccount from "./components/CreateAccount/CreateAccount";
import Landing from "./components/Landing/Landing";
import SelectMajor from "./components/RegisteringForms/SelectMajor";

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
      { path: "/login", element: <h1>Hello!</h1> },
      { path: "/register", element: <CreateAccount /> },
      { path: "/onboarding", element: <SelectMajor /> },
      {
        path: "*",
        element: <Navigate to="/" />, // Redirect to home
      },
    ],
  },
]);
