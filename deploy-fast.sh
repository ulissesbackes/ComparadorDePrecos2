#!/bin/bash
set -e

echo "🚀 Deploy rápido iniciado..."

DEPLOY_DIR="/home/ulisses/apps/comparador-precos"
cd $DEPLOY_DIR

# Atualiza código
echo "📦 Atualizando código..."
git pull origin main

# Para apenas os serviços da aplicação (mantém monitoring)
echo "🐳 Parando serviços da aplicação..."
docker compose stop backend frontend nginx

# Build com cache e paralelo
echo "🔨 Build otimizado..."
docker compose build --parallel backend frontend

# Inicia serviços
echo "🔄 Iniciando serviços..."
docker compose up -d backend frontend nginx

# Health check
echo "⏳ Aguardando inicialização..."
sleep 15

echo "📊 Status dos containers:"
docker ps --format "table {{.Names}}\t{{.Status}}"

echo "✅ Deploy rápido concluído!"