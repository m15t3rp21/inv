// -----------------------------------------------------------------------
// <copyright file="FlowMovementType.cs" company="DNR Process Solutions Pte Ltd">
//  Copyright© DNR Process Solutions Pte Ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace DNRPS.POIMS.CommonResources.Enums
{
    public class FlowMovementType
    {
        public static string Print(EnumLiteral printMe)
        {
            return Names.ResourceManager.GetString(string.Format("FlowMovementType{0}", printMe));
        }

        public enum EnumLiteral
        {
            Filling = 1,
            Draining = 2,
            Static = 4
        }
    }
}
