# My Aanchal NoteBook - Smart Dairy
> An intelligent dairy management platform that uses OCR to automate milk entry tracking for farmers.

## 🔗 Demo


## 📋 Description
My Aanchal NoteBook (Smart Dairy) is a digital solution that helps dairy farmers and dairy owners track their daily milk deposits, earnings, and bonuses. By leveraging optical character recognition (OCR), users can simply upload a picture of their milk slip, and the system automatically extracts vital metrics like Fat, SNF, Quantity, and Rate. This eliminates manual data entry errors and provides users with a comprehensive financial dashboard powered by interactive data visualizations to track their productivity over time.

## ⚙️ Tech Stack
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET_8-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/EF_Core_9-%23690081.svg?style=for-the-badge&logo=.net&logoColor=white)
![Microsoft SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![jQuery](https://img.shields.io/badge/jquery-%230769AD.svg?style=for-the-badge&logo=jquery&logoColor=white)

## ✨ Features
- **Automated Slip Scanning (OCR):** Upload milk receipts and automatically extract Fat, SNF, Quantity, and Rate using Tesseract OCR.
- **Financial Dashboard:** View total 15-day earnings, monthly earnings, and calculated bonuses securely on your dashboard.
- **Interactive Analytics:** Visualize morning and evening milk deposit trends with line and column charts powered by Highcharts.js.
- **Manual Entry & Editing:** Add, update, or delete manual milk entries and maintain accurate historical logs.
- **Secure Authentication:** Create a personalized account, sign in securely, and manage user profile details like Dairy Name and Location.
- **Bonus Calculation System:** Automatically evaluates qualifying deposits to calculate periodic 9.7% bonuses based on milk quality and quantity.

## 📁 Project Structure
- `Controllers/` - Manages backend routing, session authentication, and OCR processing logic.
- `Models/` - Contains core Entity Framework domain models (`User`, `MilkEntry`).
- `Views/` - Contains the HTML/CSHTML presentation layer for the dashboard and registration flows.
- `Data/` - Database context (`ApplicationDbContext`) and Entity Framework configuration.
- `Repository/` - Abstracted data access layer providing `IMilkEntry` and `IRegistration` services.
- `Migrations/` - Entity Framework Core database schema migration files.
- `wwwroot/` - Static assets including CSS, Javascript, uploaded images, and Tesseract (`tessdata`) language packs.

## 🚀 Getting Started

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (Express or standard edition)
- Visual Studio 2022 (recommended) or VS Code

### Installation
1. Clone the repository
```bash
git clone https://github.com/kumarlalit79/My_Aanchal_NoteBook.git
```
2. Navigate to the project directory
```bash
cd My_Aanchal_NoteBook/My_Aanchal_NoteBook
```
3. Update the database connection string in `appsettings.json` to map to your local SQL Server instance.
4. Apply Entity Framework database migrations
```bash
dotnet ef database update
```
5. Run the application
```bash
dotnet run
```

### Environment Variables
| Variable | Description | Example |
| -------- | ----------- | ------- |
| `ConnectionStrings:DefaultConnection` | SQL Server database connection string | `Server=YOUR_SERVER_NAME\SQLEXPRESS;Database=AnchalBook;Trusted_Connection=True;` |

## 🔌 API Endpoints
| Method | Route | Description |
| ------ | ----- | ----------- |
| `GET` | `/Dashboard/GetMilkDataLineChart` | Returns JSON data for morning/evening milk deposit pricing trends |
| `GET` | `/Dashboard/GetMilkDataColumnChart` | Returns JSON data for morning/evening total milk volumes |
| `POST` | `/Dashboard/Upload` | Accepts a multipart image file, runs OCR, and returns parsed milk entry data |
| `POST` | `/Registeration/SignIn` | Authenticates user via phone number and password and returns session status |

## 👤 Author
Lalit Kumar — GitHub: kumarlalit79
