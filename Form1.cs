using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using buoi6.Model;

namespace buoi6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Khởi tạo context để làm việc với database
                Model1 context = new Model1();

                // Lấy dữ liệu từ database
                List<Faculty> listFaculties = context.Faculties.ToList();
                List<Student> listStudents = context.Students.ToList();

                // Gán dữ liệu cho combobox và gridview
                FillFacultyCombobox(listFaculties);
                BindGrid(listStudents);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillFacultyCombobox(List<Faculty> listFaculties)
        {
            this.cmbFaculty.DataSource = listFaculties;
            this.cmbFaculty.DisplayMember = "FacultyName";  // Tên hiển thị
            this.cmbFaculty.ValueMember = "FacultyID";      // Giá trị mã khoa
        }

        // Hàm gán dữ liệu vào DataGridView sinh viên
        private void BindGrid(List<Student> listStudents)
        {
            dgvStudent.Rows.Clear(); // Xóa các dòng cũ trong gridview

            foreach (var item in listStudents)
            {
                int index = dgvStudent.Rows.Add();
                dgvStudent.Rows[index].Cells[0].Value = item.StudentID;
                dgvStudent.Rows[index].Cells[1].Value = item.FullName;
                dgvStudent.Rows[index].Cells[2].Value = item.Faculty.FacultyName;
                dgvStudent.Rows[index].Cells[3].Value = item.AverageScore;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Model1 context = new Model1();
                Student newStudent = new Student
                {
                    StudentID = int.Parse(txtStudentID.Text), // Cột MSSV
                    FullName = txtFullName.Text,
                    AverageScore = double.Parse(txtAverageScore.Text),
                    Faculty = context.Faculties.Find(cmbFaculty.SelectedValue)
                };
                context.Students.Add(newStudent);
                context.SaveChanges();
                MessageBox.Show("Thêm sinh viên thành công!");
                ClearTextBoxes();
                BindGrid(context.Students.ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvStudent.CurrentRow != null)
                {
                    int studentID = int.Parse(dgvStudent.CurrentRow.Cells[0].Value.ToString());
                    Model1 context = new Model1();
                    Student editStudent = context.Students.Find(studentID);
                    if (editStudent != null)
                    {
                        editStudent.StudentID = int.Parse(txtStudentID.Text); // Sửa MSSV
                        editStudent.FullName = txtFullName.Text;
                        editStudent.AverageScore = double.Parse(txtAverageScore.Text);
                        editStudent.Faculty = context.Faculties.Find(cmbFaculty.SelectedValue);
                        context.SaveChanges();
                        MessageBox.Show("Sửa thông tin sinh viên thành công!");
                        ClearTextBoxes();
                        BindGrid(context.Students.ToList());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvStudent.CurrentRow != null)
                {
                    int studentID = int.Parse(dgvStudent.CurrentRow.Cells[0].Value.ToString());
                    Model1 context = new Model1();
                    Student deleteStudent = context.Students.Find(studentID);
                    if (deleteStudent != null)
                    {
                        context.Students.Remove(deleteStudent);
                        context.SaveChanges();
                        MessageBox.Show("Xóa sinh viên thành công!");
                        ClearTextBoxes();
                        BindGrid(context.Students.ToList());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ClearTextBoxes()
        {
            txtStudentID.Clear();
            txtFullName.Clear();
            txtAverageScore.Clear();
            cmbFaculty.SelectedIndex = -1;
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvStudent.Rows[e.RowIndex];
                txtStudentID.Text = row.Cells[0].Value.ToString();
                txtFullName.Text = row.Cells[1].Value.ToString();
                cmbFaculty.Text = row.Cells[2].Value.ToString();
                txtAverageScore.Text = row.Cells[3].Value.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();


            
        }

        private void dgvStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cmbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
