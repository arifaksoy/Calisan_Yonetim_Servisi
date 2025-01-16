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

interface DecodedToken {
    UserId: string;
    [key: string]: any;
}

const getUserIdFromToken = (token: string): string => {
    const decoded = jwtDecode(token) as DecodedToken;
    return decoded.UserId;
};

interface Page {
    pageId: string;
    pageName: string;
    pageDescription: string;
}

interface PageDto {
    pageName: string;
    pageDescription: string;
}

const Pages: React.FC = () => {
    const [pageTitle, setPageTitle] = useState('Pages');
    const [pages, setPages] = useState<Page[]>([]);
    const [loading, setLoading] = useState(true);
    const [openDialog, setOpenDialog] = useState(false);
    const [editingPage, setEditingPage] = useState<Page | null>(null);
    const [pageName, setPageName] = useState('');
    const [pageDescription, setPageDescription] = useState('');
    const [snackbar, setSnackbar] = useState({
        open: false,
        message: '',
        severity: 'success' as 'success' | 'error' | 'info'
    });

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
                pageDescription: pageDescription
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
                pageDescription: pageDescription
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
                                {pages.map((page) => (
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
                        />
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