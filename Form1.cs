using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1266309_Maksudur
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void studentToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Student_Info si = new Student_Info();
            si.MdiParent = this;
            si.Show();

        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInsertTeacher it = new frmInsertTeacher();
            it.MdiParent = this;
            it.Show();
        }

        private void editDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateDelete ud = new frmUpdateDelete();
            ud.MdiParent = this;
            ud.Show();
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTeacherView tv = new frmTeacherView();
            tv.MdiParent = this;
            tv.Show();
        }

       

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tutorInformationReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTutorInformationReport tir = new frmTutorInformationReport();
            tir.MdiParent = this;
            tir.Show();
        }

        private void subjectTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSubject fs = new frmSubject();
            fs.Show();
        }
    }
}
