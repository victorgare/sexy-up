import React, { Component } from 'react';

class NavigationItem extends Component {
    render() {
        return (
            <a className="nav-link" href={this.props.route}>
                <i className={this.props.cssClass}></i>
                <p>{this.props.title}</p>
            </a>
        );
    }
}

export default NavigationItem;
