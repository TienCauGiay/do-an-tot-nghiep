import axios from 'axios';

const URL = "https://localhost:44318/api/v1";

const instance = axios.create({
    baseURL: URL,
});

instance.interceptors.response.use(
    res => res,
    error => { throw error.response.data?.Data; }
);

export default instance;