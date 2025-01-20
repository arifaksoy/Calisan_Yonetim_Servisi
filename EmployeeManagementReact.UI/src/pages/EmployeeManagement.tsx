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
    Typography,
    Box,
    Container,
    Stack,
    Button,
    Dialog,
    DialogTitle,
    DialogContent,
    DialogActions,
    TextField,
    Select,
    MenuItem,
    FormControl,
    InputLabel,
    Snackbar,
    Alert,
    IconButton,
    Tooltip,
} from '@mui/material';
import { Add as AddIcon, Edit as EditIcon, Delete as DeleteIcon } from '@mui/icons-material';
import axios from 'axios';
import { jwtDecode } from "jwt-decode";

const getTokenFromCookie = (): string | null => {
    const token = document.cookie
        .split('; ')
        .find(row => row.startsWith('auth_token='))
        ?.split('=')[1];
    return token || null;
};

interface Employee {
    personnelId: string;
    firstName: string;
    lastName: string;
    userName: string;
    email: string;
    companyId: string;
    companyName: string;
    roleId: string;
    roleName: string;
}

interface DecodedToken {
    CompanyId: string;
    // Add other token claims if needed
}

interface Company {
    companyId: string;
    companyName: string;
}

interface Role {
    roleId: string;
    roleName: string;
}

interface NewEmployee {
    personnel: {
        firstName: string;
        lastName: string;
        email: string;
    };
    user: {
        username: string;
        password: string;
        companyId: string;
    };
    role: {
        roleId: string;
    };
}

