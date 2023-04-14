import React, { useState } from "react";
import { toast } from "react-toastify";
import "bootstrap/dist/css/bootstrap.min.css";
import "./CreateAccount.scss";
import { useRegister } from "../../lib/api";
import { useNavigate } from "react-router";
import { useAuth } from "../../lib/auth/AuthContext";

function CreateAccount() {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  const { mutate: register } = useRegister();

  const navigate = useNavigate();

  const { login } = useAuth();

  const validate = (): boolean => {
    // Passwords don't match
    if (password !== confirmPassword) {
      // Display error to user
      toast.error("Passwords don't match!");
      return false;
    }

    // Field is missing
    if (
      firstName === "" ||
      lastName === "" ||
      username === "" ||
      password === "" ||
      confirmPassword === ""
    ) {
      toast.error("Missing one or more required fields.");
      return false;
    }

    console.log(firstName, lastName, username, password);
    return true;
  };

  const handleSubmit = () => {
    if (validate()) {
      // Logic for sending API request
      // Display success and navigate to new page
      register(
        { username, firstName, lastName, password },
        {
          onSuccess: ({ token }) => {
            login(token);
            navigate("/onboarding");
          },
        }
      );
    }
  };

  return (
    <div className="container-fluid flex-fill register-page">
      <div className="row no-gutters h-100 flex-fill">
        <div className="d-none d-lg-flex col background-container"></div>
        <div className="col col-lg-6 p-5 d-flex flex-column align-items-center shadow-lg">
          <h1 className="display-1 mt-5 text-secondary">Create Account</h1>
          <div className="d-flex m-5 w-50">
            <form className="form d-flex flex-column gap-2 w-100" noValidate>
              <div className="row g-2">
                <div className="col-6 form-group">
                  <label htmlFor="firstName" className="form-label fs-5">
                    First Name
                  </label>
                  <input
                    value={firstName}
                    onChange={(e) => setFirstName(e.target.value)}
                    type="text"
                    className="form-control form-control-lg"
                    id="firstName"
                    required
                    autoComplete="off"
                  />
                </div>
                <div className="col-6 form-group">
                  <label htmlFor="lastName" className="form-label fs-5">
                    Last Name
                  </label>
                  <input
                    value={lastName}
                    onChange={(e) => setLastName(e.target.value)}
                    type="text"
                    className="form-control form-control-lg"
                    id="lastName"
                    required
                    autoComplete="off"
                  />
                </div>
              </div>

              <div className="form-group w-100">
                <label htmlFor="username" className="form-label fs-5">
                  Username
                </label>
                <input
                  value={username}
                  onChange={(e) => setUsername(e.target.value)}
                  type="text"
                  className="form-control form-control-lg"
                  id="username"
                  required
                  autoComplete="off"
                />
              </div>

              <div className="form-group w-100">
                <label htmlFor="password" className="form-label  fs-5">
                  Password
                </label>
                <input
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  type="text"
                  className="form-control form-control-lg"
                  id="password"
                  required
                  autoComplete="off"
                />
              </div>

              <div className="form-group w-100">
                <label htmlFor="confirmPassword" className="form-label fs-5">
                  Confirm Password
                </label>
                <input
                  value={confirmPassword}
                  onChange={(e) => setConfirmPassword(e.target.value)}
                  type="text"
                  className="form-control form-control-lg"
                  id="confirmPassword"
                  required
                  autoComplete="off"
                />
              </div>
              <button
                onClick={(e) => {
                  e.preventDefault();
                  handleSubmit();
                }}
                className="btn btn-primary btn-lg mt-3 fs-2 mw-75 align-self-center text-nowrap"
                type="submit"
              >
                Create Account
              </button>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
}
export default CreateAccount;
