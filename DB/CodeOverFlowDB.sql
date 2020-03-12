Create database CodeOverFlowDB
go

use CodeOverFlowDB
go

Create table Categories(

CategoryID int primary key identity(1,1),
CategoryName nvarchar(max) 
)
go

Create table Users(

UserID int primary key identity(1,1),
UserName nvarchar(max),
Email nvarchar(max),
PasswordHash nvarchar(max),
Mobile nvarchar(max),
IsAdmin bit default(0)

)
go

Create table Questions(

QuestionID int primary key identity(1,1),
QuestionName nvarchar(max),
QuestionDateAndTime datetime,
UserID int references Users(UserID) on delete cascade,
CategoryID int references Categories(CategoryID) on delete cascade,
VotesCount int,
AnswersCount int,
ViewsCount int

)
go

Create table Answers(

AnswerID int primary key identity(1,1),
AnswerText nvarchar(max),
AnswerDateAndTime datetime,
UserID int references Users(UserID),
QuestionID int references Questions(QuestionID) on delete cascade,
VotesCount int

)
go

Create table Votes(

VoteID int primary key identity(1,1),
UserID int references Users(UserID),
AnswerID int references Answers(AnswerID) on delete cascade,
VoteValue int

)
go


Insert into Users values ('Admin','Admin@codeoverflow.com','240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9','090078601',1)
go
Insert into Users values ('Saim','saimpak95@yahoo.com','2ecf12111a22299abc05a4f188954f98d7264e83246d27c54d8e9d5aa4c01c2a','03410245607',0)
go
Insert into Users values ('Tawish','TawishManzoor55@ygmail.com','2ecf12111a22299abc05a4f188954f98d7264e83246d27c54d8e9d5aa4c01c2a','03422898923',0) 
go

Insert into Categories values('HTML')
go

Insert into Categories values('CSS')
go

Insert into Categories values('JavaScript')
go

Insert into Questions values ('How to display the icon in the browser title bar?','2020-8-5 9:07 am',2,1,0,0,0)
go

Insert into Questions values ('How to set background Image in CSS?','2020-8-5 9:07 am',3,2,0,0,0)
go















