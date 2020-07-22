using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSafetySystemProtocal.YongMeiSanHeng.Model
{
    public enum PointState : int
    {
        UnKnow = 0,                     //未知
        Init = 1,                       //初始化


        OFF = 2,                        //分站：通讯中断；模拟量/开关量：断线（没有接传感器）
        ServiceOff = 3,                  //采集服务：挂了(没有初始化数据或者没有刷新数据)
        Unused = 4,                     //设备休眠(分站,模拟量,开关量)

        [Description("无馈电")]
        Feed_No = 401031,
        [Description("馈电异常")]
        Feed_Abnormal = 401032,
        [Description("馈电正常")]
        Feed_Normal = 401033,

        [Description("一级报警")]
        LevelAlarm1 = 201100,
        [Description("二级报警")]
        LevelAlarm2 = 201200,
        [Description("三级报警")]
        LevelAlarm3 = 201300,
        [Description("四级报警")]
        LevelAlarm4 = 201400,



        LinkingSwitchBoard = 15,        //分站：正在连接交换机
        [Description("交流正常")]
        SubStation_AC = 101001, //分站：交流正常
        [Description("直流正常")]
        SubStation_DC = 101002, //分站：直流正常
        [Description("通信误码")]
        SubStation_BitError = 101003, //分站: 通信误码
        [Description("通讯中断")]
        SubStation_OFF = 101004, //分站: 通讯中断
        [Description("网络中断")]
        SubStation_NetError = 101005, //分站：网络中断（连接网络设备失败）（如果采用tcp连接有效）
        [Description("连接交换机")]
        SubStation_LinkNet = 101006, //分站：正在连接交换机(KJ370F分站)
        [Description("无配置数据")]
        SubStation_NoConfigData = 101011, //分站: 分站当前无配置数据(KJ370F分站)
        [Description("有配置数据")]
        SubStation_HaveConfigData = 101012, //分站: 分站当前有配置数据(KJ370F分站)
        [Description("无历史数据")]
        SubStation_NoHistoryData = 101013, //分站: 分站当前无历史数据(KJ370F分站)
        [Description("有历史数据")]
        SubStation_HaveHistoryData = 101014, //分站: 分站当前有历史数据(KJ370F分站)
        [Description("无历史闭锁数据")]
        SubStation_NoInterlockedHistoryData = 101015,
        [Description("有历史闭锁数据")]
        SubStation_HaveInterlockedHistoryData = 101016,

        [Description("分站传感器信号状态")]
        SubStation_SensorUpdateDiffState = 101017,

        [Description("无历史通道配置变化")]
        SubStation_NoChannelChangeHistoryData = 101018,
        [Description("有历史通道配置变化")]
        SubStation_HaveChannelChangeHistoryData = 101019,

        [Description("无上传间隔中状态变化 ")]
        SubStation_NoChannelIntervalHistoryData = 101020,
        [Description("有上传间隔中状态变化 ")]
        SubStation_HaveChannelIntervalHistoryData = 101021,

        #region--电源箱信息--
        [Description("支持电源箱管理")]
        Power_Support = 101101,
        [Description("不支持电源箱管理")]
        Power_NotSupport = 101102,
        //[Description("交直流状态")]
        //Power_AdcStatus = 101110,
        //[Description("交直流状态:交流供电")]
        //Power_AdcStatus_1 = 101111,
        //[Description("交直流状态:直流供电")]
        //Power_AdcStatus_2 = 101112,
        //[Description("交直流状态:被动直流供电")]
        //Power_AdcStatus_3 = 101113,
        //[Description("交直流状态:被动直流供电后复电异常")]
        //Power_AdcStatus_4 = 101114,

        //[Description("交流供电状态")]
        //Power_AcStatus = 101120,
        //[Description("交流供电状态:供电正常")]
        //Power_AcStatus_1 = 101121,
        //[Description("交流供电状态:供电异常")]
        //Power_AcStatus_2 = 101122,

        //[Description("电池状态")]
        //Power_BatStatus = 101130,
        //[Description("电池状态:未充电")]
        //Power_BatStatus_1 = 101131,
        //[Description("电池状态:正在充电")]
        //Power_BatStatus_2 = 101132,
        //[Description("电池状态:充电异常")]
        //Power_BatStatus_3 = 101133,
        //[Description("电池状态:电池未连接")]
        //Power_BatStatus_4 = 101134,
        //[Description("电池状态:电池通讯异常")]
        //Power_BatStatus_5 = 101135,

        //[Description("电池电量")]
        //Power_BatPercent = 101140,
        //[Description("电池电压")]
        //Power_BatVol = 101141,

        //[Description("响应强制放电")]
        //Power_ReCtlOnCmd = 101160,
        //[Description("响应强制放电:成功切换")]
        //Power_ReCtlOnCmd_1 = 101161,
        //[Description("响应强制放电:电量不足10%")]
        //Power_ReCtlOnCmd_2 = 101162,
        //[Description("响应强制放电:电池断线")]
        //Power_ReCtlOnCmd_3 = 101163,
        //[Description("响应强制放电:充电电路异常")]
        //Power_ReCtlOnCmd_4 = 101164,

        //[Description("响应强制复电")]
        //Power_ReCtlOffCmd = 101170,
        //[Description("响应强制复电:成功切换,有交流")]
        //Power_ReCtlOffCmd_1 = 101171,
        //[Description("响应强制复电:成功切换,无交流")]
        //Power_ReCtlOffCmd_2 = 101172,
        //[Description("响应强制复电:切换异常")]
        //Power_ReCtlOffCmd_3 = 101173,

        //[Description("电源箱类型:12V 电源箱")]
        //Power_PowerType_0x20 = 0x20,
        //[Description("电源箱类型:18V ib电源箱")]
        //Power_PowerType_0x21 = 0x21,
        //[Description("电源箱类型:21V ib电源箱")]
        //Power_PowerType_0x22 = 0x22,
        //[Description("电源箱类型:24V ib电源箱")]
        //Power_PowerType_0x23 = 0x23,
        //[Description("电源箱类型:18V ia电源箱")]
        //Power_PowerType_0x24 = 0x24,
        //[Description("电源箱类型:21V ia电源箱")]
        //Power_PowerType_0x25 = 0x25,
        //[Description("电源箱类型:24V ia电源箱")]
        //Power_PowerType_0x26 = 0x26,
        //[Description("电源箱类型:14.8V ia电池电源")]
        //Power_PowerType_0x27 = 0x27,

        //[Description("电池充电电压")]
        //Power_BatChargVol = 101143,
        //[Description("电池放电电压")]
        //Power_BatDisChargVol= 101144,
        //[Description("电池充电电流")]
        //Power_BatChargCurrent = 101145,
        //[Description("电池放电电流")]
        //Power_BatDisChargCurrent = 101144,

        #endregion-电源箱信息-



        #region--模拟量--
        /// <summary>
        /// 模拟量：正常
        /// </summary>
        [Description("正常")]
        Analog_OK = 201001,
        /// <summary>
        /// 模拟量: 传感器断线
        /// </summary>
        [Description("断线")]
        Analog_OFF = 201002,

        /// <summary>
        /// 模拟量:上限断电
        /// </summary>
        [Description("上限断电")]
        Analog_UpperLimitSwitchingOff = 201003,


        /// <summary>
        /// 模拟量:上限报警
        /// </summary>
        [Description("上限报警")]
        Analog_UpperLimitWarning = 201004,

        /// <summary>
        /// 模拟量:下限断电
        /// </summary>
        [Description("下限断电")]
        Analog_LowerLimitSwitchingOff = 201005,

        /// <summary>
        /// 模拟量:下限报警
        /// </summary>
        [Description("下限报警")]
        Analog_LowerLimitWarning = 201006,

        /// <summary>
        /// 模拟量上溢（传感器超上限）
        /// </summary>
        [Description("模拟量上溢")]
        Analog_Overflow = 201007, //
        /// <summary>
        /// 模拟量下溢（传感器超下限）
        /// </summary>
        [Description("模拟量下溢")]
        Analog_Underflow = 201008, //
        /// <summary>
        /// 模拟量因为分站通讯失败（无回应）导致的断线
        /// </summary>
        [Description("分站断线")]
        Analog_SubStationOFF = 201011,
        //
        //[Description("模拟量:风速反向")]
        //WindSpeedReverse = 102100, //模拟量:风速反向
        #endregion-模拟量-







        #region--开关量--
        /// <summary>
        /// 开关量:0态
        /// </summary>
        [Description("S0态")]
        Switch_State0 = 301001,
        /// <summary>
        /// 开关量:1态
        /// </summary>
        [Description("S1态")]
        Switch_State1 = 301002, //开关量
        /// <summary>
        /// 开关量:2态
        /// </summary>
        [Description("S2态")]
        Switch_State2 = 301003,
        /// <summary>
        /// 开关量因为分站通讯失败（无回应）导致的断线
        /// </summary>
        [Description("分站断线")]
        Switch_SubStationOFF = 301011,
        #endregion-开关量-
        #region--控制量--

        /// <summary>
        /// 控制量:Control_State0:0态(控制量)
        /// </summary>
        [Description("0态")]
        Control_State0 = 401001, //
        /// <summary>
        /// 控制量:Control_State1:1态(控制量)
        /// </summary>
        [Description("1态")]
        Control_State1 = 401002,
        /// <summary>
        /// 控制量:执行器未在线
        /// </summary>
        [Description("断线")]
        Control_OFF = 401010,
        /// <summary>
        /// 控制量:OFF:分站引起断线(控制量)
        /// </summary>
        [Description("分站断线")]
        Control_SubStationOFF = 401011,
        /// <summary>
        /// 控制量:控制口受分站控制(控制量)
        /// </summary>
        [Description("自动控制")]
        Control_Automatic = 401021,
        /// <summary>
        /// 控制量:控制口受程序控制(控制量)
        /// </summary>
        [Description("手动控制")]
        Control_Manual = 401022,
        #endregion-控制量-
        [Description("控制量:无馈电")] Control_NoFeed = 104010,
        [Description("控制量:馈电异常")] Control_AbnormalFeed = 104011,
        [Description("控制量:馈电正常")] Control_NormalFeed = 104012,


    }
    enum ProtocolState
    {
        正常 = 0,
        报警 = 1,
        断电 = 2,
        馈电异常 = 3,
        标校 = 4,
        标校报警 = 5,
        超量程 = 8,
        分站故障 = 16,
        不巡检 = 32,
        暂停 = 64,
        传感器故障 = 128
    }

    enum ProtocalAlarmState
    {
        超限报警 = 1,
        断电报警 = 2,
        馈电异常 = 3,
        传感器断线 = 4,
        分站断电 = 5,
        分站不通 = 6,
        标校 = 7,
        超量程 = 8
    }


    /// <summary>
    /// 区域类型位置
    /// </summary>
    enum UnitType
    {

    }
    /// <summary>
    /// 传感器在区域中的位置编码
    /// </summary>
    enum AreaLocation
    {
        T0,
        T1
    }
    /// <summary>
    /// 测点类型
    /// </summary>
    enum PointType
    {
        /// <summary>
        /// 模拟量
        /// </summary>
        Analog = 1,
        /// <summary>
        /// 开关量
        /// </summary>
        Switch = 2,
        /// <summary>
        /// 累积量
        /// </summary>
        Accmulation = 3,
        /// <summary>
        /// 控制量
        /// </summary>
        Control = 4,
        /// <summary>
        /// 保护量
        /// </summary>
        Protect = 5,
        /// <summary>
        /// 多态量
        /// </summary>
        Multiple = 6,
        /// <summary>
        /// 调节量
        /// </summary>
        Ajust = 7,
    }
    enum PointTypeConverter
    {
        A = 1,
        D = 2,
        C = 3
    }
}
