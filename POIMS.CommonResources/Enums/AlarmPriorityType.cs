// -----------------------------------------------------------------------
// <copyright file="AlarmPriorityType.cs" company="DNR Process Solutions Pte Ltd">
//  Copyright© DNR Process Solutions Pte Ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace DNRPS.POIMS.CommonResources.Enums
{
    public class AlarmPriorityType
    {
        public static string Print(EnumLiteral printMe)
        {
            return Names.ResourceManager.GetString(string.Format("AlarmPriorityType{0}", printMe));
        }

        public enum EnumLiteral
        {
            Low = 0,
            Normal = 1,
            High = 2
        }
    }
}
