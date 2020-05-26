import localStorageService from './localStorageService';
import history from '../history';

const baseUrl = 'api/';

export const getUrl = (actionName) => {
    return `${baseUrl}${actionName}`;
};

export default async function apiRequest(actionName, httpMethod, data) {

    let auth = localStorageService.getAuth();

    let accessToken = auth && auth.accessToken;

    //если токен протух, то надо его обновить
    if (accessToken && new Date(auth.expirationTime) < new Date()) {

        const refreshResponse = await httpRequest('authentication/refreshtoken', 'POST', { accessToken, refreshToken: auth.refreshToken });
        const newTokens = refreshResponse.ok ? await refreshResponse.json() : {};
        localStorageService.saveAuth({ ...auth, ...newTokens });
        accessToken = newTokens.accessToken;
    }

    var response = await httpRequest(actionName, httpMethod, data, accessToken);

    if (response.status === 401) {
        history.push('/login');
    }

    if (!response.ok) {
        const error = await response.json();
        throw new Error(error.message, error.detail);
    }

    return await response.json();
}


export const upload = async (url, opts = {}, onProgress) => {
    let auth = localStorageService.getAuth();

    let accessToken = auth && auth.accessToken;

    //если токен протух, то надо его обновить
    if (accessToken && new Date(auth.expirationTime) < new Date()) {

        const refreshResponse = await httpRequest('authentication/refreshtoken', 'POST', { accessToken, refreshToken: auth.refreshToken });
        const newTokens = refreshResponse.ok ? await refreshResponse.json() : {};
        localStorageService.saveAuth({ ...auth, ...newTokens });
        accessToken = newTokens.accessToken;
    }

    return new Promise((res, rej) => {
        let xhr = new XMLHttpRequest();
        xhr.open(opts.method || 'get', url);
        for (var k in opts.headers || {})
            xhr.setRequestHeader(k, opts.headers[k]);

        if (accessToken) {
            xhr.setRequestHeader('Authorization', 'Bearer ' + accessToken);
        }

        xhr.onload = e => res(e.target.responseText);
        xhr.onerror = rej;
        if (xhr.upload && onProgress)
            xhr.upload.onprogress = onProgress; // event.loaded / event.total * 100 ; //event.lengthComputable
        xhr.send(opts.body);
    });
};

async function httpRequest(actionName, httpMethod, data, accessToken) {
    const requestOptions = {
        method: httpMethod,
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    };

    if (accessToken) {
        requestOptions.headers.Authorization = 'Bearer ' + accessToken;
    }

    const url = `${baseUrl}${actionName}`;

    return await fetch(url, requestOptions);
}