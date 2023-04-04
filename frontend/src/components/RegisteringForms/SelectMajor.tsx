import React, { useMemo, useState } from "react";
import { toast } from "react-toastify";
import Steps from "../../assets/steps-image-1.png";
import "bootstrap/dist/css/bootstrap.min.css";
import "./SelectMajor.scss";
import { useMajors } from "../../lib/api/useMajors";

function selectMajor() {
  const [major, setMajor] = useState("");
  const [minor, setMinor] = useState("");

  const { data: majors } = useMajors();
  // let majors = [
  //   //all of this is a placeholder for the real list from the database
  //   { name: "Computer science" },
  //   { name: "Business" },
  //   { name: "Biology" },
  //   { name: "Health Administration" },
  // ];

  let minors = [
    //all of this is a placeholder for the real list from the database
    { name: "None" },
    { name: "Computer science" },
    { name: "Business" },
    { name: "Biology" },
    { name: "International Studies" },
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

  //add code to get majors list here
  //currently getting a 401 unauthorized error, have to get a token/get authorized from unmerged section
  /*async function fetchMajors(){
        const response = await fetch('http://localhost:4000/Majors');
        const majorData = await response.json();
        console.log(majorData);
    }
    fetchMajors();*/

  return (
    <div className="mainSection container d-flex justify-content-center">
      <img src={Steps} />
      <div className="form">
        <div className="form-group">
          <label className="form-label">Major:</label>
          <select
            className="form-select"
            onChange={(e) => setMajor(e.target.value)}
          >
            <option disabled selected>
              Select your program of study
            </option>
            {majors
              ? majors.map((major) => (
                  <option key={major.name} value={major.name}>
                    {major.name}
                  </option>
                ))
              : null}
          </select>
        </div>
        <div className="form-group">
          <label className="form-label">Minor:</label>
          <select
            className="form-select"
            onChange={(e) => setMinor(e.target.value)}
          >
            <option disabled selected>
              Select your minor
            </option>
            {minors.map((minor) => (
              <option key={minor.name} value={minor.name}>
                {minor.name}
              </option>
            ))}
          </select>
        </div>
        <button
          onClick={(e) => {
            handleSubmit();
          }}
          className="btn btn-primary btn-lg mt-3 fs-2 mw-75 align-self-center text-nowrap"
          type="submit"
        >
          Next
        </button>
      </div>
    </div>
  );
}

export default selectMajor;
