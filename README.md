# VismaChallenge
Challenge For Visma IT Department

Challenge for Visma Company This API is deployed on Azure WebApp using a Azure Sql database. You cant see it in https://vismachallenge.azurewebsites.net/index.html. The credentials for a Admin user are "Email":"pablo@email.com" and "password":"123456". Questions
I could´t continue with the moqs test becouse today is the bithday of my daugther and i was very busy, but if you want, i can add more test methods to de test Project.
The following tools were used in this project:
●	Autommaper
●	Fluent Api
●	Entity Frameworks
●	Dependency injections
●	Fluent Validations
●	Generic Repository
●	Security API with JWT
●	Hashing Passwords
●	Swagger Documentation


Questions 
●	 Could you explain which design patterns have you used and what is the purpose of them? 
In this proje●	ct I used several design patterns. The first is dependency injection, allowing less coupling between project objects. The second design pattern that I used was UnitToWork, t●	o keep the context of the database in a single variable and to be able to work the changes in memory until they were confirmed.
●	 If we wanted to persist that information in any storage, could you please explain which type of storage you would choose and how it looks?
To store this information, it uses an sql database, because by type of information that is saved, there would not be too many structural changes. In this repository you can find the sql script with the database structure.
