import { Story, Meta } from "@storybook/react";
import TestReactQuery from "./TestReactQuery";

export default {
  title: "TestReactQuery",
  component: TestReactQuery,
} as Meta;

const Template: Story<typeof TestReactQuery> = (props) => (
  <TestReactQuery {...props} />
);
export const Base = Template.bind({});
