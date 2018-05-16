import React from 'react';
import {Link} from 'react-router-dom';

const NotFound = () => (
    <div>
        <h4>
            404! - <Link to="/"> Go back to Home Dashboard.</Link>
        </h4>
    </div>
);

export default NotFound;