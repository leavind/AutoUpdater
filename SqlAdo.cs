using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

/// <summary>
/// 常用数据库/表操作类
/// </summary>
public static class SqlAdo
{
    /// <summary>
    /// 是否写调试信息；
    /// 执行成功写到C:\\DebugSQL.txt；
    /// 执行失败写到C:\\DebugTxt.txt；
    /// </summary>
    public static bool debug = false;

    /// <summary>
    /// 从MSSQL数据库读表并返回一个DataSet
    /// </summary>
    /// <param name="Querystr">SQL查询字符串</param>
    /// <param name="sqlconn">SQL连接字符串</param>
    /// <returns>DataSet</returns>
    public static DataSet GetDataSet(string Querystr, SqlConnection sqlconn)
    {
        DataSet ds = new DataSet("SqlDataset");
        SqlDataAdapter da = new SqlDataAdapter(Querystr, sqlconn);
        try
        {
            da.Fill(ds);
            debugSQL(Querystr);
            return ds;
        }
        catch (Exception ex)
        {
            debugTxt(Querystr, ex);
            throw new Exception("GetDataSet:\n" + ex.Message);
        }
    }

    /// <summary>
    /// 从MSSQL数据库读表并返回一个DataTable
    /// </summary>
    /// <param name="Querystr">SQL查询字符串</param>
    /// <param name="sqlconn">SQL连接字符串</param>
    /// <returns>DataTable</returns>
    public static DataTable GetDataTable(string Querystr, SqlConnection sqlconn)
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(Querystr, sqlconn);
        try
        {
            da.Fill(dt);
            debugSQL(Querystr);
            return dt;
        }
        catch (Exception ex)
        {
            debugTxt(Querystr, ex);
            throw new Exception("GetDataTable:\n" + ex.Message);
        }
    }

    /// <summary>
    /// 执行一个SQL语句并返回结果集的第一列，第一行，忽略其他行列
    /// </summary>
    /// <param name="Querystr">SQL查询字符串</param>
    /// <param name="sqlconn">SQL连接字符串</param>
    /// <returns>object</returns>
    public static object ExecuteScalar(string Querystr, SqlConnection sqlconn)
    {
        SqlCommand cmd = new SqlCommand(Querystr, sqlconn);
        try
        {
            if (sqlconn.State != ConnectionState.Open)
                sqlconn.Open();
            debugSQL(Querystr);
            return cmd.ExecuteScalar();
        }
        catch (Exception ex)
        {
            debugTxt(Querystr, ex);
            throw new Exception("ExecuteScalar:\n" + ex.Message);
        }
    }

    /// <summary>
    /// 执行一个SQL语句并返回是否执行成功。
    /// </summary>
    /// <param name="Querystr">SQL查询字符串</param>
    /// <param name="sqlconn">SQL连接字符串</param>
    /// <returns>boolean</returns>
    public static bool ExecuteNonQuery(string Querystr, SqlConnection sqlconn)
    {
        Querystr = " Set NoCount ON " + Querystr + " Set NoCount OFF ";
        SqlCommand cmd = new SqlCommand(Querystr, sqlconn);
        try
        {
            if (sqlconn.State != ConnectionState.Open)
                sqlconn.Open();
            cmd.ExecuteNonQuery();
            debugSQL(Querystr);
            return true;
        }
        catch (Exception ex)
        {
            debugTxt(Querystr, ex);
            throw new Exception("ExecuteNonQuery:\n" + ex.Message);
        }
    }

    /// <summary>
    /// 执行一个SQL语句并返回SqlDataReader：以流的方式读取表
    /// </summary>
    /// <param name="Querystr">SQL查询字符串</param>
    /// <param name="sqlconn">SQL连接字符串</param>
    /// <returns>SqlDataReader</returns>
    public static SqlDataReader ExecuteReader(string Querystr, SqlConnection sqlconn)
    {
        SqlCommand cmd = new SqlCommand(Querystr, sqlconn);
        try
        {
            if (sqlconn.State != ConnectionState.Open)
                sqlconn.Open();
            debugSQL(Querystr);
            return cmd.ExecuteReader();
        }
        catch (Exception ex)
        {
            debugTxt(Querystr, ex);
            throw new Exception("SqlDataReader:\n" + ex.Message);
        }
    }

    private static void debugSQL(string sql)
    {
        if (debug)
        {
            StreamWriter sw = new StreamWriter("C:\\DebugSQL.txt", true);
            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\t" + sql);
            sw.AutoFlush = true;
            sw.Close();
            sw.Dispose();
        }
    }

    private static void debugTxt(string sql, Exception ee)
    {
        if (debug)
        {
            StreamWriter sw = new StreamWriter("C:\\DebugTxt.txt", true);
            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\t" + sql + "\n" + ee.ToString());
            sw.AutoFlush = true;
            sw.Close();
            sw.Dispose();
        }
    }

    /// <summary>
    /// 从DataRow集合中排除重复项，并返回一个DataTable
    /// </summary>
    /// <param name="drs">DataRow集合</param>
    /// <param name="ColumnName">筛选的条件列名</param>
    /// <param name="DataTableschemaClone">要将结果装入的空表的架构</param>
    /// <returns>DataTable</returns>
    public static DataTable DistInctTable(DataRow[] drs, string ColumnName, DataTable DataTableschemaClone)
    {
        try
        {
            DataTable dts = DataTableschemaClone.Clone();
            foreach (DataRow dr in drs)
                dts.ImportRow(dr);
            for (int i = dts.Rows.Count - 1; i >= 0; i--)
                if (dts.Select(ColumnName + "='" + dts.Rows[i][ColumnName].ToString() + "'").Length > 1)
                    dts.Rows.RemoveAt(i);
            dts.AcceptChanges();
            return dts;
        }
        catch (Exception ex)
        {
            throw new Exception("DistInctTable(From DataRow):\n" + ex.Message);
        }
    }

    /// <summary>
    /// 从DataTable中排除重复行，并返回一个DataTable
    /// </summary>
    /// <param name="dt">源DataTable</param>
    /// <param name="ColumnName">筛选的条件列名</param>
    /// <returns>DataTable</returns>
    public static DataTable DistInctTable(DataTable dt, string ColumnName)
    {
        try
        {
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
                if (dt.Select(ColumnName + "='" + dt.Rows[i][ColumnName].ToString() + "'").Length > 1)
                    dt.Rows.RemoveAt(i);
            dt.AcceptChanges();
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception("DistInctTable(From DataTable):\n" + ex.Message);
        }
    }
}
