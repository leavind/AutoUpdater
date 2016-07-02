using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

public partial class AutoUpdateMaker : Form
{
    public AutoUpdateMaker()
    {
        InitializeComponent();
    }

    bool columnClickValue = false;
    string starterExePath;
    DataTable dt;
    private void Form1_Load(object sender, EventArgs e)
    {
        starterExePath = Process.GetProcessById(APP.ProcId).MainModule.FileName;
        Process.GetProcessById(APP.ProcId).Kill();
        dGV1.BackgroundColor = this.BackColor;
        dGV1.AlternatingRowsDefaultCellStyle.BackColor = Color.Honeydew;
        textBox9.Text = Application.StartupPath;
        SqlAdo.ExecuteNonQuery(@"IF not EXISTS (SELECT * FROM dbo.SysObjects 
                                    WHERE ID=object_id(N'[RSAutoUpdateFile]') AND OBJECTPROPERTY(ID,'IsTable')=1) 
                                    begin
	                                   CREATE TABLE [dbo].[RSAutoUpdateFile](
	                                        [FilePath] [nvarchar](500) NOT NULL,
	                                        [FileName] [nvarchar](200) NOT NULL,
	                                        [FileContent] [varbinary](max) NOT NULL,
	                                        [MD5] [nvarchar](50) NOT NULL,
	                                        [PCName] [nvarchar](50) NOT NULL,
	                                        [UserName] [nvarchar](50) NOT NULL,
	                                        [Datetime] [datetime] NOT NULL
                                        ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
                                    end", APP.sqlconn);
        SqlAdo.ExecuteNonQuery(@"IF not EXISTS (SELECT * FROM dbo.SysObjects 
                                    WHERE ID=object_id(N'[pRSAutoUpdater]') AND OBJECTPROPERTY(ID,'IsProcedure')=1) 
                                    begin
                                           exec(' CREATE procedure [dbo].[pRSAutoUpdater]
                                            @FilePath nvarchar(500),
                                            @FileName nvarchar(200),
                                            @MD5 nvarchar(50),
                                            @PCName nvarchar(50),
                                            @UserName nvarchar(50),			
                                            @FileContent varbinary(MAX)		
                                            AS
                                            Begin tran
	                                            if EXISTS(select MD5 From RSAutoUpdateFile 
		                                            where FilePath=@FilePath and FileName=@FileName)
		                                            begin
			                                            update RSAutoUpdateFile set FileContent=@FileContent,
				                                            MD5=@MD5,PCName=@PCName,UserName=@UserName,Datetime=getdate() 
					                                            where FilePath=@FilePath and FileName=@FileName
		                                            end   
	                                            else 
		                                            begin  
			                                             insert into RSAutoUpdateFile(FilePath,FileName,
					                                            FileContent,MD5,PCName,UserName,Datetime)
			                                               values(@FilePath,@FileName,
				                                            @FileContent,@MD5,@PCName,@UserName,GETDATE())
		                                            end  
		
	                                            if(@@error<>0)
		                                            begin 
			                                            rollback
			                                            return -1           
		                                            end	
                                            commit 
                                            return 0 ')
                                    end", APP.sqlconn);
        dt = SqlAdo.GetDataTable("Select FilePath,FileName,MD5 From RSAutoUpdateFile", APP.sqlconn);
        if (APP.autoUpd)
        {
            autoUpd();
        }
    }

    private void autoUpd()
    {
        ProgressBar pb = new ProgressBar();
        pb.Maximum = dt.Rows.Count;
        pb.Step = 1;
        pb.Value = 0;
        this.Controls.Add(pb);
        pb.Dock = DockStyle.Fill;
        int OKqty = 0, NGqty = 0, TotalQty = 0;
        foreach(DataRow dr in dt.Rows)
        {
            string spath = dr["FilePath"].ToString();
            string sfile = dr["FileName"].ToString();
            string smd5 = dr["MD5"].ToString();
            string sfullName = Application.StartupPath + spath + sfile;
            if (!Directory.Exists(Application.StartupPath + spath))
                Directory.CreateDirectory(Application.StartupPath + spath);
            bool needUpd = true;
            if (File.Exists(sfullName))
            {
                string md5 = Md5.GetFileMD5(sfullName);
                if(md5==smd5)
                {
                    pb.Value += 1;
                    needUpd = false;
                }                                    
            }
            if (needUpd)
            {
                TotalQty += 1;
                if (downloadFile(sfullName, spath, sfile, smd5))
                    OKqty += 1;
                else
                    NGqty += 1;
                pb.Value += 1;
            }
        }

        pb.Value = pb.Maximum;
        int version = SystemConfig.Get("ServerVersion", 0);
        ClientConfig.Set("ClientVersion", version);
        //string msg = string.Format("共有系统文件{0}个，需要更新{1}个，\n更新成功{2}个，更新失败{3}个",
        //    dt.Rows.Count, TotalQty, OKqty, NGqty);
        //Font f = new Font(this.Font.FontFamily, 12, FontStyle.Bold); 
        ////Graphics g = this.CreateGraphics();
        ////SizeF s= g.MeasureString(msg, f);
        //////g.DrawString(msg, f, Brushes.Black, (this.Width-s.Width)/2 , (this.Height - s.Height) / 2);
        ////g.DrawString(msg, f, Brushes.Pink, 20, 20);
        new Thread(() =>
        {
            MessageBox.Show(this, string.Format(
                "共有系统文件{0}个，需要更新{1}个，\n更新成功{2}个，更新失败{3}个",
                dt.Rows.Count, TotalQty, OKqty, NGqty));
            Process.Start(starterExePath);
            this.ShowInTaskbar = false;
            this.Visible = false;
        }).Start();
    }

    public static bool downloadFile(string fullPath, string FilePath, string FileName, string md5)
    {
        try
        {
            object obj = SqlAdo.ExecuteScalar("Select FileContent From RSAutoUpdateFile Where FilePath='"
                + FilePath + "' and FileName='" + FileName + "' and MD5='" + md5 + "'", APP.sqlconn);
            if (obj != null)
            {
                ZipHelper.DeCompress2File((byte[])obj, fullPath);
            }
            return true;
        }
        catch(Exception ee)
        {
            MessageBox.Show(ee.Message);
            return false;
        }
    }

    private void button4_Click(object sender, EventArgs ee)
    {
        if (dGV1.RowCount > 0)
        {
            dGV1.ReadOnly = true;
            int qty = 0;
            foreach (DataGridViewRow dgvr in dGV1.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells[1].Value))
                {
                    string path = dgvr.Cells[0].Value.ToString();
                    string md5 = Md5.GetFileMD5(Application.StartupPath + path);
                    string file = getFileName(path);
                    path = path.Replace(file, "");
                    string filter = "FileName='" + file + "' and FilePath='" + path + "' and MD5='" + md5 + "'";
                    DataRow[] dr = dt.Select(filter);
                    if (dr.Length > 0)
                    {
                        dgvr.Cells[2].Value = "不需更新";
                    }
                    else
                    {
                        updateFile(Application.StartupPath + path + file, md5);
                        dgvr.Cells[2].Value = "已更新";
                        qty += 1;
                    }
                    dr = null;
                }                
            }
            dGV1.ReadOnly = false;
            if (qty > 0)
            {
                int version = SystemConfig.Get("ServerVersion", 0);
                SystemConfig.Set("ServerVersion", version + 1);
                ClientConfig.Set("ClientVersion", version + 1);
                MessageBox.Show("已更新!");
            }
            else
            {
                MessageBox.Show("不需更新!");
            }

        }
    }

