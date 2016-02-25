// -----------------------------------------------------------------------
// <copyright file="EntityType.cs" company="DNR Process Solutions Pte Ltd">
//  Copyright© DNR Process Solutions Pte Ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace DNRPS.POIMS.CommonResources.Enums
{
    public class EntityType
    {
        ////public static string Print(EnumLiteral printMe)
        ////{
        ////    return Names.ResourceManager.GetString(string.Format("EntityType{0}", printMe));
        ////}

        public enum EnumLiteral
        {
            Tank = 1,
            // TODO: PHASE 2: Other Entity types. e.g. Pump, Valve, etc.
        }
    }
}
