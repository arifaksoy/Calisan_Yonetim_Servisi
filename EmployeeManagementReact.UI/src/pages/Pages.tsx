import React, { useEffect, useState } from 'react';
import { getMyPages } from '../services/pageService';
import { jwtDecode } from 'jwt-decode';
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
    OutlinedInput,
    Chip,
    TablePagination
} from '@mui/material';
import { Edit as EditIcon, Delete as DeleteIcon, Add as AddIcon } from '@mui/icons-material';
import axios from 'axios';

const getTokenFromCookie = (): string | null => {
    const token = document.cookie
        .split('; ')
        .find(row => row.startsWith('auth_token='))
        ?.split('=')[1];
    return token || null;
};

interface DecodedToken {
    UserId: string;
    CompanyId: string;
    [key: string]: any;
}

const getUserIdFromToken = (token: string): string => {
    const decoded = jwtDecode(token) as DecodedToken;
    return decoded.UserId;
};

const getCompanyIdFromToken = (token: string): string => {
    const decoded = jwtDecode(token) as DecodedToken;
    return decoded.CompanyId;
};

interface Role {
    roleId: string;
    roleName: string;
}

interface Page {
    pageId: string;
    pageName: string;
    pageDescription: string;
    roles: Role[];
    status: number;
}

interface PageDto {
    pageName: string;
    pageDescription: string;
    roles: { roleId: string }[];
}

