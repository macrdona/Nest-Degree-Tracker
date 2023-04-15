import React, { useContext } from "react";
import Steps4 from "../../assets/steps-image-4.png";
import { RegisteringFormsContext } from "./RegisteringFormsContext";
import { useNavigate } from "react-router";

function RegisterComplete() {
  const { currentStep, prevStep, nextStep } = useContext(
    RegisteringFormsContext
  );

  const navigate = useNavigate();

  if (currentStep !== 3) return null;

  const handleSubmit = () => {
    navigate("/tracker");
  };

  //will need to be linked to main account page
  return (
    <div className="main container justify-content-center d-flex flex-column align-items-center">
      <img src={Steps4} />
      <h1 className="display-4 align-self-center">Registration Successful</h1>
      <p className="lead align-self-center">
        Congratulations, your account has been created. Continue to access your
        account.
      </p>
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
          Done
        </button>
      </div>
    </div>
  );
}
export default RegisterComplete;
