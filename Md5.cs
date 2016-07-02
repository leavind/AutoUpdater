using System.Text;
using System.Security.Cryptography;
using System.IO;

/// <summary>
/// 
/// </summary>
public static class Md5
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string GetMD5String(string str)
    {
        MD5 md = MD5.Create();
        byte[] b = Encoding.UTF8.GetBytes(str + "RaywindStudio");
        byte[] md5b = md.ComputeHash(b);
        md.Clear();
        return md5(md5b);
    }

    public static string GetFileMD5(string file)
    {
        MD5 md = MD5.Create();
        FileStream fs = new FileStream(file, FileMode.Open);
        byte[] md5b = md.ComputeHash(fs);
        fs.Close();
        md.Clear();
        return md5(md5b);
    }

    private static string md5(byte[] mdHash)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in mdHash)
        {
            sb.Append(item.ToString("X2"));
        }
        return sb.ToString();
    }
}