export interface Role {
  roleId: string
  roleName: string
}

export interface Page {
  pageId: string
  pageName: string
  pageDescription: string
  roles: Role[]
  status: number
} 