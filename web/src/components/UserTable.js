import React from 'react';
import ReactTable from 'react-table'


// named export is for testing b/c it's not connected to the store
class UserTable extends React.Component {

    render() {
        return (
            <div style={{margin: '20px'}}>
                <ReactTable
                    data={this.props.data}
                    columns={this.props.columns}
                    defaultSorted={[
                        {
                            id: this.props.columns[0].accessor,
                            desc: false
                        }
                    ]}
                    resizable={true}
                    defaultPageSize={10}
                    className="-striped -highlight"

                    // https://github.com/react-tools/react-table#custom-props
                    getTrProps={(state, rowInfo, column, instance) => (
                        {
                            onClick: () => this.props.onRowClick(rowInfo.original)
                        }
                    )}
                    // Component={()=> console.log('hey')}
                />
            </div>
        )
    }

}

export default UserTable;