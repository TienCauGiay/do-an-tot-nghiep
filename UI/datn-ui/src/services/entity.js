import axios from 'axios';

const URL = "https://localhost:44393/api/";

const entity = axios.create({
    baseURL: URL,
});

// Thêm interceptor để tự động thêm token vào header mỗi khi gửi yêu cầu
entity.interceptors.request.use(
    config => {
        const token = sessionStorage.getItem('token'); // Lấy token từ localStorage
        if (token) {
            config.headers.Authorization = `${token}`;
        }
        return config;
    },
    error => {
        return Promise.reject(error);
    }
);

entity.interceptors.response.use(
    res => res,
    error => { throw error.response.data?.Data; }
);

export default entity;
