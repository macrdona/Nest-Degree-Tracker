import React from 'react';
import LibraryImg from '../assets/library-image.png';
import './CreateAccount.css';

function CreateAccount(){
    return(
        <div className="createAccount">
            <div className="sectionBlue">
                <img src={LibraryImg} alt={"A college library"} />
            </div>
            <div className="sectionWhite">
                    <h1>Create Account</h1>
                <form>
                <label>Name</label>
                <input type={"text"} required/>
                <label>Email</label>
                <input type={"text"} required/>
                <label>Password</label>
                <input type={"text"} required/>
                <label>Confirm Password</label>
                <input type={"text"} required/>
                    <button>Create Account</button>
            </form>
            </div>
        </div>

    )
}
export default CreateAccount;
