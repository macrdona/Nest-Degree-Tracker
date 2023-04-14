import React, { useContext } from "react";
import Steps3 from "../../assets/steps-image-3.png";
import { RegisteringFormsContext } from "./RegisteringFormsContext";

function OtherReqs() {
  const {
    currentStep,
    prevStep,
    nextStep,
    submit,
    oralRequirementComplete,
    setOralRequirementComplete,
  } = useContext(RegisteringFormsContext);

  if (currentStep !== 2) return null;

  const handleSubmit = async () => {
    if (await submit()) nextStep();
  };

  return (
    <div className="other-reqs mainSection container d-flex flex-column align-items-stretch">
      <img src={Steps3} />
      <h1 className="display-4 align-self-center">Special Requirements</h1>
      <p className="lead align-self-center">
        Have you completed any of the special degree requirements below?
      </p>
      <div className="form d-flex flex-column align-items-center">
        <div className="form-check form-switch form-switch-lg">
          <label
            className="form-check-label fs-3"
            htmlFor="oralPresentationReqCheck"
          >
            SoC Oral Presentation Requirement
          </label>
          <input
            className="form-check-input"
            id="oralPresentationReqCheck"
            role="switch"
            type="checkbox"
            style={{
              marginRight: "1em",
            }}
            checked={oralRequirementComplete}
            onChange={(e) => {
              setOralRequirementComplete(e.target.checked);
            }}
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
            Finish
          </button>
        </div>
      </div>
    </div>
  );
}
export default OtherReqs;
