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
    Tooltip
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

interface Company {
    companyId: string;
    companyName: string;
}

interface CompanyDto {
    companyName: string;
}

const Company: React.FC = () => {
    const [pageTitle, setPageTitle] = useState('Company');
    const [companies, setCompanies] = useState<Company[]>([]);
    const [loading, setLoading] = useState(true);
    const [openDialog, setOpenDialog] = useState(false);
    const [editingCompany, setEditingCompany] = useState<Company | null>(null);
    const [companyName, setCompanyName] = useState('');
    const [snackbar, setSnackbar] = useState({
        open: false,
        message: '',
        severity: 'success' as 'success' | 'error' | 'info'
    });

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
            setLoading(false);
        } catch (error) {
            console.error('Error fetching companies:', error);
            setSnackbar({
                open: true,
                message: 'Error fetching companies',
                severity: 'error'
            });
            setLoading(false);
        }
    };

    useEffect(() => {
        const fetchPageTitle = async () => {
            try {
                const pages = await getMyPages();
                const companyPage = pages.find(page => page.url.includes('company'));
                if (companyPage) {
                    setPageTitle(companyPage.pageName);
                }
            } catch (error) {
                console.error('Error fetching page title:', error);
            }
        };

        fetchPageTitle();
        fetchCompanies();
    }, []);

    const handleAddCompany = async () => {
        try {
            const token = getTokenFromCookie();
            if (!token) {
                throw new Error('No authentication token found');
            }

            const companyDto: CompanyDto = {
                companyName: companyName
            };

            await axios.post('https://localhost:7076/api/v1/company/add-company',
                companyDto,
                {
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Content-Type': 'application/json'
                    }
                }
            );

            setSnackbar({
                open: true,
                message: 'Company added successfully',
                severity: 'success'
            });
            setOpenDialog(false);
            setCompanyName('');
            fetchCompanies();
        } catch (error) {
            console.error('Error adding company:', error);
            setSnackbar({
                open: true,
                message: 'Error adding company',
                severity: 'error'
            });
        }
    };

    const handleEditCompany = async (company: Company) => {
        setEditingCompany(company);
        setCompanyName(company.companyName);
        setOpenDialog(true);
    };

    const handleUpdateCompany = async () => {
        try {
            const token = getTokenFromCookie();
            if (!token || !editingCompany) {
                throw new Error('No authentication token found or no company selected');
            }

            const companyDto: CompanyDto = {
                companyName: companyName
            };

            await axios.put(`https://localhost:7076/api/v1/company/${editingCompany.companyId}`,
                companyDto,
                {
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Content-Type': 'application/json'
                    }
                }
            );

            setSnackbar({
                open: true,
                message: 'Company updated successfully',
                severity: 'success'
            });
            setOpenDialog(false);
            setCompanyName('');
            setEditingCompany(null);
            fetchCompanies();
        } catch (error) {
            console.error('Error updating company:', error);
            setSnackbar({
                open: true,
                message: 'Error updating company',
                severity: 'error'
            });
        }
    };

    const handleDeleteCompany = async (companyId: string) => {
        try {
            const token = getTokenFromCookie();
            if (!token) {
                throw new Error('No authentication token found');
            }

            await axios.delete(`https://localhost:7076/api/v1/company/${companyId}`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });

            setSnackbar({
                open: true,
                message: 'Company deleted successfully',
                severity: 'success'
            });
            fetchCompanies();
        } catch (error) {
            console.error('Error deleting company:', error);
            setSnackbar({
                open: true,
                message: 'Error deleting company',
                severity: 'error'
            });
        }
    };

    const handleCloseDialog = () => {
        setOpenDialog(false);
        setEditingCompany(null);
        setCompanyName('');
    };

    const handleCloseSnackbar = () => {
        setSnackbar({ ...snackbar, open: false });
    };

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
                        ADD COMPANY
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
                                        Company ID
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
                                {companies.map((company) => (
                                    <TableRow 
                                        key={company.companyId} 
                                        hover
                                        sx={{
                                            '&:last-child td': { borderBottom: 0 }
                                        }}
                                    >
                                        <TableCell sx={{ color: 'text.secondary' }}>
                                            {company.companyId}
                                        </TableCell>
                                        <TableCell>{company.companyName}</TableCell>
                                        <TableCell align="right" sx={{ width: '120px' }}>
                                            <Tooltip title="Edit functionality coming soon">
                                                <IconButton
                                                    color="info"
                                                    onClick={() => handleEditCompany(company)}
                                                    size="small"
                                                    sx={{ mr: 1 }}
                                                >
                                                    <EditIcon fontSize="small" />
                                                </IconButton>
                                            </Tooltip>
                                            <Tooltip title="Delete functionality coming soon">
                                                <IconButton
                                                    color="error"
                                                    onClick={() => handleDeleteCompany(company.companyId)}
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
                        sx: { borderRadius: 1 }
                    }}
                >
                    <DialogTitle sx={{ pb: 2, borderBottom: '1px solid #e0e0e0' }}>
                        Add New Company
                    </DialogTitle>
                    <DialogContent sx={{ mt: 2 }}>
                        <TextField
                            autoFocus
                            margin="dense"
                            label="Company Name"
                            type="text"
                            fullWidth
                            value={companyName}
                            onChange={(e) => setCompanyName(e.target.value)}
                            variant="outlined"
                        />
                    </DialogContent>
                    <DialogActions sx={{ p: 2, pt: 1 }}>
                        <Button onClick={handleCloseDialog} color="inherit">
                            Cancel
                        </Button>
                        <Button 
                            onClick={editingCompany ? handleUpdateCompany : handleAddCompany} 
                            variant="contained"
                        >
                            {editingCompany ? 'Update' : 'Add'}
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

export default Company; 