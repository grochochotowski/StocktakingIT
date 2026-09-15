import axios from 'axios';

const baseUrl = 'https://localhost:44396/api'
const refreshUrl = 'https://localhost:44396/api/account/refresh'

const getToken = () => {
    const auth = localStorage.getItem('auth');
    return auth
}

const axiosInstance = axios.create({
    baseURL: baseUrl,
    headers: {
        'Content-Type': 'application/json'
    },
    withCredentials: true,
});

const refreshToken = async () => {
    const tokens = JSON.parse(getToken())
    const refreshResponse = await axios.post(refreshUrl, {
        jwtToken: tokens.jwtToken,
        refreshToken: tokens.refreshToken
    }, {
        withCredentials: true
    });
    console.log("refresh")

    localStorage.setItem("auth", JSON.stringify(refreshResponse.data));

    return refreshResponse.data.jwtToken;
};

export { axiosInstance, refreshToken };
