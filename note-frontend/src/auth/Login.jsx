import { useAuth0 } from "@auth0/auth0-react";
import '../common/Forms.css';
import Card from '../common/Card';
import './Login.css';

const Login = () => {
    const { loginWithRedirect } = useAuth0();
    return (

       

        <Card title="Login" className="formStandard">
            <div className="button-pos">
                <button className="button primary" onClick={loginWithRedirect}>Please Click To Login</button>
            </div>

        </Card>
    );

}

export default Login;