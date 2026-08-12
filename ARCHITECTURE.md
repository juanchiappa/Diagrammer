# Diagrammer: Multi-platform CASE Tool

## 1. Core Rules & Language
*   **STRICT LANGUAGE RULE:** All code, variables, classes, comments, documentation, and commit messages MUST be written in **English**. 
*   **Tech Stack:** C# (.NET 8+), Avalonia UI (MVVM pattern), System.Text.Json (for persistence).

## 2. Clean Architecture (Dependency Rules)
The solution is divided into strict layers. Outer layers can reference inner layers, NEVER the reverse.

1.  **Diagrammer.Domain:** (Core). Base classes (`DiagramNode`, `DiagramEdge`). NO external references allowed. No UI code.
2.  **Diagrammer.Engine & Interfaces:** Routing algorithms, math, and contracts (`IProjectAnalyzer`, `IStorageService`). References Domain.
3.  **Diagrammer.Analyzers & Storage:** (Infrastructure). Isolated plugins. Implements interfaces using Roslyn or File I/O.
4.  **Diagrammer.UI.Avalonia:** (Presentation). The executable application, Views (XAML), and ViewModels. Injects all dependencies at startup.

## 3. Agent Directives (AI Instructions)
*   **UI/Logic Separation:** The graphical interface MUST NEVER know how a file is read or how code is parsed.
*   **TDD (Test-Driven Development):** Before implementing complex logic (e.g., serialization, parsing), the agent MUST write the Unit Test in xUnit.
*   **Cross-platform:** Ensure all C# code is cross-platform and OS-agnostic (Windows/Linux/macOS). DO NOT use legacy libraries like Windows Forms.