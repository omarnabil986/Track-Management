# Track Management System

A Track Management API (.NET 8, Clean Architecture, EF Core) with an Angular front-end, built
for a music distribution platform managing artists, tracks, and DSP distributions.

## Tech Stack

- **Backend:** .NET 8 Web API, EF Core, SQL Server, AutoMapper, JWT Authentication
- **Frontend:** Angular

## Prerequisites

- .NET 8 SDK
- SQL Server (local instance or container)
- Node.js + Angular CLI (`npm install -g @angular/cli`)

## Backend Setup

1. Update the connection string in `TakweneTrackManagement.API/appsettings.json`:

```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=.;Database=TrackManagement;Trusted_Connection=True;TrustServerCertificate=True"
   }
```

2. Apply migrations (run from the solution root):

dotnet ef database update -p TakweneTrackManagement.Infrastructure -s TakweneTrackManagement.API

3. Run the API:

cd TakweneTrackManagement.API
dotnet run

The database is automatically migrated and seeded on first run with sample data:
3+ artists, 8+ tracks across different genres/statuses, and 3 DSPs.

4. The API is available at `https://localhost:7053`.
   Swagger UI: `https://localhost:7053/swagger`.

## Obtaining a JWT Token

1. Send `POST /api/Authentication/login` with valid credentials.
   {
   "email": "omar@gmail.com",
   "password": "P@ssw0rd"
   }

2. Copy the `token` value from the response body.
3. **In Swagger:** click **Authorize**, enter `Bearer <token>`, confirm.
4. **In Postman:** add header `Authorization: Bearer <token>` to protected requests.

**Protected endpoint:** `GET /api/artists` requires a valid JWT — include the
`Authorization: Bearer <token>` header when calling it, or it will return `401 Unauthorized`.

## Frontend Setup

cd track-management-ui
npm install
ng serve

Runs at `http://localhost:4200`. Make sure the API is running first — the frontend calls
`https://localhost:7053/api`.

## Endpoints

| Method | Route                       | Description                               |
| ------ | --------------------------- | ----------------------------------------- |
| POST   | /api/artists                | Create an artist                          |
| GET    | /api/artists                | List all artists                          |
| POST   | /api/tracks                 | Create a track                            |
| GET    | /api/tracks                 | List tracks (`?status=&artistId=&genre=`) |
| GET    | /api/tracks/{id}            | Get track detail incl. DSP distributions  |
| POST   | /api/tracks/{id}/distribute | Submit a track to one or more DSPs        |
| PATCH  | /api/tracks/{id}/status     | Update a track's status                   |

## Notes

See `DECISIONS.md` for AI usage during development, security issues found, and fixes made.
