export const passwordMatchValidation = (getValue) => {
    const [value, entity, parameters] = getValue();


    if (value && entity.confirmPassword.value) {
        if (value === entity.confirmPassword.value) {
            return true;
        } else {
            return false;
        }
    } else {
        return true;
    }


}

export const confirmPasswordMatchValidation = (getValue) => {
    const [value, entity, parameters] = getValue();


    if (value && entity.password.value) {
        if (value === entity.password.value) {
            return true;
        } else {
            return false;
        }
    } else {
        return true;
    }


}