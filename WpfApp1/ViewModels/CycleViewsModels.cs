using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;
using WpfApp1.Conmmon;

namespace WpfApp1.ViewModels
{
    class CycleViewsModels : PropertyChangedBase
    {
          public ObservableCollection<FangAn>FangAns { get; set; }
          public ObservableCollection<FangAnChiLun>FangAnChiLuns { get; set; }
          public ObservableCollection<FangAnFaDianJi>fangAnFaDianJis { get; set; }  
          public ObservableCollection<FangAnBianLiuQi>FangAnBianLiuQis { get; set; }
          public List<string> Alarms { get; set; }
        public ObservableCollection<FangAn> FangAns1 { get; set; }

        public CycleViewsModels()
        {


            FangAns1 = new ObservableCollection<FangAn> { };


            FangAns = new ObservableCollection<FangAn>
        {
            new FangAn { ProjectName = "不停机检查", Content = "检查叶片是否存在哨声，是否存在3个叶片声音不一致现象，存在时应停机进一步检查", Time = "2周" },
            new FangAn { ProjectName = "停机检查", Content = "检查叶片迎风面、背风面、前后缘等表面是否存在裂纹、发黑、破损等问题，必要时进行修补", Time = "2个月" },
            new FangAn { ProjectName = "专项检查", Content = "强雷电天气后进行一次叶片雷击检查", Time = "不定期" },
            new FangAn { ProjectName = "叶片防雷通道", Content = "测量叶片接闪器接地电阻的大小，检查接闪器外形是否损坏、缺失，是否与叶片结合部位出现空隙。", Time = "每年" },
            new FangAn { ProjectName = "不停机检查", Content = "检查叶片是否存在哨声，是否存在3个叶片声音不一致现象，存在时应停机进一步检查", Time = "2周" },
            new FangAn { ProjectName = "停机检查", Content = "检查叶片迎风面、背风面、前后缘等表面是否存在裂纹、发黑、破损等问题，必要时进行修补", Time = "2个月" },
            new FangAn { ProjectName = "专项检查", Content = "强雷电天气后进行一次叶片雷击检查", Time = "不定期" },
            new FangAn { ProjectName = "叶片防雷通道", Content = "测量叶片接闪器接地电阻的大小，检查接闪器外形是否损坏、缺失，是否与叶片结合部位出现空隙。", Time = "每年" }
        };
            FangAnChiLuns = new ObservableCollection<FangAnChiLun>
    {
        new FangAnChiLun { ProjectName = "主轴轴承", Content = "在日常检查中发现主轴轴承支座密封处渗漏油严重或集油盒内积满油脂时，应对主轴轴承进行开箱检查，检查结合油脂更换的周期，观察油脂的状况，以及主轴轴承滚动体和轨道磨损情况，轴承内外圈和保持架是否存在裂纹等状况。", Time = "1个月" },
        new FangAnChiLun { ProjectName = "齿轮箱本体", Content = "齿轮箱本体检查主要包括油位、磁堵、箱体的检查。通过油位计检查油位，要在静止-段时间的情况下检查油位，并且确保没有油沫。齿轮箱停止运行一段时间后打开磁堵检查表面的铁屑或铁粉情况。齿轮箱箱体中行星架和齿轮箱连接位置、前端盖-大齿圈-后端盖连接位置、高速轴连接位置等渗漏油情况。", Time = "1个月" },
        new FangAnChiLun { ProjectName = "齿轮箱弹性支撑", Content = "检查齿轮箱的扭力臂是否发生窜动，弹性支撑中弹性体是否出现龟纹、开裂等老化或发生显著的移位。", Time = "1个月" },
        new FangAnChiLun { ProjectName = "润滑及冷却系统", Content = "（1）检查温度传感器和压力传感器的功能是否正常；（2）阀及管路结构、橡胶管路以及散热片等位置的渗漏油检查；（3）检查油滤和空滤等过滤器的颜色和杂质变化情况；（4）检查润滑系统和冷却系统电动机的运转和噪声情况；（5）检查油分配控制阀、温控阀、旁通阀等功能是否正常。", Time = "1个月" },
        new FangAnChiLun { ProjectName = "高强紧固件", Content = "检查齿轮箱本体连接螺栓、主轴-轮毂、轴承支座-机架、齿轮箱弹性支撑-机架、锁紧盘等8.8级以上连接螺栓的标记线是否发生偏移。", Time = "1个月" },
        new FangAnChiLun { ProjectName = "高强紧固件抽检", Content = "对上述连接螺栓采用力矩扳手按照 10%的比例进行抽检，检查力矩不大于螺栓预紧力。", Time = "6个月" },
        new FangAnChiLun { ProjectName = "主轴和齿轮箱", Content = "监测主轴轴承、齿轮箱油池和高速轴轴承温度以及过滤器压力差和分配器处压力值。", Time = "1个月" }
    };
            fangAnFaDianJis = new ObservableCollection<FangAnFaDianJi>
    {
        new FangAnFaDianJi
        {
            ProjectName = "发电机表面检查",
            Content = "无锈蚀，清扫外壳",
            Time = "6个月"
        },
        new FangAnFaDianJi
        {
            ProjectName = "发电机弹性支撑连接机架螺栓力矩检查",
            Content = "检查连接机架螺栓的力矩是否正常",
            Time = "12个月"
        },
        new FangAnFaDianJi
        {
            ProjectName = "发电机连接发电机弹性支撑螺栓力矩检查",
            Content = "检查弹性支撑螺栓的力矩是否符合要求",
            Time = "12个月"
        },
        new FangAnFaDianJi
        {
            ProjectName = "发电机滑环和电刷检查",
            Content = "清理碳粉",
            Time = "6个月"
        },
        new FangAnFaDianJi
        {
            ProjectName = "检查发电机接线紧固及绝缘检查",
            Content = "确保发电机接线牢固，绝缘良好",
            Time = "12个月"
        },
        new FangAnFaDianJi
        {
            ProjectName = "发电机轴承噪声及震动检查",
            Content = "检查轴承运行状态，识别异常噪声或震动",
            Time = "6个月"
        },
        new FangAnFaDianJi
        {
            ProjectName = "检查发电机润滑",
            Content = "根据电机型号加注润滑油",
            Time = "6个月"
        },
        new FangAnFaDianJi
        {
            ProjectName = "发电机风扇接线及功能检查",
            Content = "检查风扇接线是否完好，功能正常",
            Time = "6个月"
        },
        new FangAnFaDianJi
        {
            ProjectName = "测温 pt100 检查",
            Content = "检查功能是否正常",
            Time = "6个月"
        },
        new FangAnFaDianJi
        {
            ProjectName = "发电机冷却风扇风道检查",
            Content = "检查风道是否畅通，清理堵塞",
            Time = "6个月"
        }
    };
            FangAnBianLiuQis = new ObservableCollection<FangAnBianLiuQi>
            {

                new FangAnBianLiuQi
        {
            ProjectName = "空气滤网",
            Content = "对风冷来说，检查柜体的清洁度，如有必要用软抹布或真空吸尘器清洁；对液冷来说，需定期检查系统泄露和工作压力情况。",
            Time = "1个月"
        },
        new FangAnBianLiuQi
        {
            ProjectName = "功率电缆",
            Content = "检查螺丝和螺栓等部位是否松动，必要时需加固；检查网侧电缆、定子连接电缆、转子连接电缆的接线紧固情况和电缆完好情况，如有异常及时处理。",
            Time = "1个月"
        },
        new FangAnBianLiuQi
        {
            ProjectName = "电路板",
            Content = "检查电路板是否积灰，必要时使用防静电刷子或真空吸尘器清洁印刷电路板和功率单元。",
            Time = "3个月"
        },
        new FangAnBianLiuQi
        {
            ProjectName = "散热器",
            Content = "检查散热器的积灰情况，清理散热器灰尘。",
            Time = "1年"
        },
        new FangAnBianLiuQi
        {
            ProjectName = "电容器",
            Content = "检查电容器是否出现鼓包、漏液的情况，电容容量是否降至标称容量的80%以下，出现这种情况及时更换。",
            Time = "1年"
        },
        new FangAnBianLiuQi
        {
            ProjectName = "UPS电源",
            Content = "检查UPS工作状况，运行模式切换、工作电压是否正常，对电池做充放电测试。",
            Time = "6个月"
        },
        new FangAnBianLiuQi
        {
            ProjectName = "断路器",
            Content = "检查主断路器的动作次数是否超过规定次数，超过的话需要更换。",
            Time = "1年"
        }
            };

        }

    }
    }
