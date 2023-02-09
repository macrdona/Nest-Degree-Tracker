import {
  createBrowserRouter,
  Navigate,
  RouterProvider,
} from "react-router-dom";
import App from "./components/App/App";
import Landing from "./components/Landing/Landing";

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
      {
        path: "*",
        element: <Navigate to="/" />, // Redirect to home
      },
    ],
  },
]);
