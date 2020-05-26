import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Field, reduxForm } from 'redux-form';
import { actionCreators } from '../../../store/Authentication';
import { renderTextField } from '../../common/Controls';

import { withStyles } from '@material-ui/styles';
import Container from '@material-ui/core/Container';
import Button from '@material-ui/core/Button';
import InputAdornment from '@material-ui/core/InputAdornment';
import IconButton from '@material-ui/core/IconButton';
import Visibility from '@material-ui/icons/Visibility';
import VisibilityOff from '@material-ui/icons/VisibilityOff';

const validate = values => {
    const errors = {}
    if (!values.login) {
        errors.login = 'Заполните поле'
    }
    if (!values.password) {
        errors.password = 'Заполните поле'
    }
    return errors
}

const styles = theme => ({
    paper: {
        marginTop: theme.spacing(8),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
    },
    form: {
        width: '100%', // Fix IE 11 issue.
        marginTop: theme.spacing(3),
    },
    login: {
        margin: theme.spacing(3, 0, 2),
    },

});

class LoginPage extends Component {

    clickShowPassword = () => {
        this.setState({ ...this.state, showPassword: !(this.state && this.state.showPassword) });
    };

    render() {
        const { handleSubmit, classes } = this.props;

        return (
            <Container component="div" maxWidth="xs">
                <div className={classes.paper}>

                    <Field component={renderTextField} name="login" label="Логин *"
                        fullWidth autoComplete="login" autoFocus
                    />

                    <Field component={renderTextField} name="password" label="Пароль *"
                        fullWidth autoComplete="current-password"
                        type={this.state && this.state.showPassword ? 'text' : 'password'}
                        InputProps={{
                            endAdornment: (
                                <InputAdornment position="end">
                                    <IconButton aria-label="Toggle password visibility" onClick={this.clickShowPassword}>
                                        {this.state && this.state.showPassword ? <Visibility /> : <VisibilityOff />}
                                    </IconButton>
                                </InputAdornment>
                            ),
                        }}
                    />

                    <Button
                        type="button"
                        fullWidth
                        variant="contained"
                        color="primary"
                        className={classes.login}
                        onClick={handleSubmit}
                    >
                        Войти в систему
                    </Button>

                    login: admin; password: admin
                </div>


            </Container>
        )
    };
}

LoginPage = reduxForm({
    form: 'LoginPage', // a unique identifier for this form
    destroyOnUnmount: true,
    forceUnregisterOnUnmount: true,
    validate
})(LoginPage);


let mapState = (state) => {
    return {
        loginError: state.authentication.error
    };
};

let mapDispatch = (dispatch) => {
    return {
        onSubmit: bindActionCreators(actionCreators.login, dispatch),
    };
};


const ConnectedLoginPage = connect(mapState, mapDispatch)(LoginPage);


const StyledLoginPage = (props) => {
    const { classes } = props;
    return <ConnectedLoginPage classes={classes} />;
}

export default withStyles(styles)(StyledLoginPage);