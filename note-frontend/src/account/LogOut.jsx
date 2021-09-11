import { useState } from "react";
import Confirm from "../common/dialogs/Confirm";
import "../common/Forms.css";

const LogOut = () => {

    const [showConfirm, setShowConfirm] = useState(false);

    const onConfirmAction = () => {
    
        setShowConfirm(false);
       
    }

    const showDialog = ()=>{
        setShowConfirm(true);
    }

    return (<div>
        <div><button className="button primary" onClick={showDialog}>Show Dialog</button></div>
        <Confirm visible={showConfirm} onConfirm={onConfirmAction} onCancel={onConfirmAction} backgroundColor="red"  ></Confirm>
        <div>Logout</div></div>);
};
export default LogOut;