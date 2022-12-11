using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace avito
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id_txt.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            name_txt.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
        }
        private void SetConnection()
        {
            sql_con = new SQLiteConnection("Data Source=data_table.db;Version=3;New=False;Compress=True;");
        }

        private void ExecuteQuery(string txtQuery)
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }
        private void LoadDate()
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "select * from test";
            DB = new SQLiteDataAdapter(CommandText, sql_con);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];
            dataGridView1.DataSource = DT;
            sql_con.Close();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDate();
        }
        private void add_Click(object sender, EventArgs e)
        {
            string txtQuery = "insert into test(price,name) values ('" + id_txt.Text + "','" + name_txt.Text + "')";
            ExecuteQuery(txtQuery);
            LoadDate();
        }





        private void edit_Click(object sender, EventArgs e)
        {

            string txtQuery = "update test set name='" + name_txt.Text + "' where price = '" + id_txt.Text + "'";

            ExecuteQuery(txtQuery);
            LoadDate();

        }

        private void delete_Click(object sender, EventArgs e)
        {
            string txtQuery = "delete from test where price='" + id_txt.Text + "'";
            ExecuteQuery(txtQuery);
            LoadDate();
        }
    }
}
