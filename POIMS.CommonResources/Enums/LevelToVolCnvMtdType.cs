// -----------------------------------------------------------------------
// <copyright file="LevelToVolCnvMtdType.cs" company="DNR Process Solutions Pte Ltd">
//  Copyright© DNR Process Solutions Pte Ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace DNRPS.POIMS.CommonResources.Enums
{
    public class LevelToVolCnvMtdType
    {
        public static string Print(EnumLiteral printMe)
        {
            return Names.ResourceManager.GetString(string.Format("LevelToVolCnvMtdType{0}", printMe));
        }

        public enum EnumLiteral
        {
            StrappingTableLookups = 0,
            SymmetricalCalculations = 1
        }
    }
}
