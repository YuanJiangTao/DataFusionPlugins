using MineSafetySystemProtocal.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MineSafetySystemProtocal.YongMeiSanHeng.Model
{
    class BasePoint
    {
        public bool IsValid { get; set; }

        protected string GetPointCode(string protocalPointCode)
        {
            return protocalPointCode.Substring(protocalPointCode.Length - 6);
        }

        protected int GetSensorEquipId(string pointCode, PointTypeConverter pointType)
        {
            return pointCode.Replace(pointType.ToString(), ((int)pointType).ToString()).ToInt();
        }
        protected int GetSensorEquipId(string pointCode)
        {
            var result = 0;
            if (pointCode.Contains("A"))
            {
                result = pointCode.Replace(PointTypeConverter.A.ToString(), ((int)PointTypeConverter.A).ToString()).ToInt();
            }
            else if (pointCode.Contains("D"))
            {
                result = pointCode.Replace(PointTypeConverter.D.ToString(), ((int)PointTypeConverter.D).ToString()).ToInt();
            }
            else if (pointCode.Contains("C"))
            {
                result = pointCode.Replace(PointTypeConverter.C.ToString(), ((int)PointTypeConverter.C).ToString()).ToInt();
            }
            return result;
        }
        protected (int EquipState, int ValueState, string Value, int FeedState) GetRealValueState(string pointCode, string originalValue, ProtocolState protocolState)
        {
            int equipState = 0;
            int valueState = (int)PointState.Analog_OK;
            string value = "";
            int feedState = 201;
            if (pointCode.Contains("A"))
            {
                value = originalValue;
                var result = ConvertValueState(protocolState, PointTypeConverter.A, originalValue);
                equipState = result.equipState;
                valueState = result.valueState;
            }
            else if (pointCode.Contains("C"))
            {
                var result = ConvertValueState(protocolState, PointTypeConverter.C, originalValue);
                equipState = result.equipState;
                valueState = result.valueState;
            }
            else if (pointCode.Contains("D"))
            {
                var result = ConvertValueState(protocolState, PointTypeConverter.D, originalValue);
                equipState = result.equipState;
                valueState = result.valueState;
            }
            return (equipState, valueState, value, feedState);
        }
        protected (int valueState, int equipState) ConvertValueState(ProtocolState protocolState, PointTypeConverter pointType, string value)
        {
            int equipState = 1030000;
            int valueState = 0;
            switch (protocolState)
            {
                case ProtocolState.标校:
                    {
                        valueState = (int)PointState.Analog_OK;
                        equipState += 1;
                    }
                    break;
                case ProtocolState.正常:
                    {
                        switch (pointType)
                        {
                            case PointTypeConverter.A:
                                {
                                    valueState = (int)PointState.Analog_OK;
                                    equipState = 1030000;
                                }
                                break;
                            case PointTypeConverter.D:
                                {
                                    if (int.TryParse(value, out var realValue))
                                    {
                                        if (realValue == 0)
                                            valueState = (int)PointState.Switch_State1;
                                        else if (realValue == 1)
                                            valueState = (int)PointState.Switch_State2;
                                        else
                                            valueState = (int)PointState.Switch_State0;
                                    }
                                    else
                                        valueState = (int)PointState.Switch_State0;
                                }
                                break;
                            case PointTypeConverter.C:
                                {
                                    if (int.TryParse(value, out var realValue))
                                    {
                                        if (realValue == 0)
                                            valueState = (int)PointState.Control_State0;
                                        else
                                            valueState = (int)PointState.Control_State1;
                                    }
                                    else
                                        valueState = (int)PointState.Control_State1;
                                }
                                break;
                        }
                    }
                    break;
                case ProtocolState.断电:
                    {
                        valueState = (int)PointState.Analog_UpperLimitSwitchingOff;
                        equipState = 1030000;
                    }
                    break;
                case ProtocolState.标校报警:
                    {
                        valueState = (int)PointState.Analog_UpperLimitSwitchingOff;
                        equipState += 1;
                    }
                    break;
                case ProtocolState.超量程:
                    {
                        valueState = (int)PointState.Analog_Underflow;
                        equipState = 1030000;
                    }
                    break;
                case ProtocolState.传感器故障:
                    {
                        switch (pointType)
                        {
                            case PointTypeConverter.A:
                                {
                                    valueState = (int)PointState.Analog_OFF;
                                    equipState = 1;
                                }
                                break;
                            case PointTypeConverter.D:
                                {
                                    valueState = (int)PointState.Switch_State0;
                                    equipState = 1;
                                }
                                break;
                            case PointTypeConverter.C:
                                {
                                    valueState = (int)PointState.Control_OFF;
                                    equipState = 1;
                                }
                                break;
                        }

                    }
                    break;
                case ProtocolState.分站故障:
                    {

                        switch (pointType)
                        {
                            case PointTypeConverter.A:
                                {
                                    valueState = (int)PointState.Analog_SubStationOFF;
                                    equipState = 1;
                                }
                                break;
                            case PointTypeConverter.D:
                                {
                                    valueState = (int)PointState.Switch_SubStationOFF;
                                    equipState = 1;
                                }
                                break;
                            case PointTypeConverter.C:
                                {
                                    valueState = (int)PointState.Control_SubStationOFF;
                                    equipState = 1;
                                }
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }
            return (valueState, equipState);
        }

    }
}
