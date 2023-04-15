import Axios from "axios";
import { useAuth } from "../auth/AuthContext";

export const useAuthenticatedAxios = () => {

    return Axios.create({
        headers: {
            "Authorization": `Bearer ${localStorage.getItem('token')}`
        }
    })
}