# 🛒 Basic Online Store

This is a full-stack e-commerce web application built with **ASP.NET Core Web API** and **React**. It allows users to browse products, add items to their cart, and place orders. Admin users can manage products through a basic panel.

---

## 🚀 Features

### 👥 User
- User registration and login (JWT-based)
- View products and product details
- Add/remove items to/from shopping cart
- Place and view past orders

### 🛠 Admin
- Add, update, and delete products
- Manage categories

---

## 🧱 Tech Stack

### Backend
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **PostgreSQL** or **SQL Server**
- **JWT Authentication**

### Frontend
- **React** + **Vite**
- **Axios** for API calls
- **Tailwind CSS** for styling
- **React Router DOM** for page navigation

### Tools & Services
- **Postman** (API Testing)
- **Stripe (test mode)** for payment simulation
- **Docker** (optional)

---

## 📦 Database Structure

- **User**
  - `Id`, `Name`, `Email`, `PasswordHash`, `Role`
- **Product**
  - `Id`, `Name`, `Description`, `Price`, `ImageUrl`, `CategoryId`
- **Category**
  - `Id`, `Name`
- **CartItem**
  - `Id`, `UserId`, `ProductId`, `Quantity`
- **Order**
  - `Id`, `UserId`, `CreatedAt`, `TotalPrice`
- **OrderItem**
  - `Id`, `OrderId`, `ProductId`, `Quantity`, `Price`

---

## 📂 Project Structure

