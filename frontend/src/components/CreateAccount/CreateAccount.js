import React, {useState} from 'react';
import "bootstrap/dist/css/bootstrap.min.css";
import "./CreateAccount.css";

function CreateAccount(){
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const[error, setError] = useState(false);
    const[passError, setPassError] = useState(false);

    const handleSubmit = (e) => {
        e.preventDefault();
        if((password !== confirmPassword) || name ==0 || email==0 || password==0){
            if(password !== confirmPassword){
                setPassError(true);
            }
            if(name ==0 || email==0 || password==0){
                setError(true);
            }
        }
        else{
            setPassError(false);
            setError(false);
            console.log(name, email, password);
        }
    }


    return(
        <div className="container-fluid">
            <div className="row no-gutters">
                <div className="col">
                </div>
                <div className="col-8">
                    <h1>Create Account</h1>
                    <div className="container d-flex justify-content-center">
                        <form className="form">
                            <div className="form-group">
                                <label htmlFor="formGroupExampleInput">Name</label>
                                <input value={name} onChange={(e) => setName(e.target.value)} type="text" className="form-control"
                                       id="name" required autoComplete="off"
                                />
                            </div>
                            {error&&name.length==0? <label className="errorText">Name is required</label>:""}
                            <div className="form-group">
                                <label htmlFor="formGroupExampleInput">Email</label>
                                <input value={email} onChange={(e) => setEmail(e.target.value)} type="text" className="form-control"
                                       id="email" required autoComplete="off" />
                            </div>
                            {error&&email.length==0? <label className="errorText">Email is required</label>:""}
                            <div className="form-group">
                                <label htmlFor="formGroupExampleInput">Password</label>
                                <input value={password} onChange={(e) => setPassword(e.target.value)}type="text" className="form-control"
                                       id="password" required autoComplete="off"
                                />
                            </div>
                            {error&&password.length==0? <label className="errorText">Password is required</label>:""}
                            <div className="form-group">
                                <label htmlFor="formGroupExampleInput">Confirm Password</label>
                                <input value={confirmPassword} onChange={(e) => setConfirmPassword(e.target.value)} type="text"
                                       className="form-control" id="confirmPassword" required autoComplete="off"
                                />
                            </div>
                            {passError? <label className="errorText">Passwords do not match</label>:""}
                        </form>
                    </div>
                    <button onClick={handleSubmit} className="button text-white" type="submit">Create Account</button>

                </div>
            </div>
        </div>
    )
}
export default CreateAccount;