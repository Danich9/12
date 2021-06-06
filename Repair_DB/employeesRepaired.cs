using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Repair_DB
{
    public partial class employeesRepaired : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Repair_DB.Properties.Settings.repairConnectionString1"].ConnectionString;
        public employeesRepaired()
        {
            InitializeComponent();
        }

        private void employeesRepaired_Load(object sender, EventArgs e)
        {
            BindingSource bs1 = new BindingSource();
            var oleConnection = new OleDbConnection(connectionString);
            oleConnection.Open();
            var dataTableFill = new DataTable();
            var oleCommand = new OleDbCommand("SELECT TOP 1 Employees.Employee, Sum(Objects.Cost) AS [Sum-Cost]"+
                                              " FROM Objects INNER JOIN(Employees INNER JOIN Repair ON Employees.ID_Employees = Repair.ID_Employees)" +
                                              " ON Objects.ID_Objects = Repair.ID_Objects" +
                                              " WHERE Status_repair = 'Завершен' GROUP BY Employees.Employee ORDER BY Sum(Objects.Cost) DESC", oleConnection);
            var adapter = new OleDbDataAdapter(oleCommand);
            adapter.Fill(dataTableFill);
            bs1.DataSource = dataTableFill;
            bindingNavigator1.BindingSource = bs1;
            dataGridView1.DataSource = bs1;
            dataGridView1.Columns[0].HeaderText = "Сотрудник мастерской";
            dataGridView1.Columns[1].HeaderText = "Сумма ремонтов";
            oleConnection.Close();
        }
    }
}
