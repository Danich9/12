using System;
using System.Windows.Forms;

namespace Repair_DB
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

       
        private void должностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dolgnosti dolgnost = new dolgnosti();
            dolgnost.Show();
        }

        private void услугиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uslugi uslugi = new uslugi();
            uslugi.Show();
        }

        private void клиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clients client = new clients();
            client.Show();
        }

        private void сотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            employees empl = new employees();
            empl.Show();
        }

        private void оборудованиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            objects objectt = new objects();
            objectt.Show();
        }

        private void ремонтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            repair rep = new repair();
            rep.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
        }

        private void ExitMain_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Close();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void поФамилииКлиентаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchLastName sln = new SearchLastName();
            sln.Show();
        }

        private void выполнениеРемонтовЗаПериодToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformingRepairs prf = new PerformingRepairs();
            prf.Show();
        }

        private void комплектующиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Components cmp = new Components();
            cmp.Show();
        }

        private void остаткаЗапчастейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            remainingParts rmp = new remainingParts();
            rmp.Show();
        }

        private void заказовВРаботеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ordersProgress orp = new ordersProgress();
            orp.Show();
        }

        private void выполненныхЗаказовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            completedOrders cpo = new completedOrders();
            cpo.Show();
        }

        private void изСотрудниковБольшеВыполнилЗаПериодToolStripMenuItem_Click(object sender, EventArgs e)
        {
            employeesPeriod emper = new employeesPeriod();
            emper.Show();
        }

        private void изСотрудниковНаБольшуюСуммуОтремонтировалToolStripMenuItem_Click(object sender, EventArgs e)
        {
            employeesRepaired emrp = new employeesRepaired();
            emrp.Show();
        }

        private void формированиеКвитанцийНаОплатуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formationReceipts frmr = new formationReceipts();
            frmr.Show();
        }
    }
}