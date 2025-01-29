import axios from 'axios'
import { jwtDecode } from 'jwt-decode'

export interface Company {
  companyId: string
  companyName: string
}

export interface CompanyDto {
  companyName: string
}

const API_URL = 'https://localhost:7076/api/v1'

const getTokenFromCookie = (): string | null => {
  const token = document.cookie
    .split('; ')
    .find(row => row.startsWith('auth_token='))
    ?.split('=')[1]
  return token || null
}

export const getCompanies = async (): Promise<Company[]> => {
  const token = getTokenFromCookie()
  if (!token) {
    throw new Error('No authentication token found')
  }

  const response = await axios.get(`${API_URL}/company/get-companies`, {
    headers: {
      'Authorization': `Bearer ${token}`
    }
  })
  return response.data
}

export const addCompany = async (company: CompanyDto): Promise<void> => {
  const token = getTokenFromCookie()
  if (!token) {
    throw new Error('No authentication token found')
  }

  await axios.post(
    `${API_URL}/company/add-company`,
    company,
    {
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      }
    }
  )
}

export const updateCompany = async (companyId: string, company: CompanyDto): Promise<void> => {
  const token = getTokenFromCookie()
  if (!token) {
    throw new Error('No authentication token found')
  }

  await axios.put(
    `${API_URL}/company/${companyId}`,
    company,
    {
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      }
    }
  )
}

export const deleteCompany = async (companyId: string): Promise<void> => {
  const token = getTokenFromCookie()
  if (!token) {
    throw new Error('No authentication token found')
  }

  await axios.delete(`${API_URL}/company/${companyId}`, {
    headers: {
      'Authorization': `Bearer ${token}`
    }
  })
} 