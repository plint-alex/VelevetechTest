import { actionCreators as layoutActions } from './Layout';
import apiRequest from '../services/api';

const baseUrl = 'students/';

const getStudentsSuccessType = 'GET_STUDENTS_SUCCESS';
const getStudentSuccessType = 'GET_STUDENT_SUCCESS';
const addStudentSuccessType = 'ADD_STUDENT_SUCCESS';

const initialState = {};

export const actionCreators = {

    getStudents: (data) => {

        return async dispatch => {
            dispatch(layoutActions.loading());

            try {
                const result = await apiRequest(`${baseUrl}getStudents`, 'POST', data);
                dispatch({ type: getStudentsSuccessType, students: result });
            }
            finally {
                dispatch(layoutActions.loadingComplete());
            }
        };
    },

    deleteStudent: (data) => {

        return async dispatch => {
            dispatch(layoutActions.loading());

            try {
                await apiRequest(`${baseUrl}deleteStudent`, 'POST', data.studentId);
                const result = await apiRequest(`${baseUrl}getStudents`, 'POST', data.searchForm);
                dispatch({ type: getStudentsSuccessType, students: result });
            }
            finally {
                dispatch(layoutActions.loadingComplete());
            }
        };
    },

    getStudent: (id) => {
        return async dispatch => {
            const student = await apiRequest(`${baseUrl}getStudent/${id}`, 'GET');
            dispatch({ type: getStudentSuccessType, student });
        };
    },

    addStudent: (data) => {

        return async dispatch => {
            dispatch(layoutActions.loading());

            try {
                const result = await apiRequest(`${baseUrl}addStudent`, 'POST', data);
                dispatch({ type: addStudentSuccessType, studentId: result.id });
            }
            finally {
                dispatch(layoutActions.loadingComplete());
            }
        };
    },

    updateStudent: (data) => {

        return async dispatch => {
            dispatch(layoutActions.loading());

            try {
                await apiRequest(`${baseUrl}updateStudent`, 'POST', data);
            }
            finally {
                dispatch(layoutActions.loadingComplete());
            }
        };
    },
}

export const reducer = (state, action) => {
    state = state || initialState;
    switch (action.type) {

        case getStudentsSuccessType:
            return { ...state, ...action.students };

        case getStudentSuccessType:
            return { ...state, student: action.student };

        case addStudentSuccessType:
            return { ...state, studentId: action.studentId };

        default:
            return state;
    }
};

