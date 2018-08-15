import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle.min';
import '../src/vendor/fontawesome/css/font-awesome.min.css'
import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import Root from './components/root';
import { BrowserRouter, Route } from 'react-router-dom';
import Navigation from './components/navigation/navigation';
import Buscar from "./components/buscar/buscar"
import Servicos from './components/servicos/servicos';

ReactDOM.render((
    <BrowserRouter>
        <div>
            <Route path="/" component={Root} />
            <Route path="/buscar" component={Buscar} />
            <Route path="/servicos" component={Servicos} />
        </div>
    </BrowserRouter>
), document.getElementById('root'));
