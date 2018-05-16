import React from 'react';
import {shallow} from 'enzyme';
import UserDetailsModal from "../../components/UserDetailsModal";


test('should render UserDetailsModal component correctly with no data', () => {
    const wrapper = shallow(<UserDetailsModal/>);
    expect(wrapper).toMatchSnapshot()
});
