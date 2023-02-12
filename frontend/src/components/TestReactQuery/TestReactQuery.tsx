import { useRegister } from "../../lib/api";
import "./TestReactQuery.scss";

function TestReactQuery() {
  const { mutate } = useRegister();

  return (
    <div className="container-fluid vh-100">
      <div className="row justify-content-center py-5">
        <div className="col-lg-4">
          <div className="card p-5">
            <button
              className="btn btn-secondary btn-lg"
              onClick={() => {
                mutate({
                  firstName: "Ricky",
                  lastName: "LeDew",
                  password: "12345",
                  username: "n01451899",
                });
              }}
            >
              Click me to register!
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}
export default TestReactQuery;
