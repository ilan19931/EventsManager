using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsManager.Classes
{
    class TextValidator
    {
        public TextValidator()
        {

        }

        public bool basicTextCheck(string str, int minLength, int maxLength)
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(str) || str.Length < minLength || str.Length > maxLength)
            {
                isValid = false;
            }

            return isValid;
        }

        public string PasswordCheck(string str)
        {
            string error = "";

            if (string.IsNullOrEmpty(str))
                error += "username can't be empty\n";
            if (str.Length < 2)
                error += "username must be minimun \n";
            else if (str.Length > 30)
                error += "username must be maximum 30  \n";

            return error;
        }
    }
}
