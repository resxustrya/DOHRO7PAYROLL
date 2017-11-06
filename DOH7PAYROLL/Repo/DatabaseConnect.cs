using System;
using System.Collections.Generic;
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
                query = "SELECT (SELECT COUNT(userid) FROM users WHERE emptype = 'JO' AND (fname LIKE '" + search + "%' OR lname LIKE '%" + search + "')) as 'MAX_SIZE', u.userid,u.fname,u.lname,u.emptype,p.working_days,p.monthly_salary,p.minutes_late,p.coop,p.phic,p.disallowance,p.gsis,p.pagibig,p.excess_mobile FROM users u LEFT JOIN payroll p ON u.userid = p.userid WHERE emptype = 'JO'";
                query = query + " AND (fname LIKE '" + search + "%' OR lname LIKE '%"+search+"')";
            }
            else {
                query = "SELECT (SELECT COUNT(userid) FROM users WHERE emptype = 'JO') as 'MAX_SIZE', u.userid,u.fname,u.lname,u.emptype,p.working_days,p.monthly_salary,p.minutes_late,p.coop,p.phic,p.disallowance,p.gsis,p.pagibig,p.excess_mobile FROM users u LEFT JOIN payroll p ON u.userid = p.userid WHERE emptype = 'JO'";
            }
            query = query +" ORDER BY fname,lname LIMIT 10 OFFSET "+ DatabaseConnect.start;

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
                max_size = "0";
            }
            return list;
        }

        public List<Employee> GeneratePayroll(){
            List<Employee> list = new List<Employee>();
            string query = "SELECT u.userid,u.fname,u.lname,u.emptype,p.working_days,p.monthly_salary,p.minutes_late,p.coop,p.phic,p.disallowance,p.gsis,p.pagibig,p.excess_mobile FROM payroll p LEFT JOIN users u ON p.userid = u.userid";
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

        public Boolean ifWeekend(String dateToday) {
            DateTime dateTime = Convert.ToDateTime(dateToday).Date;
            DayOfWeek date = dateTime.DayOfWeek;
            if ((date == DayOfWeek.Saturday) || (date == DayOfWeek.Sunday))
            {
                return true;
            }
            return false;
        }

        public int GetMins(String id,String from,String to)
        {
            List<String> days = new List<String>();
            int month = int.Parse(from.Split('-')[1]);
            int year = int.Parse(from.Split('-')[0]);

            String status = "";

            int from_days = int.Parse(from.Split('-')[2]);
            int to_days = int.Parse(to.Split('-')[2]);
            for (int i=0; i <= (to_days-from_days); i++) {
                days.Add((i + from_days)+"");
            }
            String format = month+" "+year+" ";
            int mins = 0;
            string query = "SELECT datein,time,event FROM dtr_file WHERE userid = '"+id+"' AND datein BETWEEN '"+from+ "' AND '"+to+"'";
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                     int day = int.Parse(dataReader["datein"].ToString().Split('/')[1]);
                    String time = dataReader["time"].ToString();
                    String mEvent = dataReader["event"].ToString();
                    TimeSpan timeSpan = TimeSpan.Parse(time);
                    

                    if (mEvent.Equals("IN"))
                    {
                        if ((timeSpan.Hours < 12) || (timeSpan.Hours == 12 && timeSpan.Minutes == 0))
                        {
                            TimeSpan subtrahend = TimeSpan.Parse("08:00:00");
                            int seconds_subtrahend = (int)subtrahend.TotalSeconds;
                            int seconds_timeSpan = (int)timeSpan.TotalSeconds;
                            int result = (seconds_timeSpan - seconds_subtrahend) / 60;
                            if (result > 0){
                                mins += result;
                            }
                        }
                        else {
                            TimeSpan subtrahend = TimeSpan.Parse("13:00:00");
                            int seconds_subtrahend = (int)subtrahend.TotalSeconds;
                            int seconds_timeSpan = (int)timeSpan.TotalSeconds;
                            int result = (seconds_timeSpan - seconds_subtrahend) / 60;
                            if (result > 0) {
                                mins += result;
                            }
                        }
                    }
                    else {
                            if ((timeSpan.Hours < 12) || (timeSpan.Hours == 12 && timeSpan.Minutes == 0))
                            {
                            TimeSpan subtrahend = TimeSpan.Parse("12:00:00");
                            int seconds_subtrahend = (int)subtrahend.TotalSeconds;
                            int seconds_timeSpan = (int)timeSpan.TotalSeconds;
                            int result = (seconds_subtrahend - seconds_timeSpan) / 60;
                            if (result > 0){
                                mins += result;
                            }
                        }
                        else {
                            TimeSpan subtrahend = TimeSpan.Parse("17:00:00");
                            int seconds_subtrahend = (int)subtrahend.TotalSeconds;
                            int seconds_timeSpan = (int)timeSpan.TotalSeconds;
                            int result = (seconds_subtrahend - seconds_timeSpan) / 60;
                            if (result > 0){
                                mins += result;
                            }
                        }
                    }
                    
                  if (days.Contains(day+"")) {
                        days.Remove(day+"");
                  }

                  
                }
               
                for (int i = 0; i < days.Count; i++) {
                     format = month + "/" + days[i] + "/" + year;
                    if (!ifWeekend(format)) {
                        mins += 480;
                    }
                    
                }
               

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed

            }
           
           
            return mins;
        }
    }
}
