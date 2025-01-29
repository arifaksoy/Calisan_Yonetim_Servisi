<script setup lang="ts">
import { ref, onMounted, computed, watch } from 'vue'
import { useRoute } from 'vue-router'
import { getMyPages } from '@/services/pageService'
import { 
  getEmployees, 
  getCompanies, 
  getRoles,
  addEmployee,
  updateEmployee,
  deleteEmployee,
  type Employee,
  type Company,
  type Role,
  type NewEmployee
} from '@/services/employeeService'

const route = useRoute()

// State
const pageTitle = ref('Employee')
const employees = ref<Employee[]>([])
const companies = ref<Company[]>([])
const roles = ref<Role[]>([])
const loading = ref(true)
const dialog = ref(false)
const deleteDialog = ref(false)
const editingEmployee = ref<Employee | null>(null)
const employeeToDelete = ref<Employee | null>(null)
const page = ref(1)
const itemsPerPage = ref(20)
const error = ref<string | null>(null)
const showError = ref(false)

// Methods
const loadData = async () => {
  if (loading.value && employees.value.length > 0) return // Only prevent reload if we already have data

  try {
    loading.value = true
    const [employeesData, companiesData, rolesData] = await Promise.all([
      getEmployees(),
      getCompanies(),
      getRoles()
    ])
    employees.value = employeesData
    companies.value = companiesData
    roles.value = rolesData
    error.value = null
    showError.value = false
  } catch (err) {
    console.error('Error loading data:', err)
    error.value = 'Failed to load data'
    showError.value = true
    employees.value = []
    companies.value = []
    roles.value = []
  } finally {
    loading.value = false
  }
}

// Watch route changes
watch(
  () => route.path,
  (newPath) => {
    if (newPath === '/employee') {
      loadData()
    }
  },
  { immediate: true }
)

// Computed
const paginatedEmployees = computed(() => {
  const start = (page.value - 1) * itemsPerPage.value
  const end = start + itemsPerPage.value
  return employees.value.slice(start, end)
})

const totalItems = computed(() => employees.value.length)

const totalPages = computed(() => Math.ceil(employees.value.length / itemsPerPage.value))

// Watch page changes
watch([page, itemsPerPage], () => {
  if (page.value > totalPages.value && totalPages.value > 0) {
    page.value = totalPages.value
  }
})

const newEmployee = ref<NewEmployee>({
  personnel: {
    firstName: '',
    lastName: '',
    email: ''
  },
  user: {
    username: '',
    password: '',
    companyId: ''
  },
  role: {
    roleId: ''
  }
})

const loadPageTitle = async () => {
  try {
    const pages = await getMyPages()
    const employeePage = pages.find(page => page.url?.includes('employee'))
    if (employeePage) {
      pageTitle.value = employeePage.pageName
    }
  } catch (err) {
    console.error('Error loading page title:', err)
  }
}

const handleAddEmployee = async () => {
  try {
    await addEmployee(newEmployee.value)
    dialog.value = false
    resetForm()
    await loadData()
    error.value = null
    showError.value = false
  } catch (err) {
    console.error('Error adding employee:', err)
    error.value = 'Failed to add employee'
    showError.value = true
  }
}

const handleEditEmployee = (employee: Employee) => {
  editingEmployee.value = employee
  newEmployee.value = {
    personnel: {
      firstName: employee.firstName,
      lastName: employee.lastName,
      email: employee.email
    },
    user: {
      username: employee.userName,
      password: '', // Leave empty for editing
      companyId: employee.companyId
    },
    role: {
      roleId: employee.roleId
    }
  }
  dialog.value = true
}

const handleUpdateEmployee = async () => {
  if (!editingEmployee.value) return

  try {
    await updateEmployee(editingEmployee.value.personnelId, newEmployee.value)
    dialog.value = false
    resetForm()
    await loadData()
    error.value = null
    showError.value = false
  } catch (err) {
    console.error('Error updating employee:', err)
    error.value = 'Failed to update employee'
    showError.value = true
  }
}

const handleDeleteClick = (employee: Employee) => {
  employeeToDelete.value = employee
  deleteDialog.value = true
}

const handleDeleteConfirm = async () => {
  if (!employeeToDelete.value) return

  try {
    await deleteEmployee(employeeToDelete.value.personnelId)
    deleteDialog.value = false
    employeeToDelete.value = null
    await loadData()
    error.value = null
    showError.value = false
  } catch (err) {
    console.error('Error deleting employee:', err)
    error.value = 'Failed to delete employee'
    showError.value = true
  }
}

const resetForm = () => {
  editingEmployee.value = null
  newEmployee.value = {
    personnel: {
      firstName: '',
      lastName: '',
      email: ''
    },
    user: {
      username: '',
      password: '',
      companyId: ''
    },
    role: {
      roleId: ''
    }
  }
}

const closeDialog = () => {
  dialog.value = false
  resetForm()
}

// Lifecycle hooks
onMounted(() => {
  loadPageTitle()
})
</script>

