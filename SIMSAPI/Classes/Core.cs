﻿using SIMS.Entities;
using NLog;

namespace Matt40k.SIMSBulkImport.Classes
{
    /// <summary>
    /// 
    /// </summary>
    public class Core
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="udfValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static bool SetUdf(UDFValue udfValue, string newValue)
        {
            if (udfValue.TypedValueAttribute is StringAttribute)
            {
                logger.Trace("SetUdf: StringAttribute");
                ((StringAttribute)udfValue.TypedValueAttribute).Value = newValue;
            }
            else if (udfValue.TypedValueAttribute is BoolAttribute)
            {
                logger.Trace("SetUdf: BoolAttribute");
                BoolAttribute boolValue = (BoolAttribute)udfValue.TypedValueAttribute;
                if (string.IsNullOrEmpty(newValue) == false)
                {
                    switch (newValue.ToUpperInvariant())
                    {
                        case "TRUE":
                            boolValue.Value = true;
                            break;
                        case "FALSE":
                            boolValue.Value = false;
                            break;
                        default:
                            logger.Error("SetUdf: Invalid boolean value {0} for UDF {1}.", newValue, udfValue.Field.Description);
                            return false;
                    }

                }
            }
            else if (udfValue.TypedValueAttribute is EntityAttribute)
            {
                logger.Trace("SetUdf: EntityAttribute");
                EntityAttribute entityValue = (EntityAttribute)udfValue.TypedValueAttribute;
                if (entityValue.FromString(newValue) == false)
                {
                    logger.Error("SetUdf: Invalid lookup value {0} for UDF {1}.", newValue, udfValue.Field.Description);
                    return false;
                }
            }
            else
            {
                logger.Error("SetUdf: Unsupported UDF type {0} for UDF {1}.", udfValue.TypedValueAttribute, udfValue.Field.Description);
                return false;
            }

            return true;
        }

    }
}