import React from 'react';
import { connect } from 'react-redux';
import Dialog from '@material-ui/core/Dialog';

const WaitDialog = props => (
    <Dialog
        open={props.loading}
        transitionDuration={{ enter: 1000, exit: 1000 }}
    > <div></div>
    </Dialog>
);


let mapState = (state) => {

    return {
        loading: state.layout.loading
    };
};

export default connect(mapState)(WaitDialog);