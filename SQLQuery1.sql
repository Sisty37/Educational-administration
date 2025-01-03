CREATE TABLE users 
(
     id INT PRIMARY KEY IDENTITY(1,1),
     username VARCHAR(MAX) NULL,
     password VARCHAR(MAX) NULL
)     

select * from users

INSERT users (username, password) VALUES('admin', 'admin123')


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

 select* from students

DROP TABLE  teachers;
