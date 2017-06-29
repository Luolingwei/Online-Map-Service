using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;

namespace WebApplication3
{
    /// <summary>
    /// Summary description for DrawChart
    /// </summary>
    public class DrawChart : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string result = "";


            //建立数据库连接
            NpgsqlConnection connection = new NpgsqlConnection("Server = localhost;Port = 5432;UserId = postgres;" +
            "Password = postgres;Database = postgis_llw");

            connection.Open();

            //构建SQL语句进行查询
            string sqlCommand = "SELECT adclass,count(*) FROM res2_4m GROUP BY adclass";
            NpgsqlCommand command = new NpgsqlCommand(sqlCommand, connection);
            NpgsqlDataReader reader = command.ExecuteReader();

            //获取数据
            while (reader.Read())
            {
                int classId = reader.GetInt16(0);
                int count = reader.GetInt32(1);
                result += classId + ","+count+"\n";
            }



            context.Response.Write(result);

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}