# Reservation-system
Reservation System is a backend REST API for managing date reservations, 
built with ASP.NET Core and .NET 10.

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
<img src="https://github.com/Simon0824/reservation-system/blob/3df63f49116a072e1dadb8e8c49fe6e1aed48783/Screen1.png" width="700" height="500"/>


### User Login (POST)
User authentication endpoint returning JWT token.
<img src="https://github.com/Simon0824/reservation-system/blob/3df63f49116a072e1dadb8e8c49fe6e1aed48783/Screen2.png" width="700" height="500"/>


### Entering JWT Token for authorization
Example of accessing secured endpoint using Bearer token.
<img src="https://github.com/Simon0824/reservation-system/blob/3df63f49116a072e1dadb8e8c49fe6e1aed48783/Screen3.png" width="700" height="500"/>


### Create Reservation (POST)
Example request for creating a reservation.
<img src="https://github.com/Simon0824/reservation-system/blob/3df63f49116a072e1dadb8e8c49fe6e1aed48783/Screen4.png" width="700" height="500"/>


### Get All Users with Reservations (GET)
Request for checking all created users and which dates are assigned to them.
<img src="https://github.com/Simon0824/reservation-system/blob/3df63f49116a072e1dadb8e8c49fe6e1aed48783/Screen5.png" width="700" height="500"/>


## Features
- User registration and login system
- JWT-based authentication
- Create and manage reservations
- Store users and reservations in SQLite database
- Access protected endpoints using authorization
- Swagger UI with JWT authentication support

## How to run
1. Clone the repository
   git clone https://github.com/Simon0824/reservation-system
2. Add JWT secret to user-secrets:
   - dotnet user-secrets set "CreatingToken:Token" "Your_Secret"
   - dotnet user-secrets set "CreatingToken:Issuer" "Your_Issuer"
   - dotnet user-secrets set "CreatingToken:Audience" "Your_Audience"
3. Run the project:
   dotnet run
4. Open Swagger UI:
   http://localhost:{port}/swagger/index.html
