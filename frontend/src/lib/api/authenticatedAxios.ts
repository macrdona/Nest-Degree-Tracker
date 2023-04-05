import Axios from "axios";
import { useAuth } from "../auth/AuthContext";

export const useAuthenticatedAxios = () => {

    const {token} = useAuth();

    return Axios.create({
        headers: {
            "Authorization": token ? `Bearer ${token}` : ""
        }
    })
}