using System;
using System.Windows.Forms;

namespace Repair_DB
{
    public partial class clients : Form
    {
        public clients()
        {
            InitializeComponent();
        }

        private void clients_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "repairDataSet.Customers". При необходимости она может быть перемещена или удалена.
            this.customersTableAdapter.Fill(this.repairDataSet.Customers);


        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            customersTableAdapter.Update(repairDataSet);
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
               "Услуга: Наименование.\n" +
               "Телефон: Цена в числовом формате!");
        }
    }
}
