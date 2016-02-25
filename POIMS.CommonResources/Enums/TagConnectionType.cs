// -----------------------------------------------------------------------
// <copyright file="TagConnectionType.cs" company="DNR Process Solutions Pte Ltd">
//  Copyright© DNR Process Solutions Pte Ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace DNRPS.POIMS.CommonResources.Enums
{
    public class TagConnectionType
    {
        public static string Print(EnumLiteral printMe)
        {
            return Names.ResourceManager.GetString(string.Format("TagConnectionType{0}", printMe));
        }

        public enum EnumLiteral
        {
            OPC = 1,
            Modbus = 2,
            Flatfile = 3,
            SMS = 4,
            SQL = 5
        }
    }
}
