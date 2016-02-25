// -----------------------------------------------------------------------
// <copyright file="DocumentType.cs" company="DNR Process Solutions Pte Ltd">
//  Copyright© DNR Process Solutions Pte Ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace DNRPS.POIMS.CommonResources.Enums
{
    public class DocumentType
    {
        public static string Print(EnumLiteral printMe)
        {
            return Names.ResourceManager.GetString(string.Format("DocumentType{0}", printMe));
        }

        public enum EnumLiteral
        {
            Tank = 1,
            Product = 2,
            ApplicationWideRecord = 100            
        }
    }
}
