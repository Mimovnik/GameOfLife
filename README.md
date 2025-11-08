# Game of Life (Avalonia + Clean Architecture)

An educational implementation of Conway’s Game of Life that demonstrates how to pair Clean Architecture with the MVVM-friendly Avalonia UI framework. The goal is to keep the simulation domain completely UI-agnostic while showcasing Avalonia features (styles, templates, triggers, animations) in the presentation layer.

## Project layout

```
GameOfLife.Domain/           # Pure rules & data (board, rules, simulation engine)
GameOfLife.Application/      # Use cases orchestrating domain services
GameOfLife.Infrastructure/   # Adapters (file I/O, timers, RNG, etc.)
GameOfLife.Presentation/     # Avalonia UI (Views/ViewModels/resources)
GameOfLife.Domain.Specs/     # Machine.Specifications tests for the domain layer
docs/                        # Reference notes (requirements, TODO)
TODO.md                      # Vertical-slice implementation plan
```

## Dev environment

This repo uses `flake.nix` to provide the .NET SDK plus native dependencies required by Avalonia (GL/X11/GTK). Enter the shell with:

```
nix develop
```

## Building & running

```
dotnet restore GameOfLife.sln
dotnet build GameOfLife.sln
dotnet run --project GameOfLife.Presentation
```

## Testing

Domain specs use MSpec:

```
dotnet test GameOfLife.Domain.Specs
```
