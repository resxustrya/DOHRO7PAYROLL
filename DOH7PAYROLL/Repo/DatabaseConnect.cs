﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DOH7PAYROLL.Models;
using MySql.Data.MySqlClient;



namespace DOH7PAYROLL.Repo
{
    public class DatabaseConnect
    {
        public static MySqlConnection connection=null;
        public static int start=0;
        public static int end= 0;
        public static string search= "";
        private string server;
        private string database;
        private string uid;
        private string password;
        public static string max_size = "0";

        //Constructor
        public DatabaseConnect()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            if (connection == null)
            {
                server = "localhost";
                database = "dohdtr";
                uid = "root";
                password = "";
                string connectionString;
                connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

                connection = new MySqlConnection(connectionString);
            }
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        //                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        //                      MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                // MessageBox.Show(ex.Message);

                return false;
            }
        }


        //Insert Record

        public String Insert(Payroll payroll)
        {

            String query = "INSERT INTO payroll VALUES('0','" + payroll.UserId + "','" + payroll.WorkDays + "','" + payroll.Salary + "','" + payroll.MinutesLate + "','" + payroll.Coop + "','" + payroll.Phic + "','" + payroll.Disallowance + "','" + payroll.Gsis + "','" + payroll.Pagibig + "','" + payroll.ExcessMobile + "')";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                DatabaseConnect.start = 0;
            }
            return "Insert Successfully";
        }
        public String Update(Payroll payroll)
        {

            String query = "UPDATE payroll SET working_days = '"+payroll.WorkDays+"', monthly_salary = '" + payroll.Salary + "',minutes_late = '" + payroll.MinutesLate+ "',coop = '" + payroll.Coop + "',phic = '" + payroll.Phic+ "',disallowance = '" + payroll.Disallowance+ "',gsis = '" + payroll.Gsis + "',pagibig = '" + payroll.Pagibig + "',excess_mobile = '" + payroll.ExcessMobile+ "' WHERE userid = '"+payroll.UserId+"'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                DatabaseConnect.start = 0;
            }
            return "Update Successfully";
        }

        //Select statement
        public List<Employee> Select(String type,String search)
        {
            int temp_start = start;
            int temp_end = end;
            int count = 0;
            if (type.Equals("1"))
            {
                if (start - 10 <= 0)
                {
                    start = 0;
                }
                else
                {
                    start -= 10;
                }
            }
            else if (type.Equals("0"))
            {
                start = end;
            }
            else {
                start = 0;
            }

            string query = "";
            if (!search.Equals(""))
            {
                query = "SELECT (SELECT COUNT(userid) FROM users WHERE emptype = 'JO' AND fname = '"+search+"') as 'MAX_SIZE', u.userid,u.fname,u.lname,u.emptype,p.working_days,p.monthly_salary,p.minutes_late,p.coop,p.phic,p.disallowance,p.gsis,p.pagibig,p.excess_mobile FROM users u LEFT JOIN payroll p ON u.userid = p.userid WHERE emptype = 'JO'";
                query = query + " AND fname = '" + search + "'";
            }
            else {
                query = "SELECT (SELECT COUNT(userid) FROM users WHERE emptype = 'JO') as 'MAX_SIZE', u.userid,u.fname,u.lname,u.emptype,p.working_days,p.monthly_salary,p.minutes_late,p.coop,p.phic,p.disallowance,p.gsis,p.pagibig,p.excess_mobile FROM users u LEFT JOIN payroll p ON u.userid = p.userid WHERE emptype = 'JO'";
            }
            query = query +" LIMIT 10 OFFSET "+ DatabaseConnect.start;

            //Create a list to store the result
            List<Employee> list = new List<Employee>();
           

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    max_size = dataReader["MAX_SIZE"].ToString();
                    count += 1;
                    String userid = dataReader["userid"].ToString();
                    String fname = dataReader["fname"].ToString();
                    String lname = dataReader["lname"].ToString();
                    String emptype = dataReader["emptype"].ToString();
                    String flag = "1";

                    String minutes_late = dataReader["minutes_late"].ToString();
                    if (minutes_late.Equals("") || minutes_late.Equals("NULL"))
                    {
                        minutes_late = "0";
                    }
                    String working_days = dataReader["working_days"].ToString();
                    if (working_days.Equals("") || working_days.Equals("NULL"))
                    {
                        working_days = "0";
                    }
                    String monthly_salary = dataReader["monthly_salary"].ToString();
                    if (monthly_salary.Equals("") || monthly_salary.Equals("NULL") || monthly_salary.Equals("Null") || monthly_salary.Equals(null))
                    {
                        flag = "0";
                        monthly_salary = "0";
                    }
                    String coop = dataReader["coop"].ToString();
                    if (coop.Equals("") || coop.Equals("NULL"))
                    {
                        coop = "0";
                    }
                    String phic = dataReader["phic"].ToString();
                    if (phic.Equals("") || phic.Equals("NULL"))
                    {
                        phic = "0";
                    }
                    String disallowance = dataReader["disallowance"].ToString();
                    if (disallowance.Equals("") || disallowance.Equals("NULL"))
                    {
                        disallowance = "0";
                    }
                    String gsis = dataReader["gsis"].ToString();
                    if (gsis.Equals("") || gsis.Equals("NULL"))
                    {
                        gsis = "0";
                    }
                    String pagibig = dataReader["pagibig"].ToString();
                    if (pagibig.Equals("") || pagibig.Equals("NULL"))
                    {
                        pagibig = "0";
                    }
                    String excess_mobile = dataReader["excess_mobile"].ToString();
                    if (excess_mobile.Equals("") || excess_mobile.Equals("NULL"))
                    {
                        excess_mobile = "0";
                    }

                    Payroll payroll = new Payroll(userid,working_days,monthly_salary,minutes_late,coop,phic
                        ,disallowance,gsis,pagibig,excess_mobile,flag);

                    Employee employee = new Employee(userid,fname,lname,emptype,payroll);
                    list.Add(employee);
                }

                //close Data Reader
                dataReader.Close();

                end = (start + count);
                //close Connection
                this.CloseConnection();

                //return list to be displayed
                
            }
            if (count == 0) {
                start = 0;
                end = 0;
            }
            return list;
        }

        public List<Employee> GeneratePayroll(){
            List<Employee> list = new List<Employee>();
            string query = "SELECT u.userid,u.fname,u.lname,u.emptype,p.working_days,p.monthly_salary,p.minutes_late,p.coop,p.phic,p.disallowance,p.gsis,p.pagibig,p.excess_mobile FROM payroll p LEFT JOIN users p ON p.userid = u.userid WHERE emptype = 'JO'";
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    String userid = dataReader["userid"].ToString();
                    String fname = dataReader["fname"].ToString();
                    String lname = dataReader["lname"].ToString();
                    String emptype = dataReader["emptype"].ToString();
                    String flag = "1";

                    String minutes_late = dataReader["minutes_late"].ToString();
                    if (minutes_late.Equals("") || minutes_late.Equals("NULL"))
                    {
                        minutes_late = "0";
                    }
                    String working_days = dataReader["working_days"].ToString();
                    if (working_days.Equals("") || working_days.Equals("NULL"))
                    {
                        working_days = "0";
                    }
                    String monthly_salary = dataReader["monthly_salary"].ToString();
                    if (monthly_salary.Equals("") || monthly_salary.Equals("NULL") || monthly_salary.Equals("Null") || monthly_salary.Equals(null))
                    {
                        flag = "0";
                        monthly_salary = "0";
                    }
                    String coop = dataReader["coop"].ToString();
                    if (coop.Equals("") || coop.Equals("NULL"))
                    {
                        coop = "0";
                    }
                    String phic = dataReader["phic"].ToString();
                    if (phic.Equals("") || phic.Equals("NULL"))
                    {
                        phic = "0";
                    }
                    String disallowance = dataReader["disallowance"].ToString();
                    if (disallowance.Equals("") || disallowance.Equals("NULL"))
                    {
                        disallowance = "0";
                    }
                    String gsis = dataReader["gsis"].ToString();
                    if (gsis.Equals("") || gsis.Equals("NULL"))
                    {
                        gsis = "0";
                    }
                    String pagibig = dataReader["pagibig"].ToString();
                    if (pagibig.Equals("") || pagibig.Equals("NULL"))
                    {
                        pagibig = "0";
                    }
                    String excess_mobile = dataReader["excess_mobile"].ToString();
                    if (excess_mobile.Equals("") || excess_mobile.Equals("NULL"))
                    {
                        excess_mobile = "0";
                    }

                    Payroll payroll = new Payroll(userid, working_days, monthly_salary, minutes_late, coop, phic
                        , disallowance, gsis, pagibig, excess_mobile, flag);

                    Employee employee = new Employee(userid, fname, lname, emptype, payroll);
                    list.Add(employee);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed

            }
            return list;
        }
    }
}
