﻿using NLog;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace DAL
{
    public class ConnectedLayer
    {
        public readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        Logger logger = LogManager.GetCurrentClassLogger();
        SqlConnectionStringBuilder sqlsb;
        public ConnectedLayer()
        {
            sqlsb = new SqlConnectionStringBuilder(connectionString);
            var oSqlCon = new SqlConnection(connectionString);
            logger.Trace(
            "Connection to" + Environment.NewLine +
                "Data Source: " + oSqlCon.DataSource + Environment.NewLine +
                "Database: " + oSqlCon.Database + Environment.NewLine +
                "State: " + oSqlCon.State +
                "User: " + sqlsb.UserID +
                "Catalog: " + sqlsb.InitialCatalog +
                "Server: " + sqlsb.DataSource
                );

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("ConnectionStr", "SomeConnectionString"));
            config.Save();
            ConnectionStringsSection section = config.GetSection("connectionStrings") as ConnectionStringsSection;

            if (section.SectionInformation.IsProtected)
            {
                section.SectionInformation.UnprotectSection();
            }
            else
            {
                section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
            }
            config.Save();
        }
    }
}
