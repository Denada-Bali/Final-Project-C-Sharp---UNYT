using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Final_Project_C_Sharp___UNYT
{
    public partial class Score : Form
    {
        public int studentId;
        public string cName, LeGradec;
        public double Grade;


        CourseClass course = new CourseClass(); // I have called the course class
        StudentClass student = new StudentClass(); // I have called the student class
        ScoreClass score = new ScoreClass(); // I have called the score class

        public Score()
        {
            InitializeComponent();
        }


        private void Score_Load(object sender, EventArgs e)
        {

            //populate the combobox with courses name
            comboBox_course.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM `course`"));
            comboBox_course.DisplayMember = "CourseName";
            comboBox_course.ValueMember = "CourseName";
            // to show data on score datagridview

            //To Display the student list on Datagridview
            DataGridView_student.DataSource = student.getList(new MySqlCommand("SELECT `StdId`,`StdFirstName`,`StdLastName`,`Program` FROM `student`"));
            DataGridView_student.ReadOnly = true;
        }

        //create a function to show data on datagridview score
        private void showScoe()
        {
            // the data is taken from the database
            DataGridView_student.DataSource = score.getList(new MySqlCommand("SELECT score.StudentId,student.StdFirstName,student.StdLastName,score.CourseName,score.Score,score.Grade FROM student INNER JOIN score ON score.StudentId=student.StdId"));
        }

        //Display student data from student to textbox
        private void DataGridView_student_Click(object sender, EventArgs e)
        {
            textBox_stdId.Text = DataGridView_student.CurrentRow.Cells[0].Value.ToString();
        }

        //add score
        private void button_add_Click(object sender, EventArgs e)
        {
            // this contition check if the student Id and student grade are empty
            if (textBox_stdId.Text == "" || textBox_Grade.Text == "")
            {
                //the app show an error message if the data are empty
                MessageBox.Show("Need grade data", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                studentId = Convert.ToInt32(textBox_stdId.Text);
                cName = comboBox_course.Text;
                Grade = Convert.ToInt32(textBox_Grade.Text);
                LeGradec = textBox_LetterGrade.Text;

                string cs = "datasource=localhost;port=3306;username=root;password=;database=studentdb; convert zero datetime=True";

                var con = new MySqlConnection(cs);
                con.Open();

                string sql = "SELECT `Program` FROM `student` WHERE `Stdid`=" + studentId;

                var cmd = new MySqlCommand(sql, con);

                MySqlDataReader rdr = cmd.ExecuteReader();


                if (rdr.Read())
                {
                    if (rdr.GetValue(0).ToString().Equals("4 Year")) { 
                        textBox_LetterGrade.Text = rdr.GetValue(0).ToString();
                        fourthYearSystem(Grade);
                    }
                    else
                    {
                        textBox_LetterGrade.Text = rdr.GetValue(0).ToString();
                        threeYearSystem(Grade);
                    }

                }

                    //this condition checks if the grade has not been previously introduced to this student
                    //by checking the student by ID and name
                    if (!score.checkScore(studentId, cName))
                    {
                        //this condition check if the data is not empty
                        if (score.insertScore(studentId, cName, Grade, LeGradec))
                        {
                            showScoe();
                            button_clear.PerformClick();// The application automatically deletes the previously entered data
                                                        //show a message that a new course has been inserted
                            MessageBox.Show("New grade added", "Add Grade", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {   //shows an error message
                            MessageBox.Show("Grade not added", "Add Grade", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else //if the score exists the program indicates an error message
                    {
                        MessageBox.Show("The grade for this course are alerady exists", "Add Grade", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }

            private void fourthYearSystem(double Grade)
            {

                MessageBox.Show("The student is enrolled in the 4-year program.", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LeGradec = "F";

                if (Grade >= 96 && Grade <= 100)
                {
                    //MessageBox.Show("A");
                    LeGradec = "A";
                }
                else if (Grade >= 90 && Grade <= 95)
                {
                    //MessageBox.Show("A-");
                    LeGradec = "A-";
                }
                else if (Grade >= 87 && Grade <= 89)
                {
                    //MessageBox.Show("B+");
                    LeGradec = "B+";
                }
                else if (Grade >= 83 && Grade <= 86)
                {
                    //MessageBox.Show("B");
                    LeGradec = "B";
                }
                else if (Grade >= 80 && Grade <= 82)
                {
                    //MessageBox.Show("B-");
                    LeGradec = "B-";
                }
                else if (Grade >= 77 && Grade <= 79)
                {
                    //MessageBox.Show("C+");
                    LeGradec = "C+";
                }
                else if (Grade >= 73 && Grade <= 76)
                {
                    //MessageBox.Show("C");
                    LeGradec = "C";
                }
                else if (Grade >= 70 && Grade <= 72)
                {
                    //MessageBox.Show("C-");
                    LeGradec = "C-";
                }
                else if (Grade >= 67 && Grade <= 69)
                {
                    //MessageBox.Show("D+");
                    LeGradec = "D+";
                }
                else if (Grade >= 63 && Grade <= 66)
                {
                    //MessageBox.Show("D");
                    LeGradec = "D";
                }
                else if (Grade >= 60 && Grade <= 62)
                {
                    //MessageBox.Show("D-");
                    LeGradec = "D-";
                }
            }

            private void threeYearSystem(double Grade)
            {
                MessageBox.Show("The student is enrolled in the 3-year program.", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LeGradec = "F";

                if (Grade >= 90 && Grade <= 100)
                {
                    MessageBox.Show("A");
                    LeGradec = "A";
                }
                else if (Grade >= 85 && Grade <= 89)
                {
                    MessageBox.Show("A-");
                    LeGradec = "A-";
                }
                else if (Grade >= 80 && Grade <= 84)
                {
                    MessageBox.Show("B+");
                    LeGradec = "B+";
                }
                else if (Grade >= 70 && Grade <= 79)
                {
                    MessageBox.Show("B");
                    LeGradec = "B";
                }
                else if (Grade >= 65 && Grade <= 69)
                {
                    MessageBox.Show("B-");
                    LeGradec = "B-";
                }
                else if (Grade >= 60 && Grade <= 64)
                {
                    MessageBox.Show("C+");
                    LeGradec = "C+";
                }
                else if (Grade >= 50 && Grade <= 59)
                {
                    MessageBox.Show("C");
                    LeGradec = "C";
                }
                else if (Grade >= 45 && Grade <= 49)
                {
                    MessageBox.Show("C-");
                    LeGradec = "C-";
                }
                else if (Grade >= 40 && Grade <= 44)
                {
                    MessageBox.Show("D+");
                    LeGradec = "D+";
                }
                else if (Grade >= 35 && Grade <= 39)
                {
                    MessageBox.Show("D");
                    LeGradec = "D";
                }


            }

            //this button shows in DataGridView the student data (ID, first name, and last name)
            //which are taken from the student database
            private void button_sStudent_Click_1(object sender, EventArgs e)
            {
                DataGridView_student.DataSource = student.getList(new MySqlCommand("SELECT `StdId`,`StdFirstName`,`StdLastName`,`Program` FROM `student`"));
            }

            //this button displays in the datagridview the student data (ID, first and last name, course name, grade, and grade in letters).
            //which are taken from the score database 
            private void button_sScore_Click_1(object sender, EventArgs e)
            {
                showScoe();
            }

            // delete the above fields (student ID, grade, course, Letter Grade)
            private void button_clear_Click_1(object sender, EventArgs e)
            {
                textBox_stdId.Clear();
                textBox_Grade.Clear();
                comboBox_course.SelectedIndex = 0;
                textBox_LetterGrade.Clear();
            }
        }
    }