namespace ATmV2
{
    public class ATMBase2
    {

        public bool AddNewAccount(long cardNumber, int pin)
        {
            ATM atmObject = null;
            using (var dbContext = new ATMEntities())
            {
                var results = dbContext.spNewAccount(cardNumber, pin).GetEnumerator();
                while (results.MoveNext())
                {
                    var result = results.Current;
                    atmObject = new ATM();

                    atmObject.CardNumber = result.CardNumber;
                    atmObject.Name = result.Name;
                    atmObject.Pin = result.Pin;
                    //atmObject.Cvv = result.Cvv;
                    atmObject.CurrentBalance = result.CurrentBalance.Value;
                }

            }
        }
    }
}