// -----------------------------------------------------------------------
// <copyright file="TagIOType.cs" company="DNR Process Solutions Pte Ltd">
//  Copyright© DNR Process Solutions Pte Ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace DNRPS.POIMS.CommonResources.Enums
{
    public class TagIOType
    {
        public static string Print(EnumLiteral printMe)
        {
            return Names.ResourceManager.GetString(string.Format("TagIOType{0}", printMe));
        }

        public enum EnumLiteral
        {
            Input = 1,
            Output = 2,
        }
    }
}
