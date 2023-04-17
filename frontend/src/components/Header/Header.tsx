import { useNavigate } from "react-router";
import { useRegister } from "../../lib/api";
import { useAuth } from "../../lib/auth/AuthContext";
import "./Header.scss";
import { Link } from "react-router-dom";

function Header() {
  const { user, loggedIn, logout } = useAuth();

  const navigate = useNavigate();

  const doLogout = () => {
    logout();
    navigate("/");
  };

  return (
    <div className="py-0 px-3 navbar navbar-dark bg-dark justify-content-space-between">
      <Link to="/" className="navbar-brand">
        <h1>Nest Degree Tracker</h1>
      </Link>
      {loggedIn ? (
        <div className="navbar-right text-muted">
          <em className="text-light me-3">
            Logged in as {user?.firstName} {user?.lastName}
          </em>
          <button className="btn btn-primary" onClick={doLogout}>
            Logout
          </button>
        </div>
      ) : null}
    </div>
  );
}
export default Header;
