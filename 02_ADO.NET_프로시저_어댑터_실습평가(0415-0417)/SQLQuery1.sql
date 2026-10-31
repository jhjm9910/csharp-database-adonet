create table Students
(
    studentID int primary key IDENTITY,
    name nvarchar(50) NOT NULL
);

create table Grades
(
    gradeID   int primary key IDENTITY,
    studentID int,
    subject   nvarchar(50) NOT NULL,
    score     int not null,
    foreign key (studentID) references Students(studentID)
);