import React, { useState, useEffect } from 'react';
import {
  Box,
  Typography,
  Button,
  Card,
  CardContent,
  IconButton,
  Grid,
  Dialog,
  DialogTitle,
  DialogContent,
} from '@mui/material';
import { Add, Edit, Delete, ExpandMore, ExpandLess } from '@mui/icons-material';
import { ListaComprasDTO } from '../../types';
import { listaComprasService } from '../../services/listaComprasService';
import ListaComprasForm from './ListaComprasForm';
import ListaComprasItem from './ListaComprasItem';
import Loading from '../common/Loading';
import ErrorMessage from '../common/ErrorMessage';

const ListaComprasList: React.FC = () => {
  const [listas, setListas] = useState<ListaComprasDTO[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string>('');
  const [openForm, setOpenForm] = useState(false);
  const [editingLista, setEditingLista] = useState<ListaComprasDTO | null>(null);
  const [expandedLista, setExpandedLista] = useState<number | null>(null);

  useEffect(() => {
    loadListas();
  }, []);

  const loadListas = async () => {
    try {
      setLoading(true);
      const data = await listaComprasService.getAll();
      setListas(data);
    } catch (err) {
      setError('Erro ao carregar listas de compras');
    } finally {
      setLoading(false);
    }
  };

  const handleCreate = () => {
    setEditingLista(null);
    setOpenForm(true);
  };

  const handleEdit = (lista: ListaComprasDTO) => {
    setEditingLista(lista);
    setOpenForm(true);
  };

  const handleDelete = async (id: number) => {
    if (window.confirm('Tem certeza que deseja excluir esta lista?')) {
      try {
        await listaComprasService.delete(id);
        await loadListas();
      } catch (err) {
        setError('Erro ao excluir lista de compras');
      }
    }
  };

  const handleFormClose = () => {
    setOpenForm(false);
    setEditingLista(null);
  };

  const handleFormSubmit = async () => {
    await loadListas();
    handleFormClose();
  };

  const toggleExpand = (id: number) => {
    setExpandedLista(expandedLista === id ? null : id);
  };

  if (loading) return <Loading />;

  return (
    <Box p={3}>
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={3}>
        <Typography variant="h4" component="h1">
          Listas de Compras
        </Typography>
        <Button
          variant="contained"
          startIcon={<Add />}
          onClick={handleCreate}
        >
          Nova Lista
        </Button>
      </Box>

      {error && (
        <ErrorMessage message={error} onClose={() => setError('')} />
      )}

    <Box sx={{ display: 'flex', flexDirection: 'column', gap: 3 }}>
  {listas.map((lista) => (
    <Box key={lista.id}>
      <Card>
              <CardContent>
                <Box display="flex" justifyContent="space-between" alignItems="center">
                  <Box flex={1}>
                    <Typography variant="h6" component="h2">
                      {lista.nome}
                    </Typography>
                    <Typography variant="body2" color="textSecondary">
                      Criada em: {new Date(lista.criadaEm).toLocaleDateString()}
                    </Typography>
                    <Typography variant="body2" color="textSecondary">
                      Itens: {lista.itensDesejados?.length || 0}
                    </Typography>
                  </Box>
                  <Box>
                    <IconButton onClick={() => toggleExpand(lista.id)}>
                      {expandedLista === lista.id ? <ExpandLess /> : <ExpandMore />}
                    </IconButton>
                    <IconButton onClick={() => handleEdit(lista)} color="primary">
                      <Edit />
                    </IconButton>
                    <IconButton onClick={() => handleDelete(lista.id)} color="error">
                      <Delete />
                    </IconButton>
                  </Box>
                </Box>

                {expandedLista === lista.id && (
                  <Box mt={2}>
                    <ListaComprasItem
                      lista={lista}
                      onListaUpdated={loadListas}
                    />
                  </Box>
                )}
              </CardContent>
            </Card>
    </Box>
  ))}
</Box>
      <Dialog
        open={openForm}
        onClose={handleFormClose}
        maxWidth="sm"
        fullWidth
      >
        <DialogTitle>
          {editingLista ? 'Editar Lista' : 'Nova Lista'}
        </DialogTitle>
        <DialogContent>
          <ListaComprasForm
            lista={editingLista}
            onSubmit={handleFormSubmit}
            onCancel={handleFormClose}
          />
        </DialogContent>
      </Dialog>
    </Box>
  );
};

export default ListaComprasList;