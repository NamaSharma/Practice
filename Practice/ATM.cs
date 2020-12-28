using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ATmV2
{
    public class ATM
    {
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
            int rowsAffected = 0;

            SQLOperations operations = new SQLOperations();
            var parameter = new SqlParameter("@CardNumber", SqlDbType.BigInt);
            parameter.Value = cardNumber;
            var operationParam = new SqlParameter("@Operation", SqlDbType.Int);
            operationParam.Value = 1;
            rowsAffected = operations.ExecuteStoreProcScalar(Constants.ValidateCardProc, new SqlParameter[] { parameter, operationParam });

            if (rowsAffected == 1)
            {
                return true;
            }
            return false;
        }

        public bool ValidatePin(long cardNumber, int pin)
        {
            int rowsAffected = 0;

            SQLOperations operations = new SQLOperations();
            var cardNoParam = new SqlParameter("@CardNumber", SqlDbType.BigInt);
            cardNoParam.Value = cardNumber;
            var pinParam = new SqlParameter("@Pin", SqlDbType.Int);
            pinParam.Value = pin;
            var operationParam = new SqlParameter("@Operation", SqlDbType.Int);
            operationParam.Value = 2;

            rowsAffected = operations.ExecuteStoreProcScalar(Constants.ValidateCardProc, new SqlParameter[] { cardNoParam, pinParam, operationParam });

            if (rowsAffected == 1)
            {
                return true;
            }
            return false;
        }

        public bool UpdatePin(long cardNumber, int oldPin, int newPin)
        {
            int rowsAffected = 0;

            SQLOperations operations = new SQLOperations();
            var cardNoParam = new SqlParameter("@CardNumber", SqlDbType.BigInt);
            cardNoParam.Value = cardNumber;
            var pinParam = new SqlParameter("@Pin", SqlDbType.Int);
            pinParam.Value = oldPin;
            var newPinParam = new SqlParameter("@NewPin", SqlDbType.Int);
            newPinParam.Value = newPin;
            var operationParam = new SqlParameter("@Operation", SqlDbType.Int);
            operationParam.Value = 1;

            rowsAffected = operations.ExecuteStoreProcNonQuery(Constants.UpdateCardProc, new SqlParameter[] { cardNoParam, pinParam, newPinParam, operationParam });

            if (rowsAffected == 1)
            {
                return true;
            }
            return false;
        }

        public bool WithdrawBalance(long cardNumber, int pin, long newBalance)
        {
            int rowsAffected = 0;

            SQLOperations operations = new SQLOperations();
            var cardNoParam = new SqlParameter("@CardNumber", SqlDbType.BigInt);
            cardNoParam.Value = cardNumber;
            var pinParam = new SqlParameter("@Pin", SqlDbType.Int);
            pinParam.Value = pin;
            var newBalParam = new SqlParameter("@NewBalance", SqlDbType.BigInt);
            newBalParam.Value = newBalance;
            var operationParam = new SqlParameter("@Operation", SqlDbType.Int);
            operationParam.Value = 2;
            rowsAffected = operations.ExecuteStoreProcNonQuery(Constants.UpdateCardProc, new SqlParameter[] { cardNoParam, pinParam, newBalParam, operationParam });

            if (rowsAffected == 1)
            {
                return true;
            }
            return false;
        }

        public ATM GetAtmDetails(long cardNumber, int pin)
        {
            ATM atmObj = null;
            SQLOperations operations = new SQLOperations();
            var cardNoParam = new SqlParameter("@CardNumber", SqlDbType.BigInt);
            cardNoParam.Value = cardNumber;
            var pinParam = new SqlParameter("@Pin", SqlDbType.Int);
            pinParam.Value = pin;

            var dr = operations.ExecuteStoreProcReader(Constants.GetCardDetailsProc, new SqlParameter[] { cardNoParam, pinParam });

            while (dr.HasRows && dr.Read())
            {
                atmObj = new ATM(dr);
            }
            dr.Close();

            return atmObj;
        }

    }
}
