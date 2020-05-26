import localStorageService from './localStorageService';
import apiRequest from './api';

export default {
    getAuth: () => {
        return localStorageService.getAuth();
    },
    login: async (data) => {
        const auth = await apiRequest('authentication/login', 'POST', data);

        // login successful if there's a jwt token in the response
        if (auth && !auth.error) {
            localStorageService.saveAuth(auth);
        }
        else if (auth.error) {
            throw new Error(auth.error);
        }
        else {
            throw new Error('ошибка при аутентификации');
        }
        return auth;
    },

    logout: async () => {
        var auth = localStorageService.getAuth();

        await apiRequest('authentication/logout', 'POST', { userId: auth.userId });

        localStorageService.clearAuth();
    },
};

