using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;

namespace Reg
{
    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        public Installer()
        {
            InitializeComponent();
            this.AfterInstall += new InstallEventHandler(Installer_AfterInstall);
        }

        private void Installer_AfterInstall(object sender, InstallEventArgs e)
        {
            string path = this.Context.Parameters["targetdir"];//获取用户设定的安装目标路径, 注意，需要在Setup项目里面自定义操作的属性栏里面的CustomActionData添加上/targetdir="[TARGETDIR]\"
            List<string> cmds =
            new List<string>{
               "/c" + $"reg add \"HKCR\\EasyPrint\" /f /ve  /d \"EasyPrintProtocol\"",
               "/c" + $"reg add \"HKCR\\EasyPrint\" /f /v \"URL Protocol\"  /d \"",
               "/c" + $"reg add \"HKCR\\EasyPrint\\DefaultIcon\" /f /ve  /d \""+path+"\\EasyPrint.exe,1\"",
               "/c" + $"reg add \"HKCR\\EasyPrint\\shell\\open\\command\" /f /ve  /d \"\\\""+path+"\\EasyPrint.exe\\\"  \\\"%1\\\"\""
            };
            foreach (var command in cmds)
            {
                Process p = new Process
                {
                    StartInfo =
                    {
                        FileName = "cmd.exe",
                        Arguments = command,
                        UseShellExecute = false,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                p.Start();
                p.StandardInput.WriteLine("exit");
                p.Close();
            }
        }
    }
}