const EmployeeManagement: React.FC = () => {
    const [pageTitle, setPageTitle] = useState('Employee');
    const [employees, setEmployees] = useState<Employee[]>([]);
    const [loading, setLoading] = useState(true);
    const [openDialog, setOpenDialog] = useState(false);
    const [companies, setCompanies] = useState<Company[]>([]);
    const [roles, setRoles] = useState<Role[]>([]);
    const [editingEmployee, setEditingEmployee] = useState<Employee | null>(null);
    const [newEmployee, setNewEmployee] = useState<NewEmployee>({
        personnel: {
            firstName: '',
            lastName: '',
            email: '',
        },
        user: {
            username: '',
            password: '',
            companyId: '',
        },
        role: {
            roleId: '',
        },
    });
    const [snackbar, setSnackbar] = useState({
        open: false,
        message: '',
        severity: 'success' as 'success' | 'error' | 'info'
    });
    const [deleteConfirmOpen, setDeleteConfirmOpen] = useState(false);
    const [employeeToDelete, setEmployeeToDelete] = useState<Employee | null>(null);

    const fetchEmployees = async () => {
        try {
            const token = getTokenFromCookie();
            if (!token) {
                throw new Error('No authentication token found');
            }

            // Decode token to get companyId
            const decodedToken = jwtDecode<DecodedToken>(token);
            const companyId = decodedToken.CompanyId;

            const response = await axios.get(`https://localhost:7076/api/v1/company/${companyId}/employee`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            setEmployees(response.data);
            setLoading(false);
        } catch (error) {
            console.error('Error fetching employees:', error);
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

    const fetchRoles = async () => {
        try {
            const token = getTokenFromCookie();
            if (!token) {
                throw new Error('No authentication token found');
            }
            const decodedToken = jwtDecode<DecodedToken>(token);
            const companyId = decodedToken.CompanyId;
            const response = await axios.get(`https://localhost:7076/api/v1/company/${companyId}/roles`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            setRoles(response.data);
        } catch (error) {
            console.error('Error fetching roles:', error);
            setSnackbar({
                open: true,
                message: 'Error fetching roles',
                severity: 'error'
            });
        }
    };

    const handleAddEmployee = async () => {
        try {
            const token = getTokenFromCookie();
            if (!token) {
                throw new Error('No authentication token found');
            }

            const decodedToken = jwtDecode<DecodedToken>(token);
            const companyId = decodedToken.CompanyId;
            await axios.post(
                `https://localhost:7076/api/v1/company/${companyId}/employee`,
                newEmployee,
                {
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Content-Type': 'application/json'
                    }
                }
            );

            setSnackbar({
                open: true,
                message: 'Employee added successfully',
                severity: 'success'
            });
            setOpenDialog(false);
            resetNewEmployee();
            fetchEmployees();
        } catch (error) {
            console.error('Error adding employee:', error);
            setSnackbar({
                open: true,
                message: 'Error adding employee',
                severity: 'error'
            });
        }
    };

    const handleEditEmployee = async (employee: Employee) => {
        // First load companies and roles
        await Promise.all([fetchCompanies(), fetchRoles()]);
        
        setEditingEmployee(employee);
        setNewEmployee({
            personnel: {
                firstName: employee.firstName,
                lastName: employee.lastName,
                email: employee.email,
            },
            user: {
                username: employee.userName,
                password: '', // Leave empty for editing
                companyId: employee.companyId, // Use companyId instead of userCompanyId
            },
            role: {
                roleId: employee.roleId,
            },
        });
        setOpenDialog(true);
    };

    const handleUpdateEmployee = async () => {
        try {
            const token = getTokenFromCookie();
            if (!token || !editingEmployee) {
                throw new Error('No authentication token found or no employee selected');
            }

            const decodedToken = jwtDecode<DecodedToken>(token);
            const companyId = decodedToken.CompanyId;

            await axios.put(
                `https://localhost:7076/api/v1/company/${companyId}/employee/${editingEmployee.personnelId}`,
                newEmployee,
                {
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Content-Type': 'application/json'
                    }
                }
            );

            setSnackbar({
                open: true,
                message: 'Employee updated successfully',
                severity: 'success'
            });
            setOpenDialog(false);
            resetNewEmployee();
            setEditingEmployee(null);
            fetchEmployees();
        } catch (error) {
            console.error('Error updating employee:', error);
            setSnackbar({
                open: true,
                message: 'Error updating employee',
                severity: 'error'
            });
        }
    };

    const resetNewEmployee = () => {
        setNewEmployee({
            personnel: {
                firstName: '',
                lastName: '',
                email: '',
            },
            user: {
                username: '',
                password: '',
                companyId: '',
            },
            role: {
                roleId: '',
            },
        });
    };

    const handleCloseDialog = () => {
        setOpenDialog(false);
        resetNewEmployee();
        setEditingEmployee(null);
    };

    const handleCloseSnackbar = () => {
        setSnackbar({ ...snackbar, open: false });
    };

    const handleDeleteClick = (employee: Employee) => {
        setEmployeeToDelete(employee);
        setDeleteConfirmOpen(true);
    };

    const handleDeleteConfirm = async () => {
        try {
            const token = getTokenFromCookie();
            if (!token || !employeeToDelete) {
                throw new Error('No authentication token found or no employee selected');
            }

            const decodedToken = jwtDecode<DecodedToken>(token);
            const companyId = decodedToken.CompanyId;

            await axios.delete(
                `https://localhost:7076/api/v1/company/${companyId}/employee/${employeeToDelete.personnelId}`,
                {
                    headers: {
                        'Authorization': `Bearer ${token}`
                    }
                }
            );

            setSnackbar({
                open: true,
                message: 'Employee deleted successfully',
                severity: 'success'
            });
            setDeleteConfirmOpen(false);
            setEmployeeToDelete(null);
            fetchEmployees();
        } catch (error) {
            console.error('Error deleting employee:', error);
            setSnackbar({
                open: true,
                message: 'Error deleting employee',
                severity: 'error'
            });
        }
    };

    const handleDeleteCancel = () => {
        setDeleteConfirmOpen(false);
        setEmployeeToDelete(null);
    };

    useEffect(() => {
        const fetchPageTitle = async () => {
            try {
                const pages = await getMyPages();
                const employeePage = pages.find(page => page.url?.includes('employee'));
                if (employeePage) {
                    setPageTitle(employeePage.pageName);
                }
            } catch (error) {
                console.error('Error fetching page title:', error);
            }
        };

        fetchPageTitle();
        fetchEmployees();
    }, []);

    // Load companies and roles when dialog opens for adding new employee
    useEffect(() => {
        if (openDialog && !editingEmployee) {
            Promise.all([fetchCompanies(), fetchRoles()]);
        }
    }, [openDialog]);

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
                        ADD EMPLOYEE
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
                                        Personnel ID
                                    </TableCell>
                                    <TableCell sx={{ 
                                        fontWeight: 500,
                                        backgroundColor: '#fff',
                                        borderBottom: '1px solid #e0e0e0'
                                    }}>
                                        First Name
                                    </TableCell>
                                    <TableCell sx={{ 
                                        fontWeight: 500,
                                        backgroundColor: '#fff',
                                        borderBottom: '1px solid #e0e0e0'
                                    }}>
                                        Last Name
                                    </TableCell>
                                    <TableCell sx={{ 
                                        fontWeight: 500,
                                        backgroundColor: '#fff',
                                        borderBottom: '1px solid #e0e0e0'
                                    }}>
                                        Email
                                    </TableCell>
                                    <TableCell sx={{ 
                                        fontWeight: 500,
                                        backgroundColor: '#fff',
                                        borderBottom: '1px solid #e0e0e0'
                                    }}>
                                        Company Name
                                    </TableCell>
                                    <TableCell sx={{ 
                                        fontWeight: 500,
                                        backgroundColor: '#fff',
                                        borderBottom: '1px solid #e0e0e0'
                                    }}>
                                        UserName
                                    </TableCell>
                                    <TableCell sx={{ 
                                        fontWeight: 500,
                                        backgroundColor: '#fff',
                                        borderBottom: '1px solid #e0e0e0'
                                    }}>
                                        Role Name
                                    </TableCell>
                                    <TableCell sx={{ 
                                        fontWeight: 500,
                                        backgroundColor: '#fff',
                                        borderBottom: '1px solid #e0e0e0',
                                        width: '80px'
                                    }}>
                                        Actions
                                    </TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {employees.map((employee) => (
                                    <TableRow 
                                        key={employee.personnelId} 
                                        hover
                                        sx={{
                                            '&:last-child td': { borderBottom: 0 }
                                        }}
                                    >
                                        <TableCell sx={{ color: 'text.secondary' }}>
                                            {employee.personnelId}
                                        </TableCell>
                                        <TableCell>{employee.firstName}</TableCell>
                                        <TableCell>{employee.lastName}</TableCell>
                                        <TableCell>{employee.email}</TableCell>
                                        <TableCell>{employee.companyName}</TableCell>
                                        <TableCell>{employee.userName}</TableCell>
                                        <TableCell>{employee.roleName}</TableCell>
                                        <TableCell>
                                            <Tooltip title="Edit employee">
                                                <IconButton
                                                    color="info"
                                                    onClick={() => handleEditEmployee(employee)}
                                                    size="small"
                                                    sx={{ mr: 1 }}
                                                >
                                                    <EditIcon fontSize="small" />
                                                </IconButton>
                                            </Tooltip>
                                            <Tooltip title="Delete employee">
                                                <IconButton
                                                    color="error"
                                                    onClick={() => handleDeleteClick(employee)}
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
                    </Paper>
                )}

                <Dialog 
                    open={openDialog} 
                    onClose={handleCloseDialog}
                    PaperProps={{
                        sx: { borderRadius: 1, width: '500px' }
                    }}
                >
                    <DialogTitle sx={{ pb: 2, borderBottom: '1px solid #e0e0e0' }}>
                        {editingEmployee ? 'Edit Employee' : 'Add New Employee'}
                    </DialogTitle>
                    <DialogContent sx={{ mt: 2 }}>
                        <Stack spacing={2}>
                            <TextField
                                label="First Name"
                                fullWidth
                                value={newEmployee.personnel.firstName}
                                onChange={(e) => setNewEmployee(prev => ({
                                    ...prev,
                                    personnel: {
                                        ...prev.personnel,
                                        firstName: e.target.value
                                    }
                                }))}
                                variant="outlined"
                            />
                            <TextField
                                label="Last Name"
                                fullWidth
                                value={newEmployee.personnel.lastName}
                                onChange={(e) => setNewEmployee(prev => ({
                                    ...prev,
                                    personnel: {
                                        ...prev.personnel,
                                        lastName: e.target.value
                                    }
                                }))}
                                variant="outlined"
                            />
                            <TextField
                                label="Email"
                                type="email"
                                fullWidth
                                value={newEmployee.personnel.email}
                                onChange={(e) => setNewEmployee(prev => ({
                                    ...prev,
                                    personnel: {
                                        ...prev.personnel,
                                        email: e.target.value
                                    }
                                }))}
                                variant="outlined"
                            />
                            <TextField
                                label="Username"
                                fullWidth
                                value={newEmployee.user.username}
                                onChange={(e) => setNewEmployee(prev => ({
                                    ...prev,
                                    user: {
                                        ...prev.user,
                                        username: e.target.value
                                    }
                                }))}
                                variant="outlined"
                            />
                            <TextField
                                label="Password"
                                type="password"
                                fullWidth
                                value={newEmployee.user.password}
                                onChange={(e) => setNewEmployee(prev => ({
                                    ...prev,
                                    user: {
                                        ...prev.user,
                                        password: e.target.value
                                    }
                                }))}
                                helperText={editingEmployee ? "Leave empty to keep current password" : ""}
                                variant="outlined"
                            />
                            <FormControl fullWidth variant="outlined">
                                <InputLabel>Company</InputLabel>
                                <Select
                                    value={newEmployee.user.companyId}
                                    label="Company"
                                    onChange={(e) => setNewEmployee(prev => ({
                                        ...prev,
                                        user: {
                                            ...prev.user,
                                            companyId: e.target.value
                                        }
                                    }))}
                                >
                                    {companies.map((company) => (
                                        <MenuItem key={company.companyId} value={company.companyId}>
                                            {company.companyName}
                                        </MenuItem>
                                    ))}
                                </Select>
                            </FormControl>
                            <FormControl fullWidth variant="outlined">
                                <InputLabel>Role</InputLabel>
                                <Select
                                    value={newEmployee.role.roleId}
                                    label="Role"
                                    onChange={(e) => setNewEmployee(prev => ({
                                        ...prev,
                                        role: {
                                            roleId: e.target.value
                                        }
                                    }))}
                                >
                                    {roles.map((role) => (
                                        <MenuItem key={role.roleId} value={role.roleId}>
                                            {role.roleName}
                                        </MenuItem>
                                    ))}
                                </Select>
                            </FormControl>
                        </Stack>
                    </DialogContent>
                    <DialogActions sx={{ p: 2, pt: 1 }}>
                        <Button onClick={handleCloseDialog} color="inherit">
                            Cancel
                        </Button>
                        <Button 
                            onClick={editingEmployee ? handleUpdateEmployee : handleAddEmployee}
                            variant="contained"
                            disabled={
                                !newEmployee.personnel.firstName ||
                                !newEmployee.personnel.lastName ||
                                !newEmployee.personnel.email ||
                                !newEmployee.user.username ||
                                (!editingEmployee && !newEmployee.user.password) || // Only require password for new employees
                                !newEmployee.role.roleId
                            }
                        >
                            {editingEmployee ? 'Update' : 'Add'}
                        </Button>
                    </DialogActions>
                </Dialog>

                <Dialog
                    open={deleteConfirmOpen}
                    onClose={handleDeleteCancel}
                    PaperProps={{
                        sx: { borderRadius: 1 }
                    }}
                >
                    <DialogTitle sx={{ pb: 2, borderBottom: '1px solid #e0e0e0' }}>
                        Confirm Delete
                    </DialogTitle>
                    <DialogContent sx={{ mt: 2 }}>
                        <Typography>
                            Are you sure you want to delete employee{' '}
                            <strong>
                                {employeeToDelete?.firstName} {employeeToDelete?.lastName}
                            </strong>
                            ?
                        </Typography>
                    </DialogContent>
                    <DialogActions sx={{ p: 2, pt: 1 }}>
                        <Button onClick={handleDeleteCancel} color="inherit">
                            Cancel
                        </Button>
                        <Button 
                            onClick={handleDeleteConfirm}
                            variant="contained"
                            color="error"
                        >
                            Delete
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
                        variant="filled"
                        sx={{ width: '100%' }}
                    >
                        {snackbar.message}
                    </Alert>
                </Snackbar>
            </Container>
        </Stack>
    );
};

export default EmployeeManagement; 