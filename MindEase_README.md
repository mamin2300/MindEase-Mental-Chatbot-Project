# 🧠 MindEase

A mental wellness web app built for university students, featuring an AI-powered chatbot, mood tracking, and a crisis triage system with real-time risk detection — backed by a secure admin portal for managing alerts.

> Built as a collaborative academic project at Sheridan College, Winter 2025.

---

## Key Features

- **AI Chatbot** — Empathetic support powered by LLaMA 3.3 via Groq API
- **Crisis Triage** — Real-time keyword-based risk detection and escalation
- **Mood Tracking** — Log and visualize emotional trends over time
- **Analytics Dashboard** — Usage metrics and mood insights for admins
- **Admin Portal** — Role-based access with crisis alert management
- **Authentication** — Secure login and registration for students and admins

---

## Tech Stack

- ASP.NET Core MVC
- C#
- SQLite
- Entity Framework Core
- Groq API (LLaMA 3.3)
- HTML / CSS / JavaScript

---

## Getting Started

### Prerequisites
- Visual Studio 2022 or later
- .NET 8 SDK
- Groq API Key

### Running the Project

1. Clone the repository
   ```bash
   git clone https://github.com/mamin2300/MindEase.git
   ```
2. Open the solution in Visual Studio
3. Add your Groq API key to `appsettings.json`
4. Apply database migrations
   ```bash
   dotnet ef database update
   ```
5. Press **F5** to run

---

## Project Structure

```
MindEase/
├── Controllers/          # MVC Controllers
├── Models/               # Entity models
├── Views/                # Razor views
├── Services/             # Business logic & AI integration
├── Migrations/           # EF Core migrations
└── MindEaseDbContext     # Database context
```

---

## Team

Collaborative academic project — Sheridan College, Winter 2025.

| Name | 
|------|
| Mamin |
| Malika Muskan |
| Azm Khan |
| Shaymaa |

---

*Built with care and a lot of coffee ☕*
