using DataAccess;
namespace ATmV2
{
    public class ATMBase
    {

        public void AddNewAccount(long cardNumber,string name, int pin, long balance)
        {
            ATM atmObject = null;
            using (var dbContext = new ATMEntities())
            {
                var results = dbContext.spNewAccount(cardNumber,name ,pin, balance).GetEnumerator();
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