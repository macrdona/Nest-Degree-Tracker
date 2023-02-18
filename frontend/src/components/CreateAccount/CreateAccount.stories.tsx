import { Story, Meta } from "@storybook/react";
import CreateAccount from "./CreateAccount";

export default {
  title: "CreateAccount",
  component: CreateAccount,
} as Meta;

const Template: Story<typeof CreateAccount> = (props) => (
  <CreateAccount {...props} />
);
export const Base = Template.bind({});
