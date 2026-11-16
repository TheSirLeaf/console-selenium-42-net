# Garantia da Qualidade de Software
**Gestão e Qualidade de Software**  
**Professor:** Daniel Henrique Matos de Paiva  
**Instituição:** Ânima Educação

---

## Lista de Exercício

**Regras Gerais:**
* Deve ser realizada em equipes de até **05 alunos**.
* Deve ser entregue no prazo proposto.
* Todas as entregas são **individuais**.

---

## Exercícios

Nesta nova etapa, vocês irão estruturar uma *Solution* profissional do zero e automatizar cenários reais utilizando o [DuckDuckGo](https://duckduckgo.com).

### Passo 1: Criação da Solution e Projetos

Execute os comandos abaixo no seu terminal para configurar o ambiente com o novo padrão:

```bash
# 1. Criar um diretório raiz para a solution e entrar nele
dotnet new sln -n ConsoleSelenium42
cd ConsoleSelenium42

# 2. Criar o projeto principal do tipo Console Application (.NET 10)
dotnet new console -n ConsoleSelenium

# 3. Criar o projeto de testes unitários utilizando MSTest
dotnet new mstest -n ConsoleSelenium.Tests

# 4. Adicionar os projetos à Solution
dotnet sln add ConsoleSelenium/ConsoleSelenium.csproj
dotnet sln add ConsoleSelenium.Tests/ConsoleSelenium.Tests.csproj

# 5. Adicionar a referência do projeto principal dentro do projeto de testes
dotnet add ConsoleSelenium.Tests/ConsoleSelenium.Tests.csproj reference ConsoleSelenium/ConsoleSelenium.csproj

# 6. Instalar os pacotes NuGet necessários para automação no projeto de testes
dotnet add ConsoleSelenium.Tests/ConsoleSelenium.Tests.csproj package Selenium.WebDriver
dotnet add ConsoleSelenium.Tests/ConsoleSelenium.Tests.csproj package Selenium.Support
```

---

### Passo 2: Implementação dos Cenários de Teste

Na pasta de testes do seu novo projeto, crie uma classe chamada `GoogleBuscaDesafioTest.cs` (ou `BuscaDesafioTest.cs`) contendo um teste automatizado (`[TestMethod]`) para cada um dos cenários abaixo:

#### Exercício 1: A Experiência de Pesquisa Básica e Validação de Título
* **Objetivo:** Automatizar a digitação na barra de pesquisa principal do DuckDuckGo, acionar a submissão e validar o carregamento da página de resultados.
* **O que fazer no teste:**
  1. Navegar até a URL base: `https://duckduckgo.com`
  2. Localizar o campo de pesquisa (identificado pelo atributo `name="q"`).
  3. Digitar o termo `"Selenium WebDriver C#"` utilizando o método `SendKeys`.
  4. Enviar o formulário pressionando a tecla Enter (ou `Keys.Enter`).
  5. Validar por meio de asserção (`Assert`) que o título ou a URL da página de resultados contém o termo pesquisado.

#### Exercício 2: Interagindo com o Autocompletar (Suggestions)
* **Objetivo:** Lidar com menus suspensos dinâmicos gerados via JavaScript enquanto o usuário digita.
* **O que fazer no teste:**
  1. Acessar a página inicial do DuckDuckGo.
  2. Digitar o termo `"C# unit testing"` no campo de pesquisa, sem pressionar Enter imediatamente.
  3. Utilizar uma espera explícita (`WebDriverWait`) para aguardar a listagem de sugestões de autocompletar aparecer no DOM (utilizando seletores como `.search-suggestions-list li`, `.sbox-suggestions li`, `[role='option']`).
  4. Clicar na terceira opção sugerida pela lista dinâmica.
  5. Garantir através de asserção que a navegação redirecionou corretamente para os resultados da opção escolhida.

#### Exercício 3: Navegação por Histórico e Botões de Ação
* **Objetivo:** Simular interações complexas de navegação, utilizando os botões de "Voltar" e "Avançar" do navegador integrados ao fluxo de teste.
* **O que fazer no teste:**
  1. Realizar uma busca inicial por `"Automated Testing"`.
  2. Clicar no primeiro link orgânico relevante retornado na página de resultados para sair da página de busca.
  3. Validar que a página externa foi carregada com sucesso verificando a URL atual (`_driver.Url`).
  4. Utilizar o comando de navegação do Selenium (`_driver.Navigate().Back()`) para retornar à página de resultados de busca.
  5. Validar que a página de resultados foi restaurada corretamente.

---

## Instruções de Entrega

1. **Versionamento:** Crie um repositório público no GitHub com o nome `console-selenium-42-net`
2. **Padrão de Código:** Organize a solução limpa (*Clean Code*) e modularizada.
3. **Boas Práticas de Commit:** Utilize commits semânticos:
   * *Exemplo 1:* `feat(setup): cria estrutura inicial da solution console-selenium-42`
   * *Exemplo 2:* `test(google): implementa cenario de autocompletar`
4. **Entrega:** Envie o link do repositório do GitHub.

---

## Referência Visual de Execução (Test Explorer)

Abaixo está a estrutura esperada dos testes aprovados no gerenciador de testes (`ConsoleSelenium.Tests (net10.0)`):

```text
Testing (3/3 passed)
└── ConsoleSelenium.Tests
    └── BuscaDesafioTest
        ├── ✓ Exercicio1_PesquisaBasica_ValidacaoTitulo
        ├── ✓ Exercicio2_Autocompletar_Suggestions
        └── ✓ Exercicio3_NavegacaoHistorico
```