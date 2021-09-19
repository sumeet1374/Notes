import './Grid.css';

const Grid = (props) => {

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
                                props.columns.map((column) => <td>{column.valueFn(row,column.field)}</td>)


                            }
                        </tr>

                    })}

                </tbody>
            </table>
        </div>

    )
};

export default Grid;