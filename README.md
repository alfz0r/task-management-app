# Task Management Application

This project is a task management system built with:

- **Backend**: ASP.NET Core with Entity Framework Core and PostgreSQL.
- **Frontend**: Vue.js with Vite, TypeScript, and Tailwind CSS.

## Features
- User authentication (register, login, logout)
- Task management (create, read, update, delete)
- Filtering and sorting tasks
- Task editing with updated metadata
- Responsive design using Tailwind CSS

---

## Prerequisites
Ensure you have the following installed:

1. [.NET SDK](https://dotnet.microsoft.com/download/dotnet)
2. [Node.js (LTS)](https://nodejs.org/) and npm
3. [PostgreSQL](https://www.postgresql.org/download/)
4. [Git](https://git-scm.com/)

---

## Backend Setup

### 1. Clone the Repository
```bash
git clone https://github.com/your-repository/task-management-app.git
cd task-management-app 
```

### 2. Clone the Repository

1. Create a PostgreSQL database.
2. Update the connection string in appsettings.json located in the backend project folder:


```json
"ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=TaskDb;Username=yourusername;Password=yourpassword"
}
```
### 3. Apply Migrations

Run the following commands to apply database migrations in the backend folder:


```bash
dotnet ef database update -p TaskManagement.Data -s TaskManagement.Web
```

# 4. Run backend and frontend
- For the backend run `dotnet run ` in the backend folder
- For the frontend in the frontend folder first install dependencies `npm install`, afterwards run `npm run dev`



