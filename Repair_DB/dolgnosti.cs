using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Repair_DB
{
    public partial class dolgnosti : Form
    {

        public Regex reg;
        public dolgnosti()
        {
            InitializeComponent();
        }

        private void dolgnosti_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "repairDataSet.Positions". При необходимости она может быть перемещена или удалена.
            this.positionsTableAdapter.Fill(this.repairDataSet.Positions);


        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                positionsTableAdapter.Update(repairDataSet);

            }
            catch
            {
                MessageBox.Show("Зарплата должна вводится в числах");
            }
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

        private void Dolgnosti_TextChanged(object sender, EventArgs e)
        {
            reg = new Regex("[0-9]*");

        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Введите зарплату в числах!");
        }

        private void BindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }
    }
}
