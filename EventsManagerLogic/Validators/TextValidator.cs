using EventsManagerLogic.Classes;
using EventsManagerLogic.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsManager.Classes
{
    public class TextValidator
    {
        Sql sql = null;

        public bool basicTextCheck(string str, int minLength, int maxLength)
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(str) || str.Length < minLength || str.Length > maxLength)
            {
                isValid = false;
            }

            return isValid;
        }

    }
}
