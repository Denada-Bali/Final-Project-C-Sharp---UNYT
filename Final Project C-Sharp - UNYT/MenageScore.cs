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

namespace Final_Project_C_Sharp___UNYT
{
    public partial class MenageScore : Form
    {
        public int stdId;
        public string cName, LeGrade;
        public double grade;


        CourseClass course = new CourseClass();
        ScoreClass score = new ScoreClass();
        public MenageScore()
        {
            InitializeComponent();
        }
        private void ManageScore_Load(object sender, EventArgs e)
        {
            //populate the combobox with courses name
            comboBox_course.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM `course`"));
            comboBox_course.DisplayMember = "CourseName";
            comboBox_course.ValueMember = "CourseName";
            // to show score data on datagridview
            showScore();
        }

        //create a function to show data on datagridview score
        public void showScore()
        {
            DataGridView_score.ReadOnly = true;
            // the data is taken from the database
            DataGridView_score.DataSource = score.getList(new MySqlCommand("SELECT score.StudentId,student.StdFirstName,student.StdLastName,score.CourseName,score.Score,score.Grade FROM student INNER JOIN score ON score.StudentId=student.StdId"));
        }

        //Display student data from course to textbox
        private void DataGridView_course_Click(object sender, EventArgs e)
        {
            // the application display all curse information in DataGridView
            //information- student ID, course, grade, letter grade
            textBox_stdId.Text = DataGridView_score.CurrentRow.Cells[0].Value.ToString();
            comboBox_course.Text = DataGridView_score.CurrentRow.Cells[3].Value.ToString();
            textBox_Grade.Text = DataGridView_score.CurrentRow.Cells[4].Value.ToString();
            textBox_LetterGrade.Text = DataGridView_score.CurrentRow.Cells[5].Value.ToString();
        }

        //this button checks for the student, with the course name, student name, and surname
        private void button_search_Click(object sender, EventArgs e)
        {
            DataGridView_score.DataSource = score.getList(new MySqlCommand("SELECT score.StudentId, student.StdFirstName, student.StdLastName, score.CourseName, score.Grade, score.LetterGrade FROM student INNER JOIN score ON score.StudentId = student.StdId WHERE CONCAT(student.StdFirstName, student.StdLastName, score.CourseName)LIKE '%" + textBox_search.Text + "%'"));

        }
        // delete the above fields (student ID, grade, course, Letter Grade, search button)
        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_stdId.Clear();
            textBox_Grade.Clear();
            textBox_LetterGrade.Clear();
            textBox_search.Clear();
        }

