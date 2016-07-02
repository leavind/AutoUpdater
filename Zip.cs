using System.IO;
using System.IO.Compression;

/// <summary>
/// 
/// </summary>
public class ZipHelper
{
    public static void Compress2File(string sourceFile, string destinationFile)
    {
        if (!File.Exists(sourceFile))
            throw new FileNotFoundException();
        using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            using (FileStream destinationStream = new FileStream(destinationFile, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (GZipStream compressStream = new GZipStream(destinationStream, CompressionMode.Compress))
                {
                    byte[] buffer = new byte[1024 * 64];
                    int checkCount = 0;
                    while ((checkCount = sourceStream.Read(buffer, 0, buffer.Length)) >= buffer.Length)
                    {
                        compressStream.Write(buffer, 0, buffer.Length);
                    }
                    compressStream.Write(buffer, 0, checkCount);
                }
            }
        }
    }

    public static void DeCompress2File(string sourceFile, string destinationFile)
    {
        if (!File.Exists(sourceFile))
            throw new FileNotFoundException();
        using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open))
        {
            byte[] quartetBuffer = new byte[4];
            const int bufferLength = 1024 * 64;
            //压缩文件的流的最后四个字节保存的是文件未压缩前的长度信息，
            // 把该字节数组转换成int型，可获取文件长度。
            byte[] buffer = new byte[1024 * 64];
            using (GZipStream decompressedStream = new GZipStream(sourceStream, CompressionMode.Decompress, true))
            {
                using (FileStream destinationStream = new FileStream(destinationFile, FileMode.Create))
                {
                    //int total = 0;
                    int bytesRead = 0;
                    while ((bytesRead = decompressedStream.Read(buffer, 0, bufferLength)) >= bufferLength)
                    {
                        destinationStream.Write(buffer, 0, bufferLength);
                    }
                    destinationStream.Write(buffer, 0, bytesRead);
                    destinationStream.Flush();
                }
            }
        }

    }

    public static byte[] Compress2Bytes(string sourceFile)
    {
        if (!File.Exists(sourceFile))
            throw new FileNotFoundException();
        using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream compressStream = new GZipStream(ms, CompressionMode.Compress))
                {
                    byte[] buffer = new byte[1024 * 64];
                    int checkCount = 0;
                    while ((checkCount = sourceStream.Read(buffer, 0, buffer.Length)) >= buffer.Length)
                    {
                        compressStream.Write(buffer, 0, buffer.Length);
                    }
                    compressStream.Write(buffer, 0, checkCount);
                }
                return ms.ToArray();
            }
        }
    }
    public static void DeCompress2File(byte[] bt, string destinationFile)
    {

        using (MemoryStream ms = new MemoryStream(bt))
        {
            byte[] quartetBuffer = new byte[4];
            const int bufferLength = 1024 * 64;
            //压缩文件的流的最后四个字节保存的是文件未压缩前的长度信息，
            // 把该字节数组转换成int型，可获取文件长度。
            byte[] buffer = new byte[1024 * 64];
            using (GZipStream decompressedStream = new GZipStream(ms, CompressionMode.Decompress, true))
            {
                using (FileStream destinationStream = new FileStream(destinationFile, FileMode.Create))
                {
                    int bytesRead = 0;
                    while ((bytesRead = decompressedStream.Read(buffer, 0, bufferLength)) >= bufferLength)
                    {
                        destinationStream.Write(buffer, 0, bufferLength);
                    }
                    destinationStream.Write(buffer, 0, bytesRead);
                    destinationStream.Flush();
                }
            }
        }

    }
}