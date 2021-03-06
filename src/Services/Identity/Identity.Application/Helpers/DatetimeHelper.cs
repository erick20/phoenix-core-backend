using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Helpers
{
    public class DatetimeHelper
    {
        public static long DatetimeToUnixParser(DateTime? myDate)
        {
            long unixTime = ((DateTimeOffset)myDate).ToUnixTimeSeconds();
            return unixTime;
        }

        public static DateTime UnixToDatetimeParser(long myUnixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(myUnixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
