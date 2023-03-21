import { useRegister } from "../../lib/api";
import { useAuth } from "../../lib/auth/AuthContext";
import "./Header.scss";

function Header() {
  const { user, loggedIn } = useAuth()
  return (
    <div className="py-0 navbar navbar-dark bg-dark justify-content-center">
      <a className="navbar-brand">
        <h1>Nest Degree Tracker</h1>
      </a>
      {loggedIn ?
      <div className="ml-auto text-muted">
        Logged in as <strong>{`${user?.firstName} ${user?.lastName}`}</strong>
      </div> : null}
    </div>
  );
}
export default Header;
