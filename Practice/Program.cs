using System;

namespace ATmV2
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to ATM Machine....");
            Console.WriteLine("Press 1 for new account");
            Console.WriteLine("Press 2 for existing account");
            int choices = int.Parse(Console.ReadLine());
            switch (choices)
            {
                case 1:
                    {
                        while (true)
                        {
                            Console.WriteLine("Create new account in simple steps:- ");
                            Console.WriteLine("Enter your withdrawl amount (Note :-Minimum amount is 5000)");
                            int openingbalanceamount;
                            openingbalanceamount = int.Parse(Console.ReadLine());
                            if (openingbalanceamount >= 5000)
                            {
                                Console.WriteLine("Your Balace is " + openingbalanceamount);
                            }
                            else if (openingbalanceamount < 5000)
                            {
                                Console.WriteLine("Insufficient amount");
                                continue;
                            }
                            Console.WriteLine("Please enter your full name");
                            string fullname = Console.ReadLine();
                            long newcardnumber;
                            Random random = new Random();
                            newcardnumber = random.Next(10000000, 99999999);
                            Console.WriteLine("Your card number is " + newcardnumber);
                            int newatmpin;
                            Random randompin = new Random();
                            newatmpin = random.Next(1000, 9999);
                            //   newcardnumber = random.Next(Convert.ToInt64(100000000, 99));
                            Console.WriteLine("Your ATM pin is " + newatmpin);
                            ATM atm = new ATM();
                            if (!atm.AddNewAccount(newcardnumber, fullname,newatmpin,openingbalanceamount))
                            {
                                Console.WriteLine("You have created account successfully!");
                                continue;
                            }
                            Console.WriteLine("Thank You ");
                            break;
                        }
                        // while (openingbalanceamount >= 5000);
                        Environment.Exit(0);
                    }
                    break;
                case 2:
                    {
                        while (true)
                        {
                            Console.WriteLine("Please Enter your Card No -");
                            var cardNumbers = Convert.ToInt64(Console.ReadLine());
                            ATM atm = new ATM();
                            if (!atm.ValidateCardNumber(cardNumbers))
                            {
                                Console.WriteLine("Invalid Card No. Please try again.");
                                continue;
                            }
                            Console.WriteLine("Please Enter your pin");
                            int pin = int.Parse(Console.ReadLine());
                            if (!atm.ValidatePin(cardNumbers, pin))
                            {
                                Console.WriteLine("Invalid Card Pin. Please try again.");
                                continue;
                            }
                            atm = atm.GetAtmDetails(cardNumbers, pin);
                            Console.WriteLine(atm.Name);
                        ChooseOptions:
                            Console.WriteLine("Choose your operations:-\n \n-1 Pin Change \n \n-2 Check Your Current Balance " +
                                "\n\n-3 Withdraw Your Amount\n\n-4 for Exit");
                            int option = Convert.ToInt32(Console.ReadLine());
                            switch (option)
                            {
                                case 1:  // Pin Change
                                    bool isPinChange = false;
                                    do
                                    {
                                        Console.WriteLine("Enter Your New Pin -");
                                        int newPin = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Confirm Your New Pin -");
                                        int confirmNewPin = Convert.ToInt32(Console.ReadLine());
                                        if (newPin == confirmNewPin)
                                        {
                                            atm.UpdatePin(cardNumbers, pin, newPin);
                                            isPinChange = true;
                                            Console.WriteLine("Your pin has been changed");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Pin don't match. Please try again.");
                                        }
                                    } while (!isPinChange);
                                    break;
                                case 2:
                                    Console.WriteLine($"Your current balance is {atm.CurrentBalance} INR.");
                                    break;
                                case 3:
                                    bool WithdrawAmount = false;
                                    do
                                    {
                                        Console.WriteLine("Enter Amount to Withdraw-");
                                        long withdrawAmounts = Convert.ToInt64(Console.ReadLine());
                                        if (withdrawAmounts > atm.CurrentBalance)
                                        {
                                            Console.WriteLine("Withdrawal Amount should be less than Current Amount.");
                                        }
                                        var isBalanceUpdated = atm.WithdrawBalance(cardNumbers, pin, atm.CurrentBalance - withdrawAmounts);
                                        if (isBalanceUpdated)
                                        {
                                            Console.WriteLine("Withdrawal request of " + withdrawAmounts + " sucessfully processed");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Some error has occurred. Please try again.");
                                        }
                                    }
                                    while (!WithdrawAmount);
                                    break;
                                case 4:
                                    Console.WriteLine("Thank You");
                                    Environment.Exit(0);
                                    break;
                                default:
                                    Console.WriteLine("Choose the correct option.");
                                    break;
                            }
                            goto ChooseOptions;
                        }
                        break;
                    }
            }
            Console.ReadKey();
        }
    }
}
