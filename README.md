# Sistema de Gerenciamento de Pedidos de Restaurante

Este é um sistema de gerenciamento de pedidos de um restaurante construído utilizando o framework .NET e a linguagem C#. Ele utiliza o framework web ASP.NET Razor Pages e o Entity Framework Core como ORM para manipulação de dados.

---

## Requisitos de Software

Antes de executar o sistema, você precisará ter o seguinte software instalado:

- .NET SDK 6 ou superior (disponível em https://dotnet.microsoft.com/download)

## Como Executar o Projeto

Clone este repositório em sua máquina local.

```bash
git clone https://github.com/marlonangeli/sistema-pedidos-restaurante.git
```

Caso você queira utilizar o banco de dados do próprio projeto, entre no diretório `Restaurante.Cheng/Restaurante.Cheng.Web` e renomeie o arquivo `copy_restaurante.db` para `restaurante.db`. Caso contrário, você pode gerar um novo banco de dados utilizando o Entity Framework Core com os passos abaixo.

Navegue até o diretório raiz do projeto.

```bash
cd sistema-pedidos-restaurante/Restaurante.Cheng/Restaurante.Cheng.Data
```

Abra o prompt de comando ou o terminal na pasta raiz do projeto e execute o comando abaixo para criar o banco de dados.

```bash
dotnet ef database update -s ../Restaurante.Cheng.Web
```

Retorne para a pasta raiz do projeto e execute o comando abaixo para executar o projeto.


```bash
dotnet run --project Restaurante.Cheng.Web
```

Abra o navegador e acesse o endereço `https://localhost:5001` para utilizar o sistema.
