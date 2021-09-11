import './Confirm.css';
import '../Forms.css';
import Card from '../Card';

const Confirm = (props) => {



    return (



        <div className={props.visible ? "overlay" : "overlay-hidden"} >
            <Card title="Confirm" className="confirm-dialog" borderColor={props.borderColor} backgroundColor={props.backgroundColor}>
                <form className="form-main">
                    <div className="form-field-group">
                        <label className="form-lebal">
                            Are you sure ?
                        </label>
                    </div>
                    <div class="form-field-group">
                        <button className="button primary" onClick={props.onConfirm}>Ok</button>
                        <button className="button secondary" onClick={props.onCancel}>Cancel</button>
                    </div>
                </form>
            </Card>
        </div>
    );
}

export default Confirm;