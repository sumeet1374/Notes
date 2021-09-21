import { useState,useEffect } from "react";
import { maxLength, required, validateFieldOnChange } from "../common/validation";
import { useHistory } from "react-router";
import  Loader from '../common/Loader';
import  Card  from '../common/Card';
import FormFieldTextArea from "../common/forms/FormsFieldTextArea";
import { useAuth0 } from "@auth0/auth0-react";
import { postData }from '../common/ajax';
const NewNote = () => {
    // Init Models
    const [loading, setLoading] = useState(false);
    let history = useHistory();
    const [note, setNote] = useState({
        note: {
            value:"",
            validators:[{ fn: required, message: "Note is required."} ,{ fn:maxLength, message:"Notes chan be of maximum 2000 characters."}],
            validationResult: []
        }
    });
    const [apiNote,setApiNote] = useState(null);
    const { getAccessTokenSilently } = useAuth0();
    // Function to validate entire form
    const validateForm = () => {
        let isValid = true;
        const entity = note;
        const entityToUpdate = { ...note };
        const keys = Object.keys(entity);
        for (let key of keys) {
            if (!validateField(key, entity[key].value, entity, entityToUpdate)) {
                isValid = false;
            }
        }
        setNote(entityToUpdate);
        return isValid;
    }

    // Function to validate a field
    const validateField = (key, value, entity, entityToUpdate) => {
        // Validation is done based upon validation functions mentioned for each field
        const validationResult = validateFieldOnChange(key, value, entity, { maxLength:2000 });
        entityToUpdate[key].validationResult = validationResult[key].validationResult;
        entityToUpdate[key].value = value;
        return entityToUpdate[key].validationResult.length > 0 ? false : true;
    };

    // Change Event
    const handleChange = (event) => {
        const key = event.target.name;
        const value = event.target.value;
        let updatedNote = { ...note };
        // Validation is done based upon validation functions mentioned for each field
        validateField(key, value, note, updatedNote);
        setNote(updatedNote);

    }
    const onSubmit = (event) => {
        event.preventDefault();
        if(validateForm()){
            setApiNote({note:note.note.value});
        }
        else{
            console.log("Error");
        }
    };

    const onCancel = (event)=>{
        history.push("/");
    };

    useEffect(()=> {
        const saveNote = async ()=> {
            
            var result = await postData("/notes",apiNote,getAccessTokenSilently);
            setApiNote(null);
            setLoading(false);
            history.push("/");
        }

        try{
            setLoading(true);
            if(apiNote != null){
                saveNote();
               
            }
         //   
            setLoading(false);
            
        }
        catch(e){
            console.log(e);
         
            history.push("/error");
        }
        finally{
            setLoading(false);
        }

    },[apiNote]);
    return (<>
        <Loader visible={loading}></Loader>
        <Card title="Register User" className="formStandard">
            <div>
                <form className="form-main" onSubmit={onSubmit}>
                    <FormFieldTextArea name="note" value={note.note.value} isRequired="true" label="Notes" validationResult={note.note.validationResult} onChange={handleChange}></FormFieldTextArea>
                    <div className="form-field-group">
                        <button className="button primary" type="submit" >Create</button>
                        <button className="button secondary" type="button" onClick={onCancel}>Cancel</button>
                    </div>
                </form>
            </div>
        </Card> </>);
}

export default NewNote;