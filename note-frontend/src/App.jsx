import './App.css';
import Nav from './nav/Nav';
import React, { useEffect } from 'react';
import { useAuth0 } from "@auth0/auth0-react";



function App() {
  
  const { isLoading, error,isAuthenticated ,loginWithRedirect } = useAuth0();

  // useEffect(()=> {
  //   loginWithRedirect();
  // },[]);
  return (
    <div >
      <Nav isAuthenticated={isAuthenticated} />
    </div>


  );
}

export default App;
