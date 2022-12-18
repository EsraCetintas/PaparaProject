# PaparaProject

It includes the invoice management system as a graduation project for the .NET Core bootcamp organized by the partnership of Patika and Papara. In this project :

- .NET 5 used
- Onion architecture is preferred.
- MSSQL was used as database.
- Entity framework is preferred as ORM. It was developed with the code first approach.
- Generic repository pattern used.
- The payment service was developed using mongo and was written as a separate service.
- Database operations were performed using Mongo over the cloud. By logging in via the cloud, the database can be accessed with the user information specified in the project.
- Role-based authorization was made. JWT preferred for authentication.
- Fluentvalidation library was used for validation.
- InMemory Cache preferred for caching.
- Caching, validation, authorization and authentication are provided by the Aspect-Oriented Programming approach.
- Autofac library is used for Aspect Oriented Programming.
- Hangfire was preferred for business management. In this way, daily e-mails are sent to people who do not pay their bills.
- Excel was created to document card movements.
- Automapper was used for dtos
- Exception middleware  to catch for errors
- Extensions have been written for the Dependency Extension.


