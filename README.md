Financial Project Solution
==========================

Overview
--------
This project is built using Domain-Driven Design (DDD) and Clean Architecture, and it is organized into several projects to ensure clear separation of concerns and adherence to SOLID principles. The main functionalities include a RESTful API, background processing with Hangfire, and a frontend built with Vue.js and TypeScript.

Requirements
------------
• .NET 8
• SQL Server (for Hangfire storage and EF Core)
• Vue.js (frontend)
• Hangfire and Hangfire.SqlServer NuGet packages

Project Structure
------------
1. API Project:
   - Exposes endpoints to manage stocks, retrieve exchange information, and perform other business operations.
2. Domain Layer (Core):
   - Contains the core business logic, entities (Stock, StockDailyInformation), enums (StockExchange), and extension methods.
3. Application Layer:
   - Implements use cases and interfaces, such as the stock repository, and defines DTOs to transfer data between layers.
4. Infrastructure Layer:
   - Provides the concrete implementations for data access using EF Core, and repositories that implement the interfaces defined in Application.
5. HangfireJobs Project:
   - Contains background jobs (e.g., AlphaVantageUpsertJob) that perform scheduled tasks, such as fetching and upserting stock data from the AlphaVantage API.
6. WorkerService Project:
   - Hosts the Hangfire server. It is configured to schedule and execute background jobs at defined intervals (for example, every hour).
   - The Program.cs file sets up the host, registers Hangfire services, and schedules jobs using IRecurringJobManager.
7. Frontend Project:
   - A Vue.js application built with TypeScript that consumes API endpoints and provides a user interface.

Example Usage
-------------
- Adding a New Stock:
    - The API includes a controller (StockController) that accepts a POST request to add a new stock. 
- Retrieving Stock Exchanges:
   - A GET endpoint in the API returns a list of stock exchanges.
- Background Job Execution:
   - The Hangfire job AlphaVantageUpsertJob, hosted by the WorkerService, fetches data from the AlphaVantage API and performs an upsert operation on the stock daily information (A distributed lock is used to ensure that only one instance of the job runs at a time).

Running the Project
-------------------
1. Build the solution using Visual Studio or the dotnet CLI.
2. Configure the necessary connection strings and API keys via environment variables.
3. Run the API project to start the web service.
4. Run the WorkerService project to start the Hangfire server and execute background jobs.
5. (Optional) Run the Frontend project using npm commands (npm install, npm run dev) to serve the Vue.js application.

Additional Information
----------------------
- This solution adheres to SOLID principles, ensuring that each layer (Core, Application, Infrastructure, API) has well-defined responsibilities.
- The Hangfire integration in the WorkerService guarantees that background tasks are scheduled and executed in a distributed manner, with distributed locks to prevent concurrent executions.
- For further details on Clean Architecture and DDD in .NET, consider exploring related articles and videos, such as "Clean Architecture in ASP.NET Core" and "Implementing DDD with Clean Architecture."

