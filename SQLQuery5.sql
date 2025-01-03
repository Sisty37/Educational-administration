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