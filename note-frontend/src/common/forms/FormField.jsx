import '../Forms.css';

const FormField = (props) => {

    const labelClass = props.isRequired?"form-label-required":"form-label";
    const isfieldValid = props.validationResult && props.validationResult.length > 0?false:true;
    let formFieldClass = props.type ==="checkbox"?"form-field-checkbox":"form-field";
  

     formFieldClass = isfieldValid?formFieldClass:`${formFieldClass} form-field-error`;
     const validationMessages =  props.validationResult && props.validationResult.length ?props.validationResult.map((val,index)=> <div className="form-field-error-message" key={index}>{val}</div>  ) :null ;
    return (
        <div className="form-field-group">
            <label className={labelClass}>{props.label}</label>
            <input type={props.type} className={formFieldClass} name={props.name} value={props.value} onChange={props.onChange}></input>
            {validationMessages}
        </div>
     
    );
};

export default FormField;