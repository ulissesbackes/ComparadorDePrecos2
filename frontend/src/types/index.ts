export interface ListaComprasDTO {
  id: number;
  nome: string;
  usuarioId: number;
  criadaEm: string;
  itensDesejados?: ItemDesejadoDTO[];
}

export interface CreateListaComprasDTO {
  nome: string;
  usuarioId: number;
}

export interface UpdateListaComprasDTO {
  nome: string;
}

export interface ItemDesejadoDTO {
  id: number;
  nome: string;
  descricao: string;
  usuarioId: number;
  listaComprasId: number;
  criadoEm: string;
  opcoesCompra?: OpcaoCompraDTO[];
}

export interface CreateItemDesejadoDTO {
  nome: string;
  descricao: string;
  usuarioId: number;
  listaComprasId: number;
}

export interface UpdateItemDesejadoDTO {
  nome: string;
  descricao: string;
}

export interface OpcaoCompraDTO {
  id: number;
  itemDesejadoId: number;
  produtoId: number;
  descricao: string;
  criadoEm: string;
  produto: ProdutoDTO;
}

export interface CreateOpcaoCompraDTO {
  itemDesejadoId: number;
  produtoId: number;
  descricao: string;
}

export interface UpdateOpcaoCompraDTO {
  descricao: string;
}

export interface ProdutoDTO {
  id: number;
  nome: string;
  marca: string;
  precoAtual: number;
  mercado: string;
  url: string;
  urlImagem: string;
  criadoEm: string;
  atualizadoEm: string;
}

export interface CreateProdutoDTO {
  nome: string;
  marca: string;
  precoAtual: number;
  mercado: string;
  url: string;
  urlImagem: string;
}

export interface UpdateProdutoDTO {
  nome: string;
  marca: string;
  precoAtual: number;
  mercado: string;
  url: string;
  urlImagem: string;
}