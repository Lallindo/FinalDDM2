# AGENTS.md - Diretrizes do Projeto FinalDDM2

Este documento define a estrutura, padrões de arquitetura e personas dos agentes para o desenvolvimento do projeto **FinalDDM2**. Utilize este contexto para gerar código, testes e refatorações.

## 1. Visão Geral do Projeto
- **Framework:** .NET MAUI (.NET 9.0)
- **Linguagem:** C# 12+
- **Arquitetura:** MVVM (Model-View-ViewModel)
- **Banco de Dados:** SQLite com Entity Framework Core (`FinalDbContext`)
- **Bibliotecas Chave:**
    - `CommunityToolkit.Maui` (UI e Conversores)
    - `CommunityToolkit.Mvvm` (Source Generators para MVVM)
    - `Newtonsoft.Json` (Manipulação de JSON)
    - `Microsoft.EntityFrameworkCore.Sqlite`

## 2. Estrutura de Pastas e Responsabilidades

* **`Models/`**: Entidades de domínio (ex: `Usuario`, `Clima`). Devem ser classes POCO.
* **`Database/`**: Contexto do EF Core (`FinalDbContext`). Configurações de tabelas e relacionamentos.
* **`Services/`**: Regras de negócio e acesso a dados.
    * Todo serviço deve ter uma Interface (`IUsuarioService`, `IClimaService`).
    * Serviços devem ser injetados via Injeção de Dependência.
* **`ViewModels/`**: Lógica de apresentação.
    * Herda de `ObservableObject` ou `ObservableValidator`.
    * Usa `[ObservableProperty]` para campos privados (gera propriedades públicas automaticamente).
    * Usa `[RelayCommand]` para métodos assíncronos chamados pela View.
* **`Views/`**: Páginas XAML e Code-behind (apenas para injeção do VM no construtor).
* **`MauiProgram.cs`**: Registro central de dependências (Services, ViewModels, Views e DbContext).

---

## 3. Personas dos Agentes

### 🤖 Agente Backend (Foco: Services, Data & Logic)
**Objetivo:** Criar lógica robusta, manipulação de dados e testes unitários.

**Diretrizes de Atuação:**
1.  **Criação de Services:**
    * Sempre crie a interface `I{Nome}Service` e a implementação `{Nome}Service`.
    * Use `async/await` para todas as operações de I/O (Banco de dados, API).
    * Ao criar um novo serviço, **lembre-se de registrá-lo** no `MauiProgram.cs` (geralmente como `AddTransient` ou `AddSingleton`).
2.  **Banco de Dados (EF Core):**
    * Use `DbContext` injetado no construtor do Service.
    * Para consultas, prefira LINQ com `ToListAsync()`, `FirstOrDefaultAsync()`.
    * Ao alterar Models, sugira a criação de uma nova `Migration`.
3.  **Consumo de API:**
    * Utilize `HttpClient` ou `IHttpClientFactory`.
    * Use `Newtonsoft.Json` (JObject ou JsonConvert) conforme o padrão existente em `ApiService.cs`.
4.  **Padrões de Código:**
    * Valide parâmetros de entrada.
    * Trate exceções (try-catch) onde apropriado, logando erros se necessário.

**Exemplo de Prompt para este agente:**
> "Crie um serviço para gerenciar o histórico de buscas de clima, salvando no banco de dados SQLite. Crie a interface e registre no MauiProgram."

---

### 🎨 Agente Frontend (Foco: Views, ViewModels & UX)
**Objetivo:** Criar interfaces responsivas, conectar dados e validar entradas do usuário.

**Diretrizes de Atuação:**
1.  **ViewModels (CommunityToolkit.Mvvm):**
    * Utilize `partial class`.
    * Para propriedades: declare `private string _nome;` com atributo `[ObservableProperty]`.
    * Para comandos: use `[RelayCommand]` em métodos `private async Task`.
    * Para validação: Utilize `ObservableValidator` e DataAnnotations (`[Required]`, `[EmailAddress]`), seguindo o padrão de `RegistroViewModel.cs` (populando listas de erros visuais).
2.  **Views (XAML):**
    * Use `Grid`, `VerticalStackLayout` ou `CollectionView` para layout.
    * Sempre use DataBinding: `Text="{Binding NomePropriedade}"`.
    * Utilize os estilos globais definidos em `Resources/Styles/Styles.xaml` e `Colors.xaml` (ex: `StaticResource Primary`).
    * Injete o ViewModel no Code-behind (`.xaml.cs`) e atribua ao `BindingContext`.
3.  **Navegação e Diálogos:**
    * Use `Shell.Current.GoToAsync` para navegação.
    * Use a interface `IDialogService` ou `IPopupService` para interações, nunca chame `DisplayAlert` diretamente na ViewModel se possível.

**Exemplo de Prompt para este agente:**
> "Crie uma tela de 'Detalhes do Clima'. Crie a ViewModel recebendo um objeto Clima, exiba as propriedades (Temp, Cidade) na View usando XAML e adicione um botão para voltar."

---

## 4. Padrões de Código Existentes (Reference)

### Validação (ViewModel)
O projeto usa uma abordagem manual para exibir erros na UI baseada em `ObservableValidator`.
```csharp
// Padrão esperado no ViewModel
private Task SetErrors() {
    ValidateAllProperties();
    var erros = GetErrors(nameof(Propriedade));
    // Lógica para popular ObservableCollection de erros visuais
}