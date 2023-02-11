import { withRouter } from "storybook-addon-react-router-v6";
import React from "react";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ToastContainer } from "react-toastify";
import Axios from "axios";
import "../src/styles.scss";

export const parameters = {
  actions: { argTypesRegex: "^on[A-Z].*" },
  controls: {
    matchers: {
      color: /(background|color)$/i,
      date: /Date$/,
    },
  },
  layout: "fullscreen",
  reactRouter: {},
};

Axios.defaults.baseURL = "http://localhost:4000/";

const queryClient = new QueryClient();

export const decorators = [
  withRouter,
  (Story) => {
    return (
      <>
        <QueryClientProvider client={queryClient}>
          <Story />
        </QueryClientProvider>
        <ToastContainer />
      </>
    );
  },
];
