import React, { useState, useEffect } from 'react';
import {
  Box,
  TextField,
  Button,
  Stack,
} from '@mui/material';
import { ItemDesejadoDTO, CreateItemDesejadoDTO, UpdateItemDesejadoDTO } from '../../types';
import { itemDesejadoService } from '../../services/itemDesejadoService';

interface ItemDesejadoFormProps {
  item?: ItemDesejadoDTO | null;
  listaComprasId: number;
  onSubmit: () => void;
  onCancel: () => void;
}

const ItemDesejadoForm: React.FC<ItemDesejadoFormProps> = ({
  item,
  listaComprasId,
  onSubmit,
  onCancel,
}) => {
  const [nome, setNome] = useState('');
  const [descricao, setDescricao] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  useEffect(() => {
    if (item) {
      setNome(item.nome || '');
      setDescricao(item.descricao || '');
    }
  }, [item]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError('');

    try {
      // TODO: Obter o ID do usuário logado
      const usuarioId = 18; // Temporário

      if (item) {
        const updateData: UpdateItemDesejadoDTO = { nome, descricao };
        await itemDesejadoService.update(item.id, updateData);
      } else {
        const createData: CreateItemDesejadoDTO = {
          nome,
          descricao,
          usuarioId: 18,
          listaComprasId,
        };
        await itemDesejadoService.create(createData);
      }
      onSubmit();
    } catch (err) {
      setError('Erro ao salvar item desejado');
    } finally {
      setLoading(false);
    }
  };

  return (
    <Box component="form" onSubmit={handleSubmit} mt={2}>
      <TextField
        fullWidth
        label="Nome do Item"
        value={nome}
        onChange={(e) => setNome(e.target.value)}
        margin="normal"
        required
      />
      <TextField
        fullWidth
        label="Descrição"
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
          disabled={loading || !nome.trim()}
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

export default ItemDesejadoForm;