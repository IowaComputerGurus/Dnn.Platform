import React, { Component, PropTypes } from "react";
import { connect } from "react-redux";
import resx from "../../../resources";
import styles from "./style.less";
import "./style.less";

class HtmlEditorManagerPanelBody extends Component {
    constructor() {
        super();
    }

    getIframeUrl() {
        return "/Host/HTMLEditorManager?portalid=" + this.props.portalId + "&portpopUp=true";
    }

    /* eslint-disable react/no-danger */
    render() {
        return (
            <div className={styles.htmlEditorManager}>
                <div className="htmlEditor-back" onClick={this.props.closeHtmlEditorManager.bind(this)}>{resx.get("BackToSiteBehavior")}</div>
                <iframe className="htmlEditorIframe" src={this.getIframeUrl()} frameBorder="0" />
            </div>
        );
    }
}

HtmlEditorManagerPanelBody.propTypes = {
    dispatch: PropTypes.func.isRequired,
    tabIndex: PropTypes.number,
    portalId: PropTypes.number,
    closeHtmlEditorManager: PropTypes.func
};

function mapStateToProps(state) {
    return {
        tabIndex: state.pagination.tabIndex
    };
}

export default connect(mapStateToProps)(HtmlEditorManagerPanelBody);