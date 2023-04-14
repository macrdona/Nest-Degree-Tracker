import { useContext, useState } from "react";
import SelectCourses from "./SelectCourses";
import {
  RegisteringFormsContext,
  RegisteringFormsContextProvider,
} from "./RegisteringFormsContext";
import SelectMajor from "./SelectMajor";
import OtherReqs from "./OtherReqs";
import RegisterComplete from "./RegisterComplete";

function RegisteringForm() {
  return (
    <RegisteringFormsContextProvider>
      <SelectMajor />
      <SelectCourses />
      <OtherReqs />
      <RegisterComplete />
    </RegisteringFormsContextProvider>
  );
}

export default RegisteringForm;
