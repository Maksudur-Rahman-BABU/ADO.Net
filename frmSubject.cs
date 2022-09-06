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
    public partial class frmSubject : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0CD6RRS;Initial Catalog=EducationSyestem;Integrated Security=True");
        public frmSubject()
        {
            InitializeComponent();
        }
        private void show() {
            con.Open();
            string Quary = "select * from subjects";
            SqlDataAdapter sda = new SqlDataAdapter(Quary, con);
            SqlCommandBuilder B = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dgSubject.DataSource = ds.Tables[0];
            con.Close();
        
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO subjects(id,name) VALUES(@i,@n)";
            cmd.Parameters.AddWithValue("@i", txtsubid.Text);
            cmd.Parameters.AddWithValue("@n", txtsubname.Text);
            
            

            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Data inserted successfully!!!");
            }
            con.Close();
            
        }
    
    }
}
