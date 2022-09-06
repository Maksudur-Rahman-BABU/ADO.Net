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
    public partial class frmTeacherView : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0CD6RRS;Initial Catalog=EducationSyestem;Integrated Security=True");
        public frmTeacherView()
        {
            InitializeComponent();
        }

        private void frmTeacherView_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT t.teacherId,t.teacherName,t.teacherContact,t.teacherEmail,t.picture,s.name FROM teacher t INNER JOIN subjects s ON t.subjectId=s.id", con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}
