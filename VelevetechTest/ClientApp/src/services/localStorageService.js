const tokenKey = 'TOKEN_KEY';

export default {
    getAuth: () => {
        let auth = localStorage.getItem(tokenKey);
        if (auth) {
            return JSON.parse(localStorage.getItem(tokenKey));
        }
        return auth;
    },

    saveAuth: (auth) => {
        localStorage.setItem(tokenKey, JSON.stringify(auth));
    },

    clearAuth: () => {
        localStorage.removeItem(tokenKey);
    }
}