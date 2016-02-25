// -----------------------------------------------------------------------
// <copyright file="TempToDensityCnvMtdType.cs" company="DNR Process Solutions Pte Ltd">
//  Copyright© DNR Process Solutions Pte Ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace DNRPS.POIMS.CommonResources.Enums
{
    public class TempToDensityCnvMtdType
    {
        public static string Print(EnumLiteral printMe)
        {
            return Names.ResourceManager.GetString(string.Format("TempToDensityCnvMtdType{0}", printMe));
        }

        public enum EnumLiteral
        {
            DensityTableLookups = 0,
            FormulaCalculations = 1,
            Linear = 2
        }
    }
}
