import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import { Link } from 'react-router-dom';
import { withRouter } from "react-router";

import WaitDialog from './WaitDialog';
import Notifier from './Notifier';

import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import AppBar from '@material-ui/core/AppBar';
import CssBaseline from '@material-ui/core/CssBaseline';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';

const useStyles = makeStyles(theme => ({
    root: {
        display: 'flex',
        minHeight: '100vh',

    },
    headerHeight: {
        height: theme.mixins.toolbar.minHeight,
    },
    appBar: {
        [theme.breakpoints.up('sm')]: {
            zIndex: theme.zIndex.drawer + 1,
        },
    },
    content: {
        padding: theme.spacing(3),
        width: '100%',
    },
}));

const Layout = (props) => {
    const classes = useStyles();

    const pathName = props.location.pathname;

    function getTabValue(pathName) {
        if (pathName.includes('login')) {
            return false;
        }
        if (pathName.includes('student')) {
            return '/students';
        }

        return pathName;
    }

    return (
        <div>
            <div className={classes.root}>
                <WaitDialog />
                <Notifier />
                <CssBaseline />
                <AppBar position="fixed" className={classes.appBar}>
                    <Toolbar>
                        <Typography variant="h6">
                                VelevetechTest
                        </Typography>
                        <Tabs indicatorColor="secondary" value={getTabValue(pathName)} >
                            <Tab value={'/'} label="Home" component={Link} to={'/'} />
                            <Tab value={'/about'} label="About" component={Link} to={'/about'} />
                            <Tab value={'/students'} label="Students" component={Link} to={'/students'} />
                        </Tabs>
                    </Toolbar>
                </AppBar>

                <main className={classes.content}>
                    <div className={classes.headerHeight} />
                    <CssBaseline />
                    {props.children}
                </main>
            </div>
        </div>
    );
};

export default withRouter(Layout);