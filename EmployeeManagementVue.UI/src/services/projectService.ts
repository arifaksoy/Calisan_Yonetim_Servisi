import axios from 'axios'
import { jwtDecode } from 'jwt-decode'

export interface Project {
  projectId: string
  projectName: string
  projectDescription: string
  companyId: string
  companyName: string
  status: number
}

export interface ProjectDto {
  projectName: string
  projectDescription: string
  companyId: string
}

export interface Company {
  companyId: string
  companyName: string
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

export const getProjects = async (): Promise<Project[]> => {
  const token = getTokenFromCookie()
  if (!token) {
    throw new Error('No authentication token found')
  }

  const decodedToken = jwtDecode<DecodedToken>(token)
  const companyId = decodedToken.CompanyId

  const response = await axios.get(`${API_URL}/company/${companyId}/projects`, {
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

export const addProject = async (project: ProjectDto): Promise<void> => {
  const token = getTokenFromCookie()
  if (!token) {
    throw new Error('No authentication token found')
  }

  const decodedToken = jwtDecode<DecodedToken>(token)
  const companyId = decodedToken.CompanyId

  await axios.post(
    `${API_URL}/company/${companyId}/projects`,
    project,
    {
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      }
    }
  )
}

export const updateProject = async (projectId: string, project: ProjectDto): Promise<void> => {
  const token = getTokenFromCookie()
  if (!token) {
    throw new Error('No authentication token found')
  }

  const decodedToken = jwtDecode<DecodedToken>(token)
  const companyId = decodedToken.CompanyId

  await axios.put(
    `${API_URL}/company/${companyId}/projects/${projectId}`,
    project,
    {
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      }
    }
  )
}

export const deleteProject = async (projectId: string): Promise<void> => {
  const token = getTokenFromCookie()
  if (!token) {
    throw new Error('No authentication token found')
  }

  const decodedToken = jwtDecode<DecodedToken>(token)
  const companyId = decodedToken.CompanyId

  await axios.delete(`${API_URL}/company/${companyId}/projects/${projectId}`, {
    headers: {
      'Authorization': `Bearer ${token}`
    }
  })
} 