# OpenGraph Tilemaker Reborn

[![Build Status](https://github.com/michaelvolz/OpenGraphTilemakerReborn/actions/workflows/dotnet.yml/badge.svg)](https://github.com/michaelvolz/OpenGraphTilemakerReborn/actions/workflows/dotnet.yml)
[![SonarCloud](https://github.com/michaelvolz/OpenGraphTilemakerReborn/actions/workflows/sonarcloud.yml/badge.svg)](https://github.com/michaelvolz/OpenGraphTilemakerReborn/actions/workflows/sonarcloud.yml)
[![CodeQL](https://github.com/michaelvolz/OpenGraphTilemakerReborn/actions/workflows/codeql.yml/badge.svg)](https://github.com/michaelvolz/OpenGraphTilemakerReborn/actions/workflows/codeql.yml)
[![License: Unlicense](https://img.shields.io/badge/license-Unlicense-blue.svg)](https://en.wikipedia.org/wiki/Unlicense)
[![Dependabot enabled](https://img.shields.io/badge/Dependabot-enabled-blue.svg)](https://docs.github.com/en/code-security/dependabot/working-with-dependabot)

## Experimental DotNet 8 Blazor

This project uses both Blazor Server- & WebAssembly-Interactive.

### Code Analyzers/Decorators used

- JetBrains.Annotations
- Meziantou.Analyzer
- Microsoft.CodeAnalysis.CSharp
- Microsoft.CodeAnalysis.NetAnalyzers
- Roslynator.Core
- StyleCop.Analyzers

Trying to evaluate the performance impact of using so many analyzers at the same time and if it is feasible as well as useful.

### Frontend technologies used

- SCSS (Less)
- Bootstrap (ZURB Foundation/Bulma/Fomantic-UI)
- WebCompiler (Dart SCSS)
- LibMan (VS Package Installer)
- As little JavaScript as possible

I probably will try different CSS Frameworks and tools to evaluate what is still useful today, or abandoning them completely would be the better choice for this kind of project.

[...]
