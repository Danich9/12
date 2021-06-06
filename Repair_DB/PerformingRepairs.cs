using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Repair_DB
{
    public partial class PerformingRepairs : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Repair_DB.Properties.Settings.repairConnectionString1"].ConnectionString;
        public PerformingRepairs()
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd.MM.yyyy";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd.MM.yyyy";
        }

        private void PerformingRepairs_Load(object sender, EventArgs e)
        {
            var oleConnection = new OleDbConnection(connectionString);
            oleConnection.Open();
            var dataTableFill = new DataTable();
            var oleCommand = new OleDbCommand("SELECT * FROM Employees", oleConnection);
            var adapter = new OleDbDataAdapter(oleCommand);
            adapter.Fill(dataTableFill);
            comboBox1.DataSource = dataTableFill;
            comboBox1.DisplayMember = "Employee"; // столбец для отображения
            comboBox1.ValueMember = "ID_Employees"; //столбец с id
            comboBox1.SelectedIndex = -1;
            oleConnection.Close();
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        { try
            {
                ComboBox senderComboBox = (ComboBox)sender;
                var d_start = "#" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "#";
                var d_end = "#" + dateTimePicker2.Value.Date.ToString("yyyy-MM-dd") + "#";
                var id = senderComboBox.SelectedValue.ToString();
                BindingSource bs1 = new BindingSource();
                var oleConnection = new OleDbConnection(connectionString);
                oleConnection.Open();
                var dataTableFill = new DataTable();
                var oleCommand = new OleDbCommand("SELECT Positions.Positions, Objects.Objects, Services.Service, Repair.Date_repair" +
                                                  " FROM Objects INNER JOIN(Services INNER JOIN((Positions INNER JOIN Employees ON Positions.ID_Positions = Employees.ID_Positions)" +
                                                  " INNER JOIN Repair ON Employees.ID_Employees = Repair.ID_Employees) ON Services.ID_Services = Repair.ID_Services) ON Objects.ID_Objects = Repair.ID_Objects" +
                                                  " WHERE Repair.Date_repair Between " + d_start + " AND " + d_end + " AND Employees.ID_Employees = " + id, oleConnection);
                var adapter = new OleDbDataAdapter(oleCommand);
                adapter.Fill(dataTableFill);
                bs1.DataSource = dataTableFill;
                bindingNavigator1.BindingSource = bs1;
                dataGridView1.DataSource = bs1;
                dataGridView1.Columns[0].HeaderText = "Наименование должности";
                dataGridView1.Columns[1].HeaderText = "Наименование товара";
                dataGridView1.Columns[2].HeaderText = "Выполненные работы";
                dataGridView1.Columns[3].HeaderText = "Дата ремонта";
                oleConnection.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
