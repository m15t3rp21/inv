// -----------------------------------------------------------------------
// <copyright file="ProfibusStatusType.cs" company="DNR Process Solutions Pte Ltd">
//  Copyright© DNR Process Solutions Pte Ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace DNRPS.POIMS.CommonResources.Enums
{
    /// <summary>
    /// The PROFIBUS status of the data read from PLC
    /// </summary>
    public class ProfibusStatusType
    {
        public enum Quality
        {
            Bad = 0, // "Bad"
            Uncertain, // "Uncertain"
            Good_NonCascade, // "Good (Non-Cascaded)"
            Good_Cascade, // "Good (Cascaded)"
            Undefined // "Undefined"
        };

        public enum SubStatus
        {
            /*Bad Quality Sub-Statuses */
            Bad_Non_specific = 0, // "Non-specific"
            Bad_ConfigError, // "Configuration Error"
            Bad_NotConnected, // "Not Connected"
            Bad_DeviceFailure, // "Device Failure"
            Bad_SensorFailure, // "Sensor Failure"
            Bad_NoComm_LastUsableVal, // "No Comm Last Usable Value"
            Bad_NoComm_NoUsableVal, // "No Comm No Usable Value"
            Bad_OS, // "Out of Service"
            Bad_Reserved, // "Reserved"

            /* Uncertain Quality Sub-Status */
            Uncertain_NonSpecific, // "Not Specific"
            Uncertain_LastUsableVal, // "Last Usable Value"
            Uncertain_SubstituteSet, // "Substitute Set"
            Uncertain_InitialValue, // "Initial Value"
            Uncertain_NonAccurateSensorConversion, // "Sensor Conversion Not Accurate"
            Uncertain_EUViolation, // "Engineering Unit Violation (Unit is not in valid set)"
            Uncertain_SubNormal, // "Sub-normal"
            Uncertain_ConfigError, // "Configuration Error"
            Uncertain_SimValue, // "Simulated Value"
            Uncertain_SensorCalibration, // "Sensor Calibration"
            Uncertain_Reserved, // "Reserved"

            /* Good Non-Cascaded Quality Sub-Statuses */
            GoodNonCascaded_InvalidStatus, // "Invalid statusValue"
            GoodNonCascaded_OK, // "Ok"
            GoodNonCascaded_UpdateEvent, // "Update Event"
            GoodNonCascaded_ActiveAdvisoryAlarm, // "Active Advisory Alarm (priority < 8)"
            GoodNonCascaded_ActiveCriticalAlarm, // "Active Critical Alarm (priority > 8)"
            GoodNonCascaded_UnackUpdateEvent, // "Unacknowledged Update Event"
            GoodNonCascaded_UnackAdvisoryAlarm, // "Unacknowledged Advisory Alarm"
            GoodNonCascaded_UnackCriticalAlarm, // "Unacknowledged Critical Alarm"
            GoodNonCascaded_InitialFailSafe, // "Initial Fail Safe"
            GoodNonCascaded_MaintenanceRequired, // "Maintenance Required"
            GoodNonCascaded_Reserved, // "Reserved"

            /* Good Cascaded Quality Sub-Statuses */
            GoodCascaded_InvalidStatus, // "Invalid statusValue"
            GoodCascaded_OK, // "Ok"
            GoodCascaded_InitialisationAck, // "Initialisation Acknowledged"
            GoodCascaded_InitialisationRequest, // "Initialisation Request"
            GoodCascaded_NotInvited, // "Not Invited"
            GoodCascaded_Reserved, // "Reserved"
            GoodCascaded_DoNotSelect, // "Do Not Select"
            GoodCascaded_LocalOverride, // "Local Override"
            GoodCascaded_InitiateFailSafe, // "Initiate Fail Safe"

            undefined // "Undefined"
        };

        public enum Limits
        {
            Ok = 0, // "Ok"
            HighLimited, // "High Limited"
            LowLimited, // "Low Limited"
            Constant, // "Constant"

            undefined // "Undefined"
        };

        private static string PrintQuality(Quality printMe)
        {
            return Names.ResourceManager.GetString(string.Format("DataStatusTypeQuality{0}", printMe));
        }
        private static string PrintSubStatus(SubStatus printMe)
        {
            return Names.ResourceManager.GetString(string.Format("DataStatusTypeSubStatus{0}", printMe));
        }
        private static string PrintLimits(Limits printMe)
        {
            return Names.ResourceManager.GetString(string.Format("DataStatusTypeLimits{0}", printMe));
        }

        public static void InterpretStatus(byte statusByte, out Quality quality, out SubStatus subStatus,
            out Limits limit, out string qualityLabel, out string subStatusLabel, out string limitLabel)
        {
            if (statusByte < 0)
            {
                quality = Quality.Undefined;
                limit = Limits.undefined;
                subStatus = SubStatus.undefined;
            }
            else
            {
                #region Codes to check for Quality
                byte byQuality = (byte)(statusByte & 0xC0);    // Mask statusValue with (1100 0000)2

                switch (byQuality)
                {
                    case 0x00:
                        quality = Quality.Bad;                  // (0000 0000)2
                        break;
                    case 0x40:
                        quality = Quality.Uncertain;            // (0100 0000)2
                        break;
                    case 0x80:
                        quality = Quality.Good_NonCascade;  // (1000 0000)2
                        break;
                    case 0xC0:
                        quality = Quality.Good_Cascade;     // (1100 0000)2
                        break;
                    default:
                        quality = Quality.Undefined;
                        break;
                }
                #endregion Codes to check for Quality

                #region Codes to check for Limits
                byte byLimits = (byte)(statusByte & 0x03);     // Mask statusValue with (0000 0011)2

                switch (byLimits)
                {
                    case 0x00:
                        limit = Limits.Ok;          // (0000 0000)2
                        break;
                    case 0x01:
                        limit = Limits.LowLimited;  // (0000 0001)2
                        break;
                    case 0x02:
                        limit = Limits.HighLimited; // (0000 0010)2
                        break;
                    case 0x03:
                        limit = Limits.Constant;        // (0000 0011)2
                        break;
                    default:
                        limit = Limits.undefined;
                        break;
                }
                #endregion Codes to check for Limits

                #region Codes to check for Sub-status

                byte bySubstatusValue = (byte)(statusByte & 0x3C);     // Mask statusValue with (0011 1100)2

                // Interpret sub-statusValue according to the Quality
                if (quality == Quality.Undefined)
                {
                    subStatus = SubStatus.undefined;
                }
                else if (quality == Quality.Bad)
                {
                    switch (bySubstatusValue)
                    {
                        case 0x00:
                            subStatus = SubStatus.Bad_Non_specific;
                            
                            break;
                        case 0x04:
                            subStatus = SubStatus.Bad_ConfigError;
                            
                            break;
                        case 0x08:
                            subStatus = SubStatus.Bad_NotConnected;
                            
                            break;
                        case 0x0C:
                            subStatus = SubStatus.Bad_DeviceFailure;
                            
                            break;
                        case 0x10:
                            subStatus = SubStatus.Bad_SensorFailure;
                            
                            break;
                        case 0x14:
                            subStatus = SubStatus.Bad_NoComm_LastUsableVal;
                            
                            break;
                        case 0x18:
                            subStatus = SubStatus.Bad_NoComm_NoUsableVal;
                            
                            break;
                        case 0x1C:
                            subStatus = SubStatus.Bad_OS;
                            
                            break;
                        default:
                            subStatus = SubStatus.Bad_Reserved;
                            
                            break;
                    }
                }
                else if (quality == Quality.Uncertain)
                {
                    switch (bySubstatusValue)
                    {
                        case 0x00:
                            subStatus = SubStatus.Uncertain_NonSpecific;
                            
                            break;
                        case 0x04:
                            subStatus = SubStatus.Uncertain_LastUsableVal;
                            
                            break;
                        case 0x08:
                            subStatus = SubStatus.Uncertain_SubstituteSet;
                            
                            break;
                        case 0x0C:
                            subStatus = SubStatus.Uncertain_InitialValue;
                            
                            break;
                        case 0x10:
                            subStatus = SubStatus.Uncertain_NonAccurateSensorConversion;
                            
                            break;
                        case 0x14:
                            subStatus = SubStatus.Uncertain_EUViolation;
                            
                            break;
                        case 0x18:
                            subStatus = SubStatus.Uncertain_SubNormal;
                            
                            break;
                        case 0x1C:
                            subStatus = SubStatus.Uncertain_ConfigError;
                            
                            break;
                        case 0x20:
                            subStatus = SubStatus.Uncertain_SimValue;
                            
                            break;
                        case 0x24:
                            subStatus = SubStatus.Uncertain_SensorCalibration;
                            
                            break;
                        default:
                            subStatus = SubStatus.Uncertain_Reserved;
                            
                            break;
                    }
                }
                else if (quality == Quality.Good_NonCascade)
                {
                    switch (bySubstatusValue)
                    {
                        case 0x00:
                            if (Limits.HighLimited == limit ||
                                    Limits.LowLimited == limit)
                            {
                                subStatus = SubStatus.GoodNonCascaded_InvalidStatus;
                                
                            }
                            else
                            {
                                subStatus = SubStatus.GoodNonCascaded_OK;
                                
                            }
                            break;
                        case 0x04:
                            subStatus = SubStatus.GoodNonCascaded_UpdateEvent;
                            
                            break;
                        case 0x08:
                            subStatus = SubStatus.GoodNonCascaded_ActiveAdvisoryAlarm;
                            
                            break;
                        case 0x0C:
                            subStatus = SubStatus.GoodNonCascaded_ActiveCriticalAlarm;
                            
                            break;
                        case 0x10:
                            subStatus = SubStatus.GoodNonCascaded_UnackUpdateEvent;
                            
                            break;
                        case 0x14:
                            subStatus = SubStatus.GoodNonCascaded_UnackAdvisoryAlarm;
                            
                            break;
                        case 0x18:
                            subStatus = SubStatus.GoodNonCascaded_UnackCriticalAlarm;
                            
                            break;
                        case 0x20:
                            subStatus = SubStatus.GoodNonCascaded_InitialFailSafe;
                            
                            break;
                        case 0x24:
                            subStatus = SubStatus.GoodNonCascaded_MaintenanceRequired;
                            
                            break;
                        default:
                            subStatus = SubStatus.GoodNonCascaded_Reserved;
                            
                            break;
                    }
                }
                else //if (Quality == (byte) WindowsApp.Readings.ObservedReadings_Quality.Good_Cascade)
                {
                    switch (bySubstatusValue)
                    {
                        case 0x00:
                            if (Limits.HighLimited == limit ||
                                    Limits.LowLimited == limit)
                            {
                                subStatus = SubStatus.GoodCascaded_InvalidStatus;
                                
                            }
                            else
                            {
                                subStatus = SubStatus.GoodCascaded_OK;
                                
                            }
                            break;
                        case 0x04:
                            subStatus = SubStatus.GoodCascaded_InitialisationAck;
                            
                            break;
                        case 0x08:
                            subStatus = SubStatus.GoodCascaded_InitialisationRequest;
                            
                            break;
                        case 0x0C:
                            subStatus = SubStatus.GoodCascaded_NotInvited;
                            
                            break;
                        case 0x10:
                            subStatus = SubStatus.GoodCascaded_Reserved;
                            
                            break;
                        case 0x14:
                            subStatus = SubStatus.GoodCascaded_DoNotSelect;
                            
                            break;
                        case 0x18:
                            subStatus = SubStatus.GoodCascaded_LocalOverride;
                            
                            break;
                        case 0x1C:
                            subStatus = SubStatus.GoodCascaded_Reserved;
                            
                            break;
                        case 0x20:
                            subStatus = SubStatus.GoodCascaded_InitiateFailSafe;
                            
                            break;
                        default:
                            subStatus = SubStatus.GoodCascaded_Reserved;
                            
                            break;
                    }
                }

                #endregion Codes to check for Sub-statusValue
            }

            qualityLabel = PrintQuality(quality);
            subStatusLabel = PrintSubStatus(subStatus);
            limitLabel = PrintLimits(limit);
        }
    }
}
