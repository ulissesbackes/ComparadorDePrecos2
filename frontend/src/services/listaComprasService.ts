import api from './api';
import { ListaComprasDTO, CreateListaComprasDTO, UpdateListaComprasDTO } from '../types';

export const listaComprasService = {
  getAll: async (): Promise<ListaComprasDTO[]> => {
    const response = await api.get<ListaComprasDTO[]>('/ListasCompras');
    return response.data;
  },

  getById: async (id: number): Promise<ListaComprasDTO> => {
    const response = await api.get<ListaComprasDTO>(`/ListasCompras/${id}`);
    return response.data;
  },

  create: async (data: CreateListaComprasDTO): Promise<ListaComprasDTO> => {
    const response = await api.post<ListaComprasDTO>('/ListasCompras', data);
    return response.data;
  },

  update: async (id: number, data: UpdateListaComprasDTO): Promise<void> => {
    await api.put(`/ListasCompras/${id}`, data);
  },

  delete: async (id: number): Promise<void> => {
    await api.delete(`/ListasCompras/${id}`);
  },
};