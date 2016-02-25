// -----------------------------------------------------------------------
// <copyright file="TriggerConditionType.cs" company="DNR Process Solutions Pte Ltd">
//  Copyright© DNR Process Solutions Pte Ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace DNRPS.POIMS.CommonResources.Enums
{
    using System;

    public class TriggerConditionType
    {
        public static EnumLiteral Parse(string parseMe)
        {
            if (parseMe.Contains("=") || parseMe.Contains("<") || parseMe.Contains(">"))
            {
                switch (parseMe)
                {
                    case "<": return EnumLiteral.LessThan;
                    case "<=": return EnumLiteral.LessThanOrEqual;
                    case ">": return EnumLiteral.MoreThan;
                    case ">=": return EnumLiteral.MoreThanOrEqual;
                    case "!=": return EnumLiteral.NotEqual;
                    default: return EnumLiteral.Equal;
                }
            }
            else
            {
                return (EnumLiteral)Enum.Parse(typeof(EnumLiteral), parseMe);
            }
        }

        public static string Print(EnumLiteral printMe)
        {
            return Names.ResourceManager.GetString(string.Format("TriggerConditionType{0}", printMe));
        }

        public enum EnumLiteral
        {
            Equal, LessThan, LessThanOrEqual, MoreThanOrEqual, MoreThan, NotEqual
        }
    }
}
