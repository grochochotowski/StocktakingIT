import axios from '../api/axios'
import useAuth from './useAuth'

export default function useRefreshToken() {
    const { setAuth } = useAuth()

    async function refresh() {
        const response = await axios.get('/refresh');
    }

    setAuth(prev => {
        console.log(JSON.stringify(prev))
        console.log(JSON.stringify(response.data))
        return {...prev, accessToken: response.data}
    })
    
    return refresh
}
