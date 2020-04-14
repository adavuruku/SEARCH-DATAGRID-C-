
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Search_DGridView
{
    public partial class Form1 : Form
    {
        MySqlCommand sCommand;
        MySqlDataAdapter sAdapter;
        MySqlCommandBuilder sBuilder;
        DataSet sDs;
        DataTable sTable;
        DataView dv;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            MySqlConnection con = new MySqlConnection(ConString);
            con = new MySqlConnection(ConString);
            string sql = "";
          
                sql = "SELECT id,Department,semester,Course_Code,Course_Title,staff_id FROM atbu_course";
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            sCommand = new MySqlCommand(sql, con);
            sAdapter = new MySqlDataAdapter(sCommand);
            sBuilder = new MySqlCommandBuilder(sAdapter);
            sDs = new DataSet();
            dv = new DataView();
            sAdapter.Fill(sDs, "atbu_course");
            sTable = sDs.Tables["atbu_course"];
            dv.Table = sDs.Tables["atbu_course"];
            con.Close();
            dataGridView1.DataSource = dv;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           // dv.RowFilter = "Course_Code like '%" + textBox1.Text + "%'";
            dv.RowFilter = "Course_Code like '%" + textBox1.Text + "%'";
            dataGridView1.DataSource = dv;
        }
    }
}
