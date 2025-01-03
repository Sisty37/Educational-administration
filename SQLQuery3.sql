CREATE TABLE teachers
(
   id INT PRIMARY KEY IDENTITY(1,1),
   teacher_id VARCHAR(MAX) NULL,
   teacher_name VARCHAR(MAX) NULL,
   teacher_gender VARCHAR(MAX) NULL,
   teacher_address VARCHAR(MAX) NULL,
   teacher_image VARCHAR(MAX) NULL,
   teacher_status VARCHAR(MAX) NULL,
   date_insert DATE NULL,
   date_update DATE NULL,
   date_delete DATE NULL
);

select * from teachers

