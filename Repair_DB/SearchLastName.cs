using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Repair_DB
{
    public partial class SearchLastName : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Repair_DB.Properties.Settings.repairConnectionString1"].ConnectionString;
        public SearchLastName()
        {
            InitializeComponent();
          
        }

        private void SearchLastName_Load(object sender, EventArgs e)
        {
            var oleConnection = new OleDbConnection(connectionString);
            oleConnection.Open();
            var dataTableFill = new DataTable();
            var oleCommand = new OleDbCommand("SELECT * FROM Customers", oleConnection);
            var adapter = new OleDbDataAdapter(oleCommand);
            adapter.Fill(dataTableFill);
            comboBox1.DataSource = dataTableFill;
            comboBox1.DisplayMember = "Customer"; // столбец для отображения
            comboBox1.ValueMember = "ID_Customers"; //столбец с id
            comboBox1.SelectedIndex = -1;
            oleConnection.Close();
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;
            var id = senderComboBox.SelectedValue.ToString();
            BindingSource bs1 = new BindingSource();
            var oleConnection = new OleDbConnection(connectionString);
            oleConnection.Open();
            var dataTableFill = new DataTable();
            var oleCommand = new OleDbCommand("SELECT Objects.Objects, Objects.Cost, Objects.Date_issue, Repair.Date_repair FROM(Customers INNER JOIN Objects ON Customers.ID_Customers = Objects.ID_Customers) INNER JOIN Repair ON Objects.ID_Objects = Repair.ID_Objects WHERE Customers.ID_Customers = "+ id, oleConnection);
            var adapter = new OleDbDataAdapter(oleCommand);
            adapter.Fill(dataTableFill);
            bs1.DataSource = dataTableFill;
            bindingNavigator1.BindingSource = bs1;
            dataGridView1.DataSource = bs1;
            dataGridView1.Columns[0].HeaderText = "Наименование товара";
            dataGridView1.Columns[1].HeaderText = "Стоимость ремонта";
            dataGridView1.Columns[2].HeaderText = "Дата изготовления";
            dataGridView1.Columns[3].HeaderText = "Дата ремонта";
            oleConnection.Close();
        }
    }
}
