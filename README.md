# User & Product Service with JWT Authentication

This project implements **User Authentication** with **JWT** and a **Product Management Service**. The **UserService** allows user registration, login, and logout, while the **ProductService** manages products and requires authentication for access.

## 🚀 Features

- User authentication with JWT 
- Register, login, and logout users
- Secure Product API with JWT-based authorization
- Logging with **ILogger** for tracking requests and errors
- Global exception handling for **500 Internal Server Error**
- ASP.NET Core 8 with Entity Framework Core
- SQL Server as the database
- Swagger API documentation

---

## 📌 Technologies Used

- **.NET 8** (ASP.NET Core Web API)
- **Entity Framework Core** (EF Core)
- **JWT Authentication** (JSON Web Token)
- **Microsoft SQL Server**
- **ILogger** (built-in logging framework)
- **Swagger** (API documentation)

---

## 🛠️ Setup Instructions

### 1️⃣ Clone the Repository

```sh
git clone https://github.com/your-repo-url.git
cd your-project-folder
```

### 2️⃣ Configure Database Connection

Update `appsettings.json` in both **UserService** and **ProductService**:

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
},

"JwtSettings": {
    "Secret": "your-256-bit-secret-key-must-be-long-enough",
    "Issuer": "your-app",
    "Audience": "your-audience",
    "ExpirationInMinutes": "60"
}
```

### 3️⃣ Run Database Migrations

```sh
dotnet ef database update --project UserService.Infrastructure

dotnet ef database update --project ProductService.Infrastructure
```

⚠️ **Note:** Since this project follows the Code-First approach, make sure to run `Update-Database` before launching the services.

### 4️⃣ Run the Project

```sh
dotnet run --project UserService.API
dotnet run --project ProductService.API
```

### 5️⃣ Open Swagger for API Testing

- **User Service:** `http://localhost:5000/swagger`
- **Product Service:** `http://localhost:5001/swagger`

---

## 📜 API Endpoints

### 🔑 Authentication (UserService)

#### 1️⃣ Register a New User

```http
POST /api/auth/register
```

**Request Body:**

```json
{
  "username": "test@user.com",
  "password": "Password123",
  "fullName": "John Doe"
}
```

**Response:**

```json
{ "message": "User registered successfully" }
```

#### 2️⃣ Login and Get JWT Token

```http
POST /api/auth/login
```

**Request Body:**

```json
{
  "username": "test@user.com",
  "password": "Password123"
}
```

**Response:**

```json
{
  "token": "your.jwt.token"
}
```

#### 3️⃣ Logout User

```http
POST /api/auth/logout
```

**Response:**

```json
{ "message": "User logged out successfully" }
```

---

### 📦 Product Management (ProductService)

#### 1️⃣ Get All Products (Requires JWT)

```http
GET /api/products
Authorization: Bearer your.jwt.token
```

**Response:**

```json
[ {
  "productId": 1,
  "productName": "Laptop",
  "description": "Gaming Laptop",
  "imageUrl": "https://example.com/image.jpg",
  "price": 1500.99,
  "quantity": 10
} ]
```

#### 2️⃣ Create a New Product (Requires JWT)

```http
POST /api/products
Authorization: Bearer your.jwt.token
```

**Request Body:**

```json
{
  "productName": "Laptop",
  "description": "Gaming Laptop",
  "imageUrl": "https://example.com/image.jpg",
  "price": 1500.99,
  "quantity": 10
}
```

---

## 📜 Logging & Error Handling

### 🔍 Logging with `ILogger`

The project uses `ILogger` for logging requests and errors. Logs are written to **console** and can be extended to log files or monitoring tools like **Serilog**.

Example usage in controllers:

```csharp
private readonly ILogger<UserController> _logger;

public UserController(ILogger<UserController> logger)
{
    _logger = logger;
}

_logger.LogInformation("User {Username} is logging in", request.Username);
_logger.LogError(ex, "Error occurred while logging in user {Username}", request.Username);
```

### 🚨 Global Exception Handling

A middleware handles **500 Internal Server Errors** and logs the error details.

```csharp
app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            logger.LogError($"Internal Server Error: {contextFeature.Error}");

            await context.Response.WriteAsync(new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error. Please try again later."
            }.ToString());
        }
    });
});
```

---

## 🎯 Notes

- Ensure that **JWT is included in the Authorization header** when accessing ProductService.
- Modify `JwtSettings:Secret` with a secure **256-bit key**.
- Logging can be extended with **Serilog** for file logging and external monitoring.

---

## 📌 License

This project is open-source and available under the **MIT License**.

---

🚀 Happy Coding! If you have any issues, feel free to open an issue or contribute! 🎉

