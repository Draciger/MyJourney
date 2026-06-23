# CoverBox

A console-based game tracker built with C# and SQLite. Track games you have played, save games you want to play later, and rate titles you finish.

## Features

- Add, update, and delete games (name, system, genre, release year)
- Create and manage users
- Personal lists: **To Be Played** and **Played**
- Rate games after marking them as played (updates average rating)
- SQLite persistence — data survives between runs

## Requirements

- [.NET 9 SDK](https://dotnet.microsoft.com/download)

## Getting started

Clone the repository, then from the solution folder:

```bash
dotnet restore
dotnet run --project CoverBox
```

The app creates `Database/CoverBoxDatabase.db` automatically on first run.

## Running tests

```bash
dotnet test
```

## Project structure

```
CoverBox/
├── UserInterface/          # Console menu and user input
├── Business Logic Layer/   # GameManager, UserManager
├── Data Access Layer/      # SQLite repositories + schema setup
└── Models/                 # Game, User, UserGame entities

CoverBox.Tests/             # NUnit tests for data access layer
```

## Architecture

Three-layer design:

1. **UI** — `Menu` handles console interaction
2. **Business logic** — managers enforce rules and coordinate repositories
3. **Data access** — `GameLibrary`, `UserLibrary`, and `UserGameLibrary` talk to SQLite

Patterns used include Repository, Facade (managers), and constructor-based dependency injection in `Program.cs`.

## License

Academic project — see course requirements for usage.
