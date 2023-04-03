import React from "react";
import SelectMajor from "./SelectMajor";
import { Story } from "@storybook/react";

export default {
  title: "selectMajor",
  component: SelectMajor,
};
const Template: Story<typeof SelectMajor> = (props) => (
  <SelectMajor {...props} />
);
export const Base = Template.bind({});
