using System;

namespace ATmV2
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to ATM Machine....");

            while (true)
            {
                Console.WriteLine("Please Enter your Card No -");
                var cardNo = Convert.ToInt64(Console.ReadLine());
                ATM atm = new ATM();

                if (!atm.ValidateCardNumber(cardNo))
                {
                    Console.WriteLine("Invalid Card No. Please try again.");
                    continue;
                }

                Console.WriteLine("Please Enter your pin");
                int pin = int.Parse(Console.ReadLine());
                if (!atm.ValidatePin(cardNo, pin))
                {
                    Console.WriteLine("Invalid Card Pin. Please try again.");
                    continue;
                }

                atm = atm.GetAtmDetails(cardNo, pin);
                Console.WriteLine(atm.Name);

            ChooseOptions:
                Console.WriteLine("Choose your opetions:-\n \n-1 Pin Change \n \n-2 Check Your Current Balance \n\n-3 Withdraw Your Amount\n\n-4 for Exit");
                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:  // Pin Change
                    PinChange:
                        Console.WriteLine("Enter Your New Pin -");
                        int newPin = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Confirm Your New Pin -");
                        int confirmNewPin = Convert.ToInt32(Console.ReadLine());
                        if (newPin == confirmNewPin)
                        {
                            atm.UpdatePin(cardNo, pin, newPin);
                            Console.WriteLine("Your pin has been changed");
                        }
                        else
                        {
                            Console.WriteLine("Pin don't match. Please try again.");
                            goto PinChange;
                        }
                        break;
                    case 2:
                        Console.WriteLine($"Your current balance is {atm.CurrentBalance} INR.");
                        break;
                    case 3:
                    WithdrawAmount:
                        Console.WriteLine("Enter Amount to Withdraw-");
                        long withdrawAmt = Convert.ToInt64(Console.ReadLine());
                        if (withdrawAmt > atm.CurrentBalance)
                        {
                            Console.WriteLine("Withdrawal Amount should be less than Current Amount.");
                            goto WithdrawAmount;
                        }
                        var isBalanceUpdated = atm.WithdrawBalance(cardNo, pin, atm.CurrentBalance - withdrawAmt);
                        if (isBalanceUpdated)
                        {
                            Console.WriteLine("Withdrawal request of " + withdrawAmt + " sucessfully processed");
                        }
                        else
                        {
                            Console.WriteLine("Some error has occurred. Please try again.");
                            goto WithdrawAmount;
                        }
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

            Console.ReadKey();

        }

    }
}
