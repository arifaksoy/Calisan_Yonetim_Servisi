<script setup lang="ts">
import { ref, onMounted, computed, watch } from 'vue'
import { 
  getEmployees, 
  getProjects, 
  getTimeEntries, 
  addTimeEntries, 
  updateTimeEntry, 
  deleteTimeEntriesForProject,
  type Employee,
  type Project,
  type TimeEntry
} from '@/services/timeEntriesService'

const selectedEmployee = ref<string | null>(null)
const selectedProject = ref<string | null>(null)
const employees = ref<Employee[]>([])
const projects = ref<Project[]>([])
const weekDates = ref<string[]>([])
const timeEntries = ref<TimeEntry[]>([])
const projectEntries = ref<string[]>([])
const loading = ref(true)
const error = ref<string | null>(null)
const showError = ref(false)

// Computed properties for formatting
const formattedWeekDates = computed(() => {
  return weekDates.value.map(date => {
    const d = new Date(date)
    return {
      full: date,
      day: d.toLocaleDateString('en-US', { weekday: 'short' }),
      date: d.toLocaleDateString('en-US', { day: 'numeric', month: 'numeric' })
    }
  })
})

const availableProjects = computed(() => {
  return projects.value.filter(project => !projectEntries.value.includes(project.projectId))
})

const selectedEmployeeName = computed(() => {
  const employee = employees.value.find(e => e.personnelId === selectedEmployee.value)
  return employee ? `${employee.firstName} ${employee.lastName}` : ''
})

// Methods
const initializeWeekDates = () => {
  const today = new Date()
  const monday = new Date(today)
  const currentDay = monday.getDay()
  const daysToSubtract = currentDay === 0 ? 6 : currentDay - 1
  monday.setDate(today.getDate() - daysToSubtract)
  monday.setHours(0, 0, 0, 0)

  const dates = Array.from({ length: 7 }, (_, i) => {
    const date = new Date(monday)
    date.setDate(monday.getDate() + i)
    return date.toISOString().split('T')[0]
  })
  weekDates.value = dates
}

const loadData = async () => {
  try {
    loading.value = true
    const [employeesData, projectsData] = await Promise.all([
      getEmployees(),
      getProjects()
    ])
    employees.value = employeesData
    projects.value = projectsData
  } catch (err) {
    console.error('Error loading data:', err)
    error.value = 'Failed to load data'
    showError.value = true
  } finally {
    loading.value = false
  }
}

const loadTimeEntries = async () => {
  if (!selectedEmployee.value || weekDates.value.length === 0) return

  try {
    const entries = await getTimeEntries(
      selectedEmployee.value,
      weekDates.value[0],
      weekDates.value[6]
    )
    timeEntries.value = entries
    const uniqueProjectIds = Array.from(new Set(entries.map(entry => entry.projectId)))
    projectEntries.value = uniqueProjectIds
  } catch (err) {
    console.error('Error loading time entries:', err)
    error.value = 'Failed to load time entries'
    showError.value = true
  }
}

const handleAddNewEntry = async () => {
  if (!selectedEmployee.value || !selectedProject.value) {
    error.value = 'Please select an employee and project'
    showError.value = true
    return
  }

  try {
    const timeEntriesToAdd = weekDates.value.map(date => ({
      timeEntriesDate: `${date}T00:00:00`,
      projectId: selectedProject.value,
      personnelId: selectedEmployee.value,
      amount: 0
    }))

    await addTimeEntries(timeEntriesToAdd)
    await loadTimeEntries()

    if (!projectEntries.value.includes(selectedProject.value)) {
      projectEntries.value.push(selectedProject.value)
    }

    selectedProject.value = null
    error.value = null
    showError.value = false
  } catch (err) {
    console.error('Error adding time entries:', err)
    error.value = 'Failed to add time entries'
    showError.value = true
  }
}

const handleHoursChange = async (projectId: string, date: string, newAmount: number) => {
  const entry = timeEntries.value.find(e => 
    e.projectId === projectId && 
    e.timeEntriesDate.startsWith(date)
  )

  if (!entry) {
    console.error('No entry found for update')
    return
  }

  try {
    await updateTimeEntry(entry.timeEntriesId, {
      timeEntriesDate: entry.timeEntriesDate,
      projectId: entry.projectId,
      personnelId: entry.personnelId,
      amount: newAmount
    })
    await loadTimeEntries()
  } catch (err) {
    console.error('Error updating time entry:', err)
    error.value = 'Failed to update time entry'
    showError.value = true
  }
}

const handleDeleteProject = async (projectId: string) => {
  if (!selectedEmployee.value || weekDates.value.length === 0) return

  try {
    await deleteTimeEntriesForProject(
      projectId,
      selectedEmployee.value,
      weekDates.value[0],
      weekDates.value[6]
    )
    projectEntries.value = projectEntries.value.filter(id => id !== projectId)
    await loadTimeEntries()
  } catch (err) {
    console.error('Error deleting time entries:', err)
    error.value = 'Failed to delete time entries'
    showError.value = true
  }
}

const getEntryAmount = (projectId: string, date: string): number => {
  const entry = timeEntries.value.find(e => 
    e.projectId === projectId && 
    e.timeEntriesDate.startsWith(date)
  )
  return entry?.amount || 0
}

const getProjectName = (projectId: string): string => {
  const entry = timeEntries.value.find(e => e.projectId === projectId)
  return entry?.projectName || 'Unknown Project'
}

const calculateDailyTotal = (date: string): number => {
  return projectEntries.value.reduce((total, projectId) => {
    return total + getEntryAmount(projectId, date)
  }, 0)
}

