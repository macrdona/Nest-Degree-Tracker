import { useContext, useState } from "react";
import SelectCourses from "./SelectCourses";
import {
  RegisteringFormsContext,
  RegisteringFormsContextProvider,
} from "./RegisteringFormsContext";
import SelectMajor from "./SelectMajor";

function RegisteringForm() {
  return (
    <RegisteringFormsContextProvider>
      <SelectMajor />
      <SelectCourses />
    </RegisteringFormsContextProvider>
  );
}

export default RegisteringForm;
