<script setup lang="ts">
import { ref, onMounted, computed, watch } from 'vue'
import { useRoute } from 'vue-router'
import { getMyPages } from '@/services/pageService'
import { 
  getCompanies,
  addCompany,
  updateCompany,
  deleteCompany,
  type Company
} from '@/services/companyService'

const route = useRoute()

// State
const pageTitle = ref('Company')
const companies = ref<Company[]>([])
const loading = ref(true)
const dialog = ref(false)
const deleteDialog = ref(false)
const editingCompany = ref<Company | null>(null)
const companyToDelete = ref<Company | null>(null)
const page = ref(1)
const itemsPerPage = ref(20)
const error = ref<string | null>(null)
const showError = ref(false)

const newCompany = ref({
  companyName: ''
})

// Methods
const loadData = async () => {
  if (loading.value && companies.value.length > 0) return

  try {
    loading.value = true
    const companiesData = await getCompanies()
    companies.value = companiesData
    error.value = null
    showError.value = false
  } catch (err) {
    console.error('Error loading data:', err)
    error.value = 'Failed to load data'
    showError.value = true
    companies.value = []
  } finally {
    loading.value = false
  }
}

// Watch route changes
watch(
  () => route.path,
  (newPath) => {
    if (newPath === '/company') {
      loadData()
    }
  },
  { immediate: true }
)

// Computed
const paginatedCompanies = computed(() => {
  const start = (page.value - 1) * itemsPerPage.value
  const end = start + itemsPerPage.value
  return companies.value.slice(start, end)
})

const totalPages = computed(() => Math.ceil(companies.value.length / itemsPerPage.value))

// Watch page changes
watch([page, itemsPerPage], () => {
  if (page.value > totalPages.value && totalPages.value > 0) {
    page.value = totalPages.value
  }
})

const handleAddCompany = async () => {
  try {
    await addCompany(newCompany.value)
    dialog.value = false
    resetForm()
    await loadData()
    error.value = null
    showError.value = false
  } catch (err) {
    console.error('Error adding company:', err)
    error.value = 'Failed to add company'
    showError.value = true
  }
}

const handleEditCompany = (company: Company) => {
  editingCompany.value = company
  newCompany.value = {
    companyName: company.companyName
  }
  dialog.value = true
}

const handleUpdateCompany = async () => {
  if (!editingCompany.value) return

  try {
    await updateCompany(editingCompany.value.companyId, newCompany.value)
    dialog.value = false
    resetForm()
    await loadData()
    error.value = null
    showError.value = false
  } catch (err) {
    console.error('Error updating company:', err)
    error.value = 'Failed to update company'
    showError.value = true
  }
}

const handleDeleteClick = (company: Company) => {
  companyToDelete.value = company
  deleteDialog.value = true
}

const handleDeleteConfirm = async () => {
  if (!companyToDelete.value) return

  try {
    await deleteCompany(companyToDelete.value.companyId)
    deleteDialog.value = false
    companyToDelete.value = null
    await loadData()
    error.value = null
    showError.value = false
  } catch (err) {
    console.error('Error deleting company:', err)
    error.value = 'Failed to delete company'
    showError.value = true
  }
}

const resetForm = () => {
  editingCompany.value = null
  newCompany.value = {
    companyName: ''
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
  <div class="company-container">
    <h1 class="text-h4 mb-4">Company Management</h1>

    <!-- Add Company Button -->
    <div class="d-flex justify-end mb-4">
      <v-btn
        color="primary"
        prepend-icon="mdi-plus"
        @click="dialog = true"
        :disabled="loading"
        block
      >
        ADD COMPANY
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

    <!-- Companies Table -->
    <v-row v-else>
      <v-col cols="12">
        <v-card>
          <v-card-text>
            <v-table>
              <thead>
                <tr>
                  <th>Company ID</th>
                  <th>Company Name</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="company in paginatedCompanies" :key="company.companyId">
                  <td>{{ company.companyId }}</td>
                  <td>{{ company.companyName }}</td>
                  <td>
                    <v-btn
                      icon="mdi-pencil"
                      variant="text"
                      color="primary"
                      density="compact"
                      @click="handleEditCompany(company)"
                    />
                    <v-btn
                      icon="mdi-delete"
                      variant="text"
                      color="error"
                      density="compact"
                      @click="handleDeleteClick(company)"
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
                {{ page }}-{{ Math.min(page * itemsPerPage, companies.length) }} of {{ companies.length }}
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
          {{ editingCompany ? 'Edit Company' : 'Add Company' }}
        </v-card-title>
        <v-card-text>
          <v-form @submit.prevent="editingCompany ? handleUpdateCompany() : handleAddCompany()">
            <v-text-field
              v-model="newCompany.companyName"
              label="Company Name"
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
            @click="editingCompany ? handleUpdateCompany() : handleAddCompany()"
            :loading="loading"
          >
            {{ editingCompany ? 'Update' : 'Add' }}
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <!-- Delete Confirmation Dialog -->
    <v-dialog v-model="deleteDialog" max-width="400px">
      <v-card>
        <v-card-title>Confirm Delete</v-card-title>
        <v-card-text>
          Are you sure you want to delete this company?
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
.company-container {
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
