use master
go

if db_id('Ex.5.1_SchoolDatabase') is not null
drop database [Ex.5.1_SchoolDatabase]

create database [Ex.5.1_SchoolDatabase]
go

use [Ex.5.1_SchoolDatabase]
go

create table Roles (
	IDRole int primary key identity(1,1) not null,
	Label nvarchar(100) not null check (Label in ('Teacher', 'Student')),
	Description nvarchar(500) not null,
)
go

create table People (
	IDPeople int primary key identity(1,1) not null,
	FirstName nchar(50) not null,
	LastName nchar(50) not null,
	BirthDate date not null check (Year(BirthDate) > 1960 and Year(BirthDate) < year(getdate())),
	Roles int not null references Roles (IDRole)
)
go

create table CurricularUnits (
	IDCurricularUnit int primary key identity(1,1) not null,
	Name nvarchar (100) not null,
	Objectives nvarchar(500) not null,
)
go

create table ClassDetails (
	IDClassDetail int primary key identity(1,1) not null,
	Name nvarchar(80) not null,
	Year int not null,
	Teacher int not null references People(IDPeople),
	CurricularUnit int not null references CurricularUnits(IDCurricularUnit),
)
go

create table Classes (
	IDClass int references ClassDetails(IDClassDetail) ,
	Student int not null references People(IDPeople),
)
go

insert into Roles values
('Student', 'Person who is learning'),
('Teacher', 'Person who is teaching')
go

insert into People values
('Mark', 'Otto', '2000-11-20', 2),
('Jacob', 'Thornton', '2002-05-18', 2),
('Larry', 'the Bird', '2001-09-30', 2),
('Edward', 'Cullen', '1996-03-02', 1),
('Adam', 'Carlsen', '1989-10-12', 1),
('Chuck', 'Bass', '2000-03-15', 2),
('Nathaniel', 'Archibald', '1999-04-05', 2),
('Alex', 'Volkov', '1995-12-25', 2),
('Dante', 'Russo', '1993-07-08', 2),
('Levi', 'Ward', '1992-04-21', 2),
('Rhys', 'Larsen', '1992-07-26', 2),
('Noah', 'Slade', '1995-05-09', 2),
('Rowan', 'Kane', '1995-11-23', 2),
('Declan', 'Kane', '1989-09-03', 1),
('Callum', 'Kane', '1991-06-07', 1),
('Jack', 'Smith', '1986-11-30', 1),
('Atlas', 'Corrigan', '1997-02-15', 2),
('Santiago', 'Alatorre', '1995-06-30', 2),
('Maggie', 'Gloria', '2003-01-05', 2),
('Nannie', 'Nellie', '1997-08-20', 2),
('Olive', 'Carlsen', '1996-08-28', 2),
('Taylor', 'Swift', '1989-12-13', 1),
('Mercedes', 'Benz', '1984-07-22', 1),
('Iris', 'Declan', '1994-03-14', 1),
('Lana', 'Mayers', '1997-02-01', 1),
('Rachel', 'Green', '1991-09-05', 1),
('Monica', 'Geller', '1990-11-15', 1),
('Amy', 'Santiago', '1998-11-21', 1),
('Katniss', 'Everdeen', '2003-07-23', 2),
('Aaron', 'Blackford', '1993-12-17', 1)
go

insert into CurricularUnits values 
('Introduction to Computer Science', 'To provide an overview of fundamental concepts in computer science and programming.'),
('Data Structures and Algorithms', 'To study the organization and manipulation of data and algorithms for problem-solving.'),
('Database Systems', 'To understand the principles and practices of database design and management.'),
('Software Engineering', 'To learn the methodologies and best practices for software development.'),
('Operating Systems', 'To explore the design and management of computer operating systems.'),
('Computer Networks', 'To study the principles and protocols of computer networking.'),
('Web Development', 'To learn the technologies and practices for creating interactive websites and web applications.'),
('Artificial Intelligence', 'To delve into the theory and applications of artificial intelligence techniques.'),
('Cybersecurity', 'To understand methods for securing computer systems and networks.'),
('Capstone Project', 'To apply knowledge and skills in a culminating project, demonstrating proficiency in computer science.')
go

insert into ClassDetails values 
('CS101', 2023, 10, 1),
('CS201', 2023, 11, 2),
('CS301', 2023, 12, 3),
('CS401', 2023, 3, 4),
('CS501', 2023, 4, 5),
('CS601', 2023, 8, 6),
('CS701', 2023, 14, 7),
('CS801', 2023, 9, 8),
('CS901', 2023, 2, 9),
('CS1001', 2023, 4, 10)
go

insert into Classes values
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10),
(1, 11),
(2, 12),
(3, 13),
(4, 14),
(5, 15),
(6, 16),
(7, 17),
(8, 18),
(9, 19),
(10, 20),
(1, 21),
(2, 22),
(3, 23),
(4, 24),
(5, 25),
(6, 26),
(7, 27),
(8, 28),
(9, 29),
(10, 30)
go
