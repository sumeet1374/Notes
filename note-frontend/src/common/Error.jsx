import Card from './Card';
import './Error.css';
import { Link } from 'react-router-dom';


const Error = (props)=> {

    return (<Card title="Error" className="formStandard" borderColor="red" backgroundColor="red"> 
        <div className="errorBox">
           <div className="errorHeader"> Some Error Occured. </div>
           <div className="errorDetail">Got to the Home Page <Link to="/">Home</Link></div>
           <div className="errorDetail">If the error persists,please contact administrator.`` </div>
        </div>
    </Card>);
}

export default Error;