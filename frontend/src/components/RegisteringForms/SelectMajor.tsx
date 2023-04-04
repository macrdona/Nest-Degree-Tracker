import React, { useMemo, useState } from "react";
import { toast } from "react-toastify";
import Steps from "../../assets/steps-image-1.png";
import "bootstrap/dist/css/bootstrap.min.css";
import "./SelectMajor.scss";
import { useMajors } from "../../lib/api/useMajors";
import { Typeahead } from "react-bootstrap-typeahead";

function selectMajor() {
  const [major, setMajor] = useState("");
  const [minor, setMinor] = useState("");

  // const { data: majors } = useMajors();
  let majors = [
    //all of this is a placeholder for the real list from the database
    { name: "Computer Science (CS)" },
    { name: "Information Technology (IT)" },
    { name: "Information Science (IS)" },
    { name: "Data Science (DS)" },
  ];

  let minors = [
    //all of this is a placeholder for the real list from the database
    { name: "None" },
    { name: "Mathematics" },
  ];

  const validate = (): boolean => {
    // Passwords don't match
    if (major === "" || minor === "") {
      toast.error("Missing one or more required fields.");
      return false;
    }
    console.log(major, minor);
    return true;
  };

  const handleSubmit = () => {
    if (validate()) {
      //send information to database and continue to next page
      //set up link for the next page
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
            labelKey={"name"}
            placeholder="Select area of study..."
            highlightOnlyResult
          />
        </div>
        <div className="form-group">
          <label className="form-label fs-3">Minor</label>
          <Typeahead
            options={minors ?? []}
            labelKey={"name"}
            placeholder="Select minor..."
            highlightOnlyResult
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

export default selectMajor;
