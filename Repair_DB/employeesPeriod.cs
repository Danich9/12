using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Repair_DB
{
    public partial class employeesPeriod : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Repair_DB.Properties.Settings.repairConnectionString1"].ConnectionString;
        public employeesPeriod()
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd.MM.yyyy";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd.MM.yyyy";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var d_start = "#" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "#";
                var d_end = "#" + dateTimePicker2.Value.Date.ToString("yyyy-MM-dd") + "#";

                BindingSource bs1 = new BindingSource();
                var oleConnection = new OleDbConnection(connectionString);
                oleConnection.Open();
                var dataTableFill = new DataTable();
                var oleCommand = new OleDbCommand("SELECT TOP 1 Employees.Employee, Count(Employees.ID_Employees) AS [Cnt]" +
                                                  " FROM Services INNER JOIN(Objects INNER JOIN(Employees INNER JOIN Repair ON Employees.ID_Employees = Repair.ID_Employees)" +
                                                  " ON Objects.ID_Objects = Repair.ID_Objects) ON Services.ID_Services = Repair.ID_Services" +
                                                  " WHERE Repair.Date_repair Between " + d_start + " AND " + d_end + " AND Repair.Status_repair = 'Завершен' GROUP BY Employees.Employee ORDER BY Count(Employees.ID_Employees) DESC", oleConnection);
                var adapter = new OleDbDataAdapter(oleCommand);
                adapter.Fill(dataTableFill);
                bs1.DataSource = dataTableFill;
                bindingNavigator1.BindingSource = bs1;
                dataGridView1.DataSource = bs1;
                dataGridView1.Columns[0].HeaderText = "Сотрудник мастерской";
                dataGridView1.Columns[1].HeaderText = "Количество ремонтов";
                oleConnection.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void EmployeesPeriod_Load(object sender, EventArgs e)
        {

        }
    }
}
