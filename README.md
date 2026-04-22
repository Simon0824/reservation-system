# Reservation-system
<hr>
Reservation system is a simple project that demonstrates how a reservation app could work.
This is my first WEB API program created to learn backend development

## Used technologies and frameworks
<hr>
### JWT(JSON Web Token)
Custom implementation of Bearer Token to create safe authentication, logging a created user existing in database,
based on user data provided by  ASP.NET Identity configured with TokenValidationParameters and integrated with Swagger for testing endpoints.

### ASP.NET Identity 
Used for user registration and login functionality, managed with UserManager and SignInManager.
Responsible for hashing user passwords and storing in SQlite database

### Entity Framework Core(EF Core)
Used as an ORM to communicate with a SQLite database.  
Handles database access through a DbContext and manages schema changes using migrations. 
