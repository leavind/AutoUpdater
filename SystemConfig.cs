using System;

public class SystemConfig
{
    private static bool initsql = false;
    private static void initSql()
    {
        if (!initsql)
        {
            SqlAdo.ExecuteNonQuery(@"IF not EXISTS (SELECT * FROM dbo.SysObjects 
                                    WHERE ID = object_id(N'[SystemConfig]') 
                                    AND ((OBJECTPROPERTY(ID,'IsTable') = 1) or (OBJECTPROPERTY(ID,'IsView') = 1))) ) 
                                    begin
                                            CREATE TABLE [dbo].[SystemConfig](
	                                            [ID] [int] IDENTITY(1,1) NOT NULL,
	                                            [String] [nvarchar](50) NOT NULL,
	                                            [Value] [nvarchar](2000) NULL,
                                             CONSTRAINT [PK_SystemConfig] PRIMARY KEY CLUSTERED 
                                            (
	                                            [ID] ASC
                                            )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, 
                                            IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
                                             ON [PRIMARY]
                                            ) ON [PRIMARY]
                                    end", APP.sqlconn);
            initsql = true;
        }
    }

    public static string Get(string configString, string defaultValue)
    {
        initSql();
        object obj = SqlAdo.ExecuteScalar("select Value From SystemConfig Where String='"
            + configString + "'", APP.sqlconn);
        if (obj == null)
        {
            SqlAdo.ExecuteNonQuery("Insert into SystemConfig(String,Value) Values('"
            + configString + "','" + defaultValue + "')", APP.sqlconn);
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
        object obj = SqlAdo.ExecuteScalar("select Value From SystemConfig Where String='"
            + configString + "'", APP.sqlconn);
        if (obj == null)
        {
            SqlAdo.ExecuteNonQuery("Insert into SystemConfig(String,Value) Values('"
            + configString + "','" + Value + "')", APP.sqlconn);
        }
        else
        {
            SqlAdo.ExecuteNonQuery("Update SystemConfig set Value='" + Value
                + "' Where String='" + configString + "'", APP.sqlconn);
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

