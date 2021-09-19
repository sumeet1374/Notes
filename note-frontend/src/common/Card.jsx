import './Card.css';

const Card = (props)=> {


    return <div className={`card ${props.className}`} style={props.borderColor?{borderColor:props.borderColor}:null}>
        <div className="cardTitle" style={props.backgroundColor?{backgroundColor:props.backgroundColor}:null}>
            {props.title}
        </div>
        <div className="cardContent">
            {props.children}
        </div>
    </div>
};

export default Card;