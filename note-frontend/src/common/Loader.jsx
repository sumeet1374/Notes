import './Loader.css';

const Loader = (props) => {

    const loaderClass = `${props.visible ? "overlay-loader" : "overlay-hidden-loader"}`;
    return <div className={loaderClass}>
        <svg className="loaderBox" height="7rem" width="7rem">
            <g>
                <circle cx="3rem" cy="3rem"  r="3rem" fill="black">
                 
                </circle>
                <text x="1rem" y="3rem" fill="white" >
                    Loading ...
                </text>
               
            </g>


        </svg>

    </div>
}

export default Loader;