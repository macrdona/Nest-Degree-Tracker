import React, { useContext, useMemo, useState } from "react";
import { toast } from "react-toastify";
import Steps from "../../assets/steps-image-1.png";
import "./SelectMajor.scss";
import { useMajors } from "../../lib/api/useMajors";
import { Typeahead } from "react-bootstrap-typeahead";
import CoursesDropdown from "../forms/CoursesDropdown/CoursesDropdown";
import { RegisteringFormsContext } from "./RegisteringFormsContext";
import { Major, Minor } from "../../lib/api/types";
import { Option } from "react-bootstrap-typeahead/types/types";

function SelectMajor() {
  const { currentStep, nextStep, major, setMajor, minor, setMinor } =
    useContext(RegisteringFormsContext);
  if (currentStep !== 0) return null; // Only display if on the first step

  const { data: majors } = useMajors();
  // let majors = [
  //   //all of this is a placeholder for the real list from the database
  //   { name: "Computer Science (CS)" },
  //   { name: "Information Technology (IT)" },
  //   { name: "Information Science (IS)" },
  //   { name: "Data Science (DS)" },
  // ];

  let minors: Minor[] = [
    //all of this is a placeholder for the real list from the database
    { name: "None" },
    { name: "Mathematics" },
  ];

  const validate = (): boolean => {
    // Passwords don't match
    if (!major || !minor) {
      toast.error("Missing one or more required fields.");
      return false;
    }
    console.log(major, minor);
    return true;
  };

  const handleSubmit = () => {
    if (validate()) {
      nextStep();
    }
  };

  return (
    <div className="select-major mainSection container d-flex flex-column align-items-stretch">
      <img src={Steps} />
      <h1 className="display-4 align-self-center">Welcome</h1>
      <p className="lead align-self-center">
        To get started, tell us a bit about your academic goals.
      </p>
      <div className="form d-flex flex-column align-items-stretch">
        <div className="form-group">
          <label className="form-label fs-3">Major</label>
          <Typeahead
            options={majors ?? []}
            onChange={(selected) => {
              setMajor((selected[0] as Major) ?? undefined);
            }}
            selected={major ? ([major] as Option[]) : undefined}
            labelKey={"majorName"}
            placeholder="Select area of study..."
            highlightOnlyResult
            clearButton
          />
        </div>
        <div className="form-group">
          <label className="form-label fs-3">Minor</label>
          <Typeahead
            options={minors ?? []}
            onChange={(selected) => {
              setMinor((selected[0] as Minor) ?? undefined);
            }}
            selected={minor ? ([minor] as Option[]) : undefined}
            labelKey={"name"}
            placeholder="Select minor..."
            highlightOnlyResult
            clearButton
          />
        </div>
        <button
          onClick={(e) => {
            handleSubmit();
          }}
          className="btn btn-primary btn-lg mt-3 align-self-center text-nowrap"
          type="submit"
        >
          Next
        </button>
      </div>
    </div>
  );
}

export default SelectMajor;
