using EventsManagerLogic;
using EventsManagerLogic.Classes;
using EventsManagerLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsManager
{
    public class Helper
    {
        public static User user = null;
        public static Sql sql = new Sql();
        public static AppSettings appSettings = new AppSettings();
        public static Security security = new Security();

        internal static void OpenWindow()
        {
            throw new NotImplementedException();
        }
    }
}
