import React from 'react';
import { ListaComprasDTO } from '../../types';
import ItensDesejadosList from '../ItensDesejados/ItensDesejadosList';

interface ListaComprasItemProps {
  lista: ListaComprasDTO;
  onListaUpdated: () => void;
}

const ListaComprasItem: React.FC<ListaComprasItemProps> = ({ lista, onListaUpdated }) => {
  return (
    <ItensDesejadosList
      listaComprasId={lista.id}
      onItensUpdated={onListaUpdated}
    />
  );
};

export default ListaComprasItem;