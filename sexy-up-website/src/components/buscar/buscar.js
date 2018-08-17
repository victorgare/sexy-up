import React, { Component } from "react"

class Buscar extends Component {

    search() {
        console.log("clicou");
    }

    render() {
        return (
            <div>
                <div className="row">
                    <div className="col-lg-8 offset-lg-2">
                        <div className="input-group">
                            <input type="text" className="form-control" />
                            <div className="input-group-append">
                                <button onClick={this.search} className="btn btn-circle btn-primary">
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
