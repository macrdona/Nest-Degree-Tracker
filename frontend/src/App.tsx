import { useState } from "react";
import reactLogo from "./assets/react.svg";
import { Navigate, RouterProvider } from "react-router";
import { createBrowserRouter } from "react-router-dom";

import "./App.css";
import Landing from "./Landing/Landing";
import Header from "./Header/Header";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Landing />,
  },
  {
    path: "*",
    element: <Navigate to="/" />,
  },
]);

function App() {
  return (
    <div className="App vh-100 d-flex flex-column">
      <Header />
      <RouterProvider router={router} />
    </div>
  );
}

export default App;
