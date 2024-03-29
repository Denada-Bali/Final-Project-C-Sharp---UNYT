﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Final_Project_C_Sharp___UNYT
{
    class StudentClass
    {
        DBconnect connect = new DBconnect();
        //create a function to add a new students to the database
        public bool insertStudent(string fname, string lname, DateTime bdate, string gender, string phone, string address, string program, byte[] img)
        {
            //mysql connection represent an open connection to my database
            //connection string is a string that specifies information about data source                                                                   //The VALUES command specifies the values of an INSERT INTO statement.
            MySqlCommand command = new MySqlCommand("INSERT INTO `student`(`StdFirstName`, `StdLastName`, `Birthdate`, `Gender`, `Phone`, `Address`, `Program`, `Photo`) VALUES(@fn, @ln, @bd, @gd, @ph, @adr,@prg, @img)", connect.getconnection);

            // add dates in my database server
            //@fn, @ln, @bd, @gd, @ph, @adr, @img
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@bd", MySqlDbType.Date).Value = bdate;
            command.Parameters.Add("@gd", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@adr", MySqlDbType.VarChar).Value = address;
            command.Parameters.Add("@prg", MySqlDbType.VarChar).Value = program;
            command.Parameters.Add("@img", MySqlDbType.Blob).Value = img;

            connect.openConnect();// open the connection
            //returns the number of rows affected by the query we are executing.
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }

        }
        //create a function  to get student table
        public DataTable getStudentlist(MySqlCommand command)
        {
            //sets some of its properties, retrieves data from a table, and displays it. 
            //And open the connection
            command.Connection = connect.getconnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        // Create a function to execute the count query(total, male , female)
        public string exeCount(string query)
        {
            MySqlCommand command = new MySqlCommand(query, connect.getconnection);
            connect.openConnect();
            string count = command.ExecuteScalar().ToString();
            connect.closeConnect();
            return count;
        }

        //to get the total student
        public string totalStudent()
        {
            return exeCount("SELECT COUNT(*) FROM student");
        }

        public string totalPrograms()
        {
            return exeCount("SELECT COUNT(DISTINCT Program) FROM student");
        }
        // to get the male student count
        public string maleStudent()
        {
            return exeCount("SELECT COUNT(*) FROM student WHERE `Gender`='Male'");
        }
        
        // to get the female student count
        public string femaleStudent()
        {
            return exeCount("SELECT COUNT(*) FROM student WHERE `Gender`='Female'");
        }

        public string fourthYearProgram()
        {
            return exeCount("SELECT COUNT(*) FROM student WHERE `Program`='4 Year'");
        }

        public string threeYearProgram()
        {
            return exeCount("SELECT COUNT(*) FROM student WHERE `Program`='3 Year'");
        }

        //create a function search for student (first name, last name, address)
        public DataTable searchStudent(string searchdata)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `student` WHERE CONCAT(`StdFirstName`,`StdLastName`,`Address`,`Program`) LIKE '%" + searchdata + "%'", connect.getconnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        
        //create a function edit for student
        public bool updateStudent(int id, string fname, string lname, DateTime bdate, string gender, string phone, string address, string program, byte[] img)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `student` SET `StdFirstName`=@fn,`StdLastName`=@ln,`Birthdate`=@bd,`Gender`=@gd,`Phone`=@ph,`Address`=@adr,`Program`=@prg,`Photo`=@img WHERE  `StdId`= @id", connect.getconnection);

            //@id,@fn, @ln, @bd, @gd, @ph, @adr, @img
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@bd", MySqlDbType.Date).Value = bdate;
            command.Parameters.Add("@gd", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@adr", MySqlDbType.VarChar).Value = address;
            command.Parameters.Add("@prg", MySqlDbType.VarChar).Value = program;
            command.Parameters.Add("@img", MySqlDbType.Blob).Value = img;

            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }

        }
        //Create a function to delete data
        //we need only id 
        public bool deleteStudent(int id)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `student` WHERE `StdId`=@id", connect.getconnection);

            //@id
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }

        }
        // create a function for any command in studentDb
        public DataTable getList(MySqlCommand command)
        {
            command.Connection = connect.getconnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}
