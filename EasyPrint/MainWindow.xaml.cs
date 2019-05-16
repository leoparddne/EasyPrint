using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
        public MainWindow()
        {
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
                MessageBox.Show("没有找到Microsoft XPS Document Writer打印机");
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
    }
}
