// -----------------------------------------------------------------------
// <copyright file="TankShapeType.cs" company="DNR Process Solutions Pte Ltd">
//  Copyright© DNR Process Solutions Pte Ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace DNRPS.POIMS.CommonResources.Enums
{
    public class TankShapeType
    {
        public static string Print(EnumLiteral printMe)
        {
            return Names.ResourceManager.GetString(string.Format("TankShapeType{0}", printMe));
        }

        public enum EnumLiteral
        {
            VerticalCylindrical = 0,
            Spherical,
            HorizontalCylindrical,
            Defragmented,
        }
    }
}
