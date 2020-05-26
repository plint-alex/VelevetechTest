/* eslint-disable react/prop-types */
import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { withSnackbar } from 'notistack';
import { actionCreators as notifierActions } from '../../store/Notifier';

import IconButton from '@material-ui/core/IconButton';
import CloseIcon from '@material-ui/icons/Close';

class Notifier extends Component {
    displayed = [];

    storeDisplayed = (id) => {
        this.displayed = [...this.displayed, id];
    };

    shouldComponentUpdate({ notifications: newSnacks = [] }) {
        if (!newSnacks.length) {
            this.displayed = [];
            return false;
        }

        const { notifications: currentSnacks } = this.props;
        let notExists = false;
        for (let i = 0; i < newSnacks.length; i += 1) {
            const newSnack = newSnacks[i];
            if (newSnack.dismissed) {
                this.props.closeSnackbar(newSnack.key);
                this.props.removeSnackbar(newSnack.key);
            }

            if (notExists) continue;

            notExists = notExists || !currentSnacks.filter(({ key }) => newSnack.key === key).length;
        }
        return notExists;
    }

    componentDidUpdate() {
        const props = this.props;
        const { notifications = [], closeSnackbar1, enqueueSnackbar } = props;

        notifications.forEach(({ key, message, options = {} }) => {
            // Do nothing if snackbar is already displayed
            if (this.displayed.includes(key)) return;
            // Display snackbar using notistack
            enqueueSnackbar(message, {
                action: key => (<IconButton
                    key="close"
                    aria-label="убрать"
                    color="inherit"
                    onClick={() => {
                        return closeSnackbar1(key);
                    }
                    }
                >
                    <CloseIcon />
                </IconButton>),
                ...options,
                onClose: (event, reason, key) => {
                    if (options.onClose) {
                        options.onClose(event, reason, key);
                    }
                    // Dispatch action to remove snackbar from redux store
                    this.props.removeSnackbar(key);
                }
            });
            // Keep track of snackbars that we've displayed
            this.storeDisplayed(key);
        });
    }

    render() {
        return null;
    }
}

const mapStateToProps = store => ({
    notifications: store.notifier.notifications,
});

const mapDispatchToProps = dispatch => {
    return {
        removeSnackbar: bindActionCreators(notifierActions.removeSnackbar, dispatch),
        closeSnackbar1: bindActionCreators(notifierActions.closeSnackbar, dispatch),
    };
};

export default withSnackbar(connect(
    mapStateToProps,
    mapDispatchToProps,
)(Notifier));