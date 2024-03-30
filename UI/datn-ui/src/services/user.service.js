import axios from 'axios';
import config from './config';
import { authHeader } from '../helpers';

export const userService = {
    login,
    logout,
    register,
    getAll,
    getById,
    update,
    delete: _delete
};

function login(username, password) {
    return axios.post(`${config.apiUrl}/users/authenticate`, { username, password })
        .then(handleResponse)
        .then(user => {
            if (user.token) {
                localStorage.setItem('user', JSON.stringify(user));
            }
            return user;
        });
}

function logout() {
    localStorage.removeItem('user');
}

function register(user) {
    return axios.post(`${config.apiUrl}/users/register`, user).then(handleResponse);
}

function getAll() {
    return axios.get(`${config.apiUrl}/users`, { headers: authHeader() }).then(handleResponse);
}

function getById(id) {
    return axios.get(`${config.apiUrl}/users/${id}`, { headers: authHeader() }).then(handleResponse);
}

function update(user) {
    return axios.put(`${config.apiUrl}/users/${user.id}`, user, { headers: { ...authHeader(), 'Content-Type': 'application/json' } }).then(handleResponse);
}

function _delete(id) {
    return axios.delete(`${config.apiUrl}/users/${id}`, { headers: authHeader() }).then(handleResponse);
}

function handleResponse(response) {
    const data = response.data;
    if (!response.ok) {
        if (response.status === 401) {
            logout();
            location.reload(true);
        }
        const error = (data && data.message) || response.statusText;
        return Promise.reject(error);
    }
    return data;
}
 export default userService;