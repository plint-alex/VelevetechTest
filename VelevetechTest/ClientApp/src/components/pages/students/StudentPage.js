import React, { Component } from 'react';
import { withRouter } from "react-router";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Field, reduxForm } from 'redux-form';

import history from '../../../history';
import { actionCreators } from '../../../store/Students';
import { renderTextField, renderSelectField } from '../../common/Controls';

import { withStyles } from '@material-ui/styles';
import Button from '@material-ui/core/Button';
import MenuItem from '@material-ui/core/MenuItem';
import Container from '@material-ui/core/Container';


const validate = values => {
    const errors = {};
    if (values.uid && (values.uid.length < 6 || values.uid.length > 16)) {
        errors.uid = 'Length must be between 6 and 16';
    }

    if (!values.sex) {
        errors.sex = 'This field is required';
    }

    if (!values.firstName) {
        errors.firstName = 'This field is required';
    }
    else if (values.firstName.length > 40) {
        errors.firstName = 'Length must be less or equal 40';
    }

    if (!values.lastName) {
        errors.lastName = 'This field is required';
    }
    else if (values.lastName.length > 40) {
        errors.lastName = 'Length must be less or equal 40';
    }

    if (values.middleName && values.middleName.length > 60) {
        errors.middleName = 'Length must be less or equal 60';
    }
    return errors;
}

const styles = theme => ({
    paper: {
        marginTop: theme.spacing(8),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
    },
    formControl: {
        margin: theme.spacing(1),
        //minWidth: 400,
    },

    saveButton: {
        margin: theme.spacing(3, 0, 2),
    },

});


class Student extends Component {
    constructor(props) {
        super(props);
        this.state = {
            canRedirect: false,
        };
    }

    componentDidMount() {
        const studentId = this.props.match.params.id;
        if (studentId && studentId !== 'new') {
            this.props.getStudent(studentId);
        }
    }

    componentDidUpdate() {
        const studentId = this.props.match.params.id;
        if ((!studentId || studentId === 'new') && this.props.studentId) {
            if (this.state.canRedirect) {
                history.push(`/students/${this.props.studentId}`);
                this.setState({ canRedirect: false });
            }
        }
        else if (studentId && studentId !== 'new' && this.props.studentId && this.props.studentId !== studentId) {
            this.props.getStudent(studentId);
        }
    }

    render() {
        const { handleSubmit, onAddStudent, onUpdateStudent, classes, studentId } = this.props;

        const isNew = !studentId || studentId === 'new';
        return (

            <Container component="div" maxWidth="xs">
                <div className={classes.paper}>
                    <Field fullWidth component={renderTextField} name="uid" label="UID" className={classes.formControl} inputProps={{ minLength: 6, maxLength: 16 }} />

                    <Field fullWidth component={renderSelectField} name="sex" label="Sex" className={classes.formControl}>
                        <MenuItem value={'Male'}>Male</MenuItem>
                        <MenuItem value={'Female'}>Female</MenuItem>
                    </Field>

                    <Field fullWidth component={renderTextField} name="lastName" label="Last Name" className={classes.formControl} inputProps={{ maxLength: 40 }} />

                    <Field fullWidth component={renderTextField} name="firstName" label="First name" className={classes.formControl} inputProps={{ maxLength: 40 }} />

                    <Field fullWidth component={renderTextField} name="middleName" label="Middle Name" className={classes.formControl} inputProps={{ maxLength: 60 }} />

                    <Button
                        fullWidth
                        type="button"
                        variant="contained"
                        color="primary"
                        className={classes.saveButton}
                        onClick={handleSubmit(data => { this.setState({ canRedirect: true }); if (isNew) onAddStudent(data); else onUpdateStudent({ ...data, id: studentId }); })}
                    >
                        Save
                    </Button>
                </div>
            </Container>
        )
    };
}

Student = reduxForm({
    form: 'Student', // a unique identifier for this form
    destroyOnUnmount: true,
    forceUnregisterOnUnmount: true,
    enableReinitialize: true,
    validate
})(Student);


let mapState = (state) => {

    return {
        initialValues: {
            ...state.students.student,
        },
        studentId: state.students.studentId,
    };
};

let mapDispatch = (dispatch) => {
    return {
        onAddStudent: bindActionCreators(actionCreators.addStudent, dispatch),
        onUpdateStudent: bindActionCreators(actionCreators.updateStudent, dispatch),
        getStudent: bindActionCreators(actionCreators.getStudent, dispatch),
    };
};


const ConnectedStudent = connect(mapState, mapDispatch)(Student);


const StyledStudent = (props) => {
    return <ConnectedStudent {...props} />;
}

export default withRouter(withStyles(styles)(StyledStudent));