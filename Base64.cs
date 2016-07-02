using System.Text;
using System.IO;

    /// <summary>
    /// 
    /// </summary>
    public static class Base64
    {
        /// 
        /// 将一串字符串转换为其 Base64 编码形式, 并返回
        /// 
        /// 要转换的字符串
        /// 
        /// 
        public static string GetEncodeText(string text)
        {
            byte[] myBt;
            // ERROR: Not supported in C#: ReDimStatement

            myBt = Encoding.Unicode.GetBytes(text);
            return System.Convert.ToBase64String(myBt);
        }

        /// 
        /// 将 Base 编码形式还原为原始字符串, 并返回
        /// 
        /// Base 64 编码字符串
        /// 
        /// 
        public static string GetDecodeText(string text)
        {
            byte[] myBt;
            myBt = System.Convert.FromBase64String(text);
            return Encoding.Unicode.GetString(myBt);
        }

        /// 
        /// 将文件进行 Base64 编码
        /// 
        /// 源文件,即要编码的文件的位置
        /// 目标文件, 编码后的文件要保存的位置, 如果目标文件已经存在, 将会被覆盖
        /// 
        public static void EncodeFile(string srcFile, string desFile)
        {
            byte[] srcBt = new byte[] { };
            FileStream srcFS = new FileStream(srcFile, FileMode.Open);
            // ERROR: Not supported in C#: ReDimStatement


            srcFS.Read(srcBt, 0, srcFile.Length);
            srcFS.Close();

            string destStr = EncodeToByte(srcBt);

            if (File.Exists(desFile))
            {
                File.Delete(desFile);
            }

            using (StreamWriter desFS = new StreamWriter(srcFile, false))
            {
                desFS.Write(destStr);
            }
        }

        /// 
        /// 将文件进行 Base64 解码
        /// 
        /// 源文件,即要编码的文件的位置
        /// 目标文件, 编码后的文件要保存的位置, 如果目标文件已经存在, 将会被覆盖
        /// 
        public static void DecodeFile(string srcFile, string desFile)
        {
            //读取源内容
            string srcContent;
            using (StreamReader srcSR = new StreamReader(srcFile))
            {
                srcContent = srcSR.ReadToEnd();
            }

            byte[] myBt = DecodeToByte(srcContent);

            if (File.Exists(desFile))
            {
                File.Delete(desFile);
            }

            //将源内容写入文件
            using (FileStream fs = new FileStream(desFile, FileMode.CreateNew))
            {
                fs.Write(myBt, 0, myBt.Length);
            }
        }

        private static string EncodeToByte(byte[] bt)
        {
            return System.Convert.ToBase64String(bt);
        }

        private static byte[] DecodeToByte(string content)
        {
            byte[] bt = System.Convert.FromBase64String(content);
            return bt;
        }
    }
