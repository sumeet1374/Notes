import './Nav.css';
import React, {  useEffect,  useState } from 'react';

import { ReactComponent as ReactLogo } from '../resources/images/apple-ibooks.svg';
import { BrowserRouter as Router, Route, Link, Switch } from 'react-router-dom';
import NoteList from '../notes/NoteList';
import UserList from '../account/UserList';
import LogOut from '../account/LogOut';
import RegisterUser from '../account/RegisterUser';
import Login from '../auth/Login';
import Error from '../common/Error';
import RegisterUserAdmin from '../account/RegisterUserAdmin';
import NewNote from '../notes/NewNote';



const Nav = (props) => {

    const container = React.createRef();
    const [navListClasses, setNavListClasses] = useState({ hidden: true, listClasses: "nav-list" });
    const [userSubMenuClasses, setUserSubMenuClasses] = useState({ hidden: true, listClasses: "submenu-list" });
    const menuClick = () => {
        if (navListClasses.hidden) {
            setNavListClasses({ hidden: false, listClasses: "nav-list nav-list-show" });
        } else {
            setNavListClasses({ hidden: true, listClasses: "nav-list" });
        }


    }
    const subMenuLinkClick = () => {
        setUserSubMenuClasses({ hidden: true, listClasses: "submenu-list" });
    }

    const subMenuButtonClick = () => {

        setUserSubMenuClasses({ hidden: false, listClasses: "submenu-list active" });
    }

    const collapsseMenu = (event) => {
        if(container.current && !container.current.contains(event.target)){
            setNavListClasses({ hidden: true, listClasses: "nav-list" });
            setUserSubMenuClasses({ hidden: true, listClasses: "submenu-list" });
        }
    }

    useEffect(()=> {
        document.addEventListener("click",collapsseMenu);
        return ()=> {
            document.removeEventListener("click",collapsseMenu);
        }
    },[container]);

    return (
        <Router>
            <nav className="navbar" ref={container}>
                <div className="logo">
                    <div className="logoContainer">
                        <div className="logoImage">
                            <ReactLogo height="40px" width="40px" />
                        </div>

                    </div>
                </div>
                <ul className={navListClasses.listClasses} >
                    <li className="list-item">
                       { props.isAuthenticated && props.profile? <Link to="/">My Notes</Link> :null }
                    </li>
                    <li className="list-item">
                      { props.isAuthenticated && props.profile && props.profile.isAdmin? <Link to="/users">Users</Link> :null }
                    </li>
                    <li className="list-item submenu-list-item">
                        <a href="#" className="submenu-button" onClick={subMenuButtonClick}> {props.profile?props.profile.email:"Profile"} &#9660;</a>
                        <ul className={userSubMenuClasses.listClasses} >
                            
                           
                            { props.isAuthenticated ?<li className="submenu-child-list-item" ><Link to="/logout" onClick={subMenuLinkClick} > Log Out </Link></li> :null }
                           
                            { props.isAuthenticated ? null :<li className="submenu-child-list-item" ><Link to="/register" onClick={subMenuLinkClick} >Register User </Link> </li>}
                           
                            { props.isAuthenticated && props.profile && props.profile.isAdmin ? <li className="submenu-child-list-item" ><Link to="/registeradmin" onClick={subMenuLinkClick} >Create User </Link> </li>:null}
                            
                        </ul>
                    </li>
                </ul>
                <div className="menu" onClick={menuClick}>
                    <div className="menu-line"></div>
                    <div className="menu-line"></div>
                    <div className="menu-line"></div>
                </div>
            </nav>

            <Switch>
            { props.isAuthenticated ?  <Route exact path="/" component={NoteList} /> : <Route  exact path="/" component={Login}></Route> }
                <Route exact path="/users" component={UserList} domain={props.domain} />
                <Route exact path="/logout" component={LogOut}/>
                <Route exact path="/register" component={RegisterUser}/>
                <Route exact path="/registeradmin" component={RegisterUserAdmin}/>
                <Route exact path="/error" component={Error} />
                <Route exact path="/createnote" component={NewNote} />
            </Switch>


        </Router>
    );
};

export default Nav;