import React from 'react';
import { ItemDesejadoDTO } from '../../types';
import OpcoesCompraList from '../OpcoesCompra/OpcoesCompraList';

interface ItemDesejadoItemProps {
  item: ItemDesejadoDTO;
  onItemUpdated: () => void;
}

const ItemDesejadoItem: React.FC<ItemDesejadoItemProps> = ({ item, onItemUpdated }) => {
  return (
    <OpcoesCompraList
      itemDesejadoId={item.id}
      onOpcoesUpdated={onItemUpdated}
    />
  );
};

export default ItemDesejadoItem;