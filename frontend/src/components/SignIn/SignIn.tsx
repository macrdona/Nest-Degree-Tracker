import React, { useState } from "react";
import { toast } from "react-toastify";
import "bootstrap/dist/css/bootstrap.min.css";
import "./SignIn.scss";
import { useLogin } from "../../lib/api";
import { useNavigate } from "react-router";

function SignIn() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const { mutate: login } = useLogin();

  const navigate = useNavigate();

  const validate = (): boolean => {
    // Field is missing
    if (username === "" || password === "") {
      toast.error("Missing one or more required fields.");
      return false;
    }
    console.log(username, password);
    return true;
  };

  const handleSubmit = () => {
    if (validate()) {
      login(
        { username: username, password: password },
        {
          onSuccess: () => {
            navigate("/");
          },
        }
      );
    }
  };

  return (
    <div className="container-fluid flex-fill login-page">
      <div className="row no-gutters h-100 flex-fill">
        <div className="d-none d-lg-flex col background-container"></div>
        <div className="col col-lg-6 p-5 d-flex flex-column align-items-center shadow-lg">
          <h1 className="display-1 mt-5 text-secondary">Login</h1>
          <div className="d-flex m-5 w-50">
            <form className="form d-flex flex-column gap-2 w-100">
              <div className="form-group w-100">
                <label
                  htmlFor="formGroupExampleInput"
                  className="form-label fs-5"
                >
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
                <label
                  htmlFor="formGroupExampleInput"
                  className="form-label  fs-5"
                >
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
              <button
                onClick={(e) => {
                  e.preventDefault();
                  handleSubmit();
                }}
                className="btn btn-primary btn-lg mt-3 fs-2 mw-75 align-self-center text-nowrap"
                type="submit"
              >
                Login
              </button>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
}
export default SignIn;