const Pages: React.FC = () => {
    const [pageTitle, setPageTitle] = useState('Pages');
    const [pages, setPages] = useState<Page[]>([]);
    const [loading, setLoading] = useState(true);
    const [openDialog, setOpenDialog] = useState(false);
    const [editingPage, setEditingPage] = useState<Page | null>(null);
    const [pageName, setPageName] = useState('');
    const [pageDescription, setPageDescription] = useState('');
    const [availableRoles, setAvailableRoles] = useState<Role[]>([]);
    const [selectedRoleIds, setSelectedRoleIds] = useState<string[]>([]);
    const [page, setPage] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(10);
    const [snackbar, setSnackbar] = useState({
        open: false,
        message: '',
        severity: 'success' as 'success' | 'error' | 'info'
    });

    const fetchRoles = async () => {
        try {
            const token = getTokenFromCookie();
            if (!token) {
                throw new Error('No authentication token found');
            }
            
            const companyId = getCompanyIdFromToken(token);
            const response = await axios.get(`https://localhost:7076/api/v1/company/${companyId}/roles`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            setAvailableRoles(response.data);
        } catch (error) {
            console.error('Error fetching roles:', error);
            setSnackbar({
                open: true,
                message: 'Error fetching roles',
                severity: 'error'
            });
        }
    };

    const fetchPages = async () => {
        try {
            const token = getTokenFromCookie();
            if (!token) {
                throw new Error('No authentication token found');
            }
            
            const userId = getUserIdFromToken(token);
            const response = await axios.get(`https://localhost:7076/api/v1/pages/${userId}/my-pages`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            setPages(response.data);
            setLoading(false);
        } catch (error) {
            console.error('Error fetching pages:', error);
            setSnackbar({
                open: true,
                message: 'Error fetching pages',
                severity: 'error'
            });
            setLoading(false);
        }
    };

    useEffect(() => {
        const fetchPageTitle = async () => {
            try {
                const pages = await getMyPages();
                const pagesPage = pages.find(page => page.url?.includes('pages'));
                if (pagesPage) {
                    setPageTitle(pagesPage.pageName);
                }
            } catch (error) {
                console.error('Error fetching page title:', error);
            }
        };

        fetchPageTitle();
        fetchPages();
        fetchRoles();
    }, []);

    const handleAddPage = async () => {
        try {
            const token = getTokenFromCookie();
            if (!token) {
                throw new Error('No authentication token found');
            }

            const userId = getUserIdFromToken(token);
            const pageDto: PageDto = {
                pageName: pageName,
                pageDescription: pageDescription,
                roles: selectedRoleIds.map(id => ({ roleId: id }))
            };

            await axios.post(`https://localhost:7076/api/v1/pages/${userId}/add-page`,
                pageDto,
                {
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Content-Type': 'application/json'
                    }
                }
            );

            setSnackbar({
                open: true,
                message: 'Page added successfully',
                severity: 'success'
            });
            setOpenDialog(false);
            setPageName('');
            setPageDescription('');
            setSelectedRoleIds([]);
            fetchPages();
        } catch (error) {
            console.error('Error adding page:', error);
            setSnackbar({
                open: true,
                message: 'Error adding page',
                severity: 'error'
            });
        }
    };

    const handleEditPage = async (page: Page) => {
        setEditingPage(page);
        setPageName(page.pageName);
        setPageDescription(page.pageDescription);
        setSelectedRoleIds(page.roles.map(role => role.roleId));
        setOpenDialog(true);
    };

    const handleUpdatePage = async () => {
        try {
            const token = getTokenFromCookie();
            if (!token || !editingPage) {
                throw new Error('No authentication token found or no page selected');
            }

            const userId = getUserIdFromToken(token);
            const pageDto: PageDto = {
                pageName: pageName,
                pageDescription: pageDescription,
                roles: selectedRoleIds.map(id => ({ roleId: id }))
            };

            await axios.put(`https://localhost:7076/api/v1/pages/${userId}/page/${editingPage.pageId}`,
                pageDto,
                {
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Content-Type': 'application/json'
                    }
                }
            );

            setSnackbar({
                open: true,
                message: 'Page updated successfully',
                severity: 'success'
            });
            setOpenDialog(false);
            setPageName('');
            setPageDescription('');
            setSelectedRoleIds([]);
            setEditingPage(null);
            fetchPages();
        } catch (error) {
            console.error('Error updating page:', error);
            setSnackbar({
                open: true,
                message: 'Error updating page',
                severity: 'error'
            });
        }
    };

    const handleDeletePage = async (pageId: string) => {
        try {
            const token = getTokenFromCookie();
            if (!token) {
                throw new Error('No authentication token found');
            }

            const userId = getUserIdFromToken(token);
            await axios.delete(`https://localhost:7076/api/v1/pages/${userId}/page/${pageId}`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });

            setSnackbar({
                open: true,
                message: 'Page deleted successfully',
                severity: 'success'
            });
            fetchPages();
        } catch (error) {
            console.error('Error deleting page:', error);
            setSnackbar({
                open: true,
                message: 'Error deleting page',
                severity: 'error'
            });
        }
    };

    const handleCloseDialog = () => {
        setOpenDialog(false);
        setEditingPage(null);
        setPageName('');
        setPageDescription('');
        setSelectedRoleIds([]);
    };

    const handleCloseSnackbar = () => {
        setSnackbar({ ...snackbar, open: false });
    };

    const handleRoleChange = (event: any) => {
        const value = event.target.value;
        setSelectedRoleIds(typeof value === 'string' ? value.split(',') : value);
    };

    const handleRoleDelete = (roleIdToDelete: string) => (event: React.MouseEvent) => {
        event.preventDefault();
        event.stopPropagation();
        setSelectedRoleIds(selectedRoleIds.filter(roleId => roleId !== roleIdToDelete));
    };

    const handleChangePage = (event: unknown, newPage: number) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };

    // Calculate the current page's data
    const currentPageData = pages.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage);

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
                        ADD PAGE
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
                                        Page ID
                                    </TableCell>
                                    <TableCell sx={{ 
                                        fontWeight: 500,
                                        backgroundColor: '#fff',
                                        borderBottom: '1px solid #e0e0e0'
                                    }}>
                                        Page Name
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
                                        Roles
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
                                {currentPageData.map((page) => (
                                    <TableRow 
                                        key={page.pageId} 
                                        hover
                                        sx={{
                                            '&:last-child td': { borderBottom: 0 }
                                        }}
                                    >
                                        <TableCell sx={{ color: 'text.secondary' }}>
                                            {page.pageId}
                                        </TableCell>
                                        <TableCell>{page.pageName}</TableCell>
                                        <TableCell>{page.pageDescription}</TableCell>
                                        <TableCell>
                                            {page.roles?.map(role => role.roleName).join(', ')}
                                        </TableCell>
                                        <TableCell align="right" sx={{ width: '120px' }}>
                                            <Tooltip title="Edit page">
                                                <IconButton
                                                    color="info"
                                                    onClick={() => handleEditPage(page)}
                                                    size="small"
                                                    sx={{ mr: 1 }}
                                                >
                                                    <EditIcon fontSize="small" />
                                                </IconButton>
                                            </Tooltip>
                                            <Tooltip title="Delete page">
                                                <IconButton
                                                    color="error"
                                                    onClick={() => handleDeletePage(page.pageId)}
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
                            count={pages.length}
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
                        {editingPage ? 'Edit Page' : 'Add New Page'}
                    </DialogTitle>
                    <DialogContent sx={{ mt: 2 }}>
                        <TextField
                            autoFocus
                            margin="dense"
                            label="Page Name"
                            type="text"
                            fullWidth
                            value={pageName}
                            onChange={(e) => setPageName(e.target.value)}
                            variant="outlined"
                            sx={{ mb: 2 }}
                        />
                        <TextField
                            margin="dense"
                            label="Description"
                            type="text"
                            fullWidth
                            value={pageDescription}
                            onChange={(e) => setPageDescription(e.target.value)}
                            variant="outlined"
                            sx={{ mb: 2 }}
                        />
                        <FormControl fullWidth variant="outlined">
                            <InputLabel id="roles-label">Roles</InputLabel>
                            <Select
                                labelId="roles-label"
                                multiple
                                value={selectedRoleIds}
                                onChange={handleRoleChange}
                                input={<OutlinedInput label="Roles" />}
                                renderValue={(selected) => (
                                    <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 0.5 }}>
                                        {selected.map((roleId) => {
                                            const role = availableRoles.find(r => r.roleId === roleId);
                                            return role ? (
                                                <Chip 
                                                    key={roleId} 
                                                    label={role.roleName}
                                                    onDelete={handleRoleDelete(roleId)}
                                                    onMouseDown={(event) => {
                                                        event.stopPropagation();
                                                    }}
                                                    sx={{ m: 0.5 }}
                                                />
                                            ) : null;
                                        })}
                                    </Box>
                                )}
                            >
                                {availableRoles
                                    .filter(role => !selectedRoleIds.includes(role.roleId))
                                    .map((role) => (
                                        <MenuItem key={role.roleId} value={role.roleId}>
                                            {role.roleName}
                                        </MenuItem>
                                    ))
                                }
                            </Select>
                        </FormControl>
                    </DialogContent>
                    <DialogActions sx={{ p: 2, pt: 1 }}>
                        <Button onClick={handleCloseDialog} color="inherit">
                            Cancel
                        </Button>
                        <Button 
                            onClick={editingPage ? handleUpdatePage : handleAddPage} 
                            variant="contained"
                        >
                            {editingPage ? 'Update' : 'Add'}
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

export default Pages; 