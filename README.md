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

<h3>Mongo<h3>
There are 2 tables in DB. Cards and CardsActivities.

<img width="787" alt="Ekran Alıntısı" src="https://user-images.githubusercontent.com/80174519/208326071-dbd8c55b-2821-43b3-98a5-feb2da723aba.PNG">

<h3>Hangfire</h3>
<img width="810" alt="2" src="https://user-images.githubusercontent.com/80174519/208326232-09a15740-ee5e-4fc7-9808-605c805d47dd.PNG">


Sign in or register with auth. In this way, coins are obtained.
![image](https://user-images.githubusercontent.com/80174519/208325021-efcfc7e7-d043-42f6-add3-c77481cb549d.png)

to pay bills and dues
![image](https://user-images.githubusercontent.com/80174519/208325160-ef93dd96-0eb5-4240-98c7-d24f0cf181f4.png)

Creating excel for adding-deleting cards and card transactions
![image](https://user-images.githubusercontent.com/80174519/208325169-39beddd0-a784-4a61-88c5-33543e348b3e.png)

In addition, flat dues can be seen or all dues can be listed. It may also list unpaid or paid dues.
![image](https://user-images.githubusercontent.com/80174519/208325041-405ac361-9fc2-41d8-bca5-fc22aca22b6d.png)

Processing is carried out according to the flat.
![image](https://user-images.githubusercontent.com/80174519/208325068-b6061fc1-5978-4db5-8a30-7dca2c96e0dd.png)

Processing is carried out according to the flat types.
![image](https://user-images.githubusercontent.com/80174519/208325086-4101d97b-4683-428f-af89-c1a0445d3d19.png)

In addition, flat invoices can be seen or all invoices can be listed. It may also list unpaid or paid invoices.
![image](https://user-images.githubusercontent.com/80174519/208325096-86375cc5-0809-42c8-9c75-e2c11d2448d1.png)

Processing is carried out according to the invoice types.
![image](https://user-images.githubusercontent.com/80174519/208325107-283b2219-fd65-4e0c-b9be-4980f8deed38.png)

Read or unread messages can be listed or messages sent to the user can be listed.
![image](https://user-images.githubusercontent.com/80174519/208325118-debfe4ce-7415-48f6-ac8d-57efdaf85aa6.png)

Role-related operations are performed for authorization
![image](https://user-images.githubusercontent.com/80174519/208325133-b5b6fab8-5f1c-446b-9ba5-6df0ee7c3240.png)

User-related transactions are carried out
![image](https://user-images.githubusercontent.com/80174519/208325144-86f92419-f041-4767-918b-445bb0fcfc75.png)

