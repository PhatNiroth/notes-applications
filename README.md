# Notes Application

Full-stack notes app with CRUD operations and JWT authentication.

## Tech Stack

**Frontend**
- Vue 3 + TypeScript
- Vite
- Vue Router
- Pinia (state management)
- Axios (HTTP client)
- Tailwind CSS

**Backend**
- ASP.NET Core 8 Web API
- Dapper (micro-ORM)
- JWT Authentication
- BCrypt (password hashing)

**Database**
- SQL Server 2022 (Docker)

## Features

- Register / Login with JWT
- Create, read, update, delete notes
- Each user only sees their own notes
- Search, filter, and sort notes
- Responsive design
- Cambodia timezone display

## Setup

### 1. Database (Docker)

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Notes@12345" \
  -p 1433:1433 --name sqlserver \
  -d mcr.microsoft.com/mssql/server:2022-latest
```

Create tables:

```sql
CREATE DATABASE NotesDb;
USE NotesDb;

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(100) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME DEFAULT GETUTCDATE()
);

CREATE TABLE Notes (
    Id INT PRIMARY KEY IDENTITY,
    UserId INT NOT NULL,
    Title NVARCHAR(255) NOT NULL,
    Content NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
```

### 2. Backend

```bash
cd backend
dotnet restore
dotnet run
```

Backend runs at `http://localhost:5051`

### 3. Frontend

```bash
cd frontend
npm install
npm run dev
```

Frontend runs at `http://localhost:5173`

## Configuration

Update `backend/appsettings.json` with your SQL Server connection string and JWT settings.
