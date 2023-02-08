import { Story, Meta } from "@storybook/react";
import Header from "../Header/Header";
import Landing from "./Landing";

export default {
  title: "Landing",
  component: Landing,
  decorators: [
    // Used to add the navbar to the top of the page even though it isn't part of this component
    (Story) => (
      <div className="App vh-100 d-flex flex-column">
        <Header />
        <Story />
      </div>
    ),
  ],
} as Meta;

const Template: Story<typeof Landing> = (props) => <Landing {...props} />;
export const Base = Template.bind({});
