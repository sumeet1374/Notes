import './Grid.css';
const PagerButton = (props)=> {
  

    if(props.isFirsrtButton){
        if(props.currentPage === 1)
              return <div className="pager-button">&lt;&lt;</div>;
        else
            return <div className="pager-button"><button  name="1" onClick={props.onClick} type="button">&lt;&lt;</button></div>; // This button will always go to first page
             
    }

    if(props.isLastButton){
        if(props.currentPage === props.totalPages)
              return <div className="pager-button">&gt;&gt;</div>;
        else
            return <div className="pager-button"><button  name={props.totalPages} onClick={props.onClick} type="button">&gt;&gt;</button></div>; // This button will always go to last page
             
    }
      
   

    if(+props.number === props.currentPage){
        return <div className="pager-button">{props.number}</div>
    }

    if(+props.number > +props.totalPages){
        return <div className="pager-button">{props.number}</div>
    }



    return <div className="pager-button"><button  name={props.number} onClick={props.onClick} type="button">{props.number}</button></div>

}

export default PagerButton;