# Reservation-system
Reservation system is a simple project that demonstrates how a reservation app could work.
This is my first WEB API program created to learn backend development

## Used technologies and frameworks
### JWT(JSON Web Token)
Custom configuration of Bearer Token to create secure authentication
Token validation using TokenValidationParameters and integrated with Swagger for testing secured endpoints.

### ASP.NET Identity 
Used for user registration and login functionality, managed with UserManager and SignInManager.
Responsible for hashing user passwords and storing in SQlite database

### Entity Framework Core(EF Core)
Used as an ORM to communicate with a SQLite database.  
Handles database access through a DbContext and manages schema changes using migrations. 

## API Preview
### Swagger UI
Overview of all available endpoints.
<img src="https://github.com/Simon0824/reservation-system/blob/3df63f49116a072e1dadb8e8c49fe6e1aed48783/Screen1.png" width="1200" height="1000"/>

### Login to Account (POST)
User authentication endpoint returning JWT token.
<img src="https://github.com/Simon0824/reservation-system/blob/3df63f49116a072e1dadb8e8c49fe6e1aed48783/Screen2.png" width="1200" height="1000"/>

### Typing token for authorization (JWT)
Example of accessing secured endpoint using Bearer token.
<img src="https://github.com/Simon0824/reservation-system/blob/3df63f49116a072e1dadb8e8c49fe6e1aed48783/Screen3.png" width="1200" height="1000"/>

### Create Reservation (POST)
Example request for creating a reservation.
<img src="https://github.com/Simon0824/reservation-system/blob/3df63f49116a072e1dadb8e8c49fe6e1aed48783/Screen4.png" width="1200" height="1000"/>

### Get all created profiles with their reservations (GET)
Request for checking all created users and which dates are assigned to them.
<img src="https://github.com/Simon0824/reservation-system/blob/3df63f49116a072e1dadb8e8c49fe6e1aed48783/Screen5.png" width="1200" height="1000"/>

## Features
- User registration and login system
- JWT-based authentication
- Create and manage reservations
- Store users and reservations in SQLite database
- Access protected endpoints using authorization
- Swagger UI with JWT authentication support

