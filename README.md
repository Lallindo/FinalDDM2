# Projeto Final - DDM2

Este é o projeto final para a disciplina de Desenvolvimento para Dispositivos Móveis 2 (DDM2). O aplicativo foi desenvolvido utilizando .NET MAUI e demonstra a aplicação de conceitos modernos de desenvolvimento mobile.

## 📝 Visão Geral

O aplicativo consiste em um sistema de cadastro e login de usuários, com uma funcionalidade principal de consulta de previsão do tempo. Ele foi estruturado para ser modular, escalável e de fácil manutenção, seguindo a arquitetura MVVM.

## ✨ Funcionalidades

-   [x] **Cadastro de Usuário:** Permite que novos usuários criem uma conta com validação de dados.
-   [x] **Login de Usuário:** Autenticação de usuários existentes.
-   [ ] **Consulta de Clima:** Exibe a previsão do tempo para uma localidade.
-   [x] **Armazenamento Local:** Utiliza SQLite para persistir os dados do usuário no dispositivo.

## 🛠️ Tecnologias Utilizadas

-   **.NET MAUI:** Framework para criação de aplicativos multiplataforma (Android, iOS, Windows, macOS).
-   **C# 12:** Linguagem de programação principal.
-   **XAML:** Linguagem de marcação para a definição da interface do usuário (UI).
-   **Arquitetura MVVM (Model-View-ViewModel):** Padrão de arquitetura para separar a lógica de apresentação da lógica de negócio.
-   **Entity Framework Core com SQLite:** Para o banco de dados local.
-   **CommunityToolkit.Mvvm:** Para implementação simplificada do padrão MVVM com source generators (`[ObservableProperty]`, `[RelayCommand]`).
-   **CommunityToolkit.Maui:** Para componentes de UI e conversores auxiliares.

## 📂 Estrutura do Projeto

O projeto segue uma estrutura de pastas clara para organizar as responsabilidades:

-   **`Models/`**: Contém as entidades de domínio (ex: `Usuario`).
-   **`Database/`**: Define o `DbContext` do Entity Framework Core.
-   **`Services/`**: Centraliza a lógica de negócio e o acesso a dados (banco de dados e APIs externas).
-   **`ViewModels/`**: Contém a lógica de apresentação e o estado das Views.
-   **`Views/`**: Contém as páginas da interface do usuário (arquivos `.xaml` e `.xaml.cs`).
-   **`MauiProgram.cs`**: Ponto de entrada do aplicativo, onde é feita a injeção de dependências (DI).

## 🚀 Como Executar

1.  **Clone o repositório.**
2.  **Abra o projeto** em sua IDE de preferência (Visual Studio 2022 ou JetBrains Rider).
3.  **Restaure as dependências** do NuGet (geralmente acontece de forma automática ao abrir a solução).
4.  **Execute o projeto** selecionando a plataforma desejada (ex: Android Emulator, Windows Machine).
