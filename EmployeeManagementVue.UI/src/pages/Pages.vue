<script setup lang="ts">
import { ref, onMounted, computed, watch } from 'vue'
import { useRoute } from 'vue-router'
import { getMyPages } from '@/services/pageService'
import type { Page, Role } from '@/types'

const route = useRoute()

// State
const pages = ref<Page[]>([])
const loading = ref(true)
const dialog = ref(false)
const deleteDialog = ref(false)
const editingPage = ref<Page | null>(null)
const pageToDelete = ref<Page | null>(null)
const page = ref(1)
const itemsPerPage = ref(20)
const error = ref<string | null>(null)
const showError = ref(false)

const newPage = ref({
  pageName: '',
  pageDescription: '',
  roles: [] as Role[],
  status: 1
})

// Methods
const loadData = async () => {
  if (loading.value && pages.value.length > 0) return

  try {
    loading.value = true
    const pagesData = await getMyPages()
    pages.value = pagesData
    error.value = null
    showError.value = false
  } catch (err) {
    console.error('Error loading data:', err)
    error.value = 'Failed to load data'
    showError.value = true
    pages.value = []
  } finally {
    loading.value = false
  }
}

// Watch route changes
watch(
  () => route.path,
  (newPath) => {
    if (newPath === '/pages') {
      loadData()
    }
  },
  { immediate: true }
)

// Computed
const paginatedPages = computed(() => {
  const start = (page.value - 1) * itemsPerPage.value
  const end = start + itemsPerPage.value
  return pages.value.slice(start, end)
})

const totalPages = computed(() => Math.ceil(pages.value.length / itemsPerPage.value))

// Watch page changes
watch([page, itemsPerPage], () => {
  if (page.value > totalPages.value && totalPages.value > 0) {
    page.value = totalPages.value
  }
})

const handleAddPage = async () => {
  try {
    // await addPage(newPage.value)
    dialog.value = false
    resetForm()
    await loadData()
    error.value = null
    showError.value = false
  } catch (err) {
    console.error('Error adding page:', err)
    error.value = 'Failed to add page'
    showError.value = true
  }
}

const handleEditPage = (pageItem: Page) => {
  editingPage.value = pageItem
  newPage.value = {
    pageName: pageItem.pageName,
    pageDescription: pageItem.pageDescription,
    roles: pageItem.roles,
    status: pageItem.status
  }
  dialog.value = true
}

const handleUpdatePage = async () => {
  if (!editingPage.value) return

  try {
    // await updatePage(editingPage.value.pageId, newPage.value)
    dialog.value = false
    resetForm()
    await loadData()
    error.value = null
    showError.value = false
  } catch (err) {
    console.error('Error updating page:', err)
    error.value = 'Failed to update page'
    showError.value = true
  }
}

const handleDeleteClick = (pageItem: Page) => {
  pageToDelete.value = pageItem
  deleteDialog.value = true
}

const handleDeleteConfirm = async () => {
  if (!pageToDelete.value) return

  try {
    // await deletePage(pageToDelete.value.pageId)
    deleteDialog.value = false
    pageToDelete.value = null
    await loadData()
    error.value = null
    showError.value = false
  } catch (err) {
    console.error('Error deleting page:', err)
    error.value = 'Failed to delete page'
    showError.value = true
  }
}

const resetForm = () => {
  editingPage.value = null
  newPage.value = {
    pageName: '',
    pageDescription: '',
    roles: [],
    status: 1
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
  <div class="pages-container">
    <h1 class="text-h4 mb-4">Pages Management</h1>

    <!-- Add Page Button -->
    <div class="d-flex justify-end mb-4">
      <v-btn
        color="primary"
        prepend-icon="mdi-plus"
        @click="dialog = true"
        :disabled="loading"
        block
      >
        ADD PAGE
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

    <!-- Pages Table -->
    <v-row v-else>
      <v-col cols="12">
        <v-card>
          <v-card-text>
            <v-table>
              <thead>
                <tr>
                  <th>Page ID</th>
                  <th>Page Name</th>
                  <th>Description</th>
                  <th>Roles</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="pageItem in paginatedPages" :key="pageItem.pageId">
                  <td>{{ pageItem.pageId }}</td>
                  <td>{{ pageItem.pageName }}</td>
                  <td>{{ pageItem.pageDescription }}</td>
                  <td>{{ pageItem.roles.map(role => role.roleName).join(', ') }}</td>
                  <td>
                    <v-btn
                      icon="mdi-pencil"
                      variant="text"
                      color="primary"
                      density="compact"
                      @click="handleEditPage(pageItem)"
                    />
                    <v-btn
                      icon="mdi-delete"
                      variant="text"
                      color="error"
                      density="compact"
                      @click="handleDeleteClick(pageItem)"
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
                {{ page }}-{{ Math.min(page * itemsPerPage, pages.length) }} of {{ pages.length }}
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
    <v-dialog v-model="dialog" max-width="500px">
      <v-card class="pa-4">
        <v-card-title class="text-h6 px-0 pt-0">
          Edit Page
        </v-card-title>
        <v-card-text class="px-0">
          <div>
            <div class="text-body-2 mb-1">Page Name</div>
            <v-text-field
              v-model="newPage.pageName"
              variant="plain"
              density="compact"
              bg-color="grey-lighten-3"
              hide-details
              class="mb-4 rounded"
              style="border: 1px solid #ddd"
            />

            <div class="text-body-2 mb-1">Description</div>
            <v-textarea
              v-model="newPage.pageDescription"
              variant="plain"
              density="compact"
              bg-color="grey-lighten-3"
              hide-details
              class="mb-4 rounded"
              style="border: 1px solid #ddd"
              rows="3"
            />

            <div class="text-body-2 mb-1">Roles</div>
            <v-autocomplete
              v-model="newPage.roles"
              :items="[
                { roleId: '1', roleName: 'System Admin' },
                { roleId: '2', roleName: 'Admin' },
                { roleId: '3', roleName: 'User' }
              ]"
              item-title="roleName"
              item-value="roleId"
              variant="plain"
              density="compact"
              bg-color="grey-lighten-3"
              hide-details
              class="rounded"
              style="border: 1px solid #ddd"
              multiple
              chips
              closable-chips
              :menu-props="{ closeOnContentClick: true }"
                    />
          </div>
        </v-card-text>
        <v-card-actions class="px-0 pt-4">
          <v-spacer />
          <v-btn
            color="grey-darken-1"
            variant="text"
            @click="closeDialog"
          >
            Cancel
          </v-btn>
          <v-btn
            color="primary"
            variant="text"
            @click="editingPage ? handleUpdatePage() : handleAddPage()"
            :loading="loading"
          >
            Update
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <!-- Delete Confirmation Dialog -->
    <v-dialog v-model="deleteDialog" max-width="400px">
      <v-card>
        <v-card-title>Confirm Delete</v-card-title>
        <v-card-text>
          Are you sure you want to delete this page?
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
.pages-container {
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