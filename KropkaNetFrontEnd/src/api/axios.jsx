import axios from 'axios';

const baseUrl = 'https://localhost:7238/api'
const refreshUrl = 'https://localhost:7238/api/account/refresh'

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
    try {
        const tokens = JSON.parse(getToken())
        const refreshResponse = await axios.post(refreshUrl, {
            jwtToken: tokens.jwtToken,
            refreshToken: tokens.refreshToken
        });

        localStorage.setItem("auth", JSON.stringify(refreshResponse.data));

        return refreshResponse.data.jwtToken;
    } catch (error) {
        throw error;
    }
};

export { axiosInstance, refreshToken };