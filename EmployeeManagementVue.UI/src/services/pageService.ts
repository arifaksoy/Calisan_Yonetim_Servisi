import axios from 'axios'
import { jwtDecode } from 'jwt-decode'

const API_URL = 'https://localhost:7076/api/v1'

export interface Page {
  pageId: string
  pageName: string
  url: string
  icon?: string
  parentId?: string
  order: number
  status: number
  isActive: boolean
}

interface DecodedToken {
  UserId: string
  [key: string]: any
}

const getTokenFromCookie = (): string | null => {
  const token = document.cookie
    .split('; ')
    .find(row => row.startsWith('auth_token='))
    ?.split('=')[1]
  return token || null
}

const getUserIdFromToken = (token: string): string => {
  const decoded = jwtDecode(token) as DecodedToken
  return decoded.UserId
}

export const getMyPages = async (): Promise<Page[]> => {
  try {
  const token = getTokenFromCookie()
  if (!token) {
    throw new Error('No authentication token found')
  }

    const userId = getUserIdFromToken(token)
    const response = await axios.get(`${API_URL}/pages/${userId}/my-pages`, {
    headers: {
      'Authorization': `Bearer ${token}`
    }
  })
  return response.data
  } catch (error) {
    console.error('Error fetching pages:', error)
    throw error
    }
} 