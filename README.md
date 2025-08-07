# 💼 JobTracker

A simple yet powerful web application to track your job applications. Built with **ASP.NET Core Razor Pages**, **Entity Framework Core**, and **xUnit** for testing. Ideal for job seekers who want a clean and structured way to monitor their job hunt progress.

---

## 🚀 Features

- ✅ Create, edit, and delete job applications  
- 🔍 Search by company or role  
- 🗂️ Filter by application status  
- 📊 Dashboard with status summaries  
- 📤 Export your applications to CSV  
- 🔐 Secure authentication with ASP.NET Identity  
- 🧪 Unit-tested with xUnit and InMemory DB  

---

## 📸 Screenshots

<!-- Add screenshot images and update paths accordingly -->
<!-- ![Create Application](screenshots/create.png) -->
<!-- ![Dashboard](screenshots/dashboard.png) -->

---

## 🛠️ Tech Stack

| Layer             | Technology                         |
|------------------|-------------------------------------|
| Backend          | ASP.NET Core 8.0 (Razor Pages)      |
| Frontend         | HTML, CSS (Bootstrap), Razor        |
| ORM              | Entity Framework Core               |
| Database         | InMemory (testing), SQLite/Azure DB |
| Authentication   | ASP.NET Identity                    |
| Testing          | xUnit, EF Core InMemory Provider    |

---

## ⚙️ Getting Started

### 🧱 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- (Optional) SQLite if using a local DB

### 📦 Clone the Repository

```bash
git clone https://github.com/EoinMc1502/JobTracker.git
cd JobTracker

dotnet restore
dotnet build
dotnet run --project src/JobTracker

App will run at: http://localhost:xxxx (check terminal output)

dotnet test



🧪 Sample Test Cases
Unit tests for:
GetApplicationsByUserIdAsync
GetStatusCountsAsync
Uses InMemoryDatabase to simulate a real EF Core context during tests.

📁 Project Structure
JobTracker/
├── src/
│   └── JobTracker/           # Main ASP.NET Core Razor app
├── tests/
│   └── JobTracker.Tests/     # xUnit test project
└── JobTracker.sln            # Solution file


📌 Status Types
public enum ApplicationStatus
{
    Applied,
    Interviewing,
    Offer,
    Rejected
}




👨‍💻 Author
Eoin Mc
GitHub

📄 License
This project is licensed under the MIT License.

📈 Coming Soon
🌐 Live deployment (Azure or Vercel)
🧭 Pagination & sorting
🎨 UI/UX polish with Tailwind or Bootstrap
🛡️ More security hardening
📥 Resume & cover letter uploads
🔄 Email reminders
