import React, { useState, useEffect } from 'react';
import {
  Box,
  TextField,
  Button,
  Stack,
} from '@mui/material';
import { ListaComprasDTO, CreateListaComprasDTO, UpdateListaComprasDTO } from '../../types';
import { listaComprasService } from '../../services/listaComprasService';

interface ListaComprasFormProps {
  lista?: ListaComprasDTO | null;
  onSubmit: () => void;
  onCancel: () => void;
}

const ListaComprasForm: React.FC<ListaComprasFormProps> = ({
  lista,
  onSubmit,
  onCancel,
}) => {
  const [nome, setNome] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  useEffect(() => {
    if (lista) {
      setNome(lista.nome || '');
    }
  }, [lista]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError('');

    try {
      // TODO: Obter o ID do usuário logado
      const usuarioId = 18; // Temporário

      if (lista) {
        const updateData: UpdateListaComprasDTO = { nome };
        await listaComprasService.update(lista.id, updateData);
      } else {
        const createData: CreateListaComprasDTO = { nome, usuarioId: 18 };
        await listaComprasService.create(createData);
      }
      onSubmit();
    } catch (err) {
      setError('Erro ao salvar lista de compras');
    } finally {
      setLoading(false);
    }
  };

  return (
    <Box component="form" onSubmit={handleSubmit} mt={2}>
      <TextField
        fullWidth
        label="Nome da Lista"
        value={nome}
        onChange={(e) => setNome(e.target.value)}
        margin="normal"
        required
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

export default ListaComprasForm;