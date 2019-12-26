using Hein.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hein.RulesEngine.Framework.Extensions
{
    public static class ConversionExtensions
    {
        public static object ToType(this object obj, string toType)
        {
            switch (toType.ToLower())
            {
                case "string":
                    return obj.ToType<string>();
                case "bool":
                case "boolean":
                    return obj.ToString().ToLower().ToType<bool>();
            }

            return null;
        }

        public static bool ToBoolean(this object obj)
        {
            return obj.ToString().ToLower().ToType<bool>();
        }

        public static bool ToTrueFalse(this object obj)
        {
            return obj.ToString().ToLower().ToType<bool>();
        }

        public static bool ToBool(this object obj)
        {
            return obj.ToString().ToLower().ToType<bool>();
        }

        public static int ToInt(this object obj)
        {
            return obj.ToType<int>();
        }

        public static decimal ToNumber(this object obj)
        {
            return obj.ToType<decimal>();
        }

        public static DateTime ToDateTime(this object obj)
        {
            return obj.ToType<DateTime>();
        }
    }
}
