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
                <div className="row">
                    {this.renderComponent()}
                </div>
            </div>
        );
    }
}

export default Root;