import './Grid.css';
const PagerButton = (props)=> {
  

    if(props.isFirsrtButton){
        if(props.currentPage === 1)
              return <div className="pager-button">&lt;&lt;</div>;
        else
            return <div className="pager-button"><button  name="1" onClick={props.onClick} type="button">&lt;&lt;</button></div>; // This button will always go to first page
             
    }

    if(props.isPreviousSlotButton){
        if(props.currentSlot > 1){
            const slot = +props.currentSlot -1;
            const page = ((slot)*(+props.lastSlot));
            return <div className="pager-button"><button  name={page} onClick={props.onClick} type="button">&lt;</button></div>; 
        }
        else {
          return  <div className="pager-button">&lt;</div>;
        }
    }

    if(props.isNextSlotButton){
        if(+props.currentSlot < +props.lastSlot){
            const page = ((+props.currentSlot)*(+props.lastSlot)) + 1;
            return <div className="pager-button"><button  name={page} onClick={props.onClick} type="button">&gt;</button></div>; 
        }
        else {
            return <div className="pager-button">&gt;</div>;
        }
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