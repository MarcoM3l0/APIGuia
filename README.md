# APIGuia

Este projeto é uma API desenvolvida em .NET para gerenciar reservas, autenticação de usuários e faturamento. Ele utiliza:

- **Entity Framework Core** para acesso ao banco de dados.
- **JWT** para autenticação.
- **Cache em memória** para otimização de consultas.

## 📌 Pré-requisitos
Antes de rodar o projeto, certifique-se de ter instalado:

- [**.NET SDK**](https://dotnet.microsoft.com/en-us/download) (versão 8.0 ou superior)
- [**Visual Studio**](https://visualstudio.microsoft.com/) ou [**Visual Studio Code**](https://code.visualstudio.com/)
- [**MySql**](https://dev.mysql.com/downloads/installer/) ou outro banco de dados compatível com Entity Framework Core
- [**Git**](https://git-scm.com/) (opcional, para clonar o repositório)

## ⚙️ Configuração do Projeto

### Clone o repositório (se aplicável):

```bash
git clone https://github.com/seu-usuario/APIGuia.git
cd APIGuia
```

### Configuração do banco de dados:

Abra o arquivo `appsettings.json` e configure a string de conexão do banco de dados:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=APIGuiaDB;Uid=SEU_USUARIO;Pwd=SUA_SENHA;"
}
```

Substitua `SEU_SERVIDOR`, `SEU_USUARIO` e `SUA_SENHA` pelas credenciais do seu banco de dados.

### Aplicar Migrations:

No terminal, navegue até a pasta do projeto e execute os seguintes comandos para criar e aplicar as migrações:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Isso criará o banco de dados e as tabelas necessárias.

Caso o Entity Framework Core Tools não esteja instalado:
- Se você ainda não tem as ferramentas do Entity Framework Core instaladas globalmente, execute o seguinte comando:
  
```bash
dotnet tool install --global dotnet-ef
```
Isso instalará as ferramentas necessárias para rodar os comandos dotnet ef.

### Configuração do JWT:

A chave secreta para geração de tokens JWT está definida na classe `Settings`:

```csharp
public static string secret = "aB1@dE2#fG3$hI4%jK5&lM6*nO7(pQ8)rS9+tU0-vWzYx1#W2!e3@R4$t5^u6&I7*o8(p9)Q0rA1-B2+c3D4=E5F6*G7H8";
```

Certifique-se de que essa chave seja mantida em segredo e não seja compartilhada publicamente.

## 🚀 Executando o Projeto

### Restaurar pacotes NuGet:

No terminal, execute o seguinte comando para restaurar os pacotes NuGet:

```bash
dotnet restore
```

### Rodar a aplicação via terminal:

Execute o projeto com o seguinte comando:

```bash
dotnet run
```

A API estará disponível em `http://localhost:5143` ou de acordo com a sua configuração de porta.

### Rodar a aplicação via Visual Studio:

1. Abra o arquivo `APIGuia.sln` no **Visual Studio**.
2. Aguarde o carregamento do projeto.
3. Clique no botão de **Executar (▶️ Start)** ou pressione `F5`.

O Visual Studio iniciará a aplicação e abrirá a API no navegador padrão.

## 📌 Estrutura do Projeto

- **Controllers**: Contém os controladores da API.
  - `CadastroController`: Gerencia o cadastro de usuários.
  - `FaturamentoController`: Gerencia consultas de faturamento.
  - `LoginController`: Gerencia a autenticação de usuários.
  - `ReservasController`: Gerencia as reservas.
- **DTOs**: Contém os objetos de transferência de dados (DTOs).
- **Models**: Contém as entidades do banco de dados.
- **Services**: Contém serviços auxiliares, como a geração de tokens JWT.
- **Migrations**: Contém as migrações do Entity Framework Core.

## 📌 Endpoints da API

Aqui estão os principais endpoints disponíveis:

### 🔑 Autenticação
**POST `/Login`**: Autentica um usuário e retorna um token JWT.

Exemplo de corpo da requisição:

```json
{
  "email": "usuario@exemplo.com",
  "password": "senha123"
}
```
Exemplo de resposta:

```json
{
  "user": {
    "userId": 8,
    "nome": "José Marco",
    "email": "jose.marco@teste.com",
    "password": "",
    "tipoFuncionario": "webmaster"
  },
  "token": "..."
}
```

Explicação:
- `user`: Contém os dados do usuário autenticado, com exceção da senha (password é deixado vazio para garantir a segurança).
- `token`: O token JWT gerado após a autenticação bem-sucedida. Esse token pode ser usado para autorizar o usuário a fazer requisições em endpoints protegidos.

### 👤 Cadastro de Usuários
**POST `/Cadastro`**: Cria um novo usuário (acesso restrito a usuários com a role "webmaster").
  - Somente usuários com a role "webmaster" podem criar novos usuários.
  - Usuários com a role "suporte" podem fazer todas as ações, exceto criar novos usuários.
  - Qualquer outro papel (role) não terá permissão para realizar nenhuma requisição nos endpoints.

Exemplo de corpo da requisição:

```json
{
  "nome": "Novo Usuário",
  "email": "novo@exemplo.com",
  "password": "senha123",
  "tipoFuncionario": "webmaster ou suporte"
}
```

Exemplo de resposta (com mensagem de sucesso):

```json
{
  "message": "Usuário cadastrado com sucesso!"
}
```

### 💰 Faturamento
**GET `/faturamento`**: Retorna o faturamento mensal com base no ano e mês fornecidos.

Parâmetros:
- `ano`: Ano do faturamento.
- `mes`: Mês do faturamento.

Exemplo de requisição:

```bash
GET /Faturamento?ano=2024&mes=05
```
Exemplo de resposta:

```json
{
  "ano": 2024,
  "mes": 5,
  "totalReservas": 5,
  "totalFaturado": 1350.00
}
```

### 📅 Reservas
**GET `/Reservas`**: Retorna uma lista paginada de reservas filtradas por intervalo de datas.

Parâmetros:
- `dataInicio`: Data de início do filtro.
- `dataFim`: Data de fim do filtro.
- `page`: Número da página (opcional, padrão: 1).
- `pageSize`: Tamanho da página (opcional, padrão: 10).

Exemplo de requisição:

```bash
GET /Reservas?dataInicio=2024-05-01&dataFim=2024-06-01&page=1&pageSize=10
```

```bash
GET /Reservas?dataInicio=2024-05-01&dataFim=2024-06-01
```

Exemplo de resposta:

```json
{
  "dados": [
    {
      "reservaId": 1,
      "dataEntrada": "2024-05-10T14:00:00",
      "dataSaida": "2024-05-11T12:00:00",
      "clienteNome": "Carlos Souza",
      "suiteTipo": "Ouro",
      "valor": 200.00
    }
  ],
  "paginaAtual": 1,
  "totalPaginas": 1,
  "totalRegistros": 5
}
```
