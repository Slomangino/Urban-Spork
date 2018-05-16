import React from 'react';
import {shallow} from 'enzyme';
import PermissionRequestModal from "../../components/PermissionRequestModal";


test('should render PendingRequests component correctly with no data', () => {
    const wrapper = shallow(<PermissionRequestModal/>);
    expect(wrapper).toMatchSnapshot()
});