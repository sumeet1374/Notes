import Card from '../common/Card';
import React from 'react';
import { useEffect, useState  } from 'react';
import axios from 'axios';
import { useAuth0 } from "@auth0/auth0-react";
import Grid from '../common/Grid';
import '../common/Forms.css';
const UserList = ()=> {
  
   const getValue = (row,field)=> {
      return row[field];
   }

   const deactivateClick = (event)=> {
    console.log(event.target.name);
   }
   const getValueDeactivate =(row,field)=>{
     return <button className="button primary" name={row[field]} onClick={deactivateClick}>Deactivate</button>
   }
  const gridMetaData = [
    { title:"Id", field:"id", valueFn:getValue},
    { title:"First Name", field:"firstName", valueFn:getValue},
    { title:"Last Name", field:"lastName", valueFn:getValue},
    { title:"Email", field:"email",valueFn:getValue},
    { title:"External Id", field:"externalUserId",valueFn:getValue},
    { title:" ", field:"id",valueFn:getValueDeactivate}
  ]

    const { getAccessTokenSilently } = useAuth0();
    const [users,setUsers] = useState(null);
   
    useEffect(()=>{
        const getUserList = async ()=> {
            try{
              //  const urlAuth = `https://${process.env.REACT_APP_AUTH0_DOMAIN}/api/v2/`;
              //  console.log(urlAuth);
                const accessToken = await getAccessTokenSilently({
                    audience: `${process.env.REACT_APP_API_AUDIENCE}`,
                    scope: "read:notes"
                  });

                  let config = {
                    headers: {
                      'Authorization': `Bearer ${accessToken}`
                    }
                  }
                  console.log(config);

                  const url = `${process.env.REACT_APP_API_BASE_URL}/users?pageNumber=1&pageSize=10`;
                  const response = await axios.get(url,config);
                  setUsers(response.data);
            }
            catch(err){
                console.log(err);
            }
           

        };

        getUserList();
      
    },[]);

    return  <Card title="Users" className="formStandard" >
        {users && users.result && users.result.length? <Grid columns={gridMetaData} data={users.result} />:"No user found."}
    </Card>
}

export default UserList;