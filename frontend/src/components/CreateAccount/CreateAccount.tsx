import React, { useState } from "react";
import { toast } from "react-toastify";
import "bootstrap/dist/css/bootstrap.min.css";
import "./CreateAccount.scss";

function CreateAccount() {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  const validate = (): boolean => {
    if (
      password !== confirmPassword ||
      name === "" ||
      email === "" ||
      password === ""
    ) {
      // Passwords don't match
      if (password !== confirmPassword) {
        // Display error to user
        toast.error("Passwords don't match!");
        return false;
      }

      // Field is missing
      if (
        name === "" ||
        email === "" ||
        password === "" ||
        confirmPassword === ""
      ) {
        toast.error("Missing one or more required fields.");
        return false;
      }
    }

    console.log(name, email, password);
    return true;
  };

  const handleSubmit = () => {
    if (validate()) {
      // Logic for sending API request
      // Display success and navigate to new page
    }
  };

  return (
    <div className="container-fluid">
      <div className="row no-gutters">
        <div className="col"></div>
        <div className="col-8">
          <h1>Create Account</h1>
          <div className="container d-flex justify-content-center">
            <form className="form">
              <div className="form-group">
                <label htmlFor="formGroupExampleInput">Name</label>
                <input
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                  type="text"
                  className="form-control"
                  id="name"
                  required
                  autoComplete="off"
                />
              </div>

              <div className="form-group">
                <label htmlFor="formGroupExampleInput">Email</label>
                <input
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                  type="text"
                  className="form-control"
                  id="email"
                  required
                  autoComplete="off"
                />
              </div>

              <div className="form-group">
                <label htmlFor="formGroupExampleInput">Password</label>
                <input
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  type="text"
                  className="form-control"
                  id="password"
                  required
                  autoComplete="off"
                />
              </div>

              <div className="form-group">
                <label htmlFor="formGroupExampleInput">Confirm Password</label>
                <input
                  value={confirmPassword}
                  onChange={(e) => setConfirmPassword(e.target.value)}
                  type="text"
                  className="form-control"
                  id="confirmPassword"
                  required
                  autoComplete="off"
                />
              </div>
            </form>
          </div>
          <button
            onClick={(e) => {
              e.preventDefault();
              handleSubmit();
            }}
            className="button text-white"
            type="submit"
          >
            Create Account
          </button>
        </div>
      </div>
    </div>
  );
}
export default CreateAccount;
