# User & Product Service with JWT Authentication

This project implements **User Authentication** with **JWT** and a **Product Management Service**. The **UserService** allows user registration, login, and logout, while the **ProductService** manages products and requires authentication for access.

## 🚀 Features

- User authentication with JWT 
- Register, login, and logout users
- Secure Product API with JWT-based authorization
- ASP.NET Core 8 with Entity Framework Core
- SQL Server as the database
- Swagger API documentation

---

## 📌 Technologies Used

- **.NET 8** (ASP.NET Core Web API)
- **Entity Framework Core** (EF Core)
- **JWT Authentication** (JSON Web Token)
- **Microsoft SQL Server**
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
POST /api/user/register
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
POST /api/user/login
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
POST /api/user/logout
```

**Response:**

```json
{ "message": "User logged out successfully" }
```

#### 4️⃣ Get User Information (Requires JWT)

```http
GET /api/user/id?userId={GUID}
Authorization: Bearer your.jwt.token
```

**Response:**

```json
{
  "userId": "GUID",
  "username": "test@user.com",
  "fullName": "John Doe"
}
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

#### 2️⃣ Get Product By ID (Requires JWT)

```http
GET /api/products/id?id=1
Authorization: Bearer your.jwt.token
```

**Response:**

```json
{
  "productId": 1,
  "productName": "Laptop",
  "description": "Gaming Laptop",
  "imageUrl": "https://example.com/image.jpg",
  "price": 1500.99,
  "quantity": 10
}
```

#### 3️⃣ Create a New Product (Requires JWT)

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

#### 4️⃣ Update Product (Requires JWT)

```http
PUT /api/products/id?id=1
Authorization: Bearer your.jwt.token
```

**Request Body:**

```json
{
  "productName": "Updated Laptop",
  "description": "Updated Description",
  "imageUrl": "https://example.com/new-image.jpg",
  "price": 1400.99,
  "quantity": 5
}
```

#### 5️⃣ Delete Product (Requires JWT)

```http
DELETE /api/products/id?id=1
Authorization: Bearer your.jwt.token
```

**Response:**

```json
{ "message": "Product deleted successfully" }
```

---

## 🎯 Notes

- Ensure that **JWT is included in the Authorization header** when accessing ProductService.
- Modify `JwtSettings:Secret` with a secure **256-bit key**.

---

## 📌 License

This project is open-source and available under the **MIT License**.

---

🚀 Happy Coding! If you have any issues, feel free to open an issue or contribute! 🎉