    private static bool updateFile(string FFilePathN, string MD5)
    {
        SqlCommand comm = new SqlCommand();
        comm.Connection = APP.sqlconn;
        comm.CommandText = "pRSAutoUpdater";
        comm.CommandType = CommandType.StoredProcedure;

        SqlParameter prm1 = new SqlParameter();
        SqlParameter prm2 = new SqlParameter();
        SqlParameter prm3 = new SqlParameter();
        SqlParameter prm4 = new SqlParameter();
        SqlParameter prm5 = new SqlParameter();
        SqlParameter prm6 = new SqlParameter();

        prm1.ParameterName = "@FilePath";
        prm1.SqlDbType = SqlDbType.NVarChar;
        prm1.Direction = ParameterDirection.Input;
        prm1.Value = FFilePathN.Replace(getFileName(FFilePathN), "").Replace(Application.StartupPath, "");
        comm.Parameters.Add(prm1);

        prm2.ParameterName = "@FileName";
        prm2.SqlDbType = SqlDbType.NVarChar;
        prm2.Direction = ParameterDirection.Input;
        prm2.Value = getFileName(FFilePathN);
        comm.Parameters.Add(prm2);

        prm3.ParameterName = "@MD5";
        prm3.SqlDbType = SqlDbType.NVarChar;
        prm3.Direction = ParameterDirection.Input;
        prm3.Value = MD5;
        comm.Parameters.Add(prm3);

        prm4.ParameterName = "@FileContent";
        prm4.SqlDbType = SqlDbType.VarBinary;
        prm4.Direction = ParameterDirection.Input;
        prm4.Value = ZipHelper.Compress2Bytes(FFilePathN);
        comm.Parameters.Add(prm4);

        prm5.ParameterName = "@PCName";
        prm5.SqlDbType = SqlDbType.NVarChar;
        prm5.Direction = ParameterDirection.Input;
        prm5.Value = System.Net.Dns.GetHostName();
        comm.Parameters.Add(prm5);

        prm6.ParameterName = "@UserName";
        prm6.SqlDbType = SqlDbType.NVarChar;
        prm6.Direction = ParameterDirection.Input;
        prm6.Value = APP.UserName;
        comm.Parameters.Add(prm6);
        try
        {
            if (APP.sqlconn.State != ConnectionState.Open)
                APP.sqlconn.Open();
            comm.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show("pRSAutoUpdater:\n" + ex.Message, "系统提示",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return false;
        }
    }

    private static void writeLog(string text)
    {
        string recPath = Application.StartupPath + "\\log" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
        using (StreamWriter sw = new StreamWriter(recPath, true, System.Text.Encoding.UTF8))
        {
            sw.WriteLine(text);
            sw.Close();
        }
    }

    private void dGV1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.ColumnIndex == 1 && dGV1.RowCount > 0)
        {
            columnClickValue = !columnClickValue;
            foreach (DataGridViewRow dgvr in dGV1.Rows)
                dgvr.Cells[1].Value = columnClickValue;
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        dGV1.Rows.Clear();
        string path = textBox9.Text;
        dt = SqlAdo.GetDataTable("Select FilePath,FileName,MD5 From RSAutoUpdateFile", APP.sqlconn);
        FileInfo[] fi = new DirectoryInfo(path).GetFiles("*.*", SearchOption.AllDirectories);
        foreach (FileInfo i in fi)
        {
            if (i.FullName.ToLower() != Application.ExecutablePath.ToLower())
                dGV1.Rows.Add(i.FullName.Replace(path, ""));
        }
    }

    private static string getFileName(string filePathWithName)
    {
        int len = filePathWithName.LastIndexOf('\\');
        return filePathWithName.Substring(len + 1, filePathWithName.Length - len - 1);
    }
}
