using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1266309_Maksudur
{
    public partial class Student_Info : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0CD6RRS;Initial Catalog=EducationSyestem;Integrated Security=True");
        public Student_Info()
        {
            InitializeComponent();
        }

        private void Student_Info_Load(object sender, EventArgs e)
        {
            LoadCombo();
        }

        private void LoadCombo()
        {

            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT id,name FROM subjects", con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            colSubject.DataSource = ds.Tables[0];
            colSubject.DisplayMember = "name";
            colSubject.ValueMember = "id";
            con.Close();

        }

        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlTransaction ts = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Transaction = ts;
                cmd.CommandText = "INSERT INTO students(name,roll) VALUES(@name,@roll) SELECT @@IDENTITY";
                cmd.Parameters.AddWithValue("@name", txtStudent.Text);
                cmd.Parameters.AddWithValue("@roll", txtRollNo.Text);

                int id = Convert.ToInt32(cmd.ExecuteNonQuery());
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells["colSubject"].Value != null && dataGridView1.Rows[i].Cells["colResult"].Value != null)
                    {
                        SqlCommand cmd2 = new SqlCommand();
                        cmd2.Connection = con;
                        cmd2.Transaction = ts;
                        cmd2.CommandText = "INSERT INTO result(studentId,subjectId,result) VALUES(@studentId,@subjectId,@result)";
                        cmd2.Parameters.AddWithValue("@studentId", id);
                        cmd2.Parameters.AddWithValue("@subjectId", dataGridView1.Rows[i].Cells["colSubject"].Value);
                        cmd2.Parameters.AddWithValue("@result", dataGridView1.Rows[i].Cells["colResult"].Value);

                        cmd2.ExecuteNonQuery();
                    }
                }
                ts.Commit();
                MessageBox.Show("Data saved successfully!!!!");
            }
            catch (Exception ex)
            {
                ts.Rollback();
                MessageBox.Show(ex.Message + "\nData not saved!!!");
            }
            con.Close();
        }
    }
}
