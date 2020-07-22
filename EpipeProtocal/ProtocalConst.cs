using DataFusionProtocal.Interfaces;
using EpipeProtocal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpipeProtocal
{
    internal class ProtocalConst
    {
        public const string SANHENG_SAFETY_PROTOCAL = "永煤集团-三恒科技";


        public const string SELECT_PROTOCAL = "选择协议";
        public const string MONITOR_FILEPATH = "监听文件夹";
        public const string BACKUP_FILEPATH = "备份文件夹";
        public const string ENCODING = "编码";
        public const string FILE_EXTENSION = "扩展名";
    }
    /// <summary>
    /// 测点类型
    /// </summary>
    enum PointType : byte
    {
        Undefined = 0,                  //未定义
        SubStation = 1,                 //分站
        Analog = 2,                     //模拟量
        Switch = 3,                     //开关量
        Control = 4,                    //控制量
        Accumulation = 5,               //累计量
        Logic = 6,                      //逻辑量
        DASService = 7,                 //DAS采集软件
        PCMCIA = 8                      //3G无线网卡
    }
    /// <summary>
    /// 测点状态
    /// </summary>
    enum PointState : byte
    {
        UnKnow = 0,                     //未知
        Init = 1,                       //初始化


        OFF = 2,                        //分站：通讯中断；模拟量/开关量：断线（没有接传感器）
        ServiceOff = 3,                  //采集服务：挂了(没有初始化数据或者没有刷新数据)
        Unused = 4,                     //设备休眠(分站,模拟量,开关量)


        OverflowOFF = 5,                //模拟量上溢导致的断线
        UnderflowOFF = 6,               //模拟量下溢导致的断线
        SubStationOFF = 7,              //分站通讯中断（异常）导致的断线

        AC = 11,                        //分站：交流正常
        DC = 12,                        //分站：直流正常
        BitError = 13,                  //分站: 通信误码
        NetworkInterruption = 14,       //分站：网络中断（如果采用tcp连接有效）

        OK = 20,                          //模拟量：正常
        UpperLimitSwitchingOff = 21,    //模拟量:上限断电
        UpperLimitWarning = 22,         //模拟量:上限报警
        UpperLimitResume = 23,          //模拟量:上限恢复
        UpperLimitEarlyWarning = 24,    //模拟量:上限预警

        LowerLimitSwitchingOff = 25,    //模拟量:下限断电
        LowerLimitWarning = 26,         //模拟量:下限报警
        LowerLimitResume = 27,          //模拟量:下限恢复
        LowerLimitEarlyWarning = 28,    //模拟量:下限预警

        State0 = 30,                    //开关量:0态
        State1 = 31,                    //开关量:1态
        State2 = 32,                    //开关量:2态

        ErrorRandUpperLimit = 41,       //逻辑量:超过误差上限
        ErrorRandLowerLimit = 42,        //逻辑量:超过误差下限

        ConnectDBFailure = 51,          //采集服务器/或客户端 连接数据库失败
        SelectDBFailure = 52,           //采集服务器/或客户端 查询数据库操作失败
        UpdateDBFailure = 53,           //采集服务器/或客户端 更新数据库操作失败
        InsertDBFailure = 54,            //采集服务器/或客户端 插入数据库操作失败

        PCMCIA_OK = 61,                 //无线网卡运行正常
        PCMCIA_Fault = 62,              //无线网卡故障
        PCMCIA_Invalid = 63,             //无线网卡无效（没有插入）


        VerifyAlarm1 = 71,
        VerifyAlarm2 = 72,
        VerifyAlarm3 = 73,
        VerifyAlarm4 = 74,
        VerifyAlarm5 = 75

    }

    /// <summary>
    /// 测点报警类型
    /// </summary>
    enum AlarmState : byte
    {
        UnKnow = 0,                     //未知

        OFF = 2,                        //分站：通讯中断；模拟量/开关量：断线
        ServiceOff = 3,                  //采集服务挂了(没有初始化数据或者没有刷新数据)

        BitError = 13,                  //分站:通信误码


        UpperLimitSwitchingOff = 21,    //模拟量:上限断电
        UpperLimitWarning = 22,         //模拟量:上限报警
        UpperLimitResume = 23,          //模拟量:上限恢复
        UpperLimitEarlyWarning = 24,    //模拟量:上限预警

        LowerLimitSwitchingOff = 25,    //模拟量:下限断电
        LowerLimitWarning = 26,         //模拟量:下限报警
        LowerLimitResume = 27,          //模拟量:下限恢复
        LowerLimitEarlyWarning = 28,    //模拟量:下限预警

        State0 = 30,                    //开关量:0态
        State1 = 31,                    //开关量:1态
        State2 = 32,                    //开关量:2态
    }

    /// <summary>
    /// 馈电类型
    /// </summary>
    enum FeedState : byte
    {
        UnKnow = 0,                     //未知
        Init = 1,                       //初始化
        Abnormal = 10,					//馈电异常，未知原因
        //以下同PointState相关项一致
        OK = 20,                        //模拟量：馈电正常
        UpperLimitSwitchingOff = 21,    //模拟量:上限断电馈电异常
        UpperLimitWarning = 22,         //模拟量:上限报警馈电异常
        UpperLimitResume = 23,          //模拟量:上限恢复馈电异常
        UpperLimitEarlyWarning = 24,    //模拟量:上限预警馈电异常

        LowerLimitSwitchingOff = 25,    //模拟量:下限断电馈电异常
        LowerLimitWarning = 26,         //模拟量:下限报警馈电异常
        LowerLimitResume = 27,          //模拟量:下限恢复馈电异常
        LowerLimitEarlyWarning = 28,    //模拟量:下限预警馈电异常

        State0 = 30,                    //开关量:0态馈电异常
        State1 = 31,                    //开关量:1态馈电异常
        State2 = 32,                    //开关量:2态馈电异常         
    };




}
