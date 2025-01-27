import React, { useEffect, useState } from 'react';
import { getMyPages } from '../services/pageService';
import {
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Paper,
    CircularProgress,
    Button,
    Dialog,
    DialogTitle,
    DialogContent,
    DialogActions,
    TextField,
    IconButton,
    Snackbar,
    Alert,
    Typography,
    Box,
    Container,
    Stack,
    Tooltip,
    FormControl,
    InputLabel,
    Select,
    MenuItem,
    TablePagination
} from '@mui/material';
import { Edit as EditIcon, Delete as DeleteIcon, Add as AddIcon } from '@mui/icons-material';
import axios from 'axios';
import { jwtDecode } from "jwt-decode";

const getTokenFromCookie = (): string | null => {
    const token = document.cookie
        .split('; ')
        .find(row => row.startsWith('auth_token='))
        ?.split('=')[1];
    return token || null;
};

interface DecodedToken {
    CompanyId: string;
    [key: string]: any;
}

interface Project {
    projectId: string;
    projectName: string;
    projectDescription: string;
    companyId: string;
    companyName: string;
    status: number;
}

interface ProjectDto {
    projectName: string;
    projectDescription: string;
    companyId: string;
}

interface Company {
    companyId: string;
    companyName: string;
}

const Projects: React.FC = () => {
    const [pageTitle, setPageTitle] = useState('Projects');
    const [projects, setProjects] = useState<Project[]>([]);
    const [loading, setLoading] = useState(true);
    const [openDialog, setOpenDialog] = useState(false);
    const [editingProject, setEditingProject] = useState<Project | null>(null);
    const [projectName, setProjectName] = useState('');
    const [projectDescription, setProjectDescription] = useState('');
    const [snackbar, setSnackbar] = useState({
        open: false,
        message: '',
        severity: 'success' as 'success' | 'error' | 'info'
    });
    const [companies, setCompanies] = useState<Company[]>([]);
    const [selectedCompanyId, setSelectedCompanyId] = useState('');
    const [page, setPage] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(10);

    const fetchProjects = async () => {
        try {
            const token = getTokenFromCookie();
            if (!token) {
                throw new Error('No authentication token found');
            }
            
            const decodedToken = jwtDecode<DecodedToken>(token);
            const companyId = decodedToken.CompanyId;

            const response = await axios.get(`https://localhost:7076/api/v1/company/${companyId}/projects`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            setProjects(response.data);
            setLoading(false);
        } catch (error) {
            console.error('Error fetching projects:', error);
            setSnackbar({
                open: true,
                message: 'Error fetching projects',
                severity: 'error'
            });
            setLoading(false);
        }
    };

    const fetchCompanies = async () => {
        try {
            const token = getTokenFromCookie();
            if (!token) {
                throw new Error('No authentication token found');
            }
            
            const response = await axios.get('https://localhost:7076/api/v1/company/get-companies', {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            setCompanies(response.data);
        } catch (error) {
            console.error('Error fetching companies:', error);
            setSnackbar({
                open: true,
                message: 'Error fetching companies',
                severity: 'error'
            });
        }
    };

    useEffect(() => {
        const fetchPageTitle = async () => {
            try {
                const pages = await getMyPages();
                const projectsPage = pages.find(page => page.url?.includes('projects'));
                if (projectsPage) {
                    setPageTitle(projectsPage.pageName);
                }
            } catch (error) {
                console.error('Error fetching page title:', error);
            }
        };

        fetchPageTitle();
        fetchProjects();
        fetchCompanies();
    }, []);

    const handleAddProject = async () => {
        try {
            const token = getTokenFromCookie();
            if (!token) {
                throw new Error('No authentication token found');
            }
            const decodedToken = jwtDecode<DecodedToken>(token);
            const companyId = decodedToken.CompanyId;

            if (!selectedCompanyId) {
                setSnackbar({
                    open: true,
                    message: 'Please select a company',
                    severity: 'error'
                });
                return;
            }

            const projectDto: ProjectDto = {
                projectName,
                projectDescription,
                companyId: selectedCompanyId
            };

            await axios.post(`https://localhost:7076/api/v1/company/${companyId}/projects`,
                projectDto,
                {
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Content-Type': 'application/json'
                    }
                }
            );

            setSnackbar({
                open: true,
                message: 'Project added successfully',
                severity: 'success'
            });
            setOpenDialog(false);
            resetForm();
            fetchProjects();
        } catch (error) {
            console.error('Error adding project:', error);
            setSnackbar({
                open: true,
                message: 'Error adding project',
                severity: 'error'
            });
        }
    };

    const handleEditProject = async (project: Project) => {
        setEditingProject(project);
        setProjectName(project.projectName);
        setProjectDescription(project.projectDescription);
        setSelectedCompanyId(project.companyId);
        setOpenDialog(true);
    };

    const handleUpdateProject = async () => {
        try {
            const token = getTokenFromCookie();
            if (!token || !editingProject) {
                throw new Error('No authentication token found or no project selected');
            }

            if (!selectedCompanyId) {
                setSnackbar({
                    open: true,
                    message: 'Please select a company',
                    severity: 'error'
                });
                return;
            }

            const decodedToken = jwtDecode<DecodedToken>(token);
            const companyId = decodedToken.CompanyId;

            const projectDto: ProjectDto = {
                projectName,
                projectDescription,
                companyId: selectedCompanyId
            };

            await axios.put(`https://localhost:7076/api/v1/company/${companyId}/projects/${editingProject.projectId}`,
                projectDto,
                {
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Content-Type': 'application/json'
                    }
                }
            );

            setSnackbar({
                open: true,
                message: 'Project updated successfully',
                severity: 'success'
            });
            setOpenDialog(false);
            resetForm();
            fetchProjects();
        } catch (error) {
            console.error('Error updating project:', error);
            setSnackbar({
                open: true,
                message: 'Error updating project',
                severity: 'error'
            });
        }
    };

    const handleDeleteProject = async (projectId: string) => {
        try {
            const token = getTokenFromCookie();
            if (!token) {
                throw new Error('No authentication token found');
            }

            const decodedToken = jwtDecode<DecodedToken>(token);
            const companyId = decodedToken.CompanyId;

            await axios.delete(`https://localhost:7076/api/v1/company/${companyId}/projects/${projectId}`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });

            setSnackbar({
                open: true,
                message: 'Project deleted successfully',
                severity: 'success'
            });
            fetchProjects();
        } catch (error) {
            console.error('Error deleting project:', error);
            setSnackbar({
                open: true,
                message: 'Error deleting project',
                severity: 'error'
            });
        }
    };

    const resetForm = () => {
        setProjectName('');
        setProjectDescription('');
        setSelectedCompanyId('');
        setEditingProject(null);
    };

    const handleCloseDialog = () => {
        setOpenDialog(false);
        resetForm();
    };

    const handleCloseSnackbar = () => {
        setSnackbar({ ...snackbar, open: false });
    };

    const handleChangePage = (event: unknown, newPage: number) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };

    // Calculate the current page's data
    const currentPageData = projects.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage);

    return (
        <Stack spacing={3}>
            <Box sx={{ 
                borderBottom: '1px solid #e0e0e0',
                pb: 2,
                display: 'flex',
                alignItems: 'center'
            }}>
                <Typography 
                    variant="h4" 
                    component="h1" 
                    sx={{ 
                        color: '#666',
                        fontWeight: 500,
                        fontSize: '1.75rem'
                    }}
                >
                    {pageTitle}
                </Typography>
                <Typography 
                    variant="h4" 
                    component="span" 
                    sx={{ 
                        color: '#666',
                        fontWeight: 400,
                        fontSize: '1.75rem',
                        ml: 1
                    }}
                >
                    Management
                </Typography>
            </Box>

            <Container maxWidth={false} sx={{ p: 0 }}>
                <Box sx={{ mb: 2 }}>
                    <Button
                        variant="contained"
                        color="primary"
                        startIcon={<AddIcon />}
                        onClick={() => setOpenDialog(true)}
                        fullWidth
                        sx={{ 
                            borderRadius: 1,
                            py: 1.5,
                            backgroundColor: '#1976d2',
                            '&:hover': {
                                backgroundColor: '#1565c0'
                            }
                        }}
                    >
                        ADD PROJECT
                    </Button>
                </Box>

                {loading ? (
                    <Box sx={{ display: 'flex', justifyContent: 'center', p: 4 }}>
                        <CircularProgress />
                    </Box>
                ) : (
                    <Paper sx={{ borderRadius: 1, overflow: 'hidden' }}>
                        <Table>
                            <TableHead>
                                <TableRow>
                                    <TableCell sx={{ 
                                        fontWeight: 500,
                                        backgroundColor: '#fff',
                                        borderBottom: '1px solid #e0e0e0'
                                    }}>
                                        Project ID
                                    </TableCell>
                                    <TableCell sx={{ 
                                        fontWeight: 500,
                                        backgroundColor: '#fff',
                                        borderBottom: '1px solid #e0e0e0'
                                    }}>
                                        Project Name
                                    </TableCell>
                                    <TableCell sx={{ 
                                        fontWeight: 500,
                                        backgroundColor: '#fff',
                                        borderBottom: '1px solid #e0e0e0'
                                    }}>
                                        Description
                                    </TableCell>
                                    <TableCell sx={{ 
                                        fontWeight: 500,
                                        backgroundColor: '#fff',
                                        borderBottom: '1px solid #e0e0e0'
                                    }}>
                                        Company Name
                                    </TableCell>
                                    <TableCell align="right" sx={{ 
                                        fontWeight: 500,
                                        backgroundColor: '#fff',
                                        borderBottom: '1px solid #e0e0e0',
                                        width: '120px'
                                    }}>
                                        Actions
                                    </TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {currentPageData.map((project) => (
                                    <TableRow 
                                        key={project.projectId} 
                                        hover
                                        sx={{
                                            '&:last-child td': { borderBottom: 0 }
                                        }}
                                    >
                                        <TableCell sx={{ color: 'text.secondary' }}>
                                            {project.projectId}
                                        </TableCell>
                                        <TableCell>{project.projectName}</TableCell>
                                        <TableCell>{project.projectDescription}</TableCell>
                                        <TableCell>{project.companyName}</TableCell>
                                        <TableCell align="right" sx={{ width: '120px' }}>
                                            <Tooltip title="Edit project">
                                                <IconButton
                                                    color="info"
                                                    onClick={() => handleEditProject(project)}
                                                    size="small"
                                                    sx={{ mr: 1 }}
                                                >
                                                    <EditIcon fontSize="small" />
                                                </IconButton>
                                            </Tooltip>
                                            <Tooltip title="Delete project">
                                                <IconButton
                                                    color="error"
                                                    onClick={() => handleDeleteProject(project.projectId)}
                                                    size="small"
                                                >
                                                    <DeleteIcon fontSize="small" />
                                                </IconButton>
                                            </Tooltip>
                                        </TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                        <TablePagination
                            rowsPerPageOptions={[10, 25, 50]}
                            component="div"
                            count={projects.length}
                            rowsPerPage={rowsPerPage}
                            page={page}
                            onPageChange={handleChangePage}
                            onRowsPerPageChange={handleChangeRowsPerPage}
                        />
                    </Paper>
                )}

                <Dialog 
                    open={openDialog} 
                    onClose={handleCloseDialog}
                    PaperProps={{
                        sx: { borderRadius: 1 }
                    }}
                >
                    <DialogTitle sx={{ pb: 2, borderBottom: '1px solid #e0e0e0' }}>
                        {editingProject ? 'Edit Project' : 'Add New Project'}
                    </DialogTitle>
                    <DialogContent sx={{ mt: 2 }}>
                        <FormControl fullWidth variant="outlined" sx={{ mb: 2 }}>
                            <InputLabel id="company-select-label">Company</InputLabel>
                            <Select
                                labelId="company-select-label"
                                id="company-select"
                                value={selectedCompanyId}
                                onChange={(e) => setSelectedCompanyId(e.target.value)}
                                label="Company"
                            >
                                {companies.map((company) => (
                                    <MenuItem key={company.companyId} value={company.companyId}>
                                        {company.companyName}
                                    </MenuItem>
                                ))}
                            </Select>
                        </FormControl>
                        <TextField
                            autoFocus
                            margin="dense"
                            label="Project Name"
                            type="text"
                            fullWidth
                            value={projectName}
                            onChange={(e) => setProjectName(e.target.value)}
                            variant="outlined"
                            sx={{ mb: 2 }}
                        />
                        <TextField
                            margin="dense"
                            label="Description"
                            type="text"
                            fullWidth
                            value={projectDescription}
                            onChange={(e) => setProjectDescription(e.target.value)}
                            variant="outlined"
                            sx={{ mb: 2 }}
                        />
                    </DialogContent>
                    <DialogActions sx={{ p: 2, borderTop: '1px solid #e0e0e0' }}>
                        <Button onClick={handleCloseDialog} color="inherit">
                            Cancel
                        </Button>
                        <Button 
                            onClick={editingProject ? handleUpdateProject : handleAddProject}
                            variant="contained"
                            color="primary"
                        >
                            {editingProject ? 'Update' : 'Add'}
                        </Button>
                    </DialogActions>
                </Dialog>

                <Snackbar 
                    open={snackbar.open} 
                    autoHideDuration={6000} 
                    onClose={handleCloseSnackbar}
                    anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
                >
                    <Alert 
                        onClose={handleCloseSnackbar} 
                        severity={snackbar.severity}
                        sx={{ width: '100%' }}
                    >
                        {snackbar.message}
                    </Alert>
                </Snackbar>
            </Container>
        </Stack>
    );
};

export default Projects; 