using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1266309_Maksudur
{
    public partial class frmUpdateDelete : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0CD6RRS;Initial Catalog=EducationSyestem;Integrated Security=True");
        public frmUpdateDelete()
        {
            InitializeComponent();
        }

        private void frmUpdateDelete_Load(object sender, EventArgs e)
        {
            LoadCombo();
        }

        private void LoadCombo()
        {

            //con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT id,name FROM subjects", con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            cmbSubject.DataSource = ds.Tables[0];
            cmbSubject.DisplayMember = "name";
            cmbSubject.ValueMember = "id";

            SqlDataAdapter sda2 = new SqlDataAdapter("SELECT teacherId FROM teacher", con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            cmbId.ValueMember = "teacherId";
            cmbId.DisplayMember = "teacherId";
            cmbId.DataSource = dt2;
            //con.Close();

        }

        private void cmbId_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT teacherId,teacherName,teacherContact,teacherEmail,picture,subjectId FROM teacher WHERE teacherId=@t";
            cmd.Parameters.AddWithValue("@t", cmbId.SelectedValue);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtName.Text = dr.GetString(1);
                txtContact.Text = dr.GetString(2);
                txtEmail.Text = dr.GetString(3);
                cmbSubject.SelectedValue = dr.GetInt32(5);
                pictureBox1.Image = Image.FromStream(dr.GetStream(4));

            }
            con.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Image img = Image.FromFile(txtPictureFile.Text);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Bmp);

            SqlCommand cmd = new SqlCommand("UPDATE teacher SET teacherName=@n,teacherContact=@c,teacherEmail=@e,picture=@p,subjectId=@s WHERE teacherId=@i", con);

            cmd.Parameters.AddWithValue("@i", cmbId.SelectedValue);
            cmd.Parameters.AddWithValue("@n", txtName.Text);
            cmd.Parameters.AddWithValue("@c", txtContact.Text);
            cmd.Parameters.AddWithValue("@e", txtEmail.Text);
            cmd.Parameters.Add(new SqlParameter("@p", SqlDbType.VarBinary) { Value = ms.ToArray() });
            cmd.Parameters.AddWithValue("@s", cmbSubject.SelectedValue);
            con.Open();
            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Data Updated successfully!!!");
            }
            con.Close();
            LoadCombo();
        }



        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog1.FileName);
                this.pictureBox1.Image = img;
                txtPictureFile.Text = openFileDialog1.FileName;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM teacher WHERE teacherId=@i", con);
            cmd.Parameters.AddWithValue("@i", cmbId.SelectedValue);
            con.Open();
            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Data Deleted successfully!!!");
            }
            con.Close();
            LoadCombo();
        }
    }
}
