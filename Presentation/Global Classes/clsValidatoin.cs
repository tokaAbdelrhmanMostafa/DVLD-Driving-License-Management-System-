using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Buisness.GlobalClasses
{
   public class clsValidatoin
    {
       public static bool  ValidateEmail(string email) {
            var pattern = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";

            var regex = new Regex(pattern);
           return  regex.IsMatch(email);
        }
        public static bool ValidateInteger(string number)
        {
            var pattern = @"^[0-9]*$";
            var regex = new Regex(pattern);
            return regex.IsMatch(number);
        }
        public static bool ValidateFloat(string number)
        {
            var pattern = @"^[0-9]*(?:\.[0-9]*)?$";

            var regex = new Regex(pattern);
            return regex.IsMatch(number);
        }
        public static bool IsNumber(string Number)
        {
            return (ValidateInteger(Number) || ValidateFloat(Number));
        }
    }
}
