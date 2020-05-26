import React, { Component } from 'react';
import { withRouter } from "react-router";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import history from '../../../history';
import { actionCreators } from '../../../store/Students';

import { withStyles } from '@material-ui/styles';
import Button from '@material-ui/core/Button';
import MenuItem from '@material-ui/core/MenuItem';
import Container from '@material-ui/core/Container';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import TableSortLabel from '@material-ui/core/TableSortLabel';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
import TextField from '@material-ui/core/TextField';
import InputLabel from '@material-ui/core/InputLabel';
import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';
import TablePagination from '@material-ui/core/TablePagination';
import DeleteIcon from '@material-ui/icons/Delete';

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
    saveButton: {
        margin: theme.spacing(3, 0, 2),
    },
    visuallyHidden: {
        border: 0,
        clip: 'rect(0 0 0 0)',
        height: 1,
        margin: -1,
        overflow: 'hidden',
        padding: 0,
        position: 'absolute',
        top: 20,
        width: 1,
    },
    clickable: {
        cursor: 'pointer',
    },
});

const initialPageInfo = {
    pageNumber: 1,
    sortByField: 'FirstName',
    desc: false,
};


class Students extends Component {
    constructor(props) {
        super(props);
        this.state = {
            searchForm: {
                ...initialPageInfo,
                pageSize: 10,
            },
        };
    }

    componentDidMount() {
        this.props.getStudents({
            ...this.state.searchForm,
        });
    }

    componentDidUpdate() {

    }

    onSearchStudents() {
        this.setState(state => ({
            searchForm: {
                ...state.searchForm,
                ...initialPageInfo,
            }
        }));

        this.props.getStudents({ ...this.state.searchForm });
    }

    onChangeRowsPerPage = (event) => {
        const pageSize = parseInt(event.target.value, 10);
        this.setState(state => ({
            searchForm: {
                ...state.searchForm,
                pageSize,
                pageNumber: 1,
            }
        }));

        this.props.getStudents({
            ...this.state.searchForm,
            pageSize,
            pageNumber: 1,
        });
    };

    onChangePage = (event, pageNumber) => {
        this.setState(state => ({
            searchForm: {
                ...state.searchForm,
                pageNumber: pageNumber + 1,
            }
        }));

        this.props.getStudents({ ...this.state.searchForm, pageNumber: pageNumber + 1 });
    };

    onDeleteStudent = (studentId) => () => {
        this.props.deleteStudent({ studentId, searchForm: this.state.searchForm });
    };

    onChangeOrder = (property) => (event) => {

        const desc = this.state.searchForm.sortByField === property && !this.state.searchForm.desc;

        this.setState(state => ({
            searchForm: {
                ...state.searchForm,
                sortByField: property,
                desc: desc,
            }
        }));

        this.props.getStudents({
            ...this.state.searchForm,
            sortByField: property,
            desc: desc,
        });
    };


    onAddStudent() {
        history.push(`/students/new`);
    }

    onEditStudent(id) {
        history.push(`/students/${id}`);
    }

    onChange = item => {
        this.setState(state => ({
            searchForm: {
                ...state.searchForm,
                ...item,
            }
        }));
    };

