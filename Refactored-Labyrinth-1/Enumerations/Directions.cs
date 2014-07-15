using System;
using System.ComponentModel;
using System.Reflection;
namespace Labyrinth.Enumerations
{
    public static class Enumeration
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }


    public enum Directions
    {
        [Description("Blank")]
        B = -1,
        [Description("Left")]
        L = 0,
        [Description("Up")]
        U = 1,
        [Description("Right")]
        R = 2,
        [Description("Down")]
        D = 3
    }
}
