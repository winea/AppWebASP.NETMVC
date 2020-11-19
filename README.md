# AppWebASP.NETMVC
curso DIO Desenvolvimento de aplicações com .NET

## Passos criacao projeto:
No visual Studio criar projeto modelo web MVC

criar Models Categoria.cs
Aba Ferramentas gerenciador de pacotes Nuget

#### Install-Package Microsoft.EntityFrameworkCore.SqlServer
#### Install-Package Microsoft.EntityFrameworkCore.Tools

Apos criar a classe Context.cs (com conexao com o BD), use como exemplo:
#### @"Server=(localdb)\mssqllocaldb;Database=cursoMvc;Integrated Security=True"

```
 protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //inserir a string de conexao
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=cursoMvc;Integrated Security=True");

        }
```

clica no projeto com direito -> gerenciador de pacotes Nuget:
Criar tabela
#### Add-Migration InicialCreate

Criar BD
#### Update-Database


pasta controler -> clica direito -> adicionar item com Scaffolding -> Controlador MVC com Entity...
criar CategoriasControler responsavel por se ligar ao Context e Categorias -> rodar e ver no navegador
possui os metodos GET, POST, DELETE, PUT...

Apos criar model Produto e add no Categoria List<Produto> e no Context um DbSet Produto
```
 public List<Produto> Produtos { get; set; }
```

```
 public DbSet<Produto> Produtos { get; set; }
```

gerenciador de pacotes Nuget:
Add-Migration TabelaProduto
Update-Database

pasta controler -> clica direito -> adicionar item com Scaffolding -> Controlador MVC com Entity...
Produto com Context

### Acessar BD
Para acessar no BD, no visual studio ABA Gerenciador de Servidores , clicar conectar a um BD
Nome servidor: (localdb)\mssqllocaldb -> seleciona cursoMvc

## API
o visual Studio acrescentar projeto modelo web MVC -> api

### Instalar Swagger
clica com  direito no projeto -> adicionar pacote Nuget
busque Swashbuckle.ASPNetCore

### Configurar Swagger
clica botao direito projetos -> la no final Propriedades -> 
Aba Compilar marcar caixa Saida -> arquivo de documentacao XML
Aba Depurar mudar nome da caixa no Iniciar Navegador -> swagger

no Startup.cs adicionar o import do swagger (injecao dependencia) 
```
public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "curso API", Version = "v1" }); });
        }
```
Add midleware

```
 public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Adiciona Middleware
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "curso API"); });

            app.UseHttpsRedirection();
```
build e gera xml

clica no projeto botão direito -> adiciona referencia projeto -> check cursoMvc

pasta Controler api clica com botão direito -> adicionar item com Scaffolding -> Controlador API com Entity...
modelo: Categoria (CursoMvc), Context(CursoMvc)

no Startup.cs adicionar o import do Context 
```
 public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<Context>();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "curso API", Version = "v1" }); });
        }
```

Para rodar os dois simultâneos clica direito na Solucao -> propriedades
Projeto de Inicializacao -> selecione Varios projetos de inicializacao -> iniciar em ambos

rode e vc vera os tipos de retornos da API 

Obs: Gerou erro Não foi possivel conectar ao servidor web iis express

Solucao: basta ir na pasta do projeto -> Properties e alterar no launchSettings
 "sslPort": 44337, salve e rode seu projeto normalmente

Para testar a api clique em try -> execute 

pasta Controler api clica com botão direito -> adicionar item com Scaffolding -> Controlador API com Entity...
modelo: Produto (CursoMvc), Context(CursoMvc)

No ProdutosController adicionar Include para que api mostre a descricao, pode colocar mais Includes se precisar

```
 // GET: api/Produtoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            return await _context.Produtos.Include("Categoria").ToListAsync();
        }
```

## Projeto de Teste
 Add Novo projeto -> procure por tipo teste xUnit(.net core)

clica com direto sobre projeto teste -> add referencia projeto -> marque ambos projetos

clica com  direito no projeto -> adicionar pacote Nuget
pesquise entity core sql server
####  Microsoft.EntityFrameworkCore.SqlServer
####  Microsoft.EntityFrameworkCore.Tools
#### moq
