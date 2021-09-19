import './Grid.css';
import PagerButton  from './PagerButton';

const Grid = (props) => {
    const pagesToShow = 5;
    const totalPages = +props.totalPages;
    const currentPage = +props.currentPage;

  

    const totalSlots = Math.floor(totalPages / pagesToShow) + ((totalPages % pagesToShow) > 0 ? 1 : 0);

    const currentSlotNumber = Math.floor(currentPage / pagesToShow) + ((currentPage % pagesToShow) > 0 ? 1 : 0);

    const slotStartPageNumber = (currentSlotNumber - 1) * pagesToShow + 1;
    
    const slotEndPageNumber = currentSlotNumber * pagesToShow;

    const pagerClicked = (event)=> {
      
        props.onClick(event);
    }

    const canShowLink = (number)=>{
   

        if(+number === currentPage)
            return false;
       

        if(+number > totalPages){
            return false;
           
        }
        return  true;
    };

    const pagerArray = [];
    for (let i = slotStartPageNumber; i <= slotEndPageNumber; i++)
        pagerArray.push(i);

        
    return (

        <div className="grid">
            <table >
                <thead>
                    <tr>
                        {props.columns.map((column) => <th>{column.title}</th>)}
                    </tr>
                </thead>
                <tbody>
                    {props.data.map((row) => {
                        return <tr>
                            {
                                props.columns.map((column) => <td>{column.valueFn(row, column.field)}</td>)


                            }
                        </tr>

                    })}

                </tbody>
            </table>
            <div className="pager">
                <div>
                    {/* <div className="pager-button" >&lt;&lt;</div> */}
                    <PagerButton isFirsrtButton="true"  currentPage={currentPage} totalPages={totalPages}  onClick={pagerClicked}></PagerButton>
                    <div className="pager-button">&lt;</div>
                    { pagerArray.map((number,index)=><PagerButton number={number} currentPage={currentPage} totalPages={totalPages} key={index} onClick={pagerClicked}></PagerButton>)}
                
                    <div className="pager-button">&gt;</div>
                    <PagerButton isLastButton="true"  currentPage={currentPage} totalPages={totalPages}  onClick={pagerClicked}></PagerButton>
                </div>
            </div>
        </div>

    )
};

export default Grid;