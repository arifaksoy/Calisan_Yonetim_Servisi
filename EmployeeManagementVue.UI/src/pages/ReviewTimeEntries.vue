<script setup lang="ts">
import { ref, onMounted, computed, watch } from 'vue'
import { 
  getEmployees, 
  getProjects, 
  getTimeEntries,
  type Employee,
  type Project,
  type TimeEntry
} from '@/services/timeEntriesService'

const selectedEmployee = ref('all')
const selectedProject = ref('all')
const employees = ref<Employee[]>([])
const projects = ref<Project[]>([])
const timeEntries = ref<TimeEntry[]>([])
const loading = ref(true)
const page = ref(1)
const itemsPerPage = ref(20)
const error = ref<string | null>(null)
const showError = ref(false)

// Computed properties
const filteredTimeEntries = computed(() => {
  return timeEntries.value.filter(entry => {
    const matchesEmployee = selectedEmployee.value === 'all' || entry.personnelId === selectedEmployee.value
    const matchesProject = selectedProject.value === 'all' || entry.projectId === selectedProject.value
    // Only show entries for projects that exist in the projects list
    const projectExists = projects.value.some(project => project.projectId === entry.projectId)
    return matchesEmployee && matchesProject && projectExists
  })
})

const paginatedTimeEntries = computed(() => {
  const start = (page.value - 1) * itemsPerPage.value
  const end = start + itemsPerPage.value
  return filteredTimeEntries.value.slice(start, end)
})

const totalPages = computed(() => Math.ceil(filteredTimeEntries.value.length / itemsPerPage.value))

const totalItems = computed(() => filteredTimeEntries.value.length)

const getEmployeeName = (personnelId: string) => {
  const employee = employees.value.find(emp => emp.personnelId === personnelId)
  return employee ? `${employee.firstName} ${employee.lastName}` : 'Unknown'
}

const getProjectName = (projectId: string) => {
  const entry = timeEntries.value.find(entry => entry.projectId === projectId)
  return entry ? entry.projectName : 'Unknown'
}

const uniqueEmployees = computed(() => {
  const uniqueEmps = new Map()
  timeEntries.value.forEach(entry => {
    if (!uniqueEmps.has(entry.personnelId)) {
      uniqueEmps.set(entry.personnelId, {
        personnelId: entry.personnelId,
        personnelName: entry.personnelName
      })
    }
  })
  return Array.from(uniqueEmps.values())
})

const uniqueProjects = computed(() => {
  const uniqueProjs = new Map()
  timeEntries.value.forEach(entry => {
    if (!uniqueProjs.has(entry.projectId)) {
      uniqueProjs.set(entry.projectId, {
        projectId: entry.projectId,
        projectName: entry.projectName
      })
    }
  })
  return Array.from(uniqueProjs.values())
})

// Methods
const loadTimeEntries = async () => {
  try {
    const entries = await getTimeEntries(
      selectedEmployee.value !== 'all' ? selectedEmployee.value : '',
      '', // startDate - not filtering by date in review
      '', // endDate - not filtering by date in review
      selectedProject.value !== 'all' ? selectedProject.value : ''
    )
    timeEntries.value = entries
    page.value = 1 // Reset to first page when new data is loaded
  } catch (err) {
    console.error('Error loading time entries:', err)
    error.value = 'Failed to load time entries'
    showError.value = true
  }
}

// Lifecycle hooks
onMounted(async () => {
  try {
    loading.value = true
    await Promise.all([
      getEmployees().then(data => employees.value = data),
      getProjects().then(data => projects.value = data)
    ])
    await loadTimeEntries()
  } catch (err) {
    console.error('Error loading initial data:', err)
    error.value = 'Failed to load initial data'
    showError.value = true
  } finally {
    loading.value = false
  }
})

// Watch for filter changes
watch([selectedEmployee, selectedProject], () => {
  if (!loading.value) {
    loadTimeEntries()
  }
})

// Watch page changes
watch([page, itemsPerPage], () => {
  if (page.value > totalPages.value && totalPages.value > 0) {
    page.value = totalPages.value
  }
})
</script>

<template>
  <div class="review-time-entries-container">
    <v-container fluid>
      <!-- Filters -->
      <v-row class="mb-4">
        <v-col cols="12">
          <v-card>
            <v-card-text>
              <v-row>
                <v-col cols="12" sm="6" md="4">
                  <v-select
                    v-model="selectedEmployee"
                    :items="[
                      { title: 'All Employees', value: 'all' },
                      ...employees.map(emp => ({
                        title: `${emp.firstName} ${emp.lastName}`,
                        value: emp.personnelId
                      }))
                    ]"
                    label="Employee"
                    :loading="loading"
                    :disabled="loading"
                  />
                </v-col>
                <v-col cols="12" sm="6" md="4">
                  <v-select
                    v-model="selectedProject"
                    :items="[
                      { title: 'All Projects', value: 'all' },
                      ...projects.map(proj => ({
                        title: proj.projectName,
                        value: proj.projectId
                      }))
                    ]"
                    label="Project"
                    :loading="loading"
                    :disabled="loading"
                  />
                </v-col>
              </v-row>
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>

      <!-- Time Entries Table -->
      <v-row>
        <v-col cols="12">
          <v-card>
            <v-card-text>
              <v-table>
                <thead>
                  <tr>
                    <th>Employee</th>
                    <th>Project</th>
                    <th>Date</th>
                    <th>Hours</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="entry in paginatedTimeEntries" :key="entry.timeEntriesId">
                    <td>{{ entry.personnelName }}</td>
                    <td>{{ entry.projectName }}</td>
                    <td>{{ new Date(entry.timeEntriesDate).toLocaleDateString() }}</td>
                    <td>{{ entry.amount }}</td>
                  </tr>
                  <tr v-if="paginatedTimeEntries.length === 0">
                    <td colspan="4" class="text-center">No data available</td>
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
                  {{ (page - 1) * itemsPerPage + 1 }}-{{ Math.min(page * itemsPerPage, filteredTimeEntries.length) }} of {{ filteredTimeEntries.length }}
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

      <!-- Loading Overlay -->
      <v-overlay
        :model-value="loading"
        class="align-center justify-center"
      >
        <v-progress-circular
          indeterminate
          size="64"
        />
      </v-overlay>

      <!-- Error Snackbar -->
      <v-snackbar
        v-model="showError"
        color="error"
        timeout="5000"
      >
        {{ error }}
        <template v-slot:actions>
          <v-btn
            variant="text"
            @click="showError = false"
          >
            Close
          </v-btn>
        </template>
      </v-snackbar>
    </v-container>
  </div>
</template>

<style scoped>
.review-time-entries-container {
  padding: 20px;
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
