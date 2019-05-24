using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace EasyPrint
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length != 1)
            {
                MessageBox.Show("参数数量错误");
                Shutdown();
            }
            else
            {
                var value = e.Args[0];
                var startStr = "easyprint://";
                if (value.StartsWith(startStr))
                {
                    var index = value.IndexOf(startStr);
                    value = value.Substring(index + startStr.Length);
                    //去除末尾【/】
                    value = value.Substring(0, value.Length - 1);
                }

                var data = value.Split('&');
                if (data.Length != 2)
                {
                    MessageBox.Show("参数错误");
                    Shutdown();
                }

                switch (data[0])
                {
                    //直接打印
                    case "0":
                        var window = new EasyPrint.MainWindow(OperatorTypeEnum.PRINT, data[1]);
                        window.print();
                        break;
                    //预览
                    case "1":
                        var preview = new EasyPrint.MainWindow(OperatorTypeEnum.PREVIEW, data[1]);
                        preview.ShowDialog();
                        break;
                    default:
                        break;
                }
            }
            Shutdown();
        }
    }
}
