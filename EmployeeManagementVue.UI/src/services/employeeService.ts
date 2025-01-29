import axios from 'axios'
import { jwtDecode } from 'jwt-decode'

export interface Employee {
  personnelId: string
  firstName: string
  lastName: string
  userName: string
  email: string
  companyId: string
  companyName: string
  roleId: string
  roleName: string
}

export interface Company {
  companyId: string
  companyName: string
}

export interface Role {
  roleId: string
  roleName: string
}

export interface NewEmployee {
  personnel: {
    firstName: string
    lastName: string
    email: string
  }
  user: {
    username: string
    password: string
    companyId: string
  }
  role: {
    roleId: string
  }
}

interface DecodedToken {
  CompanyId: string
  [key: string]: any
}

const API_URL = 'https://localhost:7076/api/v1'

const getTokenFromCookie = (): string | null => {
  const token = document.cookie
    .split('; ')
    .find(row => row.startsWith('auth_token='))
    ?.split('=')[1]
  return token || null
}

export const getEmployees = async (): Promise<Employee[]> => {
  const token = getTokenFromCookie()
  if (!token) {
    throw new Error('No authentication token found')
  }

  const decodedToken = jwtDecode<DecodedToken>(token)
  const companyId = decodedToken.CompanyId

  const response = await axios.get(`${API_URL}/company/${companyId}/employee`, {
    headers: {
      'Authorization': `Bearer ${token}`
    }
  })
  return response.data
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

export const getRoles = async (): Promise<Role[]> => {
  const token = getTokenFromCookie()
  if (!token) {
    throw new Error('No authentication token found')
  }

  const decodedToken = jwtDecode<DecodedToken>(token)
  const companyId = decodedToken.CompanyId

  const response = await axios.get(`${API_URL}/company/${companyId}/roles`, {
    headers: {
      'Authorization': `Bearer ${token}`
    }
  })
  return response.data
}

export const addEmployee = async (employee: NewEmployee): Promise<void> => {
  const token = getTokenFromCookie()
  if (!token) {
    throw new Error('No authentication token found')
  }

  const decodedToken = jwtDecode<DecodedToken>(token)
  const companyId = decodedToken.CompanyId

  await axios.post(
    `${API_URL}/company/${companyId}/employee`,
    employee,
    {
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      }
    }
  )
}

export const updateEmployee = async (personnelId: string, employee: NewEmployee): Promise<void> => {
  const token = getTokenFromCookie()
  if (!token) {
    throw new Error('No authentication token found')
  }

  const decodedToken = jwtDecode<DecodedToken>(token)
  const companyId = decodedToken.CompanyId

  await axios.put(
    `${API_URL}/company/${companyId}/employee/${personnelId}`,
    employee,
    {
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      }
    }
  )
}

export const deleteEmployee = async (personnelId: string): Promise<void> => {
  const token = getTokenFromCookie()
  if (!token) {
    throw new Error('No authentication token found')
  }

  const decodedToken = jwtDecode<DecodedToken>(token)
  const companyId = decodedToken.CompanyId

  await axios.delete(`${API_URL}/company/${companyId}/employee/${personnelId}`, {
    headers: {
      'Authorization': `Bearer ${token}`
    }
  })
} 