using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AIUB
{
    internal class AddTeachersData
    {
         
        private SqlConnection connect = new SqlConnection(@"Data Source=SAIKAT\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;Encrypt=False");

        
        public int ID { get; set; }
        public string TeacherID { get; set; }
        public string TeacherName { get; set; }
        public string TeacherGender { get; set; }
        public string TeacherAddress { get; set; }
        public string TeacherImage { get; set; }
        public string Status { get; set; }
        public string DateInsert { get; set; }

      
        public List<AddTeachersData> teacherData()
        {
            List<AddTeachersData> listData = new List<AddTeachersData>();

            try
            {
                // Open the database connection if it's not already open
                if (connect.State != ConnectionState.Open)
                {
                    connect.Open();
                }

                // Define the SQL query to select all non-deleted teachers
                string sql = "SELECT * FROM teacher WHERE date_delete IS NULL";

                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    // Execute the query and get the data using a data reader
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Create a new AddTeachersData object and populate its properties
                        AddTeachersData addTD = new AddTeachersData
                        {
                            ID = (int)reader["id"],
                            TeacherID = reader["teacher_id"].ToString(),
                            TeacherName = reader["teacher_name"].ToString(),
                            TeacherGender = reader["teacher_gender"].ToString(),
                            TeacherAddress = reader["teacher_address"].ToString(),
                            TeacherImage = reader["teacher_image"].ToString(),
                            Status = reader["teacher_status"].ToString(),
                            DateInsert = reader["date_insert"].ToString()
                        };

                        // Add the object to the list
                        listData.Add(addTD);
                    }

                    // Close the reader when done
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                // Log the error message
                Console.WriteLine("Error Connecting Database: " + ex.Message);
            }
            finally
            {
                // Ensure that the connection is closed
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }

            // Return the list of teacher data
            return listData;
        }
    }
}