    render() {
        const { classes, students, total, pageNumber } = this.props;

        const { searchForm } = this.state;

        const orderBy = searchForm.sortByField;
        const order = searchForm.desc ? 'asc' : 'desc';

        return (
            <Container component="div" >
                <Grid container className={classes.root} spacing={2}>
                    <Grid item xs={12}>
                        <Grid container justify="center" spacing={2}>
                            <Grid item xs={12} sm={2}>
                                <TextField fullWidth label="Uid" value={(searchForm.uids && searchForm.uids[0]) || ''} onChange={e => this.onChange({ uids: (e.target.value && [e.target.value]) || undefined })} />
                            </Grid>
                            <Grid item xs={12} sm={1}>
                                <FormControl fullWidth className={classes.formControl}>
                                    <InputLabel>Sex</InputLabel>
                                    <Select
                                        fullWidth
                                        value={searchForm.sex || ''}
                                        onChange={e => this.onChange({ sex: e.target.value || undefined })}
                                    >
                                        <MenuItem value="">
                                            <em>None</em>
                                        </MenuItem>
                                        <MenuItem value={'Male'}>Male</MenuItem>
                                        <MenuItem value={'Female'}>Female</MenuItem>
                                    </Select>
                                </FormControl>
                            </Grid>
                            <Grid item xs={12} sm={2}>
                                <TextField fullWidth label="First name" value={searchForm.firstName || ''} onChange={e => this.onChange({ firstName: e.target.value || undefined })} />
                            </Grid>
                            <Grid item xs={12} sm={2}>
                                <TextField fullWidth label="Last name" value={searchForm.lastName || ''} onChange={e => this.onChange({ lastName: e.target.value || undefined })} />
                            </Grid>
                            <Grid item xs={12} sm={2}>
                                <TextField fullWidth label="Middle name" value={searchForm.middleName || ''} onChange={e => this.onChange({ middleName: e.target.value || undefined })} />
                            </Grid>
                            <Grid item xs={12} sm={1}>
                                <Button fullWidth
                                    type="button"
                                    variant="contained"
                                    color="primary"
                                    className={classes.saveButton}
                                    onClick={() => this.onSearchStudents()}
                                >
                                    Search
                                    </Button>
                            </Grid>
                        </Grid>
                    </Grid>
                </Grid>

                <Paper >
                    <TableContainer component={Paper}>
                        <Table className={classes.table} aria-label="simple table">
                            <TableHead>
                                <TableRow>
                                    <TableCell
                                        align={'left'}
                                        padding={'default'}
                                        sortDirection={orderBy === 'FirstName' ? order : false}
                                    >
                                        <TableSortLabel
                                            active={orderBy === 'FirstName'}
                                            direction={orderBy === 'FirstName' ? order : 'asc'}
                                            onClick={this.onChangeOrder('FirstName')}
                                        >
                                            First Name
                                        {orderBy === 'FirstName' ? (
                                                <span className={classes.visuallyHidden}>
                                                    {order === 'desc' ? 'sorted descending' : 'sorted ascending'}
                                                </span>
                                            ) : null}
                                        </TableSortLabel>
                                    </TableCell>

                                    <TableCell
                                        align={'left'}
                                        padding={'default'}
                                        sortDirection={orderBy === 'LastName' ? order : false}
                                    >
                                        <TableSortLabel
                                            active={orderBy === 'LastName'}
                                            direction={orderBy === 'LastName' ? order : 'asc'}
                                            onClick={this.onChangeOrder('LastName')}
                                        >
                                            Last Name
                                        {orderBy === 'LastName' ? (
                                                <span className={classes.visuallyHidden}>
                                                    {order === 'desc' ? 'sorted descending' : 'sorted ascending'}
                                                </span>
                                            ) : null}
                                        </TableSortLabel>
                                    </TableCell>

                                    <TableCell
                                        align={'left'}
                                        padding={'default'}
                                        sortDirection={orderBy === 'MiddleName' ? order : false}
                                    >
                                        <TableSortLabel
                                            active={orderBy === 'MiddleName'}
                                            direction={orderBy === 'MiddleName' ? order : 'asc'}
                                            onClick={this.onChangeOrder('MiddleName')}
                                        >
                                            Middle Name
                                        {orderBy === 'MiddleName' ? (
                                                <span className={classes.visuallyHidden}>
                                                    {order === 'desc' ? 'sorted descending' : 'sorted ascending'}
                                                </span>
                                            ) : null}
                                        </TableSortLabel>
                                    </TableCell>

                                    <TableCell
                                        align={'left'}
                                        padding={'default'}
                                        sortDirection={orderBy === 'Sex' ? order : false}
                                    >
                                        <TableSortLabel
                                            active={orderBy === 'Sex'}
                                            direction={orderBy === 'Sex' ? order : 'asc'}
                                            onClick={this.onChangeOrder('Sex')}
                                        >
                                            Sex
                                        {orderBy === 'Sex' ? (
                                                <span className={classes.visuallyHidden}>
                                                    {order === 'desc' ? 'sorted descending' : 'sorted ascending'}
                                                </span>
                                            ) : null}
                                        </TableSortLabel>
                                    </TableCell>

                                    <TableCell
                                        align={'left'}
                                        padding={'default'}
                                        sortDirection={orderBy === 'Uid' ? order : false}
                                    >
                                        <TableSortLabel
                                            active={orderBy === 'Uid'}
                                            direction={orderBy === 'Uid' ? order : 'asc'}
                                            onClick={this.onChangeOrder('Uid')}
                                        >
                                            UID
                                        {orderBy === 'Uid' ? (
                                                <span className={classes.visuallyHidden}>
                                                    {order === 'desc' ? 'sorted descending' : 'sorted ascending'}
                                                </span>
                                            ) : null}
                                        </TableSortLabel>
                                    </TableCell>

                                    <TableCell
                                        padding={'default'}
                                    >

                                    </TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {students && students.map((student) => (
                                    <TableRow key={student.id}>
                                        <TableCell className={classes.clickable} onClick={() => this.onEditStudent(student.id)} component="th" scope="row">
                                            {student.firstName}
                                        </TableCell>
                                        <TableCell component="th" scope="row">
                                            {student.lastName}
                                        </TableCell>
                                        <TableCell component="th" scope="row">
                                            {student.middleName}
                                        </TableCell>
                                        <TableCell component="th" scope="row">
                                            {student.sex}
                                        </TableCell>
                                        <TableCell component="th" scope="row">
                                            {student.uid}
                                        </TableCell>
                                        <TableCell component="th" scope="row">
                                            <DeleteIcon className={classes.clickable} onClick={this.onDeleteStudent(student.id)} />
                                        </TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                    </TableContainer>
                    <TablePagination
                        rowsPerPageOptions={[5, 10, 25]}
                        component="div"
                        count={total || 0}
                        rowsPerPage={searchForm.pageSize}
                        page={(pageNumber || 1) - 1}
                        onChangePage={this.onChangePage}
                        onChangeRowsPerPage={this.onChangeRowsPerPage}
                    />
                </Paper>

                <Button
                    type="button"
                    variant="contained"
                    color="primary"
                    className={classes.saveButton}
                    onClick={() => this.onAddStudent()}
                >
                    Add new student
                    </Button>
            </Container >
        )
    };
}


let mapState = (state) => {

    return {
        ...state.students,
    };
};

let mapDispatch = (dispatch) => {
    return {
        getStudents: bindActionCreators(actionCreators.getStudents, dispatch),
        deleteStudent: bindActionCreators(actionCreators.deleteStudent, dispatch),
    };
};


const ConnectedStudents = connect(mapState, mapDispatch)(Students);


const StyledStudents = (props) => {
    return <ConnectedStudents {...props} />;
}

export default withRouter(withStyles(styles)(StyledStudents));