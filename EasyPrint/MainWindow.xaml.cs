using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasyPrint
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private OperatorTypeEnum pRINT;
        private string v;

        public MainWindow(OperatorTypeEnum pRINT, string v)
        {
            this.pRINT = pRINT;
            this.v = v;
            InitializeComponent();
        }
        
        private void printBtn_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dlg = new PrintDialog();

            //从本地计算机中获取所有打印机对象(PrintQueue)
            var printers = new LocalPrintServer().GetPrintQueues();
            //选择一个打印机
            //var selectedPrinter = printers.FirstOrDefault(p => p.Name == "Microsoft XPS Document Writer");
            var selectedPrinter = printers.FirstOrDefault();

            if (selectedPrinter == null)
            {
                MessageBox.Show("没有找到默认打印机");
                return;
            }

            //设置打印机
            dlg.PrintQueue = selectedPrinter;

            dlg.PrintVisual(grid, "print test");

            //if (dlg.ShowDialog()==true)
            //{
            //    dlg.PrintVisual(grid, "print test");
            //}
        }

        private void WebBrowser_Navigated(object sender, NavigationEventArgs e)
        {
            SuppressScriptErrors((WebBrowser)sender, true);
        }
        public void SuppressScriptErrors(WebBrowser wb, bool Hide)
        {
            FieldInfo fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fiComWebBrowser == null) return;

            object objComWebBrowser = fiComWebBrowser.GetValue(wb);
            if (objComWebBrowser == null) return;

            objComWebBrowser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, objComWebBrowser, new object[] { Hide });
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            MessageBox.Show("load finish");
        }
    }
}
