import { Story, Meta } from "@storybook/react";
import Header from "../Header/Header";
import CreateAccount from "./CreateAccount";

export default {
  title: "CreateAccount",
  component: CreateAccount,
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

const Template: Story<typeof CreateAccount> = (props) => (
  <CreateAccount {...props} />
);
export const Base = Template.bind({});
