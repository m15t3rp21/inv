// -----------------------------------------------------------------------
// <copyright file="ComputeReferenceType.cs" company="DNR Process Solutions Pte Ltd">
//  Copyright© DNR Process Solutions Pte Ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace DNRPS.POIMS.CommonResources.Enums
{
    public class ComputationModeType
    {
        public static string Print(EnumLiteral printMe)
        {
            return Names.ResourceManager.GetString(string.Format("ComputationModeType{0}", printMe));
        }

        public enum EnumLiteral
        {
            ByPressure = 1,
            ByLevel = 2
        }
    }
}
