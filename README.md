# 🌟 habit-tracker

> Final Thesis Project – Full-stack web application built during my Web Development studies.


This project consists of a **RESTful API backend** using **ASP.NET Core** and a **Vue.js frontend**.  
It demonstrates full-stack development skills including API design, frontend integration, and responsive UI design.  

---

## ✨ Features

- CRUD operations via a RESTful API  
- Interactive frontend built with Vue.js  
- Responsive design for desktop and mobile  
- Example data for testing API endpoints  

---

## 🛠 Technologies

**Backend:**  
- ASP.NET Core  
- Entity Framework Core  
- SQL Server / SQLite  

**Frontend:**  
- Vue.js  
- Vue Router  
- Axios  
- TailwindCSS / Bootstrap  

**Tools:**  
- Git, VS Code / Visual Studio  
- Postman for API testing  

---

## 🚀 Getting Started

### Backend Setup

```bash
git clone https://github.com/claskarler/habit-tracker.git
cd habit-tracker/HabitTrackerApi
dotnet restore
```

Configure the database connection in appsettings.json.

Apply migrations:

```bash
dotnet ef database update
```
Run the backend:

```bash
dotnet run
```

Frontend Setup
```bash
cd ../habit-tracker-frontend
npm install
npm run serve
```

Frontend: http://localhost:8080

Backend API: http://localhost:5000 (default)

---

📝 License
This project is for educational purposes only.
