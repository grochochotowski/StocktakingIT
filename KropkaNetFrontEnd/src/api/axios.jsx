import axios from 'axios';

const BASEURL = 'https://localhost:7238/api'

export default axios.create({
    baseURL : BASEURL,
    headers: { 'Content-Type': 'application/json' },
    withCredentials: true
})