using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Repair_DB
{
    public partial class ordersProgress : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Repair_DB.Properties.Settings.repairConnectionString1"].ConnectionString;
        public ordersProgress()
        {
            InitializeComponent();
        }

        private void ordersProgress_Load(object sender, EventArgs e)
        {
            BindingSource bs1 = new BindingSource();
            var oleConnection = new OleDbConnection(connectionString);
            oleConnection.Open();
            var dataTableFill = new DataTable();
            var oleCommand = new OleDbCommand("SELECT Objects.Objects, Services.Service, Employees.Employee, Components.Component, Repair.Date_repair " +
                                              "FROM Services INNER JOIN(Objects INNER JOIN(Employees INNER JOIN(Components INNER JOIN Repair " +
                                              "ON Components.ID_Components = Repair.ID_Components) ON Employees.ID_Employees = Repair.ID_Employees) ON Objects.ID_Objects = Repair.ID_Objects) " +
                                              "ON Services.ID_Services = Repair.ID_Services WHERE Status_repair = 'В работе'  ", oleConnection);
            var adapter = new OleDbDataAdapter(oleCommand);
            adapter.Fill(dataTableFill);
            bs1.DataSource = dataTableFill;
            bindingNavigator1.BindingSource = bs1;
            dataGridView1.DataSource = bs1;
            //dataGridView1.Columns["ID_Components"].Visible = false;
            dataGridView1.Columns[0].HeaderText = "Наименование объекта";
            dataGridView1.Columns[1].HeaderText = "Наименование работ";
            dataGridView1.Columns[2].HeaderText = "Сотрудник";
            dataGridView1.Columns[3].HeaderText = "Наименование комплектующих";
            dataGridView1.Columns[4].HeaderText = "Дата ремонта";
            oleConnection.Close();
        }
    }
}
