import React, { useContext, useState } from "react";
import { toast } from "react-toastify";
import Steps2 from "../../assets/steps-image-2.png";
import "bootstrap/dist/css/bootstrap.min.css";
import "./SelectCourses.scss";
import CoursesDropdown from "../forms/CoursesDropdown/CoursesDropdown";
import { RegisteringFormsContext } from "./RegisteringFormsContext";
import { Course } from "../../lib/api/useCourses";

export function SelectCourses() {
  const { currentStep, nextStep, prevStep, setCourses, courses } = useContext(
    RegisteringFormsContext
  );

  if (currentStep !== 1) return null;

  const major = "Computer Science"; //placeholder will get real major
  const minor = "Biology"; //placeholder will get real major
  const userMajorCourses = [];
  const userMinorCourses = [];
  const userGenEds = [];

  let majorCourses = [
    //all of this is a placeholder for the real list from the database
    { name: "Intro to Databases" },
    { name: "Algorithms" },
    { name: "Data Structures" },
  ];
  let minorCourses = [
    //all of this is a placeholder for the real list from the database
    { name: "Physical Therapy" },
    { name: "Stress Management" },
  ];
  let genEds = [
    //all of this is a placeholder for the real list from the database
    { name: "Biology" },
    { name: "Earth Science" },
  ];

  const [isActive, setActive] = useState(false);

  const handleClick = () => {
    //will change the button color
    //need to add a handle submit for the next button
    //handle submit will store all the button values currently selected
    setActive((isActive) => !isActive);
  };
  const handleSubmit = () => {
    //wip
  };

  return (
    <div className="select-courses mainSection container d-flex flex-column align-items-stretch">
      <img src={Steps2} />
      <h1 className="display-4 align-self-center">Welcome</h1>
      <p className="lead align-self-center">
        Please add the courses you've taken so far.
      </p>
      <div className="form d-flex flex-column align-items-stretch">
        <div className="form-group">
          <label className="form-label fs-3">Select Courses</label>
          <CoursesDropdown
            multiple
            onChange={(option) => {
              setCourses(option as Course[]);
            }}
            selected={courses}
          />
        </div>
        <div className="d-flex flex-row justify-content-center gap-3">
          <button
            onClick={(e) => {
              prevStep();
            }}
            className="btn btn-secondary btn-lg mt-3 align-self-center text-nowrap"
            type="submit"
          >
            Back
          </button>
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
    </div>
  );
}
export default SelectCourses;
