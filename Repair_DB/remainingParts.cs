using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Repair_DB
{
    public partial class remainingParts : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Repair_DB.Properties.Settings.repairConnectionString1"].ConnectionString;
        public remainingParts()
        {
            InitializeComponent();
        }

        private void remainingParts_Load(object sender, EventArgs e)
        {
            var oleConnection = new OleDbConnection(connectionString);
            oleConnection.Open();
            var dataTableFill = new DataTable();
            var oleCommand = new OleDbCommand("SELECT * FROM Components", oleConnection);
            var adapter = new OleDbDataAdapter(oleCommand);
            adapter.Fill(dataTableFill);
            comboBox1.DataSource = dataTableFill;
            comboBox1.DisplayMember = "Component"; // столбец для отображения
            comboBox1.ValueMember = "ID_Components"; //столбец с id
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
            var oleCommand = new OleDbCommand("SELECT * FROM Components WHERE ID_Components = " + id, oleConnection);
            var adapter = new OleDbDataAdapter(oleCommand);
            adapter.Fill(dataTableFill);
            bs1.DataSource = dataTableFill;
            bindingNavigator1.BindingSource = bs1;
            dataGridView1.DataSource = bs1;
            dataGridView1.Columns["ID_Components"].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Наименование комплектующих";
            dataGridView1.Columns[2].HeaderText = "Количество комплектующих";
            dataGridView1.Columns[3].HeaderText = "Цена комплектующего";
            dataGridView1.Columns[4].HeaderText = "Остаток комплектующих";
            oleConnection.Close();
        }
    }
}
