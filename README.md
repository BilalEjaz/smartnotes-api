# SmartNotes

A full-stack note-taking application built with .NET: an ASP.NET Core Web API backed by Entity Framework Core and SQL Server, with a Blazor front end and AI-assisted features in progress.

> A hands-on .NET engineering project, actively under development.

## Tech stack

- **Backend:** ASP.NET Core Web API (.NET 9), controller-based REST
- **Data:** Entity Framework Core 9 + SQL Server (code-first migrations)
- **Frontend:** Blazor *(planned, Week 3)*
- **AI:** note auto-summarisation and auto-tagging *(planned)*
- **Quality & delivery:** xUnit tests, Docker, CI/CD, Azure deployment *(planned)*

## Features so far

- RESTful Notes API with proper HTTP status codes (`200`, `201`, `204`, `404`)
- Full CRUD endpoints (`GET`, `POST`, `DELETE`)
- EF Core code-first data layer on SQL Server
- Dependency-injected `DbContext`
- Clean separation of model, data, and API layers

## Project structure

```
smartnotes/
└── SmartNotes.Api/          ASP.NET Core Web API
    ├── Controllers/         REST endpoints (NotesController)
    ├── Note.cs              Domain entity
    ├── AppDbContext.cs      EF Core database context
    └── Migrations/          EF Core migrations
```

## Running locally

Requires the **.NET 9 SDK** and a **SQL Server** instance.

```bash
cd SmartNotes.Api
dotnet ef database update    # create the database from migrations
dotnet run                   # start the API
```

The API listens on the URL printed in the console (for example `http://localhost:5076`). Try `GET /api/notes`.

## Roadmap

- [x] Controller-based REST API with correct status codes
- [x] EF Core + SQL Server data layer and first migration
- [ ] Async CRUD persisted to the database
- [ ] Validation, DTOs, and centralised error handling
- [ ] Blazor front end
- [ ] Authentication & authorization
- [ ] AI: auto-summarise & auto-tag notes
- [ ] Tests, Docker, CI/CD, and Azure deployment
