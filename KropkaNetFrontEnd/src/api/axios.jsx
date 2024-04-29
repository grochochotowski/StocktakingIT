import axios from 'axios';

const baseURL = 'https://localhost:7238/api'

// Retrieve the JWT token from the cookie
const jwtCookie = document.cookie
    .split(';')
    .map(cookie => cookie.trim())
    .find(cookie => cookie.startsWith('jwtToken='))

let jwtToken = '';
if (jwtCookie) {
    jwtToken = jwtCookie.split('=')[1]
}

export default function instance() {
    return axios.create({
        baseURL,
        withCredentials: true,
        headers: {
            'Authorization': `Bearer ${jwtToken}`
          }
    });
}