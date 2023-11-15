# OpenGraph Tilemaker Reborn

[![Build Status](https://github.com/michaelvolz/OpenGraphTilemakerReborn/actions/workflows/dotnet.yml/badge.svg)](https://github.com/michaelvolz/OpenGraphTilemakerReborn/actions/workflows/dotnet.yml)
[![SonarCloud](https://github.com/michaelvolz/OpenGraphTilemakerReborn/actions/workflows/sonarcloud.yml/badge.svg)](https://github.com/michaelvolz/OpenGraphTilemakerReborn/actions/workflows/sonarcloud.yml)
[![CodeQL](https://github.com/michaelvolz/OpenGraphTilemakerReborn/actions/workflows/codeql.yml/badge.svg)](https://github.com/michaelvolz/OpenGraphTilemakerReborn/actions/workflows/codeql.yml)
[![last commit (master)](https://img.shields.io/github/last-commit/michaelvolz/OpenGraphTilemakerReborn/master.svg)](https://github.com/michaelvolz/OpenGraphTilemakerReborn/commits/master)

[![License: Unlicense](https://img.shields.io/badge/license-Unlicense-blue.svg)](https://en.wikipedia.org/wiki/Unlicense)
[![Dependabot enabled](https://img.shields.io/badge/Dependabot-enabled-blue.svg)](https://docs.github.com/en/code-security/dependabot/working-with-dependabot)
![GitHub language count](https://img.shields.io/github/languages/count/michaelvolz/OpenGraphTilemakerReborn)
![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/michaelvolz/OpenGraphTilemakerReborn)

## Experimental [DotNet 8][def] [Blazor](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor)

This project uses both Blazor Server- & WebAssembly-Interactive.

### Code Analyzers/Decorators

- [JetBrains.Annotations](https://www.jetbrains.com/help/resharper/Reference__Code_Annotation_Attributes.html)
- [Meziantou.Analyzer](https://github.com/meziantou/Meziantou.Analyzer)
- [Microsoft.CodeAnalysis.NetAnalyzers](https://github.com/dotnet/roslyn-analyzers) implicit
- [Microsoft.VisualStudio.Threading.Analyzers](https://github.com/Microsoft/vs-threading)
- [Roslynator.Core](https://github.com/dotnet/roslynator)
- [StyleCop.Analyzers](https://github.com/DotNetAnalyzers/StyleCopAnalyzers)

Trying to evaluate the performance impact of using so many analyzers at the same time and if it is feasible as well as useful.

### Frontend technologies

- [SCSS](https://sass-lang.com/documentation/syntax/) (or Less)
- [Bootstrap](https://getbootstrap.com/) (or ZURB Foundation/Bulma/Fomantic-UI)
- [WebCompiler](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.WebCompiler) (or Dart SCSS)
- [LibMan](https://learn.microsoft.com/en-us/aspnet/core/client-side/libman/libman-vs?view=aspnetcore-8.0) (or VS Package Installer)
- As little JavaScript as possible

I probably will try different CSS Frameworks and tools to evaluate what is still useful today, or abandoning them completely would be the better choice for this kind of project.

### [Fluxor](https://github.com/mrpmorris/Fluxor) is a zero boilerplate Flux/Redux library for Microsoft .NET

Fluxor seems to be the best all around state-management for Blazor at the moment. It has seemingly all the flexibility I need and no hard dependencies on other libraries. The code also looks extremely clean.

### [MediatR](https://github.com/jbogard/MediatR) - simple mediator implementation in .Net

One of my favorite tools all around. I need to find the borders where MediatR and Fluxor meet and how to handle them together.

[...]
