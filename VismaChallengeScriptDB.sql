

/*
drop table RolsUser
Drop table RolPermissions
Drop Table Permissions
drop table Rols
drop table Departments
drop table Users*/

create table Users
(
	Id int identity(1,1) primary key,
	Name varchar(150),
	Email varchar(150),
	Password varchar(300),
	Salt varchar(300),
	DepartmentId int
)

insert into users values ('user 1','user1@email.com','123456','',1)



create table Rols
(
	Id int identity(1,1) primary key,
	Description varchar(150)
)

insert into Rols values ('Adim')
insert into Rols values ('Employee')
insert into Rols values ('Manager')

delete from Rols where id>3

create table RolsUser
(
	Id int identity(1,1) primary key,
	RolId int,
	UserId int
)


insert into RolsUser
select 2, id from users

insert into RolsUser values (1,5)



ALTER TABLE [dbo].[RolsUser]  WITH CHECK ADD  CONSTRAINT [FK_RolsUser_Rol] FOREIGN KEY([RolId])
REFERENCES [dbo].[Rols] ([Id])
GO
ALTER TABLE [dbo].[RolsUser] CHECK CONSTRAINT [FK_RolsUser_Rol]

ALTER TABLE [dbo].[RolsUser]  WITH CHECK ADD  CONSTRAINT [FK_RolsUser_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[RolsUser] CHECK CONSTRAINT [FK_RolsUser_User]


create table Permissions
(
	Id int identity(1,1) primary key,
	Description varchar(150)
)

insert into Permissions values ('Read')
insert into Permissions values ('Add')
insert into Permissions values ('Update')
insert into Permissions values ('Delete')


create table RolPermissions
(
	Id int identity(1,1) primary key,
	RolId int,
	PermissionId int
)


insert into RolPermissions values (1,1)
insert into RolPermissions values (1,2)
insert into RolPermissions values (1,3)
insert into RolPermissions values (1,4)
insert into RolPermissions values (2,1)
insert into RolPermissions values (3,1)
insert into RolPermissions values (3,2)
insert into RolPermissions values (3,3)
insert into RolPermissions values (3,4)


ALTER TABLE [dbo].[RolPermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolPermissions_Rol] FOREIGN KEY([RolId])
REFERENCES [dbo].[Rols] ([Id])
GO
ALTER TABLE [dbo].[RolPermissions] CHECK CONSTRAINT [FK_RolPermissions_Rol]

ALTER TABLE [dbo].[RolPermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolPermissions_Permission] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[Permissions] ([Id])
GO
ALTER TABLE [dbo].[RolPermissions] CHECK CONSTRAINT [FK_RolPermissions_Permission]

create table Departments
(
	Id int identity(1,1) primary key,
	Description varchar(150),
	UserManagerId int
)

insert into Departments values('Lawful',1)
insert into Departments values('IT',2)


ALTER TABLE [dbo].[Departments]  WITH CHECK ADD  CONSTRAINT [FK_Departments_Manager] FOREIGN KEY([UserManagerId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Departments] CHECK CONSTRAINT [FK_Departments_Manager]


