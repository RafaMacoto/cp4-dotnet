GeoMaster API (CP4)

👥 Integrantes
RM556270 - Bianca Vitoria - 2TDSPZ
RM558976 Maria Eduarda Pires Vieira - 2TDSPZ
RM554992 - Rafael Macoto Magalhães Seo - 2TDSPZ


API REST para cálculos geométricos 2D e 3D com arquitetura limpa e extensível (SOLID) e documentação Swagger/OpenAPI.

✨ Funcionalidades

2D: Círculo e Retângulo → área e perímetro

3D: Esfera → volume e área superficial

Desafio Final: validação de contenção de formas (uma forma cabe dentro da outra?)

Swagger com exemplos, regras de validação e códigos de resposta (200 / 400 / 500)

📦 Requisitos

.NET SDK 9.0 (ou superior compatível)

Verifique:

dotnet --version

▶️ Como rodar o projeto (local)
1) Restaurar dependências
dotnet restore

2) Compilar
dotnet build

3) Executar
dotnet run


Ao iniciar, o console mostra as URLs, por exemplo:

Now listening on: http://localhost:5242


O Swagger UI está configurado para abrir na raiz do site.
Acesse: http://localhost:5242/ (sem “/swagger”).

4) (Opcional) Executar com hot reload
dotnet watch run

5) (Opcional) Definir ambiente

Por padrão o Swagger é habilitado em Development:

# Windows (PowerShell)
$env:ASPNETCORE_ENVIRONMENT="Development"; dotnet run

📖 Documentação — Swagger (via Swashbuckle)

Pacote: Swashbuckle.AspNetCore

Geração de XML comments habilitada no .csproj

Configuração no Program.cs com AddSwaggerGen, UseSwagger e UseSwaggerUI (RoutePrefix = "")

Como acessar

Rodando local: http://localhost:5242/ (Swagger UI)

JSON OpenAPI: http://localhost:5242/swagger/v1/swagger.json

🔌 Endpoints
Cálculos

POST /api/v1/calculos/area

POST /api/v1/calculos/perimetro

POST /api/v1/calculos/volume

POST /api/v1/calculos/area-superficial

Desafio Final

POST /api/v1/validacoes/forma-contida
Verifica se a forma interna cabe dentro da forma externa e retorna true/false.

🧪 Exemplos (copiar/colar no Swagger → Try it out)
Área — Círculo

POST /api/v1/calculos/area

{
  "tipo": "circulo",
  "parametros": { "raio": 5 }
}


Resposta esperada (200): 78.5398163397

Perímetro — Retângulo

POST /api/v1/calculos/perimetro

{
  "tipo": "retangulo",
  "parametros": { "largura": 10, "altura": 4 }
}


Resposta esperada (200): 28

Volume — Esfera

POST /api/v1/calculos/volume

{
  "tipo": "esfera",
  "parametros": { "raio": 3 }
}


Resposta esperada (200): 113.097335529

Área superficial — Esfera

POST /api/v1/calculos/area-superficial

{
  "tipo": "esfera",
  "parametros": { "raio": 3 }
}


Resposta esperada (200): 113.097335529

Desafio — Contenção de formas

true — Círculo r=5 dentro de Retângulo 10x10

{
  "externa": { "tipo": "retangulo", "parametros": { "largura": 10, "altura": 10 } },
  "interna": { "tipo": "circulo",   "parametros": { "raio": 5 } }
}


false — Círculo r=6 não cabe em Retângulo 10x10

{
  "externa": { "tipo": "retangulo", "parametros": { "largura": 10, "altura": 10 } },
  "interna": { "tipo": "circulo",   "parametros": { "raio": 6 } }
}


true — Retângulo 2x3 dentro de Círculo r=2 (cabe pela diagonal)

{
  "externa": { "tipo": "circulo", "parametros": { "raio": 2 } },
  "interna": { "tipo": "retangulo", "parametros": { "largura": 2, "altura": 3 } }
}

🧾 Regras/Validações (resumo)
DTO — FormaRequest
{
  "tipo": "circulo | retangulo | esfera",
  "parametros": {
    // circulo/esfera: "raio" > 0
    // retangulo: "largura" > 0, "altura" > 0
  }
}


[Required] para tipo e parametros

O Swagger mostra os campos obrigatórios no schema

Contenção (2D)

Círculo dentro de retângulo: cabe se 2r ≤ min(largura, altura)

Retângulo dentro de círculo: cabe se raio ≥ sqrt(largura² + altura²)/2 (meia-diagonal)

Círculo dentro de círculo: r_in ≤ r_ex

Retângulo dentro de retângulo (alinhados): w_in ≤ w_ex e h_in ≤ h_ex

(Bônus 3D) Esfera dentro de esfera: r_in ≤ r_ex

Respostas documentadas

200 OK — sucesso

400 Bad Request — dados inválidos/combinação não suportada

500 Internal Server Error — erro inesperado

🧱 Arquitetura & SOLID

SRP: classes focadas (entidades/serviços/controllers/factory)

OCP: adicionar novas formas sem quebrar código existente (apenas nova entidade + case na factory)

ISP: contratos separados (ICalculo2D, ICalculo3D)

DIP: controllers dependem de interfaces (ICalculadoraService, IFormaFactory) via DI

Estrutura (resumo)
cp4-dotnet/
├─ Controllers/
│  ├─ CalculosController.cs
│  └─ ValidacoesController.cs
├─ DTOs/
│  └─ FormaRequest.cs
├─ Models/
│  ├─ Entities/ (Circulo, Retangulo, Esfera)
│  ├─ Factory/ (FormaFactory)
│  └─ Interfaces/ (ICalculo2D, ICalculo3D, IFormaFactory)
├─ Middlewares/
│  └─ ExceptionMiddleware.cs
├─ Services/
│  └─ CalculadoraService.cs
├─ Program.cs
└─ cp4-dotnet.csproj

🧰 Dicas & Solução de Problemas

404 ao abrir http://localhost:XXXX/
→ Confirme que está em Development e que o Program.cs tem:

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GeoMaster API v1");
    c.RoutePrefix = string.Empty; // Swagger na raiz
});


Erro “arquivo .exe em uso” ao buildar (Windows)
→ Pare o processo anterior:

taskkill /F /IM cp4-dotnet.exe
dotnet clean
dotnet build
dotnet run


Forçar porta específica

dotnet run --urls "http://localhost:5242"
