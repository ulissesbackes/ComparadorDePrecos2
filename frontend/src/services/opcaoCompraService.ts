import api from './api';
import { OpcaoCompraDTO, CreateOpcaoCompraDTO, UpdateOpcaoCompraDTO } from '../types';

export const opcaoCompraService = {
  getAll: async (): Promise<OpcaoCompraDTO[]> => {
    const response = await api.get<OpcaoCompraDTO[]>('/OpcoesCompra');
    return response.data;
  },

  getById: async (id: number): Promise<OpcaoCompraDTO> => {
    const response = await api.get<OpcaoCompraDTO>(`/OpcoesCompra/${id}`);
    return response.data;
  },

  getByItemDesejadoId: async (itemDesejadoId: number): Promise<OpcaoCompraDTO[]> => {
    const response = await api.get<OpcaoCompraDTO[]>(`/OpcoesCompra/por-item/${itemDesejadoId}`);
    return response.data;
  },

  create: async (data: CreateOpcaoCompraDTO): Promise<OpcaoCompraDTO> => {
    const response = await api.post<OpcaoCompraDTO>('/OpcoesCompra', data);
    return response.data;
  },

  update: async (id: number, data: UpdateOpcaoCompraDTO): Promise<void> => {
    await api.put(`/OpcoesCompra/${id}`, data);
  },

  delete: async (id: number): Promise<void> => {
    await api.delete(`/OpcoesCompra/${id}`);
  },
};