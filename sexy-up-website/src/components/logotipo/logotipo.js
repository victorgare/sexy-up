import React from "react"

class Logotipo extends React.Component {

    render() {
        return (
            <div>
                <div className="row">
                    <div className="col-lg-4 offset-lg-4">
                        <a href="/">
                            <img src={"/logotipo.png"} className="img-fluid" alt="Sexy Up logo" />
                        </a>
                    </div>
                </div>

                <hr className="invisible" />
            </div>
        );
    }
}

export default Logotipo
