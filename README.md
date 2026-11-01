# Films — React + ASP.NET Core Web API

- `WebApplication1/` — ASP.NET Core Web API (EF Core, SQL Server LocalDB, controller → service → repository → DB).
- `client/` — React (Vite) client: login / registration and a films page (list, Add Film, Remove).

## Requirements

- .NET SDK 10
- SQL Server LocalDB (`(localdb)\MSSQLLocalDB`, comes with Visual Studio)
- Node.js 20+

## Run

### 1. Server

```bash
dotnet tool restore
dotnet ef database update --project WebApplication1
dotnet run --project WebApplication1 --launch-profile http
```

API: `http://localhost:5199`, Swagger: `http://localhost:5199/swagger`.
The connection string is in `WebApplication1/appsettings.json`; allowed client origins are in `ClientOrigins`.

### 2. Client

```bash
cd client
npm install
npm run dev
```

Open `http://localhost:5173`. The Vite dev server proxies `/api` to `http://localhost:5199`;
to call the API directly set `VITE_API_URL` (see `client/.env.example`).

## Screenshots

Folder [`screenshots/`](screenshots):

| File | What it shows |
|------|---------------|
| `01-login-form.png` | Login form |
| `02-register-form.png` | Registration form (fields match `UserRegisterContract`) |
| `03-register-success.png` | Successful registration message |
| `04-user-saved-in-db.png` | Registered user saved in the database (`GET /api/auth/users`) |
| `05-login-error.png` | Error message for wrong login data |
| `06-movies-page-after-login.png` | Films page opened after successful login |
| `07-add-film-form.png` | Add Film form |
| `08-film-added.png` | Film added to the list |
| `09-films-list.png` | Film cards |
| `10-add-film-validation-error.png` | Server validation error when adding a film |
| `11-after-reload-saved.png` | Films are still there after page reload (saved in DB) |
| `12-after-remove.png` | List after removing a film (Joker) |
| `13-after-remove-reload.png` | Removal persisted after reload |
| `14-swagger-api.png` | Server API endpoints (Swagger) |

## API

| Method | URL | Description |
|--------|-----|-------------|
| POST | `/api/auth/register` | Register (`UserRegisterContract`) |
| POST | `/api/auth/login` | Login (`UserLoginContract`) |
| GET | `/api/auth/users` | List users |
| GET | `/api/movie/movies` | List films |
| GET | `/api/movie/movies/{id}` | Get film |
| POST | `/api/movie/movies` | Add film (`MovieRequest`) |
| DELETE | `/api/movie/movies/{id}` | Remove film |
