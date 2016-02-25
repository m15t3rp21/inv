// -----------------------------------------------------------------------
// <copyright file="TagDataType.cs" company="DNR Process Solutions Pte Ltd">
//  Copyright© DNR Process Solutions Pte Ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace DNRPS.POIMS.CommonResources.Enums
{
    public class TagDataType
    {
        public static string Print(EnumLiteral printMe)
        {
            return Names.ResourceManager.GetString(string.Format("TagDataType{0}", printMe));
        }

        public enum EnumLiteral
        {
            Single = 1,
            Byte = 2,
            UInt16 = 3,
            Double = 4,
            Int16 = 5,
            Char = 6,
            String = 7,
            Boolean = 8,
            SByte = 9,
            UInt32 = 10,
            Int32 = 11,
            DateTime = 12,
        }
    }
}
