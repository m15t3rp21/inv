// -----------------------------------------------------------------------
// <copyright file="LookupInterpolationErrorType.cs" company="DNR Process Solutions Pte Ltd">
//  Copyright© DNR Process Solutions Pte Ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace DNRPS.POIMS.CommonResources.Enums
{
    public class LookupInterpolationErrorType
    {
        ////public static string Print(EnumLiteral printMe)
        ////{
        ////    return Names.ResourceManager.GetString(string.Format("EntityType{0}", printMe));
        ////}

        public enum EnumLiteral
        {
            InvalidLookupTable = -1,
            SearchKeyBelowLowLimit = -2,
            SearchKeyAboveHighLimit = -3
        }

        public static EnumLiteral GetEnumLiteral(string v)
        {
            return (EnumLiteral)Enum.Parse(typeof(EnumLiteral), v);
        }
    }
}
