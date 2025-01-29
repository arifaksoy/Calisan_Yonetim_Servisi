import axios from 'axios'
import { jwtDecode } from 'jwt-decode'

interface LoginCredentials {
  username: string
  password: string
}

interface LoginResponse {
  token: string
}

interface TokenClaims {
  sub: string
  CompanyName: string
  exp: number
  FirstName: string
  LastName: string
  Role: string
  [key: string]: any
}

const API_URL = 'https://localhost:7076/api/v1/account'

export const setAuthToken = (token: string) => {
  if (token) {
    document.cookie = `auth_token=${token}; path=/`
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
  } else {
    document.cookie = 'auth_token=; path=/; expires=Thu, 01 Jan 1970 00:00:01 GMT'
    delete axios.defaults.headers.common['Authorization']
  }
}

export const login = async (credentials: LoginCredentials): Promise<LoginResponse> => {
  const response = await axios.post(`${API_URL}/login`, credentials)
  return response.data
}

export const logout = () => {
  setAuthToken('')
}

export const getTokenClaims = (): TokenClaims | null => {
  const token = document.cookie
    .split('; ')
    .find(row => row.startsWith('auth_token='))
    ?.split('=')[1]
  if (!token) return null

  try {
    const claims = jwtDecode<TokenClaims>(token)
    return claims
  } catch {
    return null
  }
}

export const getUserInfo = () => {
  const claims = getTokenClaims()
  if (!claims) return null

  return {
    firstName: claims.FirstName,
    lastName: claims.LastName,
    role: claims.Role,
    companyName: claims.CompanyName
  }
} 
