# APIGuia

Este projeto é uma API desenvolvida em .NET para gerenciar reservas, autenticação de usuários e faturamento. Ele utiliza:

- **Entity Framework Core** para acesso ao banco de dados.
- **JWT** para autenticação.
- **Cache em memória** para otimização de consultas.

## 📌 Pré-requisitos
Antes de rodar o projeto, certifique-se de ter instalado:

- [**.NET SDK**](https://dotnet.microsoft.com/en-us/download) (versão 8.0 ou superior)
- [**Visual Studio**](https://visualstudio.microsoft.com/) ou [**Visual Studio Code**](https://code.visualstudio.com/)
- **MySql** ou outro banco de dados compatível com Entity Framework Core
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
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=APIGuiaDB;User Id=SEU_USUARIO;Password=SUA_SENHA;"
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

### Configuração do JWT:

A chave secreta para geração de tokens JWT está definida na classe `Settings`:

```csharp
public static string secret = "aB1@dE2#fG3$hI4%jK5&lM6*nO7(pQ8)rS9+tU0-vWzYx1#W2!e3@R4$t5^u6&I7*o8(p9)Q0rA1-B2+c3D4=E5F6*G7H8";
```

Certifique-se de que essa chave seja mantida em segredo e não seja compartilhada publicamente.
