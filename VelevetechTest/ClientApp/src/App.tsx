import { createMuiTheme } from '@material-ui/core/styles';
import { ThemeProvider } from '@material-ui/styles';
import { SnackbarProvider } from 'notistack';
import React from 'react';
import { Switch } from 'react-router';
import Layout from './components/layout';
import HomePage from './components/pages/home';
import AboutPage from './components/pages/about';
import { StudentsPage, StudentPage } from './components/pages/students';
import LoginPage from './components/pages/login';
import { PrivateRoute } from './components/PrivateRoute';
import { Route } from 'react-router';

const theme = createMuiTheme({
    mixins: {
        toolbar: {
            minHeight: 75,
            '@media (min-width:0px) and (orientation: landscape)': {
                minHeight: 75,
            },
            '@media (min-width:600px)': {
                minHeight: 75,
            },
        },
    },
    palette: {
        secondary: { main: '#FFA632' },
        primary: { main: '#328BCB' },
        //primary: { main: '#562800' },
       // secondary: { main: '#B1FFAF' },
    },
});

export default () => (
    <ThemeProvider theme={theme}>
        <SnackbarProvider preventDuplicate={false} autoHideDuration={20000}>
            <Switch>
                <Layout>
                    <Route exact path="/login" component={LoginPage} />
                    <Route exact path="/" component={HomePage} />
                    <Route exact path="/about" component={AboutPage} />
                    <PrivateRoute exact path="/students" component={StudentsPage} />
                    <PrivateRoute exact path="/students/:id" component={StudentPage} />
                </Layout>
            </Switch >
        </SnackbarProvider>
    </ThemeProvider>
);
