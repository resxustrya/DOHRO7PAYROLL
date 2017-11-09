using System;
using System.Collections.Generic;
using DOH7PAYROLL.Models;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;


namespace DOH7PAYROLL.Repo
{
    public class DatabaseConnect
    {
        public static MySqlConnection dohdtr=null;
        public static MySqlConnection pis = null;
        public static MySqlConnection dts = null;
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
            if (dohdtr== null)
            {
                server = "localhost";
                database = "dohdtr";
                uid = "root";
                password = "";
                string connectionString;
                connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

                dohdtr = new MySqlConnection(connectionString);
            }
            if (pis == null)
            {
                server = "localhost";
                database = "pis";
                uid = "root";
                password = "";
                string connectionString;
                connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

                pis = new MySqlConnection(connectionString);
            }
            if (dts == null)
            {
                server = "localhost";
                database = "dtsv3_0";
                uid = "root";
                password = "";
                string connectionString;
                connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

                dts = new MySqlConnection(connectionString);
            }
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                dohdtr.Open();
                dts.Open();
                pis.Open();
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
                dohdtr.Close();
                dts.Close();
                pis.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                // MessageBox.Show(ex.Message);

                return false;
            }
        }


        //Insert Record

        public String InsertPDF(String filePath)
        {
            if (!checkPdf(filePath)) {
                String query = "INSERT INTO payroll_pdf VALUES('0',now(),'" + filePath + "')";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, dohdtr);
                    //Create a data reader and Execute the command
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                }
            }
            return "PDF Generated";
        }

        public Boolean checkPdf(String filpath) {

            Boolean found = false;
            String query = "SELECT * FROM payroll_pdf WHERE file_path = '"+filpath+"'";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, dohdtr);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {

                    found = true;
                    break;   
                }
                //Create a data reader and Execute the command
                dataReader.Close();
                this.CloseConnection();

            }
            return found;
        }

        public String Update(Payroll payroll)
        {

            String query = "UPDATE payroll SET date_range = '" + payroll.PayrollDate + "', working_days = '" + payroll.WorkDays + "', monthly_salary = '" + payroll.Salary + "',minutes_late = '" + payroll.MinutesLate + "',coop = '" + payroll.Coop + "',phic = '" + payroll.Phic + "',disallowance = '" + payroll.Disallowance + "',gsis = '" + payroll.Gsis + "',pagibig = '" + payroll.Pagibig + "',excess_mobile = '" + payroll.ExcessMobile + "' WHERE userid = '" + payroll.UserId + "'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, dohdtr);
                //Create a data reader and Execute the command
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                DatabaseConnect.start = 0;
            }
            return "Update Successfully";
        }


        public String Insert(Payroll payroll)
        {

            String query = "INSERT INTO payroll VALUES('0','" + payroll.UserId + "','" + payroll.PayrollDate + "','" + payroll.WorkDays + "','" + payroll.Salary + "','" + payroll.MinutesLate + "','" + payroll.Coop + "','" + payroll.Phic + "','" + payroll.Disallowance + "','" + payroll.Gsis + "','" + payroll.Pagibig + "','" + payroll.ExcessMobile + "')";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, dohdtr);
                //Create a data reader and Execute the command
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                DatabaseConnect.start = 0;
            }
            return "Insert Successfully";
        }
        public List<PdfFile> FetchPdf(String type, String search)
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
            else
            {
                start = 0;
            }

            List<PdfFile> list = new List<PdfFile>();
            String query = "";
            if (!search.Equals(""))
            {
                String from_month = search.Split('/')[0];
                String from_day = search.Split('/')[1];
                String from_year = search.Split('/')[2].Split(' ')[0];

                String to_month = search.Split('/')[2].Split(' ')[2].Split('/')[0];
                String to_day = search.Split('/')[3];
                String to_year = search.Split('/')[4].Split(' ')[0];

                String from = from_year + "-" + from_month + "-" + from_day;
                String to = to_year + "-" + to_month + "-" + to_day;
                //query = "SELECT (SELECT COUNT(username) FROM dtsv3_0.users u,pis.personal_information p WHERE u.username = p.userid AND p.job_status = 'Job Order' AND (fname LIKE '" + search + "%' OR lname LIKE '" + search + "%')) as 'MAX_SIZE',i.position,i.tin_no, u.username,u.fname,u.lname,u.job_satus,p.date_range,p.working_days,p.monthly_salary,p.minutes_late,p.coop,p.phic,p.disallowance,p.gsis,p.pagibig,p.excess_mobile FROM dohdtr.users u LEFT JOIN dohdtr.payroll p ON u.userid = p.userid LEFT JOIN pis.personal_information i ON u.username = i.userid";
                query = "SELECT (SELECT COUNT(id) FROM payroll_pdf WHERE date_created BETWEEN '"+from+"' AND '"+to+ "') as 'MAX_SIZE',id,date_created,file_path FROM payroll_pdf WHERE date_created BETWEEN '" + from + "' AND '" + to + "'";
            }
            else
            {
                query = "SELECT (SELECT COUNT(id) FROM payroll_pdf) as 'MAX_SIZE',id,date_created,file_path FROM payroll_pdf";
                //query = "SELECT (SELECT COUNT(username) FROM dtsv3_0.users u,pis.personal_information p WHERE u.username = p.userid AND p.job_status = 'Job Order') as 'MAX_SIZE',i.position,i.tin_no,u.username,u.fname,u.lname,i.job_status,p.working_days,p.date_range,p.monthly_salary,p.minutes_late,p.coop,p.phic,p.disallowance,p.gsis,p.pagibig,p.excess_mobile FROM dtsv3_0.users u RIGHT JOIN pis.personal_information i ON u.username = i.userid RIGHT JOIN dohdtr.payroll p ON u.username = p.userid WHERE i.job_status = 'Job Order'";
            }
            query = query + " ORDER BY date_created LIMIT 10 OFFSET " + DatabaseConnect.start;

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, dohdtr);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    max_size = dataReader["MAX_SIZE"].ToString();
                    count += 1;
                    String id = dataReader["id"].ToString();
                    String date = dataReader["date_created"].ToString().Split(' ')[0];
                    String path = dataReader["file_path"].ToString();
                    PdfFile pdf = new PdfFile(id,path,date);
                    list.Add(pdf);
                }
                //Create a data reader and Execute the command
                end = (start + count);
                dataReader.Close();
                this.CloseConnection();
            }
            if (count == 0)
            {
                start = 0;
                end = 0;
                max_size = "0";
            }
            return list;
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
                //query = "SELECT (SELECT COUNT(username) FROM dtsv3_0.users u,pis.personal_information p WHERE u.username = p.userid AND p.job_status = 'Job Order' AND (fname LIKE '" + search + "%' OR lname LIKE '" + search + "%')) as 'MAX_SIZE',i.position,i.tin_no, u.username,u.fname,u.lname,u.job_satus,p.date_range,p.working_days,p.monthly_salary,p.minutes_late,p.coop,p.phic,p.disallowance,p.gsis,p.pagibig,p.excess_mobile FROM dohdtr.users u LEFT JOIN dohdtr.payroll p ON u.userid = p.userid LEFT JOIN pis.personal_information i ON u.username = i.userid";
                query = "SELECT (SELECT COUNT(username) FROM dtsv3_0.users u LEFT JOIN pis.personal_information i ON u.username = i.userid WHERE i.job_status = 'Job Order' AND (u.fname LIKE '" + search + "%' OR u.lname LIKE '" + search + "%')) as 'MAX_SIZE' , i.position,i.tin_no,u.username,u.fname,u.lname,i.job_status,p.working_days,p.date_range,p.monthly_salary,p.minutes_late,p.coop,p.phic,p.disallowance,p.gsis,p.pagibig,p.excess_mobile FROM dtsv3_0.users u LEFT JOIN pis.personal_information i ON u.username = i.userid LEFT JOIN dohdtr.payroll p ON u.username = p.userid WHERE i.job_status = 'Job Order'";
                query = query + " AND (u.fname LIKE '" + search + "%' OR u.lname LIKE '"+search+"%')";
            }
            else {
                query = "SELECT (SELECT COUNT(username) FROM dtsv3_0.users u LEFT JOIN pis.personal_information i ON u.username = i.userid WHERE i.job_status = 'Job Order') as 'MAX_SIZE' , i.position,i.tin_no,u.username,u.fname,u.lname,i.job_status,p.working_days,p.date_range,p.monthly_salary,p.minutes_late,p.coop,p.phic,p.disallowance,p.gsis,p.pagibig,p.excess_mobile FROM dtsv3_0.users u LEFT JOIN pis.personal_information i ON u.username = i.userid LEFT JOIN dohdtr.payroll p ON u.username = p.userid WHERE i.job_status = 'Job Order'";
                //query = "SELECT (SELECT COUNT(username) FROM dtsv3_0.users u,pis.personal_information p WHERE u.username = p.userid AND p.job_status = 'Job Order') as 'MAX_SIZE',i.position,i.tin_no,u.username,u.fname,u.lname,i.job_status,p.working_days,p.date_range,p.monthly_salary,p.minutes_late,p.coop,p.phic,p.disallowance,p.gsis,p.pagibig,p.excess_mobile FROM dtsv3_0.users u RIGHT JOIN pis.personal_information i ON u.username = i.userid RIGHT JOIN dohdtr.payroll p ON u.username = p.userid WHERE i.job_status = 'Job Order'";
            }
            query = query +" ORDER BY u.fname,u.lname LIMIT 10 OFFSET "+ DatabaseConnect.start;

            //Create a list to store the result
            List<Employee> list = new List<Employee>();
           

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, dohdtr);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    max_size = dataReader["MAX_SIZE"].ToString();
                    count += 1;
                    String userid = dataReader["username"].ToString();
                    String fname = dataReader["fname"].ToString();
                    String lname = dataReader["lname"].ToString();
                    String date_range = dataReader["date_range"].ToString();
                    String emptype = dataReader["position"].ToString();
                    String tin = dataReader["tin_no"].ToString();
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

                    Payroll payroll = new Payroll(userid, date_range,working_days, monthly_salary,minutes_late,coop,phic
                        ,disallowance,gsis,pagibig,excess_mobile,flag);

                    Employee employee = new Employee(userid,fname,lname,emptype,tin,"",payroll);
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
            string query = "SELECT d.description,u.username,u.fname,u.lname,i.position,i.tin_no,p.working_days,p.monthly_salary,p.minutes_late,p.coop,p.phic,p.disallowance,p.gsis,p.pagibig,p.excess_mobile FROM dohdtr.payroll p LEFT JOIN (dtsv3_0.users u LEFT JOIN dtsv3_0.division d ON u.division = d.id) ON p.userid = u.username LEFT JOIN pis.personal_information i ON p.userid = i.userid ORDER BY d.description,u.fname,u.lname ASC";
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, dohdtr);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    String tin = dataReader["tin_no"].ToString();
                    String desc = dataReader["description"].ToString().ToUpper();
                    String userid = dataReader["username"].ToString();
                    String fname = dataReader["fname"].ToString();
                    String lname = dataReader["lname"].ToString();
                    String emptype = dataReader["position"].ToString();
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

                    Payroll payroll = new Payroll(userid,"", working_days, monthly_salary, minutes_late, coop, phic
                        , disallowance, gsis, pagibig, excess_mobile, flag);

                    Employee employee = new Employee(userid, fname, lname, emptype,tin,desc, payroll);
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
        public Boolean IsHolilday(String date)
        {
            Boolean found = false;
            String month = (int.Parse(date.Split('/')[0]) > 9)? date.Split('/')[0]: "0"+date.Split('/')[0];
            String day = (int.Parse(date.Split('/')[1]) > 9) ? date.Split('/')[1] : "0" + date.Split('/')[1];
            String year = date.Split('/')[2];

            date = year + "-" + month+ "-" + day;

            List<Employee> list = new List<Employee>();
            string query = "SELECT id FROM calendar WHERE start <= '"+date+ "' AND end >= '" + date + "'";
            //Create Command
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, dohdtr);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    found = true;
                    break;
                }
                dataReader.Close();
                this.CloseConnection();
            }

                //close Data Reader
            return found;
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


        public String GetMins(String id,String from,String to)
        {
            List<String> days = new List<String>();
            int month = int.Parse(from.Split('-')[1]);
            int year = int.Parse(from.Split('-')[0]);
            int from_days = int.Parse(from.Split('-')[2]);
            int to_days = int.Parse(to.Split('-')[2]);
            int no_days = DateTime.DaysInMonth(year, month);
            int counter = 0;
            int mins = 0;
            
            for (int i=0; i <= (to_days-from_days); i++) {
                days.Add((i + from_days)+"");
            }


            //string query = "SELECT datein,time,event FROM dtr_file WHERE userid = '"+id+"' AND datein BETWEEN '"+from+ "' AND '"+to+"'";
            String query = "SELECT DISTINCT e.userid, datein,holiday,remark, (SELECT  CONCAT(t1.time, '_', t1.edited) FROM dtr_file t1 WHERE userid = d.userid and datein = d.datein and t1.time < '12:00:00' AND t1.event = 'IN' ORDER BY time ASC LIMIT 1) as am_in, (SELECT CONCAT(t2.time,'_',t2.edited) FROM dtr_file t2 WHERE userid = d.userid and datein = d.datein and (SELECT CONCAT(t1.time,'_',t1.edited) FROM dtr_file t1 WHERE userid = d.userid and datein = d.datein and t1.time < '12:00:00' AND t1.event = 'IN' ORDER BY time ASC LIMIT 1) and t2.time < '13:00:00' AND t2.event = 'OUT' AND t2.time > '08:00:00' ORDER BY t2.time DESC LIMIT 1 ) as am_out,(SELECT CONCAT(t3.time,'_',t3.edited) FROM dtr_file t3 WHERE userid = d.userid AND datein = d.datein and t3.time > '12:00:00' and t3.time < '17:00:00' AND t3.event = 'IN' ORDER BY t3.time ASC LIMIT 1) as pm_in,(SELECT CONCAT(t4.time,'_',t4.edited) FROM dtr_file t4 WHERE userid = d.userid AND datein = d.datein and t4.time >= '13:00:00' AND t4.event = 'OUT' ORDER BY time DESC LIMIT 1) as pm_out FROM dtr_file d LEFT JOIN users e ON d.userid = e.userid and datein = d.datein or (datein between '"+from+"' AND '"+to+"' and holiday = '001') or (datein between '"+from+"' AND '"+to+"' and holiday = '002' and d.userid = e.userid) or (datein between '"+from+"' AND '"+to+"' and holiday = '003' and d.userid = e.userid) or (datein between '"+from+"' AND '"+to+"' and holiday = '004' and d.userid = e.userid) or (datein between '"+from+"' AND '"+to+"' and holiday = '005' and d.userid = e.userid) or (datein between '"+from+"' AND '"+to+"' and holiday = '006' and d.userid = e.userid) WHERE d.datein BETWEEN '"+from+"' AND '"+to+"' AND e.userid = '0001' group by d.datein ORDER BY datein ASC";
            if (this.OpenConnection() == true)
            {
                //Create Command
                try
                {
                        MySqlCommand cmd = new MySqlCommand(query, dohdtr);
                        //format = "CALL GETLOGS('8:00:00','12:00:00','13:00:00','17:00:00','0001','2017-05-02','2017-05-02')";
                        //Create a data reader and Execute the command
                        MySqlDataReader dataReader = cmd.ExecuteReader();

                        //Read the data and store them in the list
                        while (dataReader.Read())
                        {
                        //int day = int.Parse(dataReader["datein"].ToString().Split('-')[2]);
                        String day = dataReader["datein"].ToString().Split('/')[1];                        
                        String am_in = dataReader["am_in"].ToString();
                        if (!am_in.Equals("NULL")) { am_in = am_in.Split('_')[0]; }
                        String am_out = dataReader["am_out"].ToString();
                        if (!am_out.Equals("NULL")) { am_out = am_out.Split('_')[0]; }
                        String pm_in = dataReader["pm_in"].ToString();
                        if (!pm_in.Equals("NULL")) { pm_in = pm_in.Split('_')[0]; }
                        String pm_out = dataReader["pm_out"].ToString();
                        if (!pm_out.Equals("NULL")) { pm_out = pm_out.Split('_')[0]; }

                        if (!am_in.Equals("NULL") && !am_in.Equals(""))
                        {
                            TimeSpan timeSpan = TimeSpan.Parse(am_in);
                            TimeSpan subtrahend = TimeSpan.Parse("08:00:00");
                            int seconds_subtrahend = (int)subtrahend.TotalSeconds;
                            int seconds_timeSpan = (int)timeSpan.TotalSeconds;
                            int result = (seconds_timeSpan - seconds_subtrahend) / 60;
                            if (result > 0)
                            {
                                mins += result;
                            }
                        }
                        else {
                            mins += 240;
                        }
                            if (!am_out.Equals("NULL") && !am_out.Equals(""))
                            {
                                TimeSpan timeSpan = TimeSpan.Parse(am_out);
                                TimeSpan subtrahend = TimeSpan.Parse("12:00:00");
                                int seconds_subtrahend = (int)subtrahend.TotalSeconds;
                                int seconds_timeSpan = (int)timeSpan.TotalSeconds;
                                int result = (seconds_subtrahend - seconds_timeSpan) / 60;
                                if (result > 0)
                                {
                                    mins += result;
                                }
                            }
                        else
                        {
                            mins += 240;
                        }
                        if (!pm_in.Equals("NULL") && !pm_in.Equals(""))
                            {
                                TimeSpan timeSpan = TimeSpan.Parse(pm_in);
                                TimeSpan subtrahend = TimeSpan.Parse("13:00:00");
                                int seconds_subtrahend = (int)subtrahend.TotalSeconds;
                                int seconds_timeSpan = (int)timeSpan.TotalSeconds;
                                int result = (seconds_timeSpan - seconds_subtrahend) / 60;
                                if (result > 0)
                                {
                                    mins += result;
                                }
                            }
                        else
                        {
                            mins += 240;
                        }
                        if (!pm_out.Equals("NULL") && !pm_out.Equals(""))
                            {
                                TimeSpan timeSpan = TimeSpan.Parse(pm_out);
                                TimeSpan subtrahend = TimeSpan.Parse("17:00:00");
                                int seconds_subtrahend = (int)subtrahend.TotalSeconds;
                                int seconds_timeSpan = (int)timeSpan.TotalSeconds;
                                int result = (seconds_subtrahend - seconds_timeSpan) / 60;
                                if (result > 0)
                                {
                                    mins += result;
                                }
                            }
                        else
                        {
                            mins += 240;
                        }

                         if (days.Contains(day + ""))
                          {
                              days.Remove(day + "");
                          }
                    }
                      
                      
                        //close Data Reader
                        dataReader.Close();

                        //close Connection
                        this.CloseConnection();

                    for (int i = 0; i < days.Count; i++)
                    {
                        String format = month + "/" + days[i] + "/" + year;
                        if (!ifWeekend(format) && !IsHolilday(format))
                        {
                            mins += 480;
                        }
                    }
                    for (int i = 0; i < no_days; i++)
                    {
                        String format = month + "/" + (i + 1) + "/" + year;
                        if (!ifWeekend(format) && !IsHolilday(format))
                        {
                            counter++;
                        }
                    }

                    //return list to be displayed
                }
                catch (Exception e)
                {
                    return e.Message.ToString();
                }
            }
          
            return mins +" "+ counter;
        }
    }
}