<template>
  <div class="employee-container">
    <h1 class="text-h4 mb-4">Employee Management</h1>

    <!-- Add Employee Button -->
    <div class="d-flex justify-end mb-4">
      <v-btn
        color="primary"
        prepend-icon="mdi-plus"
        @click="dialog = true"
        :disabled="loading"
        block
      >
        ADD EMPLOYEE
      </v-btn>
    </div>

    <!-- Loading State -->
    <v-row v-if="loading">
      <v-col cols="12" class="d-flex justify-center">
        <v-progress-circular
          indeterminate
          color="primary"
        ></v-progress-circular>
      </v-col>
    </v-row>

    <!-- Error State -->
    <v-row v-else-if="error">
      <v-col cols="12">
        <v-alert
          type="error"
          :text="error"
        ></v-alert>
      </v-col>
    </v-row>

    <!-- Employees Table -->
    <v-row v-else>
      <v-col cols="12">
        <v-card>
          <v-card-text>
            <v-table>
              <thead>
                <tr>
                  <th>Personnel ID</th>
                  <th>First Name</th>
                  <th>Last Name</th>
                  <th>Email</th>
                  <th>Company Name</th>
                  <th>UserName</th>
                  <th>Role Name</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="employee in paginatedEmployees" :key="employee.personnelId">
                  <td>{{ employee.personnelId }}</td>
                  <td>{{ employee.firstName }}</td>
                  <td>{{ employee.lastName }}</td>
                  <td>{{ employee.email }}</td>
                  <td>{{ employee.companyName }}</td>
                  <td>{{ employee.userName }}</td>
                  <td>{{ employee.roleName }}</td>
                  <td>
                    <v-btn
                      icon="mdi-pencil"
                      variant="text"
                      color="primary"
                      density="compact"
                      @click="handleEditEmployee(employee)"
                    />
                    <v-btn
                      icon="mdi-delete"
                      variant="text"
                      color="error"
                      density="compact"
                      @click="handleDeleteClick(employee)"
                    />
                  </td>
                </tr>
              </tbody>
            </v-table>

            <!-- Pagination -->
            <div class="d-flex align-center justify-space-between mt-4 px-2">
              <div class="d-flex align-center">
                <span class="text-caption me-2">Rows per page</span>
                <v-select
                  v-model="itemsPerPage"
                  :items="[10, 20, 50]"
                  variant="plain"
                  density="compact"
                  hide-details
                  class="pagination-select"
                />
              </div>
              <div class="text-caption">
                {{ page }}-{{ Math.min(page * itemsPerPage, employees.length) }} of {{ employees.length }}
              </div>
              <div class="d-flex align-center">
                <v-btn
                  icon="mdi-chevron-left"
                  variant="text"
                  density="compact"
                  :disabled="page === 1"
                  @click="page--"
                />
                <v-btn
                  icon="mdi-chevron-right"
                  variant="text"
                  density="compact"
                  :disabled="page >= totalPages"
                  @click="page++"
                />
              </div>
            </div>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>

    <!-- Add/Edit Dialog -->
    <v-dialog v-model="dialog" max-width="600px">
      <v-card>
        <v-card-title>
          {{ editingEmployee ? 'Edit Employee' : 'Add Employee' }}
        </v-card-title>
        <v-card-text>
          <v-form @submit.prevent="editingEmployee ? handleUpdateEmployee() : handleAddEmployee()">
            <v-text-field
              v-model="newEmployee.personnel.firstName"
              label="First Name"
              required
            />
            <v-text-field
              v-model="newEmployee.personnel.lastName"
              label="Last Name"
              required
            />
            <v-text-field
              v-model="newEmployee.personnel.email"
              label="Email"
              type="email"
              required
            />
            <v-text-field
              v-model="newEmployee.user.username"
              label="Username"
              required
            />
            <v-text-field
              v-model="newEmployee.user.password"
              label="Password"
              type="password"
              :required="!editingEmployee"
            />
            <v-select
              v-model="newEmployee.user.companyId"
              :items="companies"
              item-title="companyName"
              item-value="companyId"
              label="Company"
              required
            />
            <v-select
              v-model="newEmployee.role.roleId"
              :items="roles"
              item-title="roleName"
              item-value="roleId"
              label="Role"
              required
            />
          </v-form>
        </v-card-text>
        <v-card-actions>
          <v-spacer />
          <v-btn
            color="grey"
            variant="text"
            @click="closeDialog"
          >
            Cancel
          </v-btn>
          <v-btn
            color="primary"
            @click="editingEmployee ? handleUpdateEmployee() : handleAddEmployee()"
            :loading="loading"
          >
            {{ editingEmployee ? 'Update' : 'Add' }}
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <!-- Delete Confirmation Dialog -->
    <v-dialog v-model="deleteDialog" max-width="400px">
      <v-card>
        <v-card-title>Confirm Delete</v-card-title>
        <v-card-text>
          Are you sure you want to delete this employee?
        </v-card-text>
        <v-card-actions>
          <v-spacer />
          <v-btn
            color="grey"
            variant="text"
            @click="deleteDialog = false"
          >
            Cancel
          </v-btn>
          <v-btn
            color="error"
            @click="handleDeleteConfirm"
            :loading="loading"
          >
            Delete
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<style scoped>
.employee-container {
  padding: 20px;
  background-color: #f5f6fa;
  min-height: 100%;
}

.v-table {
  background-color: white;
}

.v-table th {
  font-weight: 500 !important;
  color: rgba(0, 0, 0, 0.87) !important;
  font-size: 0.875rem !important;
}

.v-table td {
  color: rgba(0, 0, 0, 0.87) !important;
  font-size: 0.875rem !important;
}

.v-btn {
  text-transform: none !important;
}

.pagination-select {
  width: 70px !important;
}

.pagination-select :deep(.v-field__input) {
  padding: 0;
  min-height: 32px;
}

.pagination-select :deep(.v-field) {
  border-radius: 0;
  box-shadow: none;
  background: transparent;
}
</style> 
