import api from './api';
import { ProdutoDTO } from '../types';

export const produtoService = {
  getAll: async (): Promise<ProdutoDTO[]> => {
    const response = await api.get<ProdutoDTO[]>('/Produtos');
    return response.data;
  },

  getById: async (id: number): Promise<ProdutoDTO> => {
    const response = await api.get<ProdutoDTO>(`/Produtos/${id}`);
    return response.data;
  },
};