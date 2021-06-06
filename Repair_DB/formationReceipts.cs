using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Drawing;

namespace Repair_DB
{
    public partial class formationReceipts : Form
    {
        
        string connectionString = ConfigurationManager.ConnectionStrings["Repair_DB.Properties.Settings.repairConnectionString1"].ConnectionString;
       
        public formationReceipts()
        {
            InitializeComponent();
        }

        private void formationReceipts_Load(object sender, EventArgs e)
        {
            var oleConnection = new OleDbConnection(connectionString);
            oleConnection.Open();
            var dataTableFill = new DataTable();
            var oleCommand = new OleDbCommand("SELECT * FROM Customers", oleConnection);
            var adapter = new OleDbDataAdapter(oleCommand);
            adapter.Fill(dataTableFill);
            comboBox1.DataSource = dataTableFill;
            comboBox1.DisplayMember = "Customer"; // столбец для отображения
            comboBox1.ValueMember = "ID_Customers"; //столбец с id
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
            var oleCommand = new OleDbCommand("SELECT Objects.Objects, Sum(Objects.Cost) AS [Sum-Cost], Components.Component, Sum(Components.Cost) AS [Sum-Cost1] " +
                                              "FROM (Customers INNER JOIN Objects ON Customers.ID_Customers = Objects.ID_Customers) INNER JOIN (Components INNER JOIN Repair ON Components.ID_Components = Repair.ID_Components) " +
                                              "ON Objects.ID_Objects = Repair.ID_Objects " +
                                              "WHERE Customers.ID_Customers = " + id + " AND Repair.Status_repair = 'Завершен' AND Repair.Status_cost = 'Не оплачен' GROUP BY Customers.Customer, Objects.Objects, Components.Component", oleConnection);
            var adapter = new OleDbDataAdapter(oleCommand);
            adapter.Fill(dataTableFill);
            bs1.DataSource = dataTableFill;
            bindingNavigator1.BindingSource = bs1;
            dataGridView1.DataSource = bs1;
            dataGridView1.Columns[0].HeaderText = "Наименование товара";
            dataGridView1.Columns[1].HeaderText = "Цена ремонта";
            dataGridView1.Columns[2].HeaderText = "Наименование комплектующих";
            dataGridView1.Columns[3].HeaderText = "Цена комплектующих";
            oleConnection.Close();
            //-----------------------------------------------------------------
            double sumRp = 0; double sumCmp = 0; double summaAll = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                sumRp += Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value);
                sumCmp += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
            }
            sumRep.Text = sumRp.ToString();
            sumComp.Text = sumCmp.ToString();
            summaAll = sumRp + sumCmp;
            summa.Text = summaAll.ToString();
            //-----------------------------------------------------------------
        }

               
        private void button1_Click(object sender, EventArgs e)
        {
            
            CaptureScreen();
            printDialog1.ShowHelp = true;
            printDialog1.Document = printDocument2;
            DialogResult result = printDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                printDocument2.Print();
            }
        }

        Bitmap memoryImage;

        private void CaptureScreen()
        {
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
        }

         private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }
    }
}
