using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace TicTacToe.Helpers
{
    public static class EnumHelper
    {
        public static string Description(this Enum value)
        {
            object[] attributes = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Any())
            {
                return (attributes.First() as DescriptionAttribute).Description;
            }

            // If no description is found, the least we can do is replace underscores with spaces
            // You can add your own custom default formatting logic here
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            return ti.ToTitleCase(ti.ToLower(value.ToString().Replace("_", " ")));
        }

        public static IEnumerable<KeyValuePair<Enum, string>> GetAllValuesAndDescriptions(Type t)
        {
            if (!t.IsEnum)
            {
                throw new ArgumentException($"{nameof(t)} must be an enum type");
            }

            return Enum.GetValues(t).Cast<Enum>().Select((e) =>
            {
                return new KeyValuePair<Enum, string>(key: e, value: e.Description());
            }).ToList();
        }
    }
}
