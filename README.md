# eBookStore

## Overview

Welcome to the Bulky Book repository! This project is a comprehensive ASP.NET Core web application demonstrate advanced concepts in ASP.NET Core development. The application is a fully functional e-commerce website where customers can browse book, add them to their cart, and complete purchases using credit cards. Admins can manage orders and handle transactions.

This project covers a wide range of topics and follows best  practices to create a robust, scalable, and maintainable application.

## Features

- **`ASP.NET Core MVC`**: Structure the project using the Model-View-Controller pattern.
- **`Identity Security`**: Implement secure authentication and authorization.
- **`Repository Pattern`**: Separate data access logic using repositories.
- **`N-Tier Architecture`**: Organize the application into layers (Presentation, Business Logic, Data Access).
- **`Identity Framework Integration`**: Extend the user model with additional fields.
- **`Entity Framework & Code-First Migrations`**: Manage the database schema and data access.
- **`Authentication & Authorization`**: Secure the application with ASP.NET Core Identity.
- **`Email Notifications`**: Send emails for various application events.
- **`Social Logins**: Integrate Facebook login.
- **`Payment Processing`**: Accept payments using Stripe.
- **`Sessions**: Manage user sessions effectively.
- **`View Components & TempData`**: Utilize advanced MVC features.
- **`Data Seeding`**: Populate the database with initial data.

## Project Structure

The project follows an N-Tier architecture with the following layers:

- **`Presentation Layer`**: Contains the ASP.NET Core MVC views and controllers.
- **`Business Logic Layer`**: Contains the service classes and business logic.
- **`Data Access Layer`**: Contains the repository classes and Entity Framework DbContext.

## Technologies Used

- ASP.NET Core
- Entity Framework Core
- ASP.NET Core Identity
- Stripe for payment processing

## Getting Started

To get started with this project, follow these steps:

1. **Clone the repository:**
    ```bash
    git clone https://github.com/yourusername/bulky-book.git
    cd bulky-book
    ```

2. **Install dependencies:**
    ```bash
    dotnet restore
    ```

3. **Update the appsettings.json file:**
    Configure your database connection string, Stripe API keys, and other settings in the `appsettings.json` file.

4. **Apply migrations and seed data:**
    ```bash
    dotnet ef database update
    ```

5. **Run the application:**
    ```bash
    dotnet run
    ```

## Contributing

Contributions are welcome! Please fork the repository and submit pull requests for any enhancements or bug fixes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

## Contact

For any questions or feedback, please contact taushif1teza@gmail.com.

---

Thank you for checking out the Bulky Book project! Happy coding!
