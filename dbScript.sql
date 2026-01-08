-- Create the 'Notenservice' database
CREATE DATABASE IF NOT EXISTS Notenservice;
USE Notenservice;

-- Create the 'Student' table
CREATE TABLE Student (
    Id INT PRIMARY KEY AUTO_INCREMENT,
	Firstname VARCHAR(50) NOT NULL,
    Lastname VARCHAR(50) NOT NULL
);

-- Create the 'User' table
CREATE TABLE User (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Firstname VARCHAR(50) NOT NULL,
    Lastname VARCHAR(50) NOT NULL,
    UserRole VARCHAR(50) NOT NULL
);

-- Create the 'Request'
CREATE TABLE Request (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    CourseName VARCHAR(50) NOT NULL,
    NewGrade DECIMAL(3,1),
    RequestDescription VARCHAR(100),
	StudentId INT,
    FOREIGN KEY (StudentId) REFERENCES Student(Id)
);

INSERT INTO Role (RoleName) VALUES
('Teacher'),
('Prorector');

-- Insert example data into 'Student'
INSERT INTO Student (Firstname, Lastname) VALUES
('Minimaler', 'Blödian'),
('Sigma', 'Mangofirma'),
('Max', 'Schradin);

-- Insert example data into 'User'
INSERT INTO User (Firstname, Lastname, RoleId) VALUES
('Christian', 'Lindauer', 1),
('Werner', 'Odermatt', 2);

-- Insert example data into 'Request'
INSERT INTO Request (CourseName, NewGrade, RequestDescription, StudentId) VALUES
('M431', 6.0, 'Very Good Grade', 1),
('M431', 5.0, 'Good Grade', 1);