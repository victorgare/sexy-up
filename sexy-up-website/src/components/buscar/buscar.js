import React, { Component } from "react"
// import logo from "./logotipo/googlelogo_color_272x92dp.png";
// import logo from "../";

class Buscar extends Component {
    render() {
        return (
            <div>

                <div className="row">
                    <div className="col-lg-8 offset-lg-2">
                        <div className="input-group">
                            <input type="text" className="form-control" />
                            <div className="input-group-append">
                                <button className="btn btn-circle btn-primary">
                                    <i className="fa fa-search" />
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default Buscar;
