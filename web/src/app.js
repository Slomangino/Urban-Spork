import React from 'react';
import ReactDOM from 'react-dom';
import {Provider} from 'react-redux';
import AppComponent from "./components/AppComponent";
import configureStore from './store/configureStore';
import 'bootstrap/dist/css/bootstrap.css';
import 'normalize.css/normalize.css';
import 'react-dates/lib/css/_datepicker.css';
import 'react-table/react-table.css'
import './styles/styles.scss';


const store = configureStore();


const jsx = (
    <Provider store={store}>
        <AppComponent/>
    </Provider>
);

ReactDOM.render(jsx, document.getElementById('app'));

// store.dispatch(getUsersData()).then(() => {
//     ReactDOM.render(jsx, document.getElementById('app'));
// });
