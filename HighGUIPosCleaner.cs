//
//  highgui_pos_cleaner.exe - OpenCVのhighguiで表示したウインドウの位置をクリアするプログラム
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace highgui_pos_cleaner
{
    class HighGUIPosCleaner
    {
        static void Main(string[] args)
        {
            Console.Write("highguiのウインドウ位置をクリアしますか？(y/N) : ");
            string str = Console.ReadLine();
            if (str != "y")
            {
                Console.WriteLine("処理を中断しました");
                return;
            }

            // HKEY_CURRENT_USER\Software\OpenCV\HighGUI\Windows 以下を全部消す
            RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"Software\OpenCV\HighGUI", true);
            reg.DeleteSubKeyTree("Windows");
            reg.Close();

            Console.WriteLine("ウインドウ位置をクリアしました");
        }
    }
}
