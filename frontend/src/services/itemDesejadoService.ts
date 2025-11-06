import api from './api';
import { ItemDesejadoDTO, CreateItemDesejadoDTO, UpdateItemDesejadoDTO } from '../types';

export const itemDesejadoService = {
  getAll: async (): Promise<ItemDesejadoDTO[]> => {
    const response = await api.get<ItemDesejadoDTO[]>('/ItensDesejados');
    return response.data;
  },

  getById: async (id: number): Promise<ItemDesejadoDTO> => {
    const response = await api.get<ItemDesejadoDTO>(`/ItensDesejados/${id}`);
    return response.data;
  },

  create: async (data: CreateItemDesejadoDTO): Promise<ItemDesejadoDTO> => {
    const response = await api.post<ItemDesejadoDTO>('/ItensDesejados', data);
    return response.data;
  },

  update: async (id: number, data: UpdateItemDesejadoDTO): Promise<void> => {
    await api.put(`/ItensDesejados/${id}`, data);
  },

  delete: async (id: number): Promise<void> => {
    await api.delete(`/ItensDesejados/${id}`);
  },
};