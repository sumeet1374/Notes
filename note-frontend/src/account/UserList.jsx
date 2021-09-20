import Card from '../common/Card';
import React from 'react';
import { useEffect, useState } from 'react';
import { useAuth0 } from "@auth0/auth0-react";
import Grid from '../common/Grid';
import '../common/Forms.css';
import Loader from '../common/Loader';
import { getData }from '../common/ajax';
const UserList = () => {

  const [pageNumber,setPageNumber] = useState(1);

  const pageSize = 10;
  const getValue = (row, field) => {
    return row[field];
  }

  const deactivateClick = (event) => {
    console.log(event.target.name);
  }
  const getValueDeactivate = (row, field) => {
    return <button className="button primary" name={row[field]} onClick={deactivateClick}>Deactivate</button>
  }
  const gridMetaData = [
    { title: "Id", field: "id", valueFn: getValue },
    { title: "First Name", field: "firstName", valueFn: getValue },
    { title: "Last Name", field: "lastName", valueFn: getValue },
    { title: "Email", field: "email", valueFn: getValue },
    { title: "External Id", field: "externalUserId", valueFn: getValue },
    { title: " ", field: "id", valueFn: getValueDeactivate }
  ]

  const { getAccessTokenSilently } = useAuth0();
  const [users, setUsers] = useState(null);
  const [loading, setLoading] = useState(false);

  const onPagerClicked = (event)=> {

    setPageNumber(event.target.name);
   // console.log(event.target.name);
  }

  useEffect(() => {
    const getUserList = async () => {

      setLoading(true);
      try {
       
        const data  = await getData(`/users?pageNumber=${pageNumber}&pageSize=${pageSize}`,getAccessTokenSilently);
     
        setUsers(data);
        setLoading(false);
      }
      catch (err) {
        console.log(err);
        setLoading(false);
      }


    };

    getUserList();

  }, [pageNumber]);


  
  

  return (

    <>
      <Loader visible={loading} />
      
      <Card title="Users" className="formStandard" >
        {users && users.result && users.result.length ? <Grid columns={gridMetaData} data={users.result} totalPages={users.totalPages} currentPage={pageNumber} onClick={onPagerClicked}/> : loading?"":"No users found."}
      </Card>
    </>
  );

}

export default UserList;