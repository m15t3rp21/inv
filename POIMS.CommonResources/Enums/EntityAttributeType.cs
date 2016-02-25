// -----------------------------------------------------------------------
// <copyright file="EntityAttributeType.cs" company="DNR Process Solutions Pte Ltd">
//  Copyright© DNR Process Solutions Pte Ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace DNRPS.POIMS.CommonResources.Enums
{
    public class EntityAttributeType
    {
        public static string Print(EnumLiteral printMe)
        {
            return Names.ResourceManager.GetString(string.Format("EntityAttributeType{0}", printMe));
        }

        public enum EnumLiteral
        {
            // Tank Attributes
            // Literal 1 to 39 corresponds to the legacy eIPFieldCode
            Tank_Average_Temperature_value = 1,
            Tank_Average_Temperature_status = 2,
            Tank_Level_value = 3,
            Tank_Level_status = 4,
            Tank_Multi_Temperature_Point_1_value = 5,
            Tank_Multi_Temperature_Point_1_status = 6,
            Tank_Multi_Temperature_Point_2_value = 7,
            Tank_Multi_Temperature_Point_2_status = 8,
            Tank_Multi_Temperature_Point_3_value = 9,
            Tank_Multi_Temperature_Point_3_status = 10,
            Tank_Multi_Temperature_Point_4_value = 11,
            Tank_Multi_Temperature_Point_4_status = 12,
            Tank_Multi_Temperature_Point_5_value = 13,
            Tank_Multi_Temperature_Point_5_status = 14,
            Tank_Multi_Temperature_Point_6_value = 15,
            Tank_Multi_Temperature_Point_6_status = 16,
            Tank_Multi_Temperature_Point_7_value = 17,
            Tank_Multi_Temperature_Point_7_status = 18,
            Tank_Multi_Temperature_Point_8_value = 19,
            Tank_Multi_Temperature_Point_8_status = 20,
            Tank_Multi_Temperature_Point_9_value = 21,
            Tank_Multi_Temperature_Point_9_status = 22,
            Tank_Multi_Temperature_Point_10_value = 23,
            Tank_Multi_Temperature_Point_10_status = 24,
            Tank_Multi_Temperature_Point_11_value = 25,
            Tank_Multi_Temperature_Point_11_status = 26,
            Tank_Multi_Temperature_Point_12_value = 27,
            Tank_Multi_Temperature_Point_12_status = 28,
            Tank_Multi_Temperature_Point_13_value = 29,
            Tank_Multi_Temperature_Point_13_status = 30,
            Tank_Multi_Temperature_Point_14_value = 31,
            Tank_Multi_Temperature_Point_14_status = 32,
            Tank_Multi_Temperature_Point_15_value = 33,
            Tank_Multi_Temperature_Point_15_status = 34,
            Tank_Multi_Temperature_Point_16_value = 35,
            Tank_Multi_Temperature_Point_16_status = 36,
            Tank_Water_Level_value = 37,
            Tank_Water_Level_status = 38,
            Tank_Level_Switch = 39,
            // Literal 40 to 69 corresponds to the legacy eOPFieldCode offset by 39. i.e. if eOPFieldCode = 1, literal = 40.
            Tank_Observed_Volume = 40,
            Tank_Standard_Volume = 41,
            Tank_Mass = 42,
            Tank_Observed_Density = 43,
            Tank_Standard_Density = 44,
            Tank_Product_Name = 45,
            Tank_Product_Description = 46,
            // legacy PVLevel = 8 ==> 47, which is essentially Tank_Level_value. So we omit this literal.
            Tank_Level_in_percentage = 48,
            Tank_Pressure = 49,
            // legacy PVAvgTemperature = 11 ==> 50, which is essentially Tank_Average_Temperature_value. So we omit this literal.
            Tank_Maximum_Operating_Volume = 51,
            Tank_Maximum_Operating_Mass = 52,
            Tank_Minimum_Operating_Volume = 53,
            // Below legacy types are essentially Tank_Multi_Temperature_Point_x_value. So we omit this literals
            ////PVSpotTemperature1 = 15, ==> 54
            ////PVSpotTemperature2 = 16,
            ////PVSpotTemperature3 = 17,
            ////PVSpotTemperature4 = 18,
            ////PVSpotTemperature5 = 19,
            ////PVSpotTemperature6 = 20,
            ////PVSpotTemperature7 = 21,
            ////PVSpotTemperature8 = 22,
            ////PVSpotTemperature9 = 23,
            ////PVSpotTemperature10 = 24,
            ////PVSpotTemperature11 = 25,
            ////PVSpotTemperature12 = 26,
            ////PVSpotTemperature13 = 27,
            ////PVSpotTemperature14 = 28,
            ////PVSpotTemperature15 = 29,
            ////PVSpotTemperature16 = 30, ==> 69
            // Literal from 70 onwards are relevant to POIMS Version 5
            Tank_Instantaneous_Volume_Flow_Rate = 70,
            Tank_Instantaneous_Mass_Flow_Rate = 71,

            // TODO: PHASE 2: Other Entity-type Attributes. e.g. for Pump, Valve, etc.
        }
    }
}
