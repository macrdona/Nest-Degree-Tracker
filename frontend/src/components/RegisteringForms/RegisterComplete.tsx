import React from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import './RegisterComplete.scss';
import Steps4 from "../../assets/steps-image-4.png";

function RegisterComplete() {

    //will need to be linked to main account page
    return(
        <div className="main container justify-content-center d-flex flex-column align-items-stretch">
            <img src={Steps4}/>
            <div className="successMessage row justify-content-center">
                <b><u>Registration successful</u></b>
            </div>
            <div className="row justify-content-center">
                Congratulations, your account has been created. Continue to access your account.
            </div>
            <button
                className="button btn btn-primary btn-lg mt-3 fs-2 mw-75 align-self-center text-nowrap"
            >
                Next
            </button>

        </div>
    );
}
export default RegisterComplete;