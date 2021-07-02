using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Helpers
{
    public static class EnumHelper // TODO make this helper common for whole project
    {
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static bool ToBool(this string value)
        {
            return bool.Parse(value);
        }
    }
}
