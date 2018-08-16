import React, { Component } from "react";
import Navigation from "./navigation/navigation";
import Buscar from "./buscar/buscar";
import { BrowserRouter, Route } from 'react-router-dom';
import Servicos from './servicos/servicos';
import LojasProximas from "./lojas-proximas/lojas-proximas";
import Logotipo from "./logotipo/logotipo";

class Root extends Component {
    constructor(props) {
        super(props);

        this.state = {
            firstLoad: true
        }
    }

    render() {
        return (
            <div>
                <div>
                    <Navigation />
                </div>

                <hr className="invisible" />

                <div className="col-lg-8 offset-lg-2">
                    <div className="container">
                        <div className="row">
                            <div className="col-lg-12">
                                <Logotipo />
                                
                                <BrowserRouter>
                                    <div>
                                        <Route exact path="/" component={Buscar} />
                                        <Route path="/buscar" component={Buscar} />
                                        <Route path="/servicos" component={Servicos} />
                                        <Route path="/lojasproximas" component={LojasProximas} />
                                    </div>
                                </BrowserRouter>
                            </div>
                        </div>
                    </div>
                </div>
            </div >
        );
    }
}

export default Root;