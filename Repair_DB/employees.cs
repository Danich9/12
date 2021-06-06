using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Repair_DB
{
    public partial class employees : Form
    {
        private OleDbConnection db_connection;
        private DataSet ds;
        private OleDbDataAdapter adapter_Employees;
        private OleDbDataAdapter adapter_Positions;
        private BindingSource bindingSource;

        public employees()
        {
            InitializeComponent();
        }

        private void employees_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "repairDataSet.Employees". При необходимости она может быть перемещена или удалена.
            //this.employeesTableAdapter.Fill(this.repairDataSet.Employees);
            ds = new DataSet();
            db_connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\repair.mdb");
            db_connection.Open();
           


            adapter_Employees = new OleDbDataAdapter(new OleDbCommand("SELECT * FROM Employees", db_connection));
            var cb_adapter_Employees = new OleDbCommandBuilder(adapter_Employees);
           

            adapter_Positions = new OleDbDataAdapter(new OleDbCommand("SELECT * FROM Positions", db_connection));
            var cb_Positions = new OleDbCommandBuilder(adapter_Positions);

            adapter_Employees.Fill(ds, "Employees"); // прочитать Employees
            adapter_Positions.Fill(ds, "Positions"); // прочитать Positions
            adapter_Employees.InsertCommand = cb_adapter_Employees.GetInsertCommand(true);
            adapter_Employees.UpdateCommand = cb_adapter_Employees.GetUpdateCommand(true);
            adapter_Employees.DeleteCommand = cb_adapter_Employees.GetDeleteCommand(true);
           db_connection.Close();

            ds.Tables["Positions"].Columns["Positions"].Unique = true;
            // Установка связи таблиц 1.ID_Positions = 2.ID_Positions
            ds.Relations.Add(new DataRelation("rlPositionsEmployees", ds.Tables["Positions"].Columns["ID_Positions"], ds.Tables["Employees"].Columns["ID_Positions"]));
            // Вывод таблицы
            dataGridView1.DataSource = ds.Tables["Employees"]; // Employees - в DataGrid
            dataGridView1.Columns["ID_Employees"].Visible = false; // скрыть колонку с идентификатором
            dataGridView1.Columns["ID_Positions"].Visible = false;
            //--------------------------header-------------------------------------
            dataGridView1.Columns["Employee"].HeaderText = "Сотрудник";
            dataGridView1.Columns["Birtday"].HeaderText = "Дата рождения";
            dataGridView1.Columns["Address"].HeaderText = "Адрес сотрудника";
            dataGridView1.Columns["Phone"].HeaderText = "Телефон сотрудника";
            // -----------------добавить combobox------------------------------------
            var cbx_Positions = new DataGridViewComboBoxColumn(); // добавить новую колонку
            cbx_Positions.Name = "Должность";
            cbx_Positions.DataSource = ds.Tables["Positions"];
            cbx_Positions.DisplayMember = "Positions"; // Отображать из Positions
            cbx_Positions.ValueMember = "ID_Positions";
            cbx_Positions.DataPropertyName = "ID_Positions"; // Для связи с Books
            cbx_Positions.MaxDropDownItems = 10;
            cbx_Positions.FlatStyle = FlatStyle.Flat;
            dataGridView1.Columns.Insert(1, cbx_Positions);
            dataGridView1.Columns[1].Width = 200;

            bindingSource = new BindingSource(this.ds.Tables["Employees"], "");
            dataGridView1.DataSource = bindingSource;
            bindingNavigator4.BindingSource = bindingSource;


        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            //employeesTableAdapter.Update(repairDataSet);
            adapter_Employees.Update(ds.Tables["Employees"]);
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Удалить  запись?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Введите данные согласно шаблону!\n" +
                "Дата рождения: дд.мм.гггг\n" +
                "Телефон:8-(ххх)-хх-хх-хх");
        }

        private void BindingNavigator4_RefreshItems(object sender, EventArgs e)
        {

        }
    }
}
