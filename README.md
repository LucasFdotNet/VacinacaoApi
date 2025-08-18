# **Desafio BackEnd \- Cartão de Vacinação**

Lucas Furini

## **Descrição**

Esta é uma API RESTful desenvolvida em C\# com .NET 8 para o gerenciamento de um cartão de vacinação digital. A aplicação permite o cadastro de pessoas e vacinas, e o registro de vacinas aplicadas a cada pessoa, formando um cartão de vacinação individual.

A arquitetura foi construída seguindo o padrão CQRS (Command Query Responsibility Segregation).

### **Entidades e Relações**

* **Pessoa**: Representa um usuário do sistema, contendo dados de identificação e credenciais de acesso.  
* **Vacina**: Representa um tipo de vacina disponível no sistema, que possui um nome e um código único(gerado pelo sistema).  
* **RegistroVacinacao**: É a entidade que conecta uma Pessoa a uma Vacina, contendo informações como a dose e a data da aplicação. Uma pessoa pode ter vários registros de vacinação.

## **Regras de Negócio (Requisitos Funcionais)**

* RF1: O cadastro de um novo usuário (Pessoa) é público.  
* RF2: Todas as outras rotas da API requerem autenticação via token JWT.  
* RF3: Ao remover uma Pessoa, todos os seus RegistroVacinacao associados são automaticamente excluídos (exclusão em cascata).  
* RF4: Não é possível registrar uma vacinação para uma Pessoa ou Vacina que não exista no banco de dados.  
* RF5: A dose da vacina aplicada deve ser um número positivo (maior que 0).  
* RF6: A data de aplicação de uma vacina não pode ser uma data no futuro.

## **Ferramentas e Bibliotecas utilizadas**

| Ferramenta / Biblioteca | Propósito na Aplicação |
| :---- | :---- |
| **.NET 8** | A mais recente versão Long-Term Support (LTS) da plataforma de desenvolvimento da Microsoft. |
| **SQL Server** | Sistema de gerenciamento de banco de dados relacional. |
| **Entity Framework Core** | ORM (Object-Relational Mapper) para a comunicação entre a aplicação e o banco de dados. |
| **MediatR** | Biblioteca para desacoplar a lógica de negócio dos controllers. |
| **FluentValidation** | Biblioteca para a criação de regras de validação. |
| **JWT (JSON Web Tokens)** | Padrão utilizado para a implementação da autenticação da API. |
| **Swagger (Swashbuckle)** | Ferramenta para documentação e testes da API. |

## **Como Configurar e Executar a Aplicação**

Siga os passos abaixo para configurar e executar a solução em um ambiente de desenvolvimento local.

### **Pré-requisitos**

* .NET 8 SDK  
* Visual Studio 2022  
* SQL Server 2019 (ou superior), versão Developer ou Express.

### **Passo 1: Clonar o Repositório**

git clone https://github.com/LucasFdotNet/VacinacaoApi.git

### **Passo 2: Configurar a Conexão com o Banco de Dados**

1. Abra o arquivo appsettings.json na raiz do projeto.  
2. Encontre a seção ConnectionStrings.  
3. **Altere os dados** (Server, User Id, Password) para corresponder à sua instância local do SQL Server.  
   * **Exemplo para uma instância local com usuário e senha:**  
     "ConnectionStrings": {  
       "DefaultConnection": "Server=localhost;Database=VacinacaoDB;User Id=SEU\_USUARIO;Password=SUA\_SENHA;TrustServerCertificate=True"  
     }

### **Passo 3: Criar o Banco de Dados**

Abra um terminal na pasta raiz do projeto (pode ser pelo terminal do Visual Studio) e execute o seguinte comando:

dotnet ef database update

Aguarde a mensagem "Done.". 

### **Passo 4: Executar a Aplicação**

Agora, basta executar o projeto pelo Visual Studio pressionando **F5** ou o botão "Play".

A aplicação será iniciada e uma janela do navegador abrirá com a interface do Swagger em https://localhost:\<SUA\_PORTA\>/swagger.

## **Como a Aplicação Funciona (Guia de Teste)**

A API utiliza autenticação JWT. Para testar os endpoints protegidos, você precisa primeiro cadastrar o usuário (Pessoa) e depois fazer login para obter um token.

### **Fluxo de Teste Recomendado**

