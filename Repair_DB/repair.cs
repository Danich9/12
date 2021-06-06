using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Repair_DB
{
    public partial class repair : Form
    {
        private OleDbConnection db_connection;
        private DataSet ds;
        private OleDbDataAdapter adapter_Repair;
        private OleDbDataAdapter adapter_Objects;
        private OleDbDataAdapter adapter_Services;
        private OleDbDataAdapter adapter_Employees;
        private OleDbDataAdapter adapter_Components;
        private BindingSource bindingSource;
        string connectionString = ConfigurationManager.ConnectionStrings["Repair_DB.Properties.Settings.repairConnectionString1"].ConnectionString;

        public static DataTable DataSource { get; private set; }

        public repair()
        {
            InitializeComponent();
            

        }

        private void repair_Load(object sender, System.EventArgs e)
        {
            ds = new DataSet();
            db_connection = new OleDbConnection(connectionString);
            db_connection.Open();

            adapter_Repair = new OleDbDataAdapter(new OleDbCommand("SELECT * FROM Repair", db_connection));
            var cb_adapter_Repair = new OleDbCommandBuilder(adapter_Repair);

            adapter_Objects = new OleDbDataAdapter(new OleDbCommand("SELECT * FROM Objects", db_connection));
            adapter_Services = new OleDbDataAdapter(new OleDbCommand("SELECT * FROM Services", db_connection));
            adapter_Employees = new OleDbDataAdapter(new OleDbCommand("SELECT Employees.* FROM Positions INNER JOIN Employees ON Positions.ID_Positions = Employees.ID_Positions" +
                                                                      " WHERE Positions.Positions = 'Техник' Or Positions.Positions = 'Инженер'", db_connection));
            adapter_Components = new OleDbDataAdapter(new OleDbCommand("SELECT * FROM Components", db_connection));

            adapter_Repair.Fill(ds, "Repair"); // прочитать Repair
            adapter_Objects.Fill(ds, "Objects"); // прочитать Objects
            adapter_Employees.Fill(ds, "Employees"); // прочитать Employees
            adapter_Services.Fill(ds, "Services"); // прочитать Services
            adapter_Components.Fill(ds, "Components"); // прочитать Components
            adapter_Repair.InsertCommand = cb_adapter_Repair.GetInsertCommand(true);
            adapter_Repair.UpdateCommand = cb_adapter_Repair.GetUpdateCommand(true);
            adapter_Repair.DeleteCommand = cb_adapter_Repair.GetDeleteCommand(true);
            db_connection.Close();

            // Установка связи таблиц 1.ID_Objects = 2.ID_Objects
            ds.Relations.Add(new DataRelation("rlRepairObjects", ds.Tables["Objects"].Columns["ID_Objects"], ds.Tables["Repair"].Columns["ID_Objects"]));
            // Установка связи таблиц 3.ID_Services = 4.ID_Services
            ds.Relations.Add(new DataRelation("rlRepairServices", ds.Tables["Services"].Columns["ID_Services"], ds.Tables["Repair"].Columns["ID_Services"]));
            // Установка связи таблиц 5.ID_Employees = 6.ID_Employees
            ds.Relations.Add(new DataRelation("rlRepairEmployees", ds.Tables["Employees"].Columns["ID_Employees"], ds.Tables["Repair"].Columns["ID_Employees"]));
            // Установка связи таблиц 7.ID_Components = 8.ID_Components
            ds.Relations.Add(new DataRelation("rlRepairComponents", ds.Tables["Components"].Columns["ID_Components"], ds.Tables["Repair"].Columns["ID_Components"]));

            // Вывод таблицы
            dataGridView1.DataSource = ds.Tables["Repair"]; // ObjectsRepair - в DataGrid
            dataGridView1.Columns["ID_Repair"].Visible = false; // скрыть колонку с идентификатором
            dataGridView1.Columns["ID_Objects"].Visible = false;
            dataGridView1.Columns["ID_Services"].Visible = false;
            dataGridView1.Columns["ID_Employees"].Visible = false;
            dataGridView1.Columns["ID_Components"].Visible = false;
            //--------------------------header-------------------------------------
            dataGridView1.Columns["Date_repair"].HeaderText = "Дата ремонта";
            dataGridView1.Columns["Status_repair"].HeaderText = "Статус ремонта";
            dataGridView1.Columns["Status_cost"].HeaderText = "Статус оплаты";

            // -----------------добавить 1 combobox------------------------------------
            var cbx_Positions = new DataGridViewComboBoxColumn(); // добавить новую колонку
            cbx_Positions.Name = "Оборудование";
            cbx_Positions.DataSource = ds.Tables["Objects"];
            cbx_Positions.DisplayMember = "Objects";
            cbx_Positions.ValueMember = "ID_Objects";
            cbx_Positions.DataPropertyName = "ID_Objects"; // Для связи 
            cbx_Positions.MaxDropDownItems = 10;
            cbx_Positions.FlatStyle = FlatStyle.Flat;
            dataGridView1.Columns.Insert(1, cbx_Positions);
            dataGridView1.Columns[1].Width =    100;

            // -----------------добавить 2 combobox------------------------------------
            var cbx_Services = new DataGridViewComboBoxColumn(); // добавить новую колонку
            cbx_Services.Name = "Услуги";
            cbx_Services.DataSource = ds.Tables["Services"];
            cbx_Services.DisplayMember = "Service";
            cbx_Services.ValueMember = "ID_Services";
            cbx_Services.DataPropertyName = "ID_Services"; // Для связи 
            cbx_Services.MaxDropDownItems = 10;
            cbx_Services.FlatStyle = FlatStyle.Flat;
            dataGridView1.Columns.Insert(2, cbx_Services);
            dataGridView1.Columns[2].Width = 100;

            // -----------------добавить 3 combobox------------------------------------
            var cbx_Employees = new DataGridViewComboBoxColumn(); // добавить новую колонку
            cbx_Employees.Name = "Сотрудники";
            cbx_Employees.DataSource = ds.Tables["Employees"];
            cbx_Employees.DisplayMember = "Employee";
            cbx_Employees.ValueMember = "ID_Employees";
            cbx_Employees.DataPropertyName = "ID_Employees"; // Для связи 
            cbx_Employees.MaxDropDownItems = 10;
            cbx_Employees.FlatStyle = FlatStyle.Flat;
            dataGridView1.Columns.Insert(3, cbx_Employees);
            dataGridView1.Columns[3].Width = 100;

            // -----------------добавить 4 combobox------------------------------------
            var cbx_Components = new DataGridViewComboBoxColumn(); // добавить новую колонку
            cbx_Components.Name = "Комплектующие";
            cbx_Components.DataSource = ds.Tables["Components"];
            cbx_Components.DisplayMember = "Component";
            cbx_Components.ValueMember = "ID_Components";
            cbx_Components.DataPropertyName = "ID_Components"; // Для связи 
            cbx_Components.MaxDropDownItems = 10;
            cbx_Components.FlatStyle = FlatStyle.Flat;
            dataGridView1.Columns.Insert(3, cbx_Components);
            dataGridView1.Columns[3].Width = 120;

            bindingSource = new BindingSource(this.ds.Tables["Repair"], "");
            dataGridView1.DataSource = bindingSource;
            bindingNavigator1.BindingSource = bindingSource;


        }

        private void saveButton_Click(object sender, System.EventArgs e)
        {
            adapter_Repair.Update(ds.Tables["Repair"]);
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
           
             "Дата: В числовом формате вида хх.хх.хххх!");
        }

       
    }
}
