import { useState } from "react";
import Confirm from "../common/dialogs/Confirm";
import "../common/Forms.css";
import { useAuth0 } from "@auth0/auth0-react";

const LogOut = () => {

    const { logout } = useAuth0();
    logout({ returnTo: window.location.origin })

    return (<div>
       Logging out ...</div>);
};
export default LogOut;