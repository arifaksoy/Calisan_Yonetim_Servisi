<script setup lang="ts">
import { ref, onMounted, computed, watch } from 'vue'
import { useRoute } from 'vue-router'
import { getMyPages } from '@/services/pageService'
import { 
  getProjects, 
  getCompanies, 
  addProject,
  updateProject,
  deleteProject,
  type Project,
  type ProjectDto,
  type Company
} from '@/services/projectService'

const route = useRoute()

// State
const pageTitle = ref('Projects')
const projects = ref<Project[]>([])
const companies = ref<Company[]>([])
const loading = ref(true)
const dialog = ref(false)
const deleteDialog = ref(false)
const editingProject = ref<Project | null>(null)
const projectToDelete = ref<Project | null>(null)
const page = ref(1)
const itemsPerPage = ref(20)
const error = ref<string | null>(null)
const showError = ref(false)

const newProject = ref<ProjectDto>({
  projectName: '',
  projectDescription: '',
  companyId: ''
})

// Methods
const loadData = async () => {
  if (loading.value && projects.value.length > 0) return

  try {
    loading.value = true
    const [projectsData, companiesData] = await Promise.all([
      getProjects(),
      getCompanies()
    ])
    projects.value = projectsData
    companies.value = companiesData
    error.value = null
    showError.value = false
  } catch (err) {
    console.error('Error loading data:', err)
    error.value = 'Failed to load data'
    showError.value = true
    projects.value = []
    companies.value = []
  } finally {
    loading.value = false
  }
}

// Watch route changes
watch(
  () => route.path,
  (newPath) => {
    if (newPath === '/projects') {
      loadData()
    }
  },
  { immediate: true }
)

// Computed
const paginatedProjects = computed(() => {
  const start = (page.value - 1) * itemsPerPage.value
  const end = start + itemsPerPage.value
  return projects.value.slice(start, end)
})

const totalPages = computed(() => Math.ceil(projects.value.length / itemsPerPage.value))

// Watch page changes
watch([page, itemsPerPage], () => {
  if (page.value > totalPages.value && totalPages.value > 0) {
    page.value = totalPages.value
  }
})

const handleAddProject = async () => {
  try {
    await addProject(newProject.value)
    dialog.value = false
    resetForm()
    await loadData()
    error.value = null
    showError.value = false
  } catch (err) {
    console.error('Error adding project:', err)
    error.value = 'Failed to add project'
    showError.value = true
  }
}

const handleEditProject = (project: Project) => {
  editingProject.value = project
  newProject.value = {
    projectName: project.projectName,
    projectDescription: project.projectDescription,
    companyId: project.companyId
  }
  dialog.value = true
}

const handleUpdateProject = async () => {
  if (!editingProject.value) return

  try {
    await updateProject(editingProject.value.projectId, newProject.value)
    dialog.value = false
    resetForm()
    await loadData()
    error.value = null
    showError.value = false
  } catch (err) {
    console.error('Error updating project:', err)
    error.value = 'Failed to update project'
    showError.value = true
  }
}

const handleDeleteClick = (project: Project) => {
  projectToDelete.value = project
  deleteDialog.value = true
}

const handleDeleteConfirm = async () => {
  if (!projectToDelete.value) return

  try {
    await deleteProject(projectToDelete.value.projectId)
    deleteDialog.value = false
    projectToDelete.value = null
    await loadData()
    error.value = null
    showError.value = false
  } catch (err) {
    console.error('Error deleting project:', err)
    error.value = 'Failed to delete project'
    showError.value = true
  }
}

const resetForm = () => {
  editingProject.value = null
  newProject.value = {
    projectName: '',
    projectDescription: '',
    companyId: ''
  }
}

const closeDialog = () => {
  dialog.value = false
  resetForm()
}

// Lifecycle hooks
onMounted(() => {
  loadData()
})
</script>

<template>
  <div class="projects-container">
    <h1 class="text-h4 mb-4">Project Management</h1>

    <!-- Add Project Button -->
    <div class="d-flex justify-end mb-4">
      <v-btn
        color="primary"
        prepend-icon="mdi-plus"
        @click="dialog = true"
        :disabled="loading"
        block
      >
        ADD PROJECT
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

    <!-- Projects Table -->
    <v-row v-else>
      <v-col cols="12">
        <v-card>
          <v-card-text>
            <v-table>
              <thead>
                <tr>
                  <th>Project ID</th>
                  <th>Project Name</th>
                  <th>Description</th>
                  <th>Company</th>
                  <th>Status</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="project in paginatedProjects" :key="project.projectId">
                  <td>{{ project.projectId }}</td>
                  <td>{{ project.projectName }}</td>
                  <td>{{ project.projectDescription }}</td>
                  <td>{{ project.companyName }}</td>
                  <td>{{ project.status === 1 ? 'Active' : 'Inactive' }}</td>
                  <td>
                    <v-btn
                      icon="mdi-pencil"
                      variant="text"
                      color="primary"
                      density="compact"
                      @click="handleEditProject(project)"
                    />
                    <v-btn
                      icon="mdi-delete"
                      variant="text"
                      color="error"
                      density="compact"
                      @click="handleDeleteClick(project)"
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
                {{ page }}-{{ Math.min(page * itemsPerPage, projects.length) }} of {{ projects.length }}
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
          {{ editingProject ? 'Edit Project' : 'Add Project' }}
        </v-card-title>
        <v-card-text>
          <v-form @submit.prevent="editingProject ? handleUpdateProject() : handleAddProject()">
            <v-text-field
              v-model="newProject.projectName"
              label="Project Name"
              required
            />
            <v-textarea
              v-model="newProject.projectDescription"
              label="Description"
              required
            />
            <v-select
              v-model="newProject.companyId"
              :items="companies"
              item-title="companyName"
              item-value="companyId"
              label="Company"
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
            @click="editingProject ? handleUpdateProject() : handleAddProject()"
            :loading="loading"
          >
            {{ editingProject ? 'Update' : 'Add' }}
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <!-- Delete Confirmation Dialog -->
    <v-dialog v-model="deleteDialog" max-width="400px">
      <v-card>
        <v-card-title>Confirm Delete</v-card-title>
        <v-card-text>
          Are you sure you want to delete this project?
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
.projects-container {
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