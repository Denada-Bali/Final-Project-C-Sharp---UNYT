using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using DGVPrinterHelper;


namespace Final_Project_C_Sharp___UNYT
{
    public partial class Print : Form

    {
    StudentClass student = new StudentClass();
    DGVPrinter printer = new DGVPrinter();

        public Print()
        {
            InitializeComponent();
        }

        private void Print_Load(object sender, EventArgs e)
        {
            showData(new MySqlCommand("SELECT * FROM `student`"));
        }
        // create a function to show the student list in datagridview
        public void showData(MySqlCommand command)
        {
            DataGridView_student.ReadOnly = true;
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            DataGridView_student.DataSource = student.getList(command);
            // column 8 is the image column index
            imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[8];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }
        private void button_check_Click_1(object sender, EventArgs e)
        {
            //check the radio button
            string selectQuery;

            if (radioButton_all.Checked)
            {
                selectQuery = "SELECT* FROM `student`";
            }

            else if (radioButton_male.Checked)
            {
                selectQuery = "SELECT * FROM `student` WHERE `Gender`='Male'";

                //if (fourth_year_program_print_rb.Checked)
                //{
                //    selectQuery = "SELECT * FROM `student` WHERE `Program`= '4-Year'";
                //}

                //else if (three_year_program_print_rb.Checked)
                //{
                //    selectQuery = "SELECT * FROM `student` WHERE `Program`= '3-Year'";
                //}
            }
            else
            {
                selectQuery = "SELECT * FROM `student` WHERE `Gender`='Female'";

                //if (fourth_year_program_print_rb.Checked)
                //{
                //    selectQuery = "SELECT * FROM `student` WHERE `Program`= '4-Year'";
                //}
                //else if (three_year_program_print_rb.Checked)
                //{
                //    selectQuery = "SELECT * FROM `student` WHERE `Program`= '3-Year'";
                //}
            }  
            showData(new MySqlCommand(selectQuery));
        }

        private void button_print_Click_1(object sender, EventArgs e)
        {
            //So, here we need DGVprinter helper for print pdf file
            printer.Title = "Unyt Students list";
            printer.SubTitle = string.Format("Date: {0}", DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "Uiversity of New York Tirana";
            printer.FooterSpacing = 15;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(DataGridView_student);
        }

        private void btn_student_search_Click(object sender, EventArgs e)
        {
            //To Search course and show on datagridview
            DataGridView_student.DataSource = student.searchStudent(txtB_search.Text);
            txtB_search.Clear();
        }

        private void DataGridView_student_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
            //This event is used to avoid the error of DataGridview Error

            MessageBox.Show("Error happened " + anError.Context.ToString());

            if (anError.Context == DataGridViewDataErrorContexts.Commit)
            {
                MessageBox.Show("Commit error");
            }
            if (anError.Context == DataGridViewDataErrorContexts.CurrentCellChange)
            {
                MessageBox.Show("Cell change");
            }
            if (anError.Context == DataGridViewDataErrorContexts.Parsing)
            {
                MessageBox.Show("parsing error");
            }
            if (anError.Context == DataGridViewDataErrorContexts.LeaveControl)
            {
                MessageBox.Show("leave control error");
            }

            if ((anError.Exception) is ConstraintException)
            {
                DataGridView view = (DataGridView)sender;
                view.Rows[anError.RowIndex].ErrorText = "an error";
                view.Rows[anError.RowIndex].Cells[anError.ColumnIndex].ErrorText = "an error";

                anError.ThrowException = false;
            }
        }

        private void DataGridView_student_DataError_1(object sender, DataGridViewDataErrorEventArgs anError)
        {
                //This event is used to avoid the error of DataGridview Error

                MessageBox.Show("Error happened " + anError.Context.ToString());

                if (anError.Context == DataGridViewDataErrorContexts.Commit)
                {
                    MessageBox.Show("Commit error");
                }
                if (anError.Context == DataGridViewDataErrorContexts.CurrentCellChange)
                {
                    MessageBox.Show("Cell change");
                }
                if (anError.Context == DataGridViewDataErrorContexts.Parsing)
                {
                    MessageBox.Show("parsing error");
                }
                if (anError.Context == DataGridViewDataErrorContexts.LeaveControl)
                {
                    MessageBox.Show("leave control error");
                }

                if ((anError.Exception) is ConstraintException)
                {
                    DataGridView view = (DataGridView)sender;
                    view.Rows[anError.RowIndex].ErrorText = "an error";
                    view.Rows[anError.RowIndex].Cells[anError.ColumnIndex].ErrorText = "an error";

                    anError.ThrowException = false;
                }

        }
    }
}
