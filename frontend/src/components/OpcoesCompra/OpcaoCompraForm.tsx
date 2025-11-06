import React, { useState, useEffect } from 'react';
import {
  Box,
  TextField,
  Button,
  Stack,
  MenuItem,
  FormControl,
  InputLabel,
  Select,
  Typography,
} from '@mui/material';
import { OpcaoCompraDTO, CreateOpcaoCompraDTO, UpdateOpcaoCompraDTO, ProdutoDTO } from '../../types';
import { opcaoCompraService } from '../../services/opcaoCompraService';

interface OpcaoCompraFormProps {
  opcao?: OpcaoCompraDTO | null;
  itemDesejadoId: number;
  produtos: ProdutoDTO[];
  onSubmit: () => void;
  onCancel: () => void;
}

const OpcaoCompraForm: React.FC<OpcaoCompraFormProps> = ({
  opcao,
  itemDesejadoId,
  produtos,
  onSubmit,
  onCancel,
}) => {
  const [produtoId, setProdutoId] = useState<number>(0);
  const [descricao, setDescricao] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  useEffect(() => {
    if (opcao) {
      setProdutoId(opcao.produtoId);
      setDescricao(opcao.descricao || '');
    }
  }, [opcao]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError('');

    try {
      if (opcao) {
        const updateData: UpdateOpcaoCompraDTO = { descricao };
        await opcaoCompraService.update(opcao.id, updateData);
      } else {
        const createData: CreateOpcaoCompraDTO = {
          itemDesejadoId,
          produtoId,
          descricao,
        };
        await opcaoCompraService.create(createData);
      }
      onSubmit();
    } catch (err) {
      setError('Erro ao salvar opção de compra');
    } finally {
      setLoading(false);
    }
  };

  const selectedProduto = produtos.find(p => p.id === produtoId);

  const formatCurrency = (value: number) => {
    return new Intl.NumberFormat('pt-BR', {
      style: 'currency',
      currency: 'BRL',
    }).format(value);
  };

  return (
    <Box component="form" onSubmit={handleSubmit} mt={2}>
      <FormControl fullWidth margin="normal" required>
        <InputLabel>Produto</InputLabel>
        <Select
          value={produtoId}
          onChange={(e) => setProdutoId(Number(e.target.value))}
          label="Produto"
          disabled={!!opcao} // Não permite alterar o produto na edição
        >
          {produtos.map((produto) => (
            <MenuItem key={produto.id} value={produto.id}>
              {produto.nome} - {produto.marca} - {formatCurrency(produto.precoAtual)}
            </MenuItem>
          ))}
        </Select>
      </FormControl>

      {selectedProduto && (
        <Box mb={2} p={2} bgcolor="grey.100" borderRadius={1}>
          <Typography variant="subtitle2">Detalhes do Produto:</Typography>
          <Typography variant="body2">
            <strong>Marca:</strong> {selectedProduto.marca}
          </Typography>
          <Typography variant="body2">
            <strong>Preço:</strong> {formatCurrency(selectedProduto.precoAtual)}
          </Typography>
          <Typography variant="body2">
            <strong>Mercado:</strong> {selectedProduto.mercado}
          </Typography>
          {selectedProduto.url && (
            <Typography variant="body2">
              <strong>URL:</strong>{' '}
              <a href={selectedProduto.url} target="_blank" rel="noopener noreferrer">
                Ver produto
              </a>
            </Typography>
          )}
        </Box>
      )}

      <TextField
        fullWidth
        label="Descrição (opcional)"
        value={descricao}
        onChange={(e) => setDescricao(e.target.value)}
        margin="normal"
        multiline
        rows={3}
      />

      {error && (
        <Box color="error.main" mt={1}>
          {error}
        </Box>
      )}

      <Stack direction="row" spacing={2} mt={3}>
        <Button
          type="submit"
          variant="contained"
          disabled={loading || (!opcao && produtoId === 0)}
        >
          {loading ? 'Salvando...' : 'Salvar'}
        </Button>
        <Button onClick={onCancel} disabled={loading}>
          Cancelar
        </Button>
      </Stack>
    </Box>
  );
};

export default OpcaoCompraForm;