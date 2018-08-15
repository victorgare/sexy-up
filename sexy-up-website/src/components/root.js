import React, { Component } from "react";
import Navigation from "./navigation/navigation";
import Buscar from "./buscar/buscar";

class Root extends Component {
    constructor(props) {
        super(props);

        this.state = {
            firstLoad: true
        }
    }

    renderComponent() {
        const path = this.props.location.pathname;

        if (path === "/") {
            return <Buscar />
        } else {
            return this.state.children;
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
                                {this.renderComponent()}
                            </div>
                        </div>
                    </div>
                </div>
            </div >
        );
    }
}

export default Root;