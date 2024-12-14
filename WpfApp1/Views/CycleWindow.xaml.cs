using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Models;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    /// <summary>
    /// CycleWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CycleWindow : Window
    {
        public CycleWindow()
        {
            InitializeComponent();
            CycleViewsModels viewModel = new CycleViewsModels();
            this.DataContext = viewModel;
            BladeResultTextBlock1.Text = $"{GlobalLifetimes.BladeLife:F0}%";
            GearboxResultTextBlock.Text = $"{GlobalLifetimes.GearboxLife:F0}%";
            GeneratorResultTextBlock.Text = $"{GlobalLifetimes.GeneratorLife:F0}%";
            ConverterResultTextBlock.Text = $"{GlobalLifetimes.ConverterLife:F0}%";
            double currentLifePercentage = GlobalLifetimes.BladeLife;
            double currentBladeLifePercentage = GlobalLifetimes.GearboxLife;
            double currentGeneratorLifePercentage = GlobalLifetimes.GeneratorLife;
            double currentConverterLifePercentage = GlobalLifetimes.ConverterLife;

            //bia
            if (currentConverterLifePercentage >= 25 && currentConverterLifePercentage <= 75)
            {
                UpdateBianLiuQiContentForRange(viewModel.FangAnBianLiuQis, "25-75");
            }
            else if (currentConverterLifePercentage >= 0 && currentConverterLifePercentage < 25)
            {
                UpdateBianLiuQiContentForRange(viewModel.FangAnBianLiuQis, "0-25");
            }

            //发电机跟新状态
            if (currentGeneratorLifePercentage >= 25 && currentGeneratorLifePercentage <= 75)
            {
                UpdateContentForFadianji(viewModel.fangAnFaDianJis, "25-75");
            }
            else if (currentGeneratorLifePercentage >= 0 && currentGeneratorLifePercentage < 25)
            {
                UpdateContentForFadianji(viewModel.fangAnFaDianJis, "0-25");
            }

            //齿轮箱跟新状态
            if (currentBladeLifePercentage >= 30 && currentBladeLifePercentage <= 80)
            {
                UpdateChilunContentForRange(viewModel.FangAnChiLuns, "30-80");
            }
            else if (currentBladeLifePercentage >= 0 && currentBladeLifePercentage < 30)
            {
                UpdateChilunContentForRange(viewModel.FangAnChiLuns, "0-30");
            }

            //叶片跟新状态
            if (currentLifePercentage >= 30 && currentLifePercentage < 80)
            {
                UpdateContentForRange(viewModel.FangAns, "80-30");
            }
            else if (currentLifePercentage < 30)
            {
                UpdateContentForRange(viewModel.FangAns, "30-0");
            }


        }
        private void UpdateContentForRange(ObservableCollection<FangAn> fangAns, string range)//叶片跟新逻辑
        {
            fangAns.Clear();

            if (range == "80-30")
            {
                celue.Text = "       当叶片出现不同的故障时，需要根据不同的表现去排查维修或更换。例如出现裂纹时需要用环氧树脂等涂料进行填充加强，叶根断裂时则直接更换新叶片，旧叶片则参照剩余寿命为零进行循环利用。";
                            
    
                fangAns.Add(new FangAn
                {
                    ProjectName = "前缘",
                    Content = "腐蚀、开裂",
                    Time = "涂料填充"
                });
                fangAns.Add(new FangAn
                {
                    ProjectName = "后缘",
                    Content = "损坏",
                    Time = "切割和加固"
                });
                fangAns.Add(new FangAn
                {
                    ProjectName = "叶根",
                    Content = "断裂",
                    Time = "换新叶片，旧叶片按照剩余寿命为0循环利用"
                });
                fangAns.Add(new FangAn
                {
                    ProjectName = "表面",
                    Content = "开裂、雷击",
                    Time = "结构性损伤换新叶片，非结构性损伤通过涂覆或切割方式进行修复"
                });
            }
            else if (range == "30-0")
            {
                celue.Text = "";
                fangAns.Add(new FangAn
                {
                    ProjectName = "再利用",
                    Content = "作为板材利用，可制作为挡板、托盘等就近梯次利用到农庄、物流等场景",
                    Time = "切割成合适的板材后，可制作为挡板、托盘等就近梯次利用到农庄、物流等场景"
                });
                fangAns.Add(new FangAn
                {
                    ProjectName = "再利用",
                    Time = "作为建材利用",
                    Content = "根据不同的场景将叶片切割成10cm~20cm或其他尺寸长条状小块，作为新型复合材料取代木质复合材料，可用于地板、塑料路面障碍等。本方法并未将叶片复合材料分离，而是将叶片切割后直接制作成建筑材料，因此成本较低。"
                });
                fangAns.Add(new FangAn
                {
                    ProjectName = "再利用",
                    Content = "景观利用",
                    Time = "废弃的风机叶片可以被改造成艺术品，用于城市公园、展览等场合。"
                });
                fangAns.Add(new FangAn
                {
                    ProjectName = "再循环",
                    Content = "降解",
                    Time = "(a)打碎成粉末  (b)化学降解 (c)热解焦 (d)热解油"
                });

            }
        }

        private void UpdateChilunContentForRange(ObservableCollection<FangAnChiLun> fangAnChiLuns, string range)//齿轮箱跟新逻辑
        {
            fangAnChiLuns.Clear();

            if (range == "30-80")
            {
                celue1.Text = "当齿轮箱出现不同的故障时，需要根据不同的表现去排查维修或更换零部件。例如齿轮、滚轴等零件都可以直接更换，换下来的零件则根据磨损程度进行材料级报废或者再制造以供下次更换。";

                fangAnChiLuns.Add(new FangAnChiLun
                {
                    ProjectName = "本体",
                    Content = "箱体和行星架的损坏、齿轮失效、齿轮轴和轴失效、滚动轴承失效",
                    Time = "用备用件替换，换下来的原件进行再制造"
                });
                fangAnChiLuns.Add(new FangAnChiLun
                {
                    ProjectName = "弹性支撑",
                    Content = "弹性支撑故障",
                    Time = "维修器件"
                });
                fangAnChiLuns.Add(new FangAnChiLun
                {
                    ProjectName = "润滑及冷却系统",
                    Content = "润滑及冷却系统连接阀及管路、橡胶管路老化，油分配控制阀、温控阀、旁通阀等阀门的故障损坏，润滑系统和冷却系统电动机及电气设备的故障损坏，压力传感器、温度传感器等监测设备的故障损坏，油滤和空滤等过滤器的故障损坏",
                    Time = "更换老化的器件，更换下来的器件进行材料级回收"
                });
            }
            else if (range == "0-30")
            {
                celue.Text = "齿轮箱运行末期的循环利用方式主要有再利用和再循环两种方式。";

                fangAnChiLuns.Add(new FangAnChiLun
                {
                    ProjectName = "再利用",
                    Content = "将齿轮箱直接用到其他风电场",
                    Time = "几乎不需要成本，利用价值高，但是性能不如新产品"
                });
                fangAnChiLuns.Add(new FangAnChiLun
                {
                    ProjectName = "再制造",
                    Content = "在原有制造的基础上进行一次新的制造",
                    Time = "接近甚至超过新品品质，但是成本更高"
                });
                fangAnChiLuns.Add(new FangAnChiLun
                {
                    ProjectName = "再循环",
                    Content = "通过电弧炉将部件铸成钢锭",
                    Time = "简单、成本低。但是在回收过程中可能会出现分类错误而导致钢锭品质较低"
                });
            }
        }

        private void UpdateContentForFadianji(ObservableCollection<FangAnFaDianJi> fangAnFaDianJis, string range)
        {
            fangAnFaDianJis.Clear();

            if (range == "25-75")
            {
                celue2.Text = "在发电机使用寿命处于25%-75%范围时，应定期检测关键部件并及时维修以保证运行稳定性。";

                fangAnFaDianJis.Add(new FangAnFaDianJi
                {
                    ProjectName = "轴承",
                    Content = "轴承高温卡死、定转子扫膛",
                    Time = "故障排除，针对性解决"
                });
                fangAnFaDianJis.Add(new FangAnFaDianJi
                {
                    ProjectName = "转子",
                    Content = "转子断条、动平衡故障",
                    Time = "修补转子，更换动平衡衬块"
                });
                fangAnFaDianJis.Add(new FangAnFaDianJi
                {
                    ProjectName = "绕组",
                    Content = "绕组绝缘击穿",
                    Time = "更换转子或重新绕制绕组"
                });
                fangAnFaDianJis.Add(new FangAnFaDianJi
                {
                    ProjectName = "集电环",
                    Content = "集电环短路烧毁、滑道磨损严重、刷架损坏",
                    Time = "更换集电环"
                });
                fangAnFaDianJis.Add(new FangAnFaDianJi
                {
                    ProjectName = "冷却系统",
                    Content = "冷却风道阻塞、冷却风扇老化",
                    Time = "清理灰尘，更换老化器件"
                });
            }
            else if (range == "0-25")
            {
                celue2.Text = "当发电机寿命降至25%以下时，优先考虑更换或材料回收以降低运行风险。";

                fangAnFaDianJis.Add(new FangAnFaDianJi
                {
                    ProjectName = "再利用",
                    Content = "将发电机直接用到其他风电场",
                    Time = "几乎不需要成本，利用价值高，但是性能不如新产品"
                });
                fangAnFaDianJis.Add(new FangAnFaDianJi
                {
                    ProjectName = "再制造",
                    Content = "在原有制造的基础上进行一次新的制造",
                    Time = "再制造产品可以做到接近甚至超过新品品质，但是成本更高"
                });
                fangAnFaDianJis.Add(new FangAnFaDianJi
                {
                    ProjectName = "再制造",
                    Content = "机组改造",
                    Time = "铜线拆解后用于金属回收"
                });
         
            }
        }

        private void UpdateBianLiuQiContentForRange(ObservableCollection<FangAnBianLiuQi> FangAnBianLiuQis, string range)
        {
            FangAnBianLiuQis.Clear();

            if (range == "25-75")
            {
                celue3.Text = "当变流器出现不同的故障时，需要根据不同的故障表现去排查维修。例如当出现通信故障时，可能需要排查线路是否可靠或者是否存在电子元器件的损坏，出现元器件损坏时直接更换，替换下来的元器件进行材料级回收。";

                FangAnBianLiuQis.Add(new FangAnBianLiuQi
                {
                    ProjectName = "控制板",
                    Content = "通信故障",
                    Time = "检查线缆是否接触不良、元器件是否存在损坏"
                });
                FangAnBianLiuQis.Add(new FangAnBianLiuQi
                {
                    ProjectName = "预充电回路",
                    Content = "预充电故障",
                    Time = "检查预充电电路"
                });
                FangAnBianLiuQis.Add(new FangAnBianLiuQi
                {
                    ProjectName = "IGBT",
                    Content = "温度过高",
                    Time = "检查元器件和散热情况"
                });
                FangAnBianLiuQis.Add(new FangAnBianLiuQi
                {
                    ProjectName = "滤波回路",
                    Content = "网侧控制板输入 24V 电平消失",
                    Time = "检查是否存在熔断器开关未合到位或热继电器发热断开"
                });
                FangAnBianLiuQis.Add(new FangAnBianLiuQi
                {
                    ProjectName = "接触器",
                    Content = "接触器不动作或误动作",
                    Time = "检查接触器及周围的线缆和元器件"
                });
                FangAnBianLiuQis.Add(new FangAnBianLiuQi
                {
                    ProjectName = "Crowbar",
                    Content = "误动作或失效",
                    Time = "一般由转子过流引起，须查找转子过流原因，如碳刷烧毁，机侧调制是否正常"
                });
                FangAnBianLiuQis.Add(new FangAnBianLiuQi
                {
                    ProjectName = "并网点",
                    Content = "电网频率或相序错误，接地故障等",
                    Time = "排查故障的产生原因，针对性解决"
                });
            }
            else if (range == "0-25")
            {
                celue3.Text = "变流器运行末期的循环利用方式主要有再利用和再循环两种方式";

                FangAnBianLiuQis.Add(new FangAnBianLiuQi
                {
                    ProjectName = "再利用",
                    Content = "重新利用将变流器直接用到其他风电场",
                    Time = "利用成本低，利用价值高"
                });
                FangAnBianLiuQis.Add(new FangAnBianLiuQi
                {
                    ProjectName = "再利用",
                    Content = "将损坏或检测不合格的电子器件替换为新的电子器件",
                    Time = "性能不如新产品能"
                });
                FangAnBianLiuQis.Add(new FangAnBianLiuQi
                {
                    ProjectName = "再循环",
                    Content = "物理回收法通过破碎、筛分、分选就能得到所需的产品",
                    Time = "技术简单、成本低\t，但是易造成粉尘污染"
                });
                FangAnBianLiuQis.Add(new FangAnBianLiuQi
                {
                    ProjectName = "再循环",
                    Content = "用化学溶剂提取贵重金属",
                    Time = "资源利用率高，但是会破坏金属品质，有大量污水的产生"
                });
            }
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
