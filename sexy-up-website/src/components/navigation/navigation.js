import React, { Component } from "react";
import NavigationItem from "./navigation-item";

class Navigation extends Component {

    render() {
        return (
            <nav className="navbar">
                <button className="btn btn-primary collapsed" type="button" data-toggle="collapse" data-target="#menu-itens" aria-controls="menu-itens"
                    aria-expanded="false" aria-label="Menus">
                    <span className="fa fa-bars"></span>
                </button>

                <div className="navbar-collapse collapse dropdown-menu bigmenu" id="menu-itens">
                    <table>
                        <tbody className="text-center">
                            <tr>
                                <td className="nav-item">
                                    <NavigationItem
                                        route="/buscar"
                                        cssClass="fa fa-search"
                                        title="Buscar" />
                                </td>
                                <td className="nav-item">
                                    <NavigationItem
                                        route="/lojasproximas"
                                        cssClass="fa fa-map-marker text-danger"
                                        title="Lojas Próximas" />
                                </td>
                            </tr>
                            <tr>
                                <td className="nav-item">
                                    <NavigationItem
                                        route="/servicos"
                                        cssClass="fa fa-handshake-o text-warning"
                                        title="Serviços" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </nav>
        );
    }
}

export default Navigation;