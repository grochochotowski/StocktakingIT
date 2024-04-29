import axios from 'axios';

const baseURL = 'https://localhost:7238/api';

export default function instance() {
    return axios.create({
        baseURL,
        withCredentials: true,
    });
}