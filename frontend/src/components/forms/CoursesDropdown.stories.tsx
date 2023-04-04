import { Story, Meta } from "@storybook/react";
import CoursesDropdown from "./CoursesDropdown";

export default {
  title: "CoursesDropdown",
  component: CoursesDropdown,
} as Meta;

const Template: Story<typeof CoursesDropdown> = (props) => (
  <CoursesDropdown {...props} />
);
export const Base = Template.bind({});
