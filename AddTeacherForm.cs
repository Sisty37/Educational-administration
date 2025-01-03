using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AIUB
{
    public partial class AddTeacherForm : UserControl
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\ABU JAFAR SISTY\Documents\school.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False");
        private string imagePath;

        public AddTeacherForm()
        {
            InitializeComponent();
            BindGridView();
        }

        private void teacher_addBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(teacher_id.Text)
            || string.IsNullOrEmpty(teacher_name.Text)
            || string.IsNullOrEmpty(teacher_gender.Text)
            || string.IsNullOrEmpty(teacher_address.Text)
            || string.IsNullOrEmpty(teacher_status.Text))
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                connect.Open();

                string checkTeacherID = "SELECT COUNT(*) FROM teachers WHERE teacher_id = @teacherID";
                using (SqlCommand checkTID = new SqlCommand(checkTeacherID, connect))
                {
                    checkTID.Parameters.AddWithValue("@teacherID", teacher_id.Text.Trim());
                    int count = (int)checkTID.ExecuteScalar();

                    if (count >= 1)
                    {
                        MessageBox.Show("Teacher ID: " + teacher_id.Text.Trim() + " already exists", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DateTime today = DateTime.Today;
                        string insertData = "INSERT INTO teachers " +
                            "(teacher_id, teacher_name, teacher_gender, teacher_address, " +
                            "teacher_image, teacher_status, date_insert) " +
                            "VALUES(@teacherID, @teacherName, @teacherGender, @teacherAddress, " +
                            "@teacher_image, @teacherStatus, @dateInsert)";


                        using (SqlCommand cmd = new SqlCommand(insertData, connect))
                        {
                            cmd.Parameters.AddWithValue("@teacherID", teacher_id.Text.Trim());
                            cmd.Parameters.AddWithValue("@teacherName", teacher_name.Text.Trim());
                            cmd.Parameters.AddWithValue("@teacherGender", teacher_gender.Text.Trim());
                            cmd.Parameters.AddWithValue("@teacherAddress", teacher_address.Text.Trim());
                            cmd.Parameters.AddWithValue("@teacher_image", teacherImage());
                            cmd.Parameters.AddWithValue("@teacherStatus", teacher_status.Text.Trim());
                            cmd.Parameters.AddWithValue("@dateInsert", today);

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Teacher added successfully!", "Information Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            clearFields();
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error: " + sqlEx.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connect.Close();
                BindGridView();
            }
        }

        public void clearFields()
        {
            teacher_id.Text = "";
            teacher_name.Text = "";
            teacher_gender.SelectedIndex = -1;
            teacher_address.Text = "";
            teacher_status.SelectedIndex = -1;
            teacher_image.Image = null;
            imagePath = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Image";
            ofd.Filter = "ALL IMAGE FILE (*.*) | *.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                teacher_image.Image = new Bitmap(ofd.FileName);
            }
        }

        private byte[] teacherImage()
        {
            MemoryStream ms = new MemoryStream();
            teacher_image.Image.Save(ms, teacher_image.Image.RawFormat);
            return ms.GetBuffer();
        }

        private void teacher_clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void teacher_updateBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(teacher_id.Text)
                || string.IsNullOrEmpty(teacher_name.Text)
                || string.IsNullOrEmpty(teacher_gender.Text)
                || string.IsNullOrEmpty(teacher_address.Text)
                || string.IsNullOrEmpty(teacher_status.Text))
            {
                MessageBox.Show("Please select item first", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                connect.Open();

                DialogResult check = MessageBox.Show("Are you sure you want to update Teacher ID:"
                    + teacher_id.Text.Trim() + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (check == DialogResult.Yes)
                {
                    DateTime today = DateTime.Today;

                    string updateData = "UPDATE teachers SET " +
                        "teacher_name = @teacherName, teacher_gender = @teacherGender, " +
                        "teacher_address = @teacherAddress, teacher_status = @teacherStatus, " +
                        "date_update = @dateUpdate WHERE teacher_id = @teacherID";

                    using (SqlCommand cmd = new SqlCommand(updateData, connect))
                    {
                        cmd.Parameters.AddWithValue("@teacherName", teacher_name.Text.Trim());
                        cmd.Parameters.AddWithValue("@teacherGender", teacher_gender.Text.Trim());
                        cmd.Parameters.AddWithValue("@teacherAddress", teacher_address.Text.Trim());
                        cmd.Parameters.AddWithValue("@teacherStatus", teacher_status.Text.Trim());
                        cmd.Parameters.AddWithValue("@dateUpdate", today);
                        cmd.Parameters.AddWithValue("@teacherID", teacher_id.Text.Trim());

                        cmd.ExecuteNonQuery();

                        BindGridView();

                        MessageBox.Show("Teacher data updated successfully!", "Information Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        clearFields();
                    }
                }
                else
                {
                    MessageBox.Show("Cancelled", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearFields();
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error: " + sqlEx.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connect.Close();
            }
        }

        void BindGridView()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\ABU JAFAR SISTY\Documents\school.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False"))
                {
                    string query = "select * from teachers";
                    using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
                    {
                        DataTable data = new DataTable();
                        sda.Fill(data);

                        teacher_gridData.DataSource = data;
                        DataGridViewImageColumn dgvImageColumn = (DataGridViewImageColumn)teacher_gridData.Columns["teacher_image"];
                        dgvImageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;

                        teacher_gridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        teacher_gridData.RowTemplate.Height = 80;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void teacher_deleteBtn_Click(object sender, EventArgs e)
        {
            if (teacher_gridData.SelectedRows.Count > 0)
            {
                string id = teacher_gridData.SelectedRows[0].Cells["ID"].Value.ToString();

                DialogResult result = MessageBox.Show("Are you sure you want to delete this Teacher?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DeleteTeacher(id);
                    BindGridView();
                }
            }
            else
            {
                MessageBox.Show("Please select a teacher to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DeleteTeacher(String id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\ABU JAFAR SISTY\Documents\school.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False"))
                {
                    con.Open();

                    string query = "DELETE FROM teachers WHERE ID = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }

                MessageBox.Show("Teacher deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting course: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void teacher_gridData_SelectionChanged(object sender, EventArgs e)
        {
            if (teacher_gridData.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = teacher_gridData.SelectedRows[0];

                teacher_id.Text = selectedRow.Cells["teacher_id"].Value.ToString();
                teacher_name.Text = selectedRow.Cells["teacher_name"].Value.ToString();
                teacher_gender.Text = selectedRow.Cells["teacher_gender"].Value.ToString();
                teacher_address.Text = selectedRow.Cells["teacher_address"].Value.ToString();
                teacher_status.Text = selectedRow.Cells["teacher_status"].Value.ToString();

                if (selectedRow.Cells["teacher_image"].Value != DBNull.Value)
                {
                    byte[] imageBytes = (byte[])selectedRow.Cells["teacher_image"].Value;
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        teacher_image.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    teacher_image.Image = null;
                }
            }
        }
    }
}
