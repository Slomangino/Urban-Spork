import React from 'react';
import {shallow} from 'enzyme';
import {filters, altFilters} from '../fixtures/filters';
import {UserManagementPage} from "../../components/UserManagementPage";

let wrapper, setTextFilter, history;

beforeEach(() => {
    setTextFilter = jest.fn();
    history = {push: jest.fn()};

    wrapper = shallow(<UserManagementPage
        filters={filters}
        setTextFilter={setTextFilter}
        history={history}
    />);

});

test('should render UserManagementPage component correctly with no data no filters', () => {
    expect(wrapper).toMatchSnapshot()
});


test('should render UserManagementPage component correctly with no data and altFilters', () => {
    wrapper.setProps({filters: altFilters});
    expect(wrapper).toMatchSnapshot()
});


test('should handle text change', () => {
    const value = 'name';

    // wrapper.find('Input')

    wrapper.find('Input').simulate('change', {
        target: {value}
    });

    expect(setTextFilter).toHaveBeenLastCalledWith(value);
});

test('modal should be closed by default', () => {
    expect(wrapper.state('modalIsOpen')).toBe(false)
});

test('should handle opening create user window on button click', () => {
    wrapper.find('Button').simulate('click');

    expect(history.push).toHaveBeenLastCalledWith('/create-user');
});