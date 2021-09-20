import './App.css';
import Nav from './nav/Nav';
import React, { useEffect, useState } from 'react';
import { useAuth0 } from "@auth0/auth0-react";
import { getData } from './common/ajax';




function App(props) {
  
  const { isLoading, error,isAuthenticated ,loginWithRedirect } = useAuth0();
  const [ profile,setProfile] = useState(null);

  const { getAccessTokenSilently } = useAuth0();

  useEffect(()=> {

    const getProfile = async ()=> {
      try{
        const prof = await getData("/users/extId",getAccessTokenSilently);
        setProfile(prof);

     }
     catch(e){
       console.log(e)
     // history.push('/error');
     }
    }

    if(isAuthenticated && !profile){
      getProfile();
    }

  
  },[isAuthenticated,profile]);
  return (
    <div >
      <Nav isAuthenticated={isAuthenticated} domain={props.domain} profile={profile}/>
    </div>


  );
}

export default App;
