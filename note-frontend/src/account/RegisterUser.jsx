import { useEffect, useState } from 'react';
import Card from '../common/Card';
import '../common/Forms.css';
import FormField from '../common/forms/FormField';
import { required, email, minlength, validateFieldOnChange } from '../common/validation';
import { passwordMatchValidation, confirmPasswordMatchValidation } from './RegistrationValidation';

import { useAuth0 } from "@auth0/auth0-react";
import axios from 'axios';
import { useHistory } from 'react-router';
 

const RegisterUser = () => {

    const MIN_LENGTH = 8;

    const createApiUser= ()=> {
        const apiUser = { }
        apiUser.firstName = user.firstName.value;
        apiUser.lastName = user.lastName.value;
        apiUser.email = user.email.value;
        apiUser.password = user.password.value;
        apiUser.confirmPassword = user.confirmPassword.value;
        apiUser.active = true;
        apiUser.isAdmin = false;

        return apiUser;
    }
    const SubmitUser = (event) => {

        event.preventDefault();
        if (validateForm()) {
            console.log(user);
            var newUser = createApiUser();
            console.log(newUser);
              setApiUser(newUser);
        }
        else {
            console.log("Error");
        }

    }
    // Function to validate entire form
    const validateForm = () => {
        let isValid = true;
        const entity = user;
        const entityToUpdate = { ...user };
        const keys = Object.keys(entity);
        for (let key of keys) {
            if (!validateField(key, entity[key].value, entity, entityToUpdate)) {
                isValid = false;
            }
        }
        setUser(entityToUpdate);
        return isValid;
    }
    // Function to validate a field
    const validateField = (key, value, entity, entityToUpdate) => {
        // Validation is done based upon validation functions mentioned for each field
        const validationResult = validateFieldOnChange(key, value, entity, { minlength: MIN_LENGTH });
        entityToUpdate[key].validationResult = validationResult[key].validationResult;
        entityToUpdate[key].value = value;
        return entityToUpdate[key].validationResult.length > 0 ? false : true;
    }

    // Change Event
    const handleChange = (event) => {
        const key = event.target.name;
        const value = event.target.value;
        let updatedUser = { ...user };
        // Validation is done based upon validation functions mentioned for each field
        validateField(key, value, user, updatedUser);
        setUser(updatedUser)

    }
    // Initial Model
    const { getAccessTokenSilently } = useAuth0();
    const [apiUser,setApiUser] = useState(null);
    const [loading,setLoading] = useState(false);
    let history =   useHistory();
    const [user, setUser] = useState({
        firstName: {
            value: "",
            validators: [],
            validationResult: []
        },
        lastName: {
            value: "",
            validators: [],
            validationResult: []
        },
        email: {
            value: "",
            validators: [{ fn: required, message: "Email is required" }, { fn: email, message: "Invalid Email" }],
            validationResult: []
        },
        password: {
            value: "",
            validators: [{ fn: required, message: "password is required" }, { fn: minlength, message: `Password should be minium of ${MIN_LENGTH} characters.` }, { fn: passwordMatchValidation, message: "Password and Confirm Password should match." }],
            validationResult: []

        },
        confirmPassword: {
            value: "",
            validators: [{ fn: required, message: "Confirm password is required" }, { fn: confirmPasswordMatchValidation, message: "Password and Confirm Password should match." }],
            validationResult: []

        }

    });

    useEffect(()=> {
        // Only execute when we create a user.
        console.log("Effect...");
        console.log(apiUser);
        const saveUser = async ()=> {
            try{
                // const accessToken = await getAccessTokenSilently({
                //     audience: `${process.env.REACT_APP_API_AUDIENCE}`,
                //     scope: "read:notes"
                //   });
          
                //   let config = {
                //     headers: {
                //       'Authorization': `Bearer ${accessToken}`
                //     }
                //   };

                  const url = `${process.env.REACT_APP_API_BASE_URL}/users`;
                  const response = await axios.post(url, apiUser);
                  history.push("/");

            }
            catch(e){
               history.push("/error")
            }
         
        };
        if(apiUser){
            saveUser();
        }
    },[apiUser]);
    return (
        <Card title="Register User" className="formStandard" >
            <div>
                <form className="form-main" onSubmit={SubmitUser}>
                    <FormField name="email" type="email" value={user.email.value} onChange={handleChange} isRequired="true" label="Email" validationResult={user.email.validationResult} />
                    <FormField name="firstName" type="text" value={user.firstName.value} onChange={handleChange} label="First Name" />
                    <FormField name="lastName" type="text" value={user.lastName.value} onChange={handleChange} label="Last Name" />
                    <FormField name="password" type="password" value={user.password.value} onChange={handleChange} isRequired="true" label="Password" validationResult={user.password.validationResult} />
                    <FormField name="confirmPassword" type="password" value={user.confirmPassword.value} onChange={handleChange} isRequired="true" label="Confirm Password" validationResult={user.confirmPassword.validationResult} />
                    {/* <FormField name="isAdmin" type="checkbox" value={user.isAdmin.value} onChange={handleChange} label="Is Admin" /> */}

                    <div className="form-field-group">
                        <button className="button primary" type="submit" >Register</button>
                        <button className="button secondary" type="button">Cancel</button>
                    </div>
                </form>
            </div>
        </Card>
    );

}

export default RegisterUser;