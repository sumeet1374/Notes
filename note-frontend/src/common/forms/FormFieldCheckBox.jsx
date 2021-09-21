import '../Forms.css';

const FormFieldCheckBox= (props) => {

    const labelClass = props.isRequired?"form-label-required":"form-label";
    const isfieldValid = props.validationResult && props.validationResult.length > 0?false:true;
    let formFieldClass = "form-field-checkbox";
  

     formFieldClass = isfieldValid?formFieldClass:`${formFieldClass} form-field-error`;
     const validationMessages =  props.validationResult && props.validationResult.length ?props.validationResult.map((val,index)=> <div className="form-field-error-message" key={index}>{val}</div>  ) :null ;
    return (
        <div className="form-field-group">
            <label className={labelClass}>{props.label}</label>
            <input type="checkbox" className={formFieldClass} name={props.name} checked={props.value}  onChange={props.onChange}></input> 
            {validationMessages}
        </div>
     
    );
};

export default FormFieldCheckBox;