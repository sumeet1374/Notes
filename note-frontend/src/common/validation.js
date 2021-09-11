// Common Validation Functions
export const required = (getValue) => {

    const [value, entity, parameters] = getValue();
    if (value)
        return true;

    return false;
};

export const email = (getValue) => {
    const emailPattern = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    const [value, entity, parameters] = getValue();
    if (emailPattern.test(value))
        return true;

    return false;
};

export const minlength = (getValue) => {
    const [value, entity, parameters] = getValue();
    if (value.length >= parameters.minlength)
        return true;

    return false;
}




// Validation function to validate field and if validation of field is successful validate dependent field
export const validateFieldOnChange = (key, value, entity, parameters) => {
    let fullResult = {};
    const result = validateFieldPrivate(key, value, entity, parameters);
    fullResult[key] = { validationResult: result };
    return fullResult;

}

const validateFieldPrivate = (key, value, entity, parameters) => {
    let validationResult = [];
    for (let validator of entity[key].validators) {
        // Passing Get Value Function for Validator with all parameters required for all the fields
        // Get Value function returns value to validate and parameters required to the validation function
        const result = validator.fn(() => { return [value, entity, parameters] });
        // const result = validator.fn();
        if (!result) {
            validationResult.push(validator.message);
            break;
        }

    }
    return validationResult;
}