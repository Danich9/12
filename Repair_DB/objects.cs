using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Repair_DB
{
    public partial class objects : Form
    {
        private OleDbConnection db_connection;
        private DataSet ds;
        private OleDbDataAdapter adapter_Objects;
        private OleDbDataAdapter adapter_Customers;
        private BindingSource bindingSource;

        public objects()
        {
            InitializeComponent();
        }

        private void objects_Load(object sender, System.EventArgs e)
        {

            ds = new DataSet();
            db_connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\repair.mdb");
            db_connection.Open();

            adapter_Objects = new OleDbDataAdapter(new OleDbCommand("SELECT * FROM Objects", db_connection));
            var cb_adapter_Objects = new OleDbCommandBuilder(adapter_Objects);


            adapter_Customers = new OleDbDataAdapter(new OleDbCommand("SELECT * FROM Customers", db_connection));
            var cb_Positions = new OleDbCommandBuilder(adapter_Customers);

            adapter_Objects.Fill(ds, "Objects"); // прочитать Objects
            adapter_Customers.Fill(ds, "Customers"); // прочитать Customers
            adapter_Objects.InsertCommand = cb_adapter_Objects.GetInsertCommand(true);
            adapter_Objects.UpdateCommand = cb_adapter_Objects.GetUpdateCommand(true);
            adapter_Objects.DeleteCommand = cb_adapter_Objects.GetDeleteCommand(true);
            db_connection.Close();

            //ds.Tables["Customers"].Columns["Customer"].Unique = true;
            // Установка связи таблиц 1.ID_Customers = 2.ID_Customers
            ds.Relations.Add(new DataRelation("rlCustomersObjects", ds.Tables["Customers"].Columns["ID_Customers"], ds.Tables["Objects"].Columns["ID_Customers"]));
            // Вывод таблицы
            dataGridView1.DataSource = ds.Tables["Objects"]; // Objects - в DataGrid
            dataGridView1.Columns["ID_Objects"].Visible = false; // скрыть колонку с идентификатором
            dataGridView1.Columns["ID_Customers"].Visible = false;
            // -----------------добавить combobox------------------------------------
            var cbx_Positions = new DataGridViewComboBoxColumn(); // добавить новую колонку
            cbx_Positions.Name = "Клиент";
            cbx_Positions.DataSource = ds.Tables["Customers"];
            cbx_Positions.DisplayMember = "Customer"; // Отображать из Customers
            cbx_Positions.ValueMember = "ID_Customers";
            cbx_Positions.DataPropertyName = "ID_Customers"; // Для связи с Objects
            cbx_Positions.MaxDropDownItems = 10;
            cbx_Positions.FlatStyle = FlatStyle.Flat;
            dataGridView1.Columns.Insert(1, cbx_Positions);
            dataGridView1.Columns[1].Width = 200;
            //--------------------------header-------------------------------------
            dataGridView1.Columns[2].HeaderText = "Клиент";
            dataGridView1.Columns[3].HeaderText = "Оборудование";
            dataGridView1.Columns[4].HeaderText = "Стоимость ремонта";
            dataGridView1.Columns[5].HeaderText = "Дата заявки на ремонт";
            bindingSource = new BindingSource(this.ds.Tables["Objects"], "");
            dataGridView1.DataSource = bindingSource;
            bindingNavigator5.BindingSource = bindingSource;
        }

        private void saveButton_Click(object sender, System.EventArgs e)
        {
            adapter_Objects.Update(ds.Tables["Objects"]);
        }
        
        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Удалить  запись?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

      

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Введите данные согласно шаблону!\n" +
               "Дата: дд.мм.гггг\n" +
               "Цена: В числовом формате!");
        }
    }
}
