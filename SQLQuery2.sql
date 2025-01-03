CREATE TABLE students
(
      id INT PRIMARY KEY IDENTITY(1,1),
      student_id VARCHAR(MAX) NULL,
      student_name varchar(MAX) NULL,
      student_gender varchar(MAX) NULL,
      student_address varchar(MAX) NULL,
      student_grade varchar(MAX) NULL,
      student_section varchar(MAX) NULL,
      student_image varchar(MAX) NULL,
      student_status varchar(MAX) NULL,
      date_insert DATE NULL,
      date_update DATE NULL,
      date_delete DATE NULL
 )
  


   CREATE TABLE teachers (
    ID INT PRIMARY KEY IDENTITY(1,1),
    teacher_id NVARCHAR(50) UNIQUE,
    teacher_name NVARCHAR(100),
    teacher_gender NVARCHAR(10),
    teacher_address NVARCHAR(255),
    teacher_image VARBINARY(MAX),
    teacher_status NVARCHAR(50),
    date_insert DATE,
    date_update DATE
);

 