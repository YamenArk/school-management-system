# 🏫 School Management System

A modular and scalable ASP.NET Core Web API for managing users, courses, assignments, and media in a school system. The system supports role-based access (Admin, Teacher, Student), file uploads, and background video processing.

---

## 🚀 Features

- Role-based JWT Authentication (Admin, Teacher, Student)
- Courses & Assignments Management
- File Uploads (PDF, Image, Audio, Video with queue)
- Redis Queue for video processing
- Clean architecture using Services, UseCases, and Infra layers
- FluentValidation and Swagger integration

---

## 🛠️ Local Setup (Manual)

1. **Clone the repository**
   ```bash
   git clone https://github.com/your-username/school-management-system.git
   cd school-management-system
Database Name: schoolmanagementsystem
dotnet ef database update
dotnet run
