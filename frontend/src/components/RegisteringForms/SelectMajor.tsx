import React from "react";
import Steps from "../../assets/steps-image-1.png";
import "bootstrap/dist/css/bootstrap.min.css";
import "./SelectMajor.scss";

function SelectMajor() {
  return (
    <div className="select-major mainSection container d-flex justify-content-center">
      <img src={Steps} className="steps-img" draggable={false} />
      <div className="form">
        <div className="form-group">
          <label className="form-label">University or school:</label>
          <select className="form-select">
            <option selected>Select your school</option>
            <option value="1">University of North Florida</option>
            <option value="2" disabled>
              University of Central Florida
            </option>
            <option value="3" disabled>
              Florida State College of Jacksonville
            </option>
          </select>
        </div>
        <div className="form-group">
          <label className="form-label">Major:</label>
          <select className="form-select">
            <option selected>Select your program of study</option>
            <option value="1">Computer Science</option>
            <option value="2" disabled>
              Business
            </option>
            <option value="3" disabled>
              Biology
            </option>
          </select>
        </div>
        <div className="form-group">
          <label className="form-label">Minor:</label>
          <select className="form-select">
            <option selected>Select your minor</option>
            <option value="1">N/A</option>
            <option value="2">Business Administration</option>
            <option value="3">Physical Therapy</option>
          </select>
        </div>
        <button className="btn btn-primary btn-lg mt-3 fs-2 mw-75 align-self-center text-nowrap">
          Next
        </button>
      </div>
    </div>
  );
}

export default SelectMajor;
