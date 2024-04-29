import axios from 'axios';

const baseURL = 'https://localhost:7157/api';

export default function instance() {
    return axios.create({
        baseURL,
        withCredentials: true,
    });
}