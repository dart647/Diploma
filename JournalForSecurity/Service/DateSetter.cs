using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalForSecurity.Service
{
    public class DateSetter
    {
        public static DateTime SetToday()
        {
            DateTime date = DateTime.Now.Date;
            return date;
        }
    }
}
