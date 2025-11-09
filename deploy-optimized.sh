#!/bin/bash
set -e

echo "🚀 Deploy otimizado para Raspberry Pi..."

# Limpeza de recursos não utilizados
echo "🧹 Limpando recursos Docker..."
docker system prune -f
docker builder prune -f -a

# Parar serviços (exceto monitoring para manter dados)
echo "⏹️ Parando serviços..."
docker compose down backend frontend nginx

# Build paralelo otimizado
echo "🔨 Build otimizado dos serviços..."
docker compose build \
  --parallel \
  --memory=1g \
  --build-arg NODE_ENV=production \
  backend frontend

# Iniciar serviços com dependências
echo "🔄 Iniciando serviços..."
docker compose up -d backend frontend
sleep 30  # Aguarda backend inicializar

docker compose up -d nginx

echo "✅ Deploy concluído!"
echo "📊 Serviços disponíveis:"
echo "   - Aplicação: http://localhost:80"
echo "   - Grafana: http://localhost:3001"
echo "   - Prometheus: http://localhost:9090"