# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Run Commands
- Build: `dotnet build`
- Run: `dotnet run --project JustScreenshotTool`
- Watch mode: `dotnet watch --project JustScreenshotTool run`
- Test: `dotnet test`
- Test single test: `dotnet test --filter FullyQualifiedName=Namespace.TestClass.TestMethod`
- Format code: `dotnet format`

## Code Style Guidelines
- Nullable reference types are enabled - use `string?` for nullable strings
- Use implicit usings where possible (`ImplicitUsings` is enabled)
- Private fields use camelCase with underscore prefix: `private readonly _fieldName`
- Public properties use PascalCase: `public string PropertyName { get; set; }`
- Use dependency injection rather than static instances
- Controllers should use ILogger for logging and follow RESTful patterns
- Use async/await for I/O operations
- Error handling: use try/catch only when necessary, prefer validation and return values
- Follow Microsoft's C# Coding Conventions: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions