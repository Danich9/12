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
    public partial class completedOrders : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Repair_DB.Properties.Settings.repairConnectionString1"].ConnectionString;
        public completedOrders()
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
                var oleCommand = new OleDbCommand("SELECT Objects.Objects, Objects.Cost, Objects.Date_issue, Services.Service, Repair.Date_repair" +
                                                  " FROM Objects INNER JOIN(Services INNER JOIN((Positions INNER JOIN Employees ON Positions.ID_Positions = Employees.ID_Positions)" +
                                                  " INNER JOIN Repair ON Employees.ID_Employees = Repair.ID_Employees) ON Services.ID_Services = Repair.ID_Services) ON Objects.ID_Objects = Repair.ID_Objects" +
                                                  " WHERE Repair.Date_repair Between " + d_start + " AND " + d_end + " AND Repair.Status_repair = 'Завершен'", oleConnection);
                var adapter = new OleDbDataAdapter(oleCommand);
                adapter.Fill(dataTableFill);
                bs1.DataSource = dataTableFill;
                bindingNavigator1.BindingSource = bs1;
                dataGridView1.DataSource = bs1;
                dataGridView1.Columns[0].HeaderText = "Наименование товара";
                dataGridView1.Columns[1].HeaderText = "Стоимость ремонта по заявке";
                dataGridView1.Columns[2].HeaderText = "Дата заявки";
                dataGridView1.Columns[3].HeaderText = "Выполненные работы";
                dataGridView1.Columns[4].HeaderText = "Дата ремонта";
                oleConnection.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
