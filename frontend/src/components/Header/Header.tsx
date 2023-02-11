import { useRegister } from "../../lib/api";
import "./Header.scss";

function Header() {
  const { mutate } = useRegister();

  return (
    <div className="py-0 navbar navbar-dark bg-dark justify-content-center">
      <a className="navbar-brand">
        <h1>Nest Degree Tracker</h1>
      </a>
      <button
        className="btn btn-primary btn-lg"
        onClick={() => {
          mutate({
            firstName: "Ricky",
            lastName: "LeDew",
            password: "12345",
            username: "n01451899",
          });
        }}
      >
        Click me!
      </button>
    </div>
  );
}
export default Header;
