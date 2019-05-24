using System;
using System.Collections.Generic;
using System.IO;
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
        private string fullPath;

        public MainWindow(OperatorTypeEnum pRINT, string v)
        {
            this.pRINT = pRINT;
            this.v = v;
            InitializeComponent();
        }

        private void printBtn_Click(object sender, RoutedEventArgs e)
        {
            print(true, false);

            //backup
            //PrintDialog dlg = new PrintDialog();
            ////从本地计算机中获取所有打印机对象(PrintQueue)
            //var printers = new LocalPrintServer().GetPrintQueues();
            ////选择一个打印机
            ////var selectedPrinter = printers.FirstOrDefault(p => p.Name == "Microsoft XPS Document Writer");
            //var selectedPrinter = printers.FirstOrDefault();
            //if (selectedPrinter == null)
            //{
            //    MessageBox.Show("没有找到默认打印机");
            //    return;
            //}
            ////设置打印机
            //dlg.PrintQueue = selectedPrinter;
            //dlg.PrintVisual(grid, "print test");

            ////if (dlg.ShowDialog()==true)
            ////{
            ////    dlg.PrintVisual(grid, "print test");
            ////}
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
            //MessageBox.Show("load finish");
            var path = Directory.GetCurrentDirectory();
            fullPath = System.IO.Path.Combine(path, "tmp");
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
            var r = new Random();
            var first = r.NextDouble();
            var rValue = first * 32767;
            var second = r.NextDouble();
            rValue *= second;

            var fileneme = DateTime.Now.ToString("yyyyMMddHHmmss")+ (int)rValue+".html";
            fullPath = fullPath + "/" + fileneme;
            File.WriteAllText(fullPath, v);

            broswer.Source = new Uri(fullPath);

        }
        public void print()
        {
            switch (pRINT)
            {
                case OperatorTypeEnum.PRINT:
                    this.Hide();
                    print(false, false);
                    this.Close();
                    break;
                case OperatorTypeEnum.PREVIEW:

                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="preview">是否预览</param>
        /// <param name="needChoosePrinter">是否需要选择打印机</param>
        private void print(bool preview,bool needChoosePrinter)
        {
            PrintDialog dlg = new PrintDialog();
            //从本地计算机中获取所有打印机对象(PrintQueue)
            var printers = new LocalPrintServer().GetPrintQueues();

            //不预览
            if (!preview)
            {
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
                dlg.PrintVisual(grid, "print");
            }
            else
            {
                if(needChoosePrinter)
                {
                    if (dlg.ShowDialog() == true)
                    {
                        dlg.PrintVisual(grid, "print");
                    }
                }
                //预览后直接打印不选择打印机
                else
                {
                    dlg.PrintVisual(grid, "print");
                }
            }
        }

        /// <summary>你也要
        /// 选择打印机打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChoosePrinter_Click(object sender, RoutedEventArgs e)
        {
            print(true, true);
        }
    }
}
