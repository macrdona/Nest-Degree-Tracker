import { useState } from "react";
import reactLogo from "./assets/react.svg";
import { Navigate, Outlet, RouterProvider } from "react-router";
import { createBrowserRouter } from "react-router-dom";

import "./App.scss";
import Header from "../Header/Header";

function App() {
  return (
    <div className="App vh-100 d-flex flex-column">
      {/* The navbar */}
      <Header />

      {/* Tells the router where to render the component for a given URL */}
      <Outlet />
    </div>
  );
}

export default App;
