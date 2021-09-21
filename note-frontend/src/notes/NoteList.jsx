import Card from '../common/Card';
import Loader from '../common/Loader';
import '../common/Forms.css';
import './NoteList.css';
import { useState,useEffect } from 'react';
import { useHistory } from 'react-router';
import Grid from '../common/Grid';
import { useAuth0 } from "@auth0/auth0-react";
import { getData, postData }from '../common/ajax';
const NoteList = (props) => {


    

    const [pageNumber, setPageNumber] = useState(1);
    const [loading,setLoading] = useState(false);
    const [notes,setNotes] = useState(null);
    const [deleteNote,setDeleteNote] = useState(null);

    let history = useHistory();
    const { getAccessTokenSilently } = useAuth0();
    const pageSize = 5;
    const getValue = (row, field) => {
        return row[field];
    }

    const getDeleteButton = (row, field) => {
        return <button className="button primary" name={row[field]} onClick={onDeleteClick} >Delete</button>
    }

    const getFormattedDate = (row,field) => {
        return new Date(row[field]).toLocaleString('en-GB');
    }

    const onPagerClicked = (event)=> {
        setPageNumber(event.target.name);
    };

    const onDeleteClick =(event)=> {
        const id = event.target.name;
        console.log(id);
        setDeleteNote({id:id});
        
    }

    const onNewNote = (event)=> {
        event.preventDefault();
        history.push("/createnote");
    }
    const gridMetaData = [
        { title: "Note", field: "note", valueFn: getValue , isActionField:false },
        { title: "Created Date", field: "createdDate", valueFn: getFormattedDate , isActionField:false },

        { title: " ", field: "id", valueFn: getDeleteButton , isActionField:true }
    ];

 

    useEffect(()=> {
        const getNotes = async ()=> {
            setLoading(true);
            try {
             
              const data  = await getData(`/notes?pageNumber=${pageNumber}&pageSize=${pageSize}`,getAccessTokenSilently);
           
              setNotes(data);
              setLoading(false);
            }
            catch (err) {
               console.log(err);
               history.push("/error");
              
            }
            finally {
                setLoading(false);
            }
        };

        const deleteNoteFn = async ()=> {
            setLoading(true);
            try {
             
                await postData(`/notes/remove`, deleteNote,getAccessTokenSilently);
              }
              catch (err) {
              
                console.log(err);
                history.push("/error");
               
              }
              finally {
                
               setDeleteNote(null);
               setLoading(false);
              }
        };
       
            if(deleteNote){
                deleteNoteFn();
            }
           
            getNotes();
        
      
     

    },[pageNumber,deleteNote]);

    if(props.isAuthorized)
        history.push("/error");


    return (
        <>
            <Loader visible={loading} />
            <Card title="Notes" className="formStandard">
                <div className="addButtonPane"><button type="button" className="button primary" onClick={onNewNote}>Add New Note</button></div>
                {notes && notes.result && notes.result.length ? <Grid columns={gridMetaData} data={notes.result} totalPages={notes.totalPages} currentPage={pageNumber} onClick={onPagerClicked}></Grid>:loading?"":"No Notes Found."}
            </Card>
        </>

    );
}

export default NoteList;