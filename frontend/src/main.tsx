import React from "react";
import ReactDOM from "react-dom/client";
import { RouterProvider } from "react-router";
import { router } from "./routes";
import Axios from "axios";
import { QueryClientProvider, QueryClient } from "@tanstack/react-query";
import { ToastContainer } from "react-toastify";
import "./styles.scss";
import { AuthProvider } from "./lib/auth/AuthContext";

// Need to find a better way to do this i.e. with an environment variable
// The port keeps changing every time you run the backend :/
Axios.defaults.baseURL = "http://localhost:4000/";

const queryClient = new QueryClient();

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
  <>
    <AuthProvider>
      <QueryClientProvider client={queryClient}>
        <RouterProvider router={router} />
      </QueryClientProvider>
    </AuthProvider>
    <ToastContainer position="bottom-right" />
  </>
);