const calculateProjectTotal = (projectId: string): number => {
  return weekDates.value.reduce((total, date) => {
    return total + getEntryAmount(projectId, date)
  }, 0)
}

const calculateGrandTotal = (): number => {
  return projectEntries.value.reduce((total, projectId) => {
    return total + calculateProjectTotal(projectId)
  }, 0)
}

// Lifecycle hooks
onMounted(() => {
  initializeWeekDates()
  loadData()
})

// Watch for employee changes
watch(selectedEmployee, () => {
  if (selectedEmployee.value) {
    loadTimeEntries()
  }
})
</script>

<template>
  <div class="time-entries-container">
    <!-- Header Section -->
    <div class="d-flex flex-column">
      <!-- Employee Selection -->
      <div class="d-flex justify-space-between align-center mb-4">
        <div class="d-flex align-center">
          <span class="mr-2">Employee:</span>
          <v-select
            v-model="selectedEmployee"
            :items="employees"
            :item-title="employee => `${employee.firstName} ${employee.lastName}`"
            item-value="personnelId"
            label="Select Employee"
            variant="outlined"
            density="compact"
            hide-details
            style="width: 200px"
            class="mr-2"
          />
        </div>
        <div class="date-range">
          January 26 - February 1
        </div>
      </div>

      <!-- Project Selection and Add Button -->
      <div class="d-flex mb-6" style="gap: 4px">
        <v-select
          v-model="selectedProject"
          :items="availableProjects"
          item-title="projectName"
          item-value="projectId"
          label="Select Project"
          variant="outlined"
          density="compact"
          hide-details
          style="width: 200px"
          class="flex-shrink-0"
        />
        <v-btn
          color="#0D6EFD"
          block
          class="text-none flex-grow-1"
          style="height: 40px"
          @click="handleAddNewEntry"
          :disabled="!selectedProject || !selectedEmployee"
        >
          Add Entry
        </v-btn>
      </div>
    </div>

    <!-- Time Entries Table -->
    <v-table class="time-entries-table">
      <thead>
        <tr>
          <th class="text-left">Project Name</th>
          <th v-for="date in formattedWeekDates" :key="date.full" class="text-center">
            {{ date.day }}<br>
            {{ date.date }}
          </th>
          <th class="text-center">Total</th>
          <th class="text-center" width="100">Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="projectId in projectEntries" :key="projectId">
          <td>{{ getProjectName(projectId) }}</td>
          <td v-for="date in weekDates" :key="date" class="text-center">
            <v-text-field
              :model-value="getEntryAmount(projectId, date)"
              @update:model-value="value => handleHoursChange(projectId, date, Number(value))"
              type="number"
              variant="outlined"
              density="compact"
              hide-details
              class="time-input"
              style="width: 60px; margin: 0 auto"
            />
          </td>
          <td class="text-center font-weight-bold">
            {{ calculateProjectTotal(projectId) }}
          </td>
          <td class="text-center">
            <v-btn
              icon="mdi-delete"
              color="error"
              variant="text"
              density="compact"
              @click="handleDeleteProject(projectId)"
            />
          </td>
        </tr>
        <!-- Grand Total Row -->
        <tr class="grand-total-row">
          <td class="font-weight-bold">Grand Total</td>
          <td v-for="date in weekDates" :key="date" class="text-center font-weight-bold">
            {{ calculateDailyTotal(date) }}
          </td>
          <td class="text-center font-weight-bold">{{ calculateGrandTotal() }}</td>
          <td></td>
        </tr>
      </tbody>
    </v-table>

    <!-- Error Snackbar -->
    <v-snackbar v-model="showError" color="error">
      {{ error }}
    </v-snackbar>
  </div>
</template>

<style scoped>
.time-entries-container {
  background-color: #F8F9FA;
  height: 100vh;
  padding: 1rem;
  display: flex;
  flex-direction: column;
  overflow: hidden;
}

.time-entries-table {
  background: white;
  border: 1px solid #dee2e6;
  border-radius: 4px;
  margin-top: 1rem;
  width: 100%;
  overflow-x: hidden;
}

.time-entries-table :deep(table) {
  table-layout: fixed;
  width: 100%;
}

.time-entries-table :deep(th),
.time-entries-table :deep(td) {
  white-space: nowrap;
  padding: 8px;
}

.time-entries-table :deep(th:first-child),
.time-entries-table :deep(td:first-child) {
  width: 200px;
}

.time-entries-table :deep(th:last-child),
.time-entries-table :deep(td:last-child) {
  width: 80px;
}

.time-entries-table :deep(th:nth-last-child(2)),
.time-entries-table :deep(td:nth-last-child(2)) {
  width: 80px;
}

.time-entries-table :deep(th:not(:first-child):not(:last-child):not(:nth-last-child(2))),
.time-entries-table :deep(td:not(:first-child):not(:last-child):not(:nth-last-child(2))) {
  width: calc((100% - 360px) / 7);
}

.time-input :deep(.v-field__input) {
  padding: 4px !important;
  text-align: center;
}

.time-input :deep(.v-field__outline) {
  --v-field-border-width: 1px !important;
  border-color: #dee2e6 !important;
}

.grand-total-row {
  background-color: #f8f9fa;
}

.grand-total-row td {
  border-top: 2px solid #dee2e6;
}

.date-range {
  color: #212529;
  font-size: 14px;
}

:deep(.v-field__outline) {
  --v-field-border-width: 1px !important;
  border-color: #dee2e6 !important;
}

:deep(.v-select .v-field) {
  border-radius: 4px;
  background-color: white !important;
}

:deep(.v-btn) {
  text-transform: none;
  font-weight: normal;
  letter-spacing: normal;
}
</style> 
