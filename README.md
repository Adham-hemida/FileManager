# 📁 FileManager API

A lightweight and flexible File Management REST API built with **ASP.NET Core (.NET 9)**.  
This service enables uploading, downloading, streaming, and storing files and images with database tracking.

## 🚀 Features

- ✅ Upload single or multiple files
- 🖼 Upload image files to a separate directory
- 📥 Download files by ID
- 📺 Stream files directly from the server
- 🗃 Save file metadata in the database
- 🧪 Built with Clean Architecture principles

---

## 🛠 Tech Stack

| Tech                | Description                                 |
|---------------------|---------------------------------------------|
| ASP.NET Core (.NET 9) | Web API framework                         |
| Entity Framework Core | ORM for database interaction             |
| SQL Server           | Database                                   |
| FluentValidation     | Input validation                          |
| Swagger / Scalar     | API documentation                         |

---

## 📦 Endpoints

| Method | Endpoint                         | Description                     |
|--------|----------------------------------|---------------------------------|
| POST   | `/api/files/upload`             | Upload a single file            |
| POST   | `/api/files/upload-many`        | Upload multiple files           |
| POST   | `/api/files/upload-image`       | Upload an image file            |
| GET    | `/api/files/download/{id}`      | Download a file by ID           |
| GET    | `/api/files/stream/{id}`        | Stream a file by ID             |

---

## 📂 File Storage Structure

- Files are saved to the `wwwroot/uploads` directory.
- Images are saved to the `wwwroot/images` directory.
- Metadata (filename, content type, etc.) is stored in the database (`UploadedFile` entity).



