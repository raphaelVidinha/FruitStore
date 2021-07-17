# FruitStore
Api CRUD de Frutas com autorização em JWT.
O banco de dados é o In Memory, o que significa que toda vez que a aplicação parar de rodar, os dados serão perdidos.
### instruções:
Após clonar o projeto, com o terminal vá até a raiz do projeto e rode os comandos abaixo para instalar as dependências e rodar o projeto respectivamente;
De preferência um de cada vez;
```
dotnet build
dotnet run
```

### Acesso a api:
O host da api, por default é: https://localhost:5001/
Para acessar o swagger é: https://localhost:5001/swagger/index.html

### Instruções de uso da API com SWAGGER:
Ao acessar a api através do swagger faça conforme abaixo:
- Faça login com um usuário válido, na área Login [post]
- Preencha os valore de 'username' e 'password' com admin e admin por exemplo
- Pegue o token, sem as aspas que retornou após login efetuado com sucesso (status code 200)
- No topo da página do swagger possui um botão 'Authorization', clique nele e abrirá uma modal
- Nesta modal, no campo 'Value' adicione a palavra Bearer e o token, conforme exemplo a seguir sem as aspas: "Bearer TOKEN"
- Após isso poderá efetuar qualquer operação com a api de frutas com sucesso!


### Tecnologias Utilizadas

| Tecnologia | Versão |
| ------ | ------ |
| AspNet - .NET | 5.0 |
| Entity Framework Core | 5 |
| AspNet Core - Authentication | 2.2.0 |
| Entity Framework Core InMemory | 5.0.8 |
