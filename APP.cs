using System;
using System.Data.SqlClient;
using System.Windows.Forms;

static class APP
{
    public static SqlConnection sqlconn;
    public static string UserName = "";
    public static int ProcId = 0;
    public static string MachineID;
    public static bool autoUpd = false;

    /// <summary>
    /// 应用程序的主入口点。
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
        if (args.Length >= 4)
        {
            UserName = args[0];
            sqlconn = new SqlConnection(Base64.GetDecodeText(args[1]));
            ProcId = int.Parse(args[2]);
            MachineID = args[3];
            if (args.Length == 5 && args[4] == "AutoUpdate")
                autoUpd = true;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AutoUpdateMaker());
        }
        else
            MessageBox.Show("启动参数缺失：\n参数0：用户名\n参数1：SQL连接字符串(Base64格式) "
                + "\n参数2：更新程序启动后关闭的进程ID\n参数3：启动更新程序的客户端MachineID\n参数4：(可选)AutoUpdate",
                Application.ProductName);
    }
}
