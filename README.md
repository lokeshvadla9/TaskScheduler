# Task Scheduler App

A Task Scheduling and User Management API built with **.NET Core** and **SQL Server**. This application allows you to create, update, and manage tasks, as well as handle user authentication and authorization using **Bearer JWT tokens**.

---

## Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
- [Database Migrations](#database-migrations)
- [Usage](#usage)
- [API Endpoints](#api-endpoints)
- [Authentication](#authentication)
- [Contributing](#contributing)
- [License](#license)

---

## Features

1. **User Management**  
   - Register new users.  
   - Login with username/password to obtain JWT tokens.  
   - Secure routes using Bearer JWT authentication.

2. **Task Management**  
   - Create, read, update, and delete tasks.  
   - Schedule tasks with due dates and reminders.  
   - (Optional) Assign tasks to specific users or teams.

3. **Bearer Authentication (JWT)**  
   - Protect API endpoints by requiring a valid JWT token.  
   - (Optional) Token refresh flow if implemented.

4. **Role/Permission Management (Optional)**  
   - Basic roles (e.g., Admin, User) can be assigned to control access levels.

---

## Tech Stack

- **.NET Core** (e.g., .NET 6 or later)
- **ASP.NET Core Web API**
- **Microsoft SQL Server** for data storage
- **Entity Framework Core** for data access
- **JWT (JSON Web Tokens)** for secure authentication
- **Swagger** (optional) for API documentation

---

## Prerequisites

- **.NET SDK** (version 6.0 or higher recommended)
- **SQL Server** instance running locally or on a server
- **Visual Studio / Visual Studio Code** or any IDE supporting .NET  
  (optional but recommended)

---

## Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/yourusername/TaskSchedulerApp.git
   cd TaskSchedulerApp