1. **Cadastre um usuário:** Use o endpoint público POST /api/pessoas para criar seu usuário e senha.  
2. **Faça Login:** Use o endpoint POST /api/auth/login com as credenciais criadas para receber um Bearer Token.  
3. **Autorize-se no Swagger:** Clique no botão **"Authorize"** no topo da página, digite Bearer SEU\_TOKEN e confirme.  
4. **Teste as Rotas Protegidas:** Agora você pode testar todas as outras rotas, como cadastrar vacinas, registrar vacinações, etc.

## **Detalhes dos Endpoints**

### **Autenticação**

* **POST /api/auth/login**  
  * **Descrição:** Autentica um usuário e retorna um token JWT.  
  * **Autenticação:** Não requerida.  
  * **Exemplo de Request Body:**  
    {  
      "numeroIdentificacao": "11122233344",  
      "senha": "senha123"  
    }

  * **Exemplo de Response Body (200 OK):**  
    {  
      "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOi..."  
    }

### **Pessoas**

* **POST /api/pessoas**  
  * **Descrição:** Cadastra um novo usuário no sistema.  
  * **Autenticação:** Não requerida.  
  * **Exemplo de Request Body:**  
    {  
      "nome": "Usuario de Teste",  
      "numeroIdentificacao": "11122233344",  
      "senha": "senha123"  
    }

  * **Exemplo de Response Body (201 Created):**  
    {  
      "nome": "Usuario de Teste",  
      "numeroIdentificacao": "11122233344",  
      "senha": "senha123"  
    }

* **GET /api/pessoas/{id}**  
  * **Descrição:** Consulta os dados de um usuário específico.  
  * **Autenticação:** Requerida (Bearer Token).  
  * **Exemplo de Response Body (200 OK):**  
    {  
      "id": "guid-da-pessoa-aqui",  
      "nome": "Usuario de Teste",  
      "numeroIdentificacao": "11122233344"  
    }

* **DELETE /api/pessoas/{id}**  
  * **Descrição:** Remove um usuário e todos os seus registros de vacinação.  
  * **Autenticação:** Requerida (Bearer Token).  
  * **Exemplo de Response (204 No Content):** A resposta não possui corpo.

### **Vacinas**

* **POST /api/vacinas**  
  * **Descrição:** Cadastra um novo tipo de vacina.  
  * **Autenticação:** Requerida (Bearer Token).  
  * **Exemplo de Request Body:**  
    {  
      "nome": "HEPATITE B"  
    }

  * **Exemplo de Response Body (200 OK):**  
    {  
      "id": "guid-da-nova-vacina-aqui"  
    }

* **GET /api/vacinas**  
  * **Descrição:** Lista todas as vacinas cadastradas.  
  * **Autenticação:** Requerida (Bearer Token).  
  * **Exemplo de Response Body (200 OK):**  
    \[  
      {  
        "id": "guid-da-vacina-1",  
        "nome": "HEPATITE B"  
      },  
      {  
        "id": "guid-da-vacina-2",  
        "nome": "TETRA VALENTE"  
      }  
    \]

### **Cartão de Vacinação**

* **POST /api/pessoas/{pessoaId}/vacinacoes**  
  * **Descrição:** Registra uma vacina para uma pessoa.  
  * **Autenticação:** Requerida (Bearer Token).  
  * **Exemplo de Request Body:**  
    {  
      "vacinaId": "guid-da-vacina-aqui",  
      "dose": 1,  
      "dataAplicacao": "2025-08-18T12:00:00Z"  
    }

  * **Exemplo de Response Body (200 OK):**  
    {  
      "id": "guid-do-novo-registro-aqui"  
    }

* **GET /api/pessoas/{pessoaId}/vacinacoes**  
  * **Descrição:** Consulta o cartão de vacinação de uma pessoa.  
  * **Autenticação:** Requerida (Bearer Token).  
  * **Exemplo de Response Body (200 OK):**  
    \[  
      {  
        "idDoRegistro": "guid-do-registro-1",  
        "nomeVacina": "HEPATITE B",  
        "dose": 1,  
        "dataAplicacao": "2025-08-18T12:00:00Z"  
      },  
      {  
        "idDoRegistro": "guid-do-registro-2",  
        "nomeVacina": "HEPATITE B",  
        "dose": 2,  
        "dataAplicacao": "2025-09-15T14:30:00Z"  
      }  
    \]

* **DELETE /api/registrosvacinacao/{id}**  
  * **Descrição:** Exclui um registro de vacinação específico.  
  * **Autenticação:** Requerida (Bearer Token).  
  * **Exemplo de Response (204 No Content):** A resposta não possui corpo.

