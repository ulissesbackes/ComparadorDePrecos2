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
  Chip,
} from '@mui/material';
import { Add, Edit, Delete } from '@mui/icons-material';
import { OpcaoCompraDTO, ProdutoDTO } from '../../types';
import { opcaoCompraService } from '../../services/opcaoCompraService';
import { produtoService } from '../../services/produtoService';
import OpcaoCompraForm from './OpcaoCompraForm';
import Loading from '../common/Loading';
import ErrorMessage from '../common/ErrorMessage';

interface OpcoesCompraListProps {
  itemDesejadoId: number;
  onOpcoesUpdated: () => void;
}

const OpcoesCompraList: React.FC<OpcoesCompraListProps> = ({
  itemDesejadoId,
  onOpcoesUpdated,
}) => {
  const [opcoes, setOpcoes] = useState<OpcaoCompraDTO[]>([]);
  const [produtos, setProdutos] = useState<ProdutoDTO[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string>('');
  const [openForm, setOpenForm] = useState(false);
  const [editingOpcao, setEditingOpcao] = useState<OpcaoCompraDTO | null>(null);

  useEffect(() => {
    loadOpcoes();
    loadProdutos();
  }, [itemDesejadoId]);

  const loadOpcoes = async () => {
    try {
      const data = await opcaoCompraService.getByItemDesejadoId(itemDesejadoId);
      setOpcoes(data);
    } catch (err) {
      setError('Erro ao carregar opções de compra');
    } finally {
      setLoading(false);
    }
  };

  const loadProdutos = async () => {
    try {
      const data = await produtoService.getAll();
      setProdutos(data);
    } catch (err) {
      console.error('Erro ao carregar produtos:', err);
    }
  };

  const handleCreate = () => {
    setEditingOpcao(null);
    setOpenForm(true);
  };

  const handleEdit = (opcao: OpcaoCompraDTO) => {
    setEditingOpcao(opcao);
    setOpenForm(true);
  };

  const handleDelete = async (id: number) => {
    if (window.confirm('Tem certeza que deseja excluir esta opção?')) {
      try {
        await opcaoCompraService.delete(id);
        await loadOpcoes();
        onOpcoesUpdated();
      } catch (err) {
        setError('Erro ao excluir opção de compra');
      }
    }
  };

  const handleFormClose = () => {
    setOpenForm(false);
    setEditingOpcao(null);
  };

  const handleFormSubmit = async () => {
    await loadOpcoes();
    onOpcoesUpdated();
    handleFormClose();
  };

  const formatCurrency = (value: number) => {
    return new Intl.NumberFormat('pt-BR', {
      style: 'currency',
      currency: 'BRL',
    }).format(value);
  };

  if (loading) return <Loading />;

  return (
    <Box>
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={2}>
        <Typography variant="h6">
          Opções de Compra
        </Typography>
        <Button
          variant="outlined"
          startIcon={<Add />}
          onClick={handleCreate}
          size="small"
        >
          Adicionar Opção
        </Button>
      </Box>

      {error && (
        <ErrorMessage message={error} onClose={() => setError('')} />
      )}

    <Box sx={{ 
  display: 'grid', 
  gridTemplateColumns: { xs: '1fr', md: '1fr 1fr' },
  gap: 2 
}}>
  {opcoes.map((opcao) => (
    <Box key={opcao.id}>
      <Card variant="outlined">
              <CardContent>
                <Box display="flex" justifyContent="space-between" alignItems="flex-start">
                  <Box flex={1}>
                    <Typography variant="subtitle1" fontWeight="bold">
                      {opcao.produto.nome}
                    </Typography>
                    <Typography variant="body2" color="textSecondary">
                      {opcao.produto.marca}
                    </Typography>
                    <Typography variant="h6" color="primary" my={1}>
                      {formatCurrency(opcao.produto.precoAtual)}
                    </Typography>
                    <Box mb={1}>
                      <Chip
                        label={opcao.produto.mercado}
                        size="small"
                        variant="outlined"
                      />
                    </Box>
                    {opcao.descricao && (
                      <Typography variant="body2" paragraph>
                        {opcao.descricao}
                      </Typography>
                    )}
                    <Typography variant="caption" color="textSecondary">
                      Criado em: {new Date(opcao.criadoEm).toLocaleDateString()}
                    </Typography>
                  </Box>
                  <Box>
                    <IconButton onClick={() => handleEdit(opcao)} color="primary" size="small">
                      <Edit />
                    </IconButton>
                    <IconButton onClick={() => handleDelete(opcao.id)} color="error" size="small">
                      <Delete />
                    </IconButton>
                  </Box>
                </Box>
              </CardContent>
            </Card>
    </Box>
  ))}
</Box>

      <Dialog
        open={openForm}
        onClose={handleFormClose}
        maxWidth="md"
        fullWidth
      >
        <DialogTitle>
          {editingOpcao ? 'Editar Opção de Compra' : 'Nova Opção de Compra'}
        </DialogTitle>
        <DialogContent>
          <OpcaoCompraForm
            opcao={editingOpcao}
            itemDesejadoId={itemDesejadoId}
            produtos={produtos}
            onSubmit={handleFormSubmit}
            onCancel={handleFormClose}
          />
        </DialogContent>
      </Dialog>
    </Box>
  );
};

export default OpcoesCompraList;