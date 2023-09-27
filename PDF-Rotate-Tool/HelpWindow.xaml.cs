using System;
using System.Collections.Generic;
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

namespace PDF_Rotate_Tool
{
    /// <summary>
    /// HelpWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
            InitializeComponent();

            // Initialize Help Information TextBox
            HelpInfoTbx.Text = "软件名称: PDF-Rotate-Tool。\n\n" +
                               "软件介绍: 本软件使用PDFSharp库实现批量旋转PDF文件功能。\n\n" +
                               "适用平台: Windows7/8/10/11。\n\n" +
                               "注意事项:（1）处理后的PDF文件会覆盖原文件；\n" +
                               "\t（2）软件无法处理加密或签章后的PDF文件；\n\n" +
                               "软件许可协议: \n" +
                               "重要提示：在使用本软件之前，请务必仔细阅读并理解本许可协议。如果您不同意本协议的任何条款和条件，请不要安装、复制或使用本软件。\n\n" +
                               "1.许可授予\n" +
                               "  本软件（以下简称\"软件\"）由本人（以下简称\"许可方\"）授权给您（以下简称\"用户\"）使用，遵守以下条件。\n" +
                               "2.软件使用\n" +
                               "  2.1 用户被授权在单个计算机或设备上安装和使用本软件的副本，只能用于用户个人或用户所在机构的内部用途。\n" +
                               "  2.2 用户不得复制、分发、出租、租赁、出售、转让、分发或以其他方式转让本软件或其任何部分，或为其他人提供对本软件的访问。\n" +
                               "  2.3 用户不得对本软件进行反向工程、反汇编、反编译、翻译、修改或创建派生作品。\n" +
                               "  2.4 用户不得删除或修改软件上的任何版权、商标或其他所有权声明。\n" +
                               "3.知识产权\n" +
                               "  3.1 本软件包含许可方及其许可方所指定的第三方的知识产权。本软件的所有权和知识产权仍归许可方及其许可方所指定的第三方所有。\n" +
                               "4.保修与免责声明\n" +
                               "  4.1 本软件按\"原样\"提供，没有任何明示或暗示的保证，包括但不限于对适销性、特定用途适用性、不侵权和可用性的暗示保证。用户自行承担使用本软件的风险。\n" +
                               "  4.2 许可方不对因使用或无法使用本软件而导致的任何损失或损害承担责任，包括但不限于直接、间接、特殊、附带或后果性损失。\n" +
                               "5.更新与支持\n" +
                               "  5.1 许可方可能不定期提供软件的更新、修补程序或支持服务，但无义务这样做。\n" +
                               "6.终止\n" +
                               "  6.1 如果用户违反本协议的任何条款，许可方有权立即终止本协议，用户应立即停止使用本软件并删除其所有副本。\n" +
                               "7.适用法律\n" +
                               "  7.1 本协议受到中华人民共和国法律的管辖，不考虑其冲突法规定。\n" +
                               "8.协议变更\n" +
                               "  8.1 许可方保留根据需要随时更改本协议的权利。用户应定期查看本协议以了解任何更改。继续使用本软件将被视为接受任何更改。\n" +
                               "9.联系信息\n" +
                               "  9.1 有关本许可协议的任何问题或意见，请联系：\n" +
                               "  邮箱：whuliuxiaoyong@163.com";
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void Bt_Minimum_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Bt_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
