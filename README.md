# Interview Question 009 - IT 08-1 Comment Board

A simple comment board built with ASP.NET Core and Vue 3.

## Tech Stack
- Backend: C# / .NET 11, ASP.NET Core Web API
- Database: SQLite, Entity Framework Core 11
- Frontend: Vue 3, TypeScript, Vite, Axios

## Requirements
- .NET 11 SDK
- Node.js 20.19+ or 22.12+

## Run Backend

```powershell
cd backend/ExampleCom.Interview.Api
dotnet restore
dotnet run
```

API: http://localhost:5080  
Swagger: http://localhost:5080/swagger

## Run Frontend

Open another terminal:

```powershell
cd frontend
npm install
npm run dev
```

Open http://localhost:5173

## API

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/posts/1` | Get post and comments |
| POST | `/api/posts/1/comments` | Add a comment |

## Features
- Display IT 08-1 post and image.
- Add comments as Blend 285 by pressing Enter.
- Shift + Enter for a new line.
- Save comments to SQLite.
- Load existing comments from the database.

## Notes
- Database is created and seeded on first startup.
- No login is required for this assignment.
- The project follows the supplied Interview Question 009 requirements.