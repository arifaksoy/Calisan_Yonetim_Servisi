import axios from 'axios'
import { jwtDecode } from 'jwt-decode'

export interface Employee {
  personnelId: string
  firstName: string
  lastName: string
  userName: string
  email: string
  userCompanyId: string
  companyName: string
  roleId: string
  roleName: string
}

export interface Project {
  projectId: string
  projectName: string
  companyId: string
  status: number
}

export interface TimeEntry {
  timeEntriesId: string
  timeEntriesDate: string
  projectId: string
  projectName: string
  personnelId: string
  personnelName: string
  amount: number
}

interface DecodedToken {
  CompanyId: string
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

export const getTimeEntries = async (
  personnelId?: string,
  startDate?: string,
  endDate?: string,
  projectId?: string
): Promise<TimeEntry[]> => {
  const token = getTokenFromCookie()
  if (!token) {
    throw new Error('No authentication token found')
  }

  const decodedToken = jwtDecode<DecodedToken>(token)
  const companyId = decodedToken.CompanyId

  const queryParams = new URLSearchParams()
  if (personnelId) queryParams.append('personnelId', personnelId)
  if (startDate) queryParams.append('startDate', startDate)
  if (endDate) queryParams.append('endDate', endDate)
  if (projectId) queryParams.append('projectId', projectId)

  const response = await axios.get(
    `${API_URL}/company/${companyId}/time-entries${queryParams.toString() ? `?${queryParams.toString()}` : ''}`,
    {
      headers: {
        'Authorization': `Bearer ${token}`
      }
    }
  )
  return response.data
}

export const addTimeEntries = async (timeEntries: { timeEntriesDate: string; projectId: string; personnelId: string; amount: number }[]) => {
  const token = getTokenFromCookie()
  if (!token) {
    throw new Error('No authentication token found')
  }

  const decodedToken = jwtDecode<DecodedToken>(token)
  const companyId = decodedToken.CompanyId

  await axios.post(
    `${API_URL}/company/${companyId}/time-entries`,
    { timeEntries },
    {
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      }
    }
  )
}

export const updateTimeEntry = async (timeEntryId: string, data: { timeEntriesDate: string; projectId: string; personnelId: string; amount: number }) => {
  const token = getTokenFromCookie()
  if (!token) {
    throw new Error('No authentication token found')
  }

  const decodedToken = jwtDecode<DecodedToken>(token)
  const companyId = decodedToken.CompanyId

  await axios.put(
    `${API_URL}/company/${companyId}/time-entries/${timeEntryId}`,
    data,
    {
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      }
    }
  )
}

export const deleteTimeEntriesForProject = async (projectId: string, personnelId: string, startDate: string, endDate: string) => {
  const token = getTokenFromCookie()
  if (!token) {
    throw new Error('No authentication token found')
  }

  const decodedToken = jwtDecode<DecodedToken>(token)
  const companyId = decodedToken.CompanyId

  // First get all time entries for the project
  const entries = await getTimeEntries(personnelId, startDate, endDate)
  const timeEntriesIds = entries
    .filter(entry => entry.projectId === projectId)
    .map(entry => entry.timeEntriesId)

  if (timeEntriesIds.length === 0) {
    throw new Error('No time entries found for deletion')
  }

  // Call the bulk delete endpoint
  await axios.delete(`${API_URL}/company/${companyId}/time-entries/bulk`, {
    headers: {
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    },
    data: timeEntriesIds
  })
} 
