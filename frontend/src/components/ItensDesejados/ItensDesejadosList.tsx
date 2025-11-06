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
import { ItemDesejadoDTO } from '../../types';
import { itemDesejadoService } from '../../services/itemDesejadoService';
import ItemDesejadoForm from './ItemDesejadoForm';
import ItemDesejadoItem from './ItemDesejadoItem';
import Loading from '../common/Loading';
import ErrorMessage from '../common/ErrorMessage';

interface ItensDesejadosListProps {
  listaComprasId: number;
  onItensUpdated: () => void;
}

const ItensDesejadosList: React.FC<ItensDesejadosListProps> = ({
  listaComprasId,
  onItensUpdated,
}) => {
  const [itens, setItens] = useState<ItemDesejadoDTO[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string>('');
  const [openForm, setOpenForm] = useState(false);
  const [editingItem, setEditingItem] = useState<ItemDesejadoDTO | null>(null);
  const [expandedItem, setExpandedItem] = useState<number | null>(null);

  useEffect(() => {
    loadItens();
  }, [listaComprasId]);

  const loadItens = async () => {
    try {
      setLoading(true);
      const data = await itemDesejadoService.getAll();
      // Filtrar itens pela lista de compras
      const itensFiltrados = data.filter(item => item.listaComprasId === listaComprasId);
      setItens(itensFiltrados);
    } catch (err) {
      setError('Erro ao carregar itens desejados');
    } finally {
      setLoading(false);
    }
  };

  const handleCreate = () => {
    setEditingItem(null);
    setOpenForm(true);
  };

  const handleEdit = (item: ItemDesejadoDTO) => {
    setEditingItem(item);
    setOpenForm(true);
  };

  const handleDelete = async (id: number) => {
    if (window.confirm('Tem certeza que deseja excluir este item?')) {
      try {
        await itemDesejadoService.delete(id);
        await loadItens();
        onItensUpdated();
      } catch (err) {
        setError('Erro ao excluir item desejado');
      }
    }
  };

  const handleFormClose = () => {
    setOpenForm(false);
    setEditingItem(null);
  };

  const handleFormSubmit = async () => {
    await loadItens();
    onItensUpdated();
    handleFormClose();
  };

  const toggleExpand = (id: number) => {
    setExpandedItem(expandedItem === id ? null : id);
  };

  if (loading) return <Loading />;

  return (
    <Box>
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={2}>
        <Typography variant="h6">
          Itens Desejados
        </Typography>
        <Button
          variant="outlined"
          startIcon={<Add />}
          onClick={handleCreate}
          size="small"
        >
          Adicionar Item
        </Button>
      </Box>

      {error && (
        <ErrorMessage message={error} onClose={() => setError('')} />
      )}

      <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
  {itens.map((item) => (
    <Box key={item.id}>
      <Card variant="outlined">
              <CardContent>
                <Box display="flex" justifyContent="space-between" alignItems="flex-start">
                  <Box flex={1}>
                    <Typography variant="h6" component="h3">
                      {item.nome}
                    </Typography>
                    <Typography variant="body2" color="textSecondary" paragraph>
                      {item.descricao}
                    </Typography>
                    <Typography variant="caption" color="textSecondary">
                      Criado em: {new Date(item.criadoEm).toLocaleDateString()}
                    </Typography>
                  </Box>
                  <Box>
                    <IconButton onClick={() => toggleExpand(item.id)} size="small">
                      {expandedItem === item.id ? <ExpandLess /> : <ExpandMore />}
                    </IconButton>
                    <IconButton onClick={() => handleEdit(item)} color="primary" size="small">
                      <Edit />
                    </IconButton>
                    <IconButton onClick={() => handleDelete(item.id)} color="error" size="small">
                      <Delete />
                    </IconButton>
                  </Box>
                </Box>

                {expandedItem === item.id && (
                  <Box mt={2}>
                    <ItemDesejadoItem
                      item={item}
                      onItemUpdated={loadItens}
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
          {editingItem ? 'Editar Item' : 'Novo Item'}
        </DialogTitle>
        <DialogContent>
          <ItemDesejadoForm
            item={editingItem}
            listaComprasId={listaComprasId}
            onSubmit={handleFormSubmit}
            onCancel={handleFormClose}
          />
        </DialogContent>
      </Dialog>
    </Box>
  );
};

export default ItensDesejadosList;