using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

namespace ATM_Machine
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to ATM Machine");
            RENTERCARDNO:
            Console.WriteLine("Enter your Card No -");
            string cardNo = Console.ReadLine();
            //string a;
            //a = "openConnection";
            //if (a != "exit")
            //{
                if (!ValidateCard(cardNo))
                {

                    //if (!ValidateCard(cardNo))
                    //{
                    Console.WriteLine("Invalid Card No. Please try again.");
                goto RENTERCARDNO;
                    // Environment.Exit(0);
                    //}
                }
                Console.WriteLine("Enter your pin");
                int pin = int.Parse(Console.ReadLine());
                if (!ValidatePin(cardNo, pin))
                {
                    Console.WriteLine("Invalid Card Pin. Please try again.");
                    Console.ReadKey();
                    //  return;
                    //Environment.Exit(0);
                }
                
                for (int choice = 0; choice < 4; choice++)
                {
               // choice = int.Parse(Console.ReadLine());
                

                    if (choice == 1)
                    {
                        Console.WriteLine("Enter your old pin");
                        int lastpin = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter your new pin");
                        int newpin = int.Parse(Console.ReadLine());
                        Console.WriteLine("Re-enter your new pin");
                        int confirmnewpin = int.Parse(Console.ReadLine());
                        if (confirmnewpin == newpin)
                        {
                            Console.WriteLine("Your pin has been changed");
                        }
                        else
                        {
                            Console.WriteLine("Please try again");
                        }
                    }
                    else if (choice == 2)
                    {
                        long BalanceInRupees = Balance(cardNo, pin);
                        Console.WriteLine("Current Balance is: " + BalanceInRupees);
                    }
                    else if (choice == 3)
                    {
                        long currentBalanceInRupees = Balance(cardNo, pin);
                        Console.WriteLine("Enter your withdraw amount");
                        int withdraw = int.Parse(Console.ReadLine());
                        if (withdraw > currentBalanceInRupees)
                        {
                            Console.WriteLine("Withdrawal Amount should be less than Current Amount.");
                            Console.ReadKey();
                            return;
                        }
                        bool isBalanceUpdated = UpdateBalance(cardNo, pin, currentBalanceInRupees - withdraw);
                        if (isBalanceUpdated)
                        {
                            Console.WriteLine("Withdrawal request of " + withdraw + " sucessfully processed");
                        }
                    }
                    else if (choice == 4)
                    {
                        //a = "exit";
                        Environment.Exit(0);
                    }
                }
            //}

            //else
            //{
            //    Environment.Exit(0);
            //}
            Console.ReadKey();
        }
        private static bool UpdateBalance(string cardNo, int pin, long updatedBalance)
        {
            string ConString = "data source=.; database=ATM; integrated security=true";
            using (SqlConnection connection = new SqlConnection(ConString))
            {
                string sqlText = $"Update dbo.CardDetails SET CurrentBalance = '{updatedBalance}' WHERE CardNumber = '{cardNo}' and Pin = { pin }";
                using (SqlCommand cmd = new SqlCommand(sqlText, connection))
                {
                    connection.Open();
                    int i = cmd.ExecuteNonQuery();
                    connection.Close();
                    return i > 0;
                }
            }
            return false;
        }
        private static bool ValidateCard(string cardNo)
        {
            //ado 
            DataTable dt;
            string ConString = "data source=.; database=ATM; integrated security=true";// remove this
            using (SqlConnection connection = new SqlConnection(ConString))
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from dbo.CardDetails where CardNumber ='" + cardNo + "'", connection);//remove statoic codes

                dt = new DataTable();
                da.Fill(dt);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                var cardnumber = Convert.ToInt64(cardNo);
                bool contains = dt.AsEnumerable().Any(row => cardnumber == row.Field<Int64>("CardNumber"));
                if (contains)
                {
                    return true;
                }
            }
            return false;
        }
        public static DataTable ExecuteTransaction(string connectioinString, string query)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectioinString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, connection);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        private static bool ValidatePin(string cardNo, int pin)
        {
            DataTable dt;
            string ConString = "data source=.; database=ATM; integrated security=true";
            dt = ExecuteTransaction(ConString, "select * from dbo.CardDetails");
            if (dt != null && dt.Rows.Count > 0)
            {
                var nu = Convert.ToInt64(cardNo);
                bool contains = dt.AsEnumerable().Any(row => nu == row.Field<Int64>("CardNumber") && pin == row.Field<int>("Pin"));
                if (contains)
                {
                    return true;
                }
            }
            return false;
        }
        private static long Balance(string cardNo, int pin)
        {
            DataTable dt;
            string ConString = "data source=.; database=ATM; integrated security=true";
            using (SqlConnection connection = new SqlConnection(ConString))
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from dbo.CardDetails where CardNumber=" + "'" + cardNo + "'", connection);
                dt = new DataTable();
                da.Fill(dt);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt64(dt.Rows[0]["CurrentBalance"]);
            }
            else
            {
                return 0;
            }
        }
    }
}

