using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace cargo_tracking
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection("server=.; Initial Catalog=login; Integrated Security=True");

        private void Form2_Load(object sender, EventArgs e)
        {
            this.cargoTableAdapter.Fill(this.loginDataSet.cargo);
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            idtextBox.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            nametextBox.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            surnametextBox.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            phonetextBox.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            addresstextBox.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            statustextBox.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();

        }

        private void idtextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String s = "insert into cargo(name,surname,phone,address,cargo_status) values(@name,@surname,@phone,@address,@cargo_status)";
            SqlCommand add = new SqlCommand(s, connection);
            add.Parameters.AddWithValue("@name", nametextBox.Text);
            add.Parameters.AddWithValue("@surname", surnametextBox.Text);
            add.Parameters.AddWithValue("@phone", phonetextBox.Text);
            add.Parameters.AddWithValue("@address", addresstextBox.Text);
            add.Parameters.AddWithValue("@cargo_status", statustextBox.Text);
            connection.Open();
            add.ExecuteNonQuery();
            connection.Close();

            this.cargoTableAdapter.Fill(this.loginDataSet.cargo);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            connection.Open();
            String s = "update cargo set name=@name, surname=@surname, phone=@phone, address=@address, cargo_status=@cargo_status where ID=@ID ";
            SqlCommand upd = new SqlCommand(s, connection);
            upd.Parameters.AddWithValue("@ID", idtextBox.Text);
            upd.Parameters.AddWithValue("@name", nametextBox.Text);
            upd.Parameters.AddWithValue("@surname", surnametextBox.Text);
            upd.Parameters.AddWithValue("@phone", phonetextBox.Text);
            upd.Parameters.AddWithValue("@address", addresstextBox.Text);
            upd.Parameters.AddWithValue("@cargo_status", statustextBox.Text);
            upd.ExecuteNonQuery();
            connection.Close();

            this.cargoTableAdapter.Fill(this.loginDataSet.cargo);

        }

        private void button4_Click(object sender, EventArgs e)
        {

            connection.Open();
            String s = "select * from cargo where cargo_status = 'On the way' ";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(s, connection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            connection.Open();
            String s = "select * from cargo";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(s, connection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            connection.Open();
            String s = "select * from cargo where cargo_status = 'Shipped' ";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(s, connection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }
    }
}
