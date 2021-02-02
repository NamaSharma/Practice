
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ATmV2
{
    public class ATM
    {
        //private long _cardNumber;
        //public long getMethod()
        //{
        //    return _cardNumber;
        //}
        //public void setMethod(long value)
        //{
        //    if (true)
        //    {
        //        _cardNumber = value;
        //    }          
        //}
        public long CardNumber { get; set; }
        public string Name { get; set; }
        public long CurrentBalance { get; set; }
        public int Cvv { get; set; }
        public int Pin { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ATM()
        {

        }
        public ATM(SqlDataReader dr)
        {
            CardNumber = dr.GetFieldValue<long>("CardNumber");
            Name = dr.GetFieldValue<string>("Name");
            Pin = dr.GetFieldValue<int>("Pin");
            Cvv = dr.GetFieldValue<int>("Cvv");
            CurrentBalance = dr.GetFieldValue<long>("CurrentBalance");
        }
        public bool ValidateCardNumber(long cardNumber)
        {
            using (var dbContext = new ATMEntities())
            {
                var result = dbContext.ValidateCardDetails(1, cardNumber,Pin);
                {
                    var effectedRows = dbContext.ValidateCardNumber(cardNumber);
                    return false;
                }
            }           
        }
            
        public bool ValidatePin(long cardNumber, int pin)
        {         
            using (var dbContext = new ATMEntities())
            {
                var result = dbContext.ValidateCardDetails(2, cardNumber, pin).GetEnumerator();
                while (result.MoveNext())
                {
                    return result.Current > 0;
                }
            }
            return false;
        }
        public bool UpdatePin(long cardNumber, int oldPin, int newPin)
        {           
            using (var dbContext = new ATMEntities())
            {
                var effectedRows = dbContext.UpdateCardDetails(1, cardNumber, oldPin, newPin,null);
                return effectedRows > 0;
            }
        }
        public bool WithdrawBalance(long cardNumber, int pin, long newBalance)
        {
            using (var dbContext = new ATMEntities())
            {
                var effectedRows = dbContext.UpdateCardDetails(2, cardNumber, pin, null, newBalance);
                return effectedRows > 0;
            }
        }
        public ATM GetAtmDetails(long cardNumber, int pin)
        {
            ATM atmObject = null;
            using (var dbContext = new ATMEntities())
            {
                var res = dbContext.GetCardDetails(cardNumber, pin).GetEnumerator();
                while (res.MoveNext())
                {
                    var result = res.Current;
                    atmObject = new ATM();
                    atmObject.CardNumber = result.CardNumber;
                    atmObject.Name = result.Name;
                    atmObject.Pin = result.Pin;
                    atmObject.Cvv = result.Cvv;
                    atmObject.CurrentBalance = result.CurrentBalance.Value;
                }
            }
            return atmObject;
        }
        public bool AddNewAccount(long cardNumber, int pin)
        {
            ATM atmObject = new ATM();
            using (var dbContext = new ATMEntities())
            {
                var results = dbContext.spNewAccount(cardNumber,Name, pin,CurrentBalance).GetEnumerator();
                while (results.MoveNext())
                {
                    var result = results.Current;
                    atmObject = new ATM();
                    atmObject.CardNumber = result.CardNumber;
                    atmObject.Name = result.Name;
                    atmObject.Pin = result.Pin;
                   // atmObject.CurrentBalance = result.CurrentBalance;
                }
            }
            return true;
        }
    }
}
