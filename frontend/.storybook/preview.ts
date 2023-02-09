import { withRouter } from "storybook-addon-react-router-v6";
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

export const decorators = [withRouter];
