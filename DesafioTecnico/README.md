# Desafio Técnico C#

Aplicação console em .NET 8 com os três exercícios, acessados por um menu.

## Como executar

    dotnet run

## Organização

Cada exercício fica em sua pasta (`Comissao`, `Estoque`, `Juros`), com as classes separadas por responsabilidade:
entidades (dados e regras próprias), regras/serviços (lógica de negócio), repositórios (leitura do JSON) e telas (console).

## Como os princípios SOLID aparecem

- **S – Responsabilidade única:** ler JSON, calcular e exibir no console ficam em classes diferentes.
- **O – Aberto/fechado:** uma nova regra de comissão, um novo tipo de movimentação ou uma nova tela entram como classes novas, sem alterar as existentes.
- **L – Substituição de Liskov:** `MovimentacaoEntrada` e `MovimentacaoSaida` podem ser usadas onde se espera uma `Movimentacao`.
- **I – Segregação de interfaces:** interfaces pequenas e específicas (`IRegraComissao`, `IVendaRepository`, `ICalculadoraJuros`...).
- **D – Inversão de dependência:** as classes dependem de interfaces, recebidas pelo construtor. A montagem acontece no `Program.cs`.

## Premissas

1. **Comissão:** regra aplicada venda a venda e somada por vendedor. R$ 100,00 exatos geram 1%; R$ 500,00 exatos geram 5%.
2. **Estoque:** ID sequencial único por movimentação; saída maior que o saldo é bloqueada; dados mantidos em memória durante a execução.
3. **Juros:** juros simples de 2,5% ao dia sobre o valor original; título não vencido não tem juros.