        private void button_Update_Click_1(object sender, EventArgs e)
        {
            // this contition check if the student ID and course Grade are empty
            if (textBox_stdId.Text == "" || textBox_Grade.Text == "")
            {
                // the app show an error message if the data are empty
                MessageBox.Show("Need grade data", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else //this condition check if the data is not empty
            {
                 stdId = Convert.ToInt32(textBox_stdId.Text);
                 cName = comboBox_course.Text;
                 grade = Convert.ToInt32(textBox_Grade.Text);
                 LeGrade = textBox_LetterGrade.Text;


                string cs = "datasource=localhost;port=3306;username=root;password=;database=studentdb; convert zero datetime=True";

                var con = new MySqlConnection(cs);
                con.Open();

                string sql = "SELECT `Program` FROM `student` WHERE `Stdid`=" + stdId;

                var cmd = new MySqlCommand(sql, con);

                MySqlDataReader rdr = cmd.ExecuteReader();


                if (rdr.Read())
                {
                    if (rdr.GetValue(0).ToString().Equals("4 Year"))
                    {
                        textBox_LetterGrade.Text = rdr.GetValue(0).ToString();
                        fourthYearSystem(grade);
                    }
                    else
                    {
                        textBox_LetterGrade.Text = rdr.GetValue(0).ToString();
                        threeYearSystem(grade);
                    }

                }

                //this condition enters the data (ID, Name, Grade, Letter Grade)
                if (score.updateScore(stdId, cName, grade, LeGrade))
                {
                    showScore();  //show score list on datagridview 
                    button_clear.PerformClick(); //The application automatically deletes the previously entered data
                                                 //show a message that a new course has been edited
                    MessageBox.Show("Grade Edited Complete", "Update Grade", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    //shows an error message if the data are not edited
                    MessageBox.Show("Grade not edit", "Update Grade", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
         
        }


        private void fourthYearSystem(double Grade)
        {

            MessageBox.Show("The student is enrolled in the 4-year program.", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LeGrade = "F";

            if (Grade >= 96 && Grade <= 100)
            {
                //MessageBox.Show("A");
                LeGrade = "A";
            }
            else if (Grade >= 90 && Grade <= 95)
            {
                //MessageBox.Show("A-");
                LeGrade = "A-";
            }
            else if (Grade >= 87 && Grade <= 89)
            {
                //MessageBox.Show("B+");
                LeGrade = "B+";
            }
            else if (Grade >= 83 && Grade <= 86)
            {
                //MessageBox.Show("B");
                LeGrade = "B";
            }
            else if (Grade >= 80 && Grade <= 82)
            {
                //MessageBox.Show("B-");
                LeGrade = "B-";
            }
            else if (Grade >= 77 && Grade <= 79)
            {
                //MessageBox.Show("C+");
                LeGrade = "C+";
            }
            else if (Grade >= 73 && Grade <= 76)
            {
                //MessageBox.Show("C");
                LeGrade = "C";
            }
            else if (Grade >= 70 && Grade <= 72)
            {
                //MessageBox.Show("C-");
                LeGrade = "C-";
            }
            else if (Grade >= 67 && Grade <= 69)
            {
                //MessageBox.Show("D+");
                LeGrade = "D+";
            }
            else if (Grade >= 63 && Grade <= 66)
            {
                //MessageBox.Show("D");
                LeGrade = "D";
            }
            else if (Grade >= 60 && Grade <= 62)
            {
                //MessageBox.Show("D-");
                LeGrade = "D-";
            }
        }

        private void threeYearSystem(double Grade)
        {
            MessageBox.Show("The student is enrolled in the 3-year program.", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LeGrade = "F";

            if (Grade >= 90 && Grade <= 100)
            {
                MessageBox.Show("A");
                LeGrade = "A";
            }
            else if (Grade >= 85 && Grade <= 89)
            {
                MessageBox.Show("A-");
                LeGrade = "A-";
            }
            else if (Grade >= 80 && Grade <= 84)
            {
                MessageBox.Show("B+");
                LeGrade = "B+";
            }
            else if (Grade >= 70 && Grade <= 79)
            {
                MessageBox.Show("B");
                LeGrade = "B";
            }
            else if (Grade >= 65 && Grade <= 69)
            {
                MessageBox.Show("B-");
                LeGrade = "B-";
            }
            else if (Grade >= 60 && Grade <= 64)
            {
                MessageBox.Show("C+");
                LeGrade = "C+";
            }
            else if (Grade >= 50 && Grade <= 59)
            {
                MessageBox.Show("C");
                LeGrade = "C";
            }
            else if (Grade >= 45 && Grade <= 49)
            {
                MessageBox.Show("C-");
                LeGrade = "C-";
            }
            else if (Grade >= 40 && Grade <= 44)
            {
                MessageBox.Show("D+");
                LeGrade = "D+";
            }
            else if (Grade >= 35 && Grade <= 39)
            {
                MessageBox.Show("D");
                LeGrade = "D";
            }


        }

        //deletes the score from the ID
        private void button_delete_Click(object sender, EventArgs e)
        {
            if (textBox_stdId.Text == "")
            { // the condition checks if the Id field is empty and shows an error message, if is empty 
                MessageBox.Show("Field Error- we need student id", "Delete Grade", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else //if it is not empty
            {
                int id = Convert.ToInt32(textBox_stdId.Text); // input ID
                //the application displays a warning message if the user wants to delete the data
                if (MessageBox.Show("Are you sure you want to remove this grade", "Delete Grade", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (score.deleteScore(id)) // if the id is deleted 
                    {
                        showScore(); //show score list on datagridview 
                        //show a message that a new course has been deleted
                        MessageBox.Show("Grade Removed", "Delete Grade", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button_clear.PerformClick();//The application automatically deletes the previously entered data
                    }
                }

            }
        }

       
    }
}
