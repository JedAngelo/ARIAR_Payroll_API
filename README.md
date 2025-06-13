# ARIAR_Payroll_API: Backend for ARIAR_PayrollSystem

## Overview

The ARIAR_Payroll_API is the robust backend service for the [ARIAR_PayrollSystem desktop application](https://github.com/jedangelo/ariar_payrollsystem). Built with ASP.NET Core 8.0, this RESTful API handles all data storage, business logic, and authentication for the payroll system. It provides a secure and scalable foundation for managing employee information, attendance, payroll calculations, and user access.

## Features

*   **User Authentication & Authorization:**
    *   Implements ASP.NET Core Identity for user management (registration, login).
    *   Supports role-based authorization (AdminRole, UserRole).
    *   Generates and validates JWT (JSON Web Tokens) for secure API access.
    *   Manages granular permissions for different user roles and UI views.
*   **Employee Management:**
    *   CRUD operations for `PersonalInformation`, `ContactInformation`, and `EmploymentDetail` records.
    *   Management of `Position` entities (add, display).
*   **Biometric Data Management:**
    *   Stores and retrieves employee biometric fingerprint data.
*   **Attendance Tracking:**
    *   Logs daily employee attendance (`MorningIn`, `MorningOut`, `AfternoonIn`, `AfternoonOut`).
    *   Calculates daily attendance status (Present, Absent, Leave, Full/Half Day).
    *   Provides attendance log counts (present, absent, leave).
*   **Payroll Processing:**
    *   Calculates `GrossSalary` and `NetSalary` based on attendance and employment details.
    *   Applies statutory deductions for SSS, PhilHealth, and Pag-IBIG (both employee and employer shares), using pre-defined tables and rates.
    *   Manages additional deductions and commissions.
*   **System Settings:**
    *   Manages core system parameters like attendance type (IN/OUT, FULL), late/early-out cut-off times, and deduction policies.
    *   Includes seed data for initial system settings and SSS contribution brackets.
*   **Data Persistence:**
    *   Utilizes Entity Framework Core for seamless interaction with a SQL Server database.
    *   Includes migrations for easy database schema management.
*   **API Documentation:**
    *   Integrated Swagger UI for interactive API exploration and testing.

## Technologies Used

*   **Backend Framework:** ASP.NET Core 8.0
*   **Database:** SQL Server
*   **ORM:** Microsoft.EntityFrameworkCore 8.0.8
*   **Authentication/Authorization:** Microsoft.AspNetCore.Identity, Microsoft.AspNetCore.Authentication.JwtBearer
*   **API Documentation:** Swashbuckle.AspNetCore
*   **Project Structure:** Clean separation of concerns with a `Payroll_Library` for domain models, DTOs, and business services.

## Project Structure

*   `ARIAR_Payroll_API/`: The main ASP.NET Core Web API project.
    *   `Controllers/`: Defines the API endpoints for different domains (Attendance, Biometric, Employee, UserAuthentication, WeatherForecast).
    *   `Properties/`: Contains `launchSettings.json` for development server configurations.
    *   `appsettings.json`, `appsettings.Development.json`: Configuration files for database connection strings, JWT settings, and logging.
    *   `Program.cs`: Configures services (DB contexts, Identity, JWT authentication, custom services) and sets up the HTTP request pipeline.
*   `Payroll_Library/`: A .NET Standard library containing shared components.
    *   `Models/`: Database entities (e.g., `PersonalInformation`, `Attendance`, `Payroll`, `Position`, `SystemSettings`, `SssMonthlyCredit`).
    *   `Models/Dto/`: Data Transfer Objects (DTOs) for clear and efficient data communication between the API and the client.
    *   `Services/`: Implements the core business logic for Attendance, Employee, and Payroll operations.
    *   `UserAuth/`: Handles ASP.NET Core Identity customization, including `ApplicationUser`, `Permission`, `ApplicationDbContext`, and `UserAuthenticationService`.
    *   `Migrations/`: Entity Framework Core database migration files.

## Setup Instructions

To get the ARIAR_Payroll_API running, follow these steps:

### Prerequisites

1.  **.NET 8.0 SDK:** Download and install the .NET 8.0 SDK from the official Microsoft website.
2.  **SQL Server:** An instance of SQL Server (Express, LocalDB, or full version) is required.
3.  **SQL Server Management Studio (SSMS)** (Optional, but recommended for database inspection).

### Installation

1.  **Clone the Repository:**
    ```bash
    git clone https://github.com/jedangelo/ariar_payroll_api.git
    cd jedangelo-ariar_payroll_api
    ```

2.  **Open in Visual Studio:**
    Open the `ARIAR_Payroll_API.sln` file in Visual Studio (2022 recommended).

3.  **Restore NuGet Packages:**
    Visual Studio should automatically prompt you to restore missing NuGet packages. If not, right-click on the solution in Solution Explorer and select "Restore NuGet Packages".

4.  **Configure Database Connection String:**
    *   Open `ARIAR_Payroll_API/appsettings.json`.
    *   Locate the `"ConnectionStrings"` section.
    *   Update the `"DefaultCon"` value to point to your SQL Server instance.
        ```json
        "ConnectionStrings": {
            "DefaultCon": "Data Source=YOUR_SERVER_NAME\\YOUR_INSTANCE_NAME;Initial Catalog=AriarPayrollDB;Persist Security Info=True;Trusted_Connection=True;TrustServerCertificate=True"
        }
        ```
        Replace `YOUR_SERVER_NAME` and `YOUR_INSTANCE_NAME` with your actual SQL Server details. If you're using SQL Server LocalDB, it might look like `(localdb)\\MSSQLLocalDB`.

5.  **Apply Database Migrations:**
    *   Open the Package Manager Console in Visual Studio (`Tools > NuGet Package Manager > Package Manager Console`).
    *   Set the "Default project" to `Payroll_Library`.
    *   Run the following command to apply the database migrations and create the database schema:
        ```powershell
        Update-Database
        ```
        This command will create the `AriarPayrollDB` database (if it doesn't exist) and apply all defined migrations, including initial seed data for `SystemSettings` and `SSS_Monthly_Credit`.

6.  **Configure JWT Secret Key:**
    *   In `ARIAR_Payroll_API/appsettings.json`, locate the `"JWT"` section.
    *   The `SecretKey` is currently hardcoded for development purposes. For production environments, it is crucial to manage this securely (e.g., via environment variables or Azure Key Vault).
        ```json
        "JWT": {
          "ValidAudience": "User",
          "ValidIssuer": "https://localhost:44376/",
          "Configuration": [
            "JWT:ValidIssuer"
          ],
          "SecretKey": "TheTreesMightLongForPeaceButTheWindWillNeverCeaseTheQuickBrownFoxJumpsOverTheLazyDog"
        }
        ```
        Ensure the `ValidIssuer` value matches the `applicationUrl` (or `sslPort` if using HTTPS) configured in `Properties/launchSettings.json` or your deployment environment.

### Running the API

1.  **From Visual Studio:**
    *   Select `ARIAR_Payroll_API` as the startup project.
    *   Press `F5` or click `Debug > Start Debugging`. This will launch the API and open the Swagger UI in your browser.
2.  **From Command Line:**
    *   Navigate to the `ARIAR_Payroll_API` directory in your terminal.
    *   Run: `dotnet run`
    *   The API will typically run on `http://localhost:5292` (HTTP) and `https://localhost:7144` (HTTPS) by default (check `Properties/launchSettings.json` for exact ports).

### Initial User Setup

After the database is set up and the API is running, you will need to register an initial administrator user. This is typically done via the client application's login screen, which calls the `/api/UserAuthentication/RegisterAdmin` endpoint.

*   **Client App (WinForms):** Use the [ARIAR_PayrollSystem client application](https://github.com/jedangelo/ariar_payrollsystem) to register the first admin. The default credentials expected by the client for the initial admin are:
    *   **Username:** `admin`
    *   **Password:** `admin`

    *Note: It is highly recommended to change these default administrative credentials immediately after the first successful login.*

## API Endpoints

Once the API is running, you can explore the available endpoints using the integrated Swagger UI:

*   **Swagger UI (Development):** `https://localhost:7144/swagger` (or your configured HTTPS/HTTP URL + `/swagger`)

Example Base URL for the client application: `http://localhost:5292` or `https://localhost:7144`.


## License

This project is licensed under the [MIT License](LICENSE)
