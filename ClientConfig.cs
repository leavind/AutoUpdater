using System;

public class ClientConfig
{
    private static bool initsql = false;
    private static void initSql()
    {
        if (!initsql)
        {
            SqlAdo.ExecuteNonQuery(@"IF not EXISTS (SELECT * FROM dbo.SysObjects 
                                    WHERE ID = object_id(N'[ClientConfig]') 
                                    AND ((OBJECTPROPERTY(ID,'IsTable') = 1) or (OBJECTPROPERTY(ID,'IsView') = 1))) ) 
                                    begin
                                        CREATE TABLE [dbo].[ClientConfig](
	                                        [MachineID] [nvarchar](50) NOT NULL,
	                                        [String] [nvarchar](100) NOT NULL,
	                                        [Value] [nvarchar](2000) NOT NULL
                                        ) ON [PRIMARY]
                                    end", APP.sqlconn);
            initsql = true;
        }
    }

    public static string Get(string configString, string defaultValue)
    {
        initSql();
        object obj = SqlAdo.ExecuteScalar("select Value From ClientConfig Where String='"
            + configString + "' and MachineID='" + APP.MachineID + "'", APP.sqlconn);
        if (obj == null)
        {
            SqlAdo.ExecuteNonQuery("Insert into ClientConfig(MachineID,String,Value) Values('"
            + APP.MachineID + "','" + configString + "','" + defaultValue + "')", APP.sqlconn);
            return defaultValue;
        }
        else
        {
            return obj.ToString();
        }
    }

    public static int Get(string configString, int defaultValue)
    {
        return Convert.ToInt32(Get(configString, defaultValue.ToString()));
    }

    public static bool Get(string configString, bool defaultValue)
    {
        string ret = Get(configString, (defaultValue ? "1" : "0"));
        if (ret == "1" || ret.ToUpper() == "TRUE")
            return true;
        else
            return false;
    }

    public static void Set(string configString, string Value)
    {
        initSql();
        object obj = SqlAdo.ExecuteScalar("select Value From ClientConfig Where String='"
            + configString + "' and MachineID='" + APP.MachineID + "'", APP.sqlconn);
        if (obj == null)
        {
            SqlAdo.ExecuteNonQuery("Insert into ClientConfig(MachineID,String,Value) Values('"
            + APP.MachineID + "','" + configString + "','" + Value + "')", APP.sqlconn);
        }
        else
        {
            SqlAdo.ExecuteNonQuery("Update ClientConfig set Value='" + Value + "' Where String='"
                + configString + "' and MachineID='" + APP.MachineID + "'", APP.sqlconn);
        }
    }

    public static void Set(string configString, int Value)
    {
        Set(configString, Value.ToString());
    }

    public static void Set(string configString, bool Value)
    {
        Set(configString, (Value ? "1" : "0"));
    }
}

