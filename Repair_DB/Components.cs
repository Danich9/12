using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Repair_DB
{
    public partial class Components : Form
    {
        public Components()
        {
            InitializeComponent();
        }

        private void Components_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "repairDataSet.Components". При необходимости она может быть перемещена или удалена.
            this.componentsTableAdapter.Fill(this.repairDataSet.Components);

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            componentsTableAdapter.Update(repairDataSet);
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Удалить  запись?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Введите данные согласно шаблону!\n" +
              "Услуга: Наименование.\n" +
              "Телефон: Цена в числовом формате!");
        }
    }
}
