using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Class1
    {
        public static void GetData()
        {
            using(var context = new ATMEntities())
            {
                var data = context.GetCardDetails(1,2).ToList();
            }


        }
    }
}
