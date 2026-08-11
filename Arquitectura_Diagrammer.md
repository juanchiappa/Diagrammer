# Proyecto: Diagrammer (Herramienta CASE Multiplataforma)

## 1. Stack Tecnológico
*   **Lenguaje:** C# (.NET 8 o superior)
*   **Interfaz Gráfica:** Avalonia UI (Patrón MVVM estricto)
*   **Lectura de Código / Parsing:** Roslyn (para .NET) / LSP (para otros lenguajes)
*   **Persistencia:** System.Text.Json (texto plano para control de versiones en Git)

## 2. Arquitectura Limpia (Regla Estricta de Dependencias)
La solución se divide en capas. Las capas superiores pueden referenciar a las inferiores, NUNCA al revés.

1.  **Diagrammer.Domain:** (Core). Clases base `DiagramNode`, `DiagramEdge`. SIN REFERENCIAS externas.
2.  **Diagrammer.Engine & Interfaces:** Lógica matemática, algoritmos de ruteo, contratos (`IProjectAnalyzer`). Referencia a Domain.
3.  **Diagrammer.Analyzers & Storage:** (Infraestructura). Plugins aislados que implementan las interfaces usando Roslyn o File I/O.
4.  **Diagrammer.UI.Avalonia:** (Presentación). La aplicación ejecutable, Vistas (XAML) y ViewModels. Inyecta todas las dependencias al arrancar.

## 3. Reglas para Agentes de IA (Antigravity)
*   **Prohibido acoplar UI con lógica:** La interfaz gráfica NUNCA debe saber cómo se lee un archivo o cómo se parsea el código.
*   **TDD (Test-Driven Development):** Antes de implementar la lógica compleja (ej. serialización o parsing), el agente debe escribir el Unit Test en xUnit.
*   **Ecosistema:** Asegurar que todo el código C# sea estrictamente multiplataforma y agnóstico al sistema operativo, para que compile y corra nativamente tanto en Windows como en entornos Linux personalizados (como gestores de ventanas en mosaico tipo Hyprland). Olvidar por completo librerías heredadas como Windows Forms.