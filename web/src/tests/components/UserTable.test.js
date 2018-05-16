import React from 'react';
import {shallow} from 'enzyme';
import UserTable from "../../components/UserTable";
import getPendingRequests from '../fixtures/get-pending-requests';
import moment from 'moment';

let wrapper, columns;

beforeEach(() => {
    columns = [
        {accessor: 'forFullName', Header: 'Name'},
        {accessor: 'permissionName', Header: 'Request'},
        {
            accessor: 'dateOfRequest',
            Header: 'Date',
            Cell: ({value}) => moment.utc(value).format('ddd MMM D YYYY').toString()
        },
    ];

    const toggleModalIsOpen = jest.fn();

    wrapper = shallow(
        <UserTable
        onRowClick={toggleModalIsOpen}
        columns={columns}
        data={getPendingRequests}
        />
    );

});



test('should render UserTable component correctly with no data', () => {
    let getPendingRequests = [];
    const toggleModalIsOpen = jest.fn();

    wrapper = shallow(
        <UserTable
            onRowClick={toggleModalIsOpen}
            columns={columns}
            data={getPendingRequests}
        />
    );

    expect(wrapper).toMatchSnapshot()
});



test('should render UserTable component correctly with data', () => {
    expect(wrapper).toMatchSnapshot()
});

