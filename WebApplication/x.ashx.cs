using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;

namespace WebApplication3
{
    /// <summary>
    /// test 的摘要说明
    /// </summary>
    public class x : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";
            string result = "";
            result = Queryid(context, result);
            context.Response.Write(result);

            /**/


            //context.Response.Write("Hello World");
        }

        virtual protected string Queryid(HttpContext context, string result)
        {
            string cityname = context.Request.QueryString["cityname"];
            string pinyin = cityname;

            //建立数据库连接
            NpgsqlConnection connection = new NpgsqlConnection("Server = localhost;Port = 5432;UserId = postgres;" +
            "Password = postgres;Database = postgis_llw");

            connection.Open();

            //构建SQL语句进行查询
            string sqlCommand = "SELECT * FROM res2_4m WHERE pinyin ='" + pinyin+"'";
            NpgsqlCommand command = new NpgsqlCommand(sqlCommand, connection);
            NpgsqlDataReader reader = command.ExecuteReader();

            //获取数据
            while (reader.Read())
            {
                double id = reader.GetInt32(0);
                result +=id+",";
            }

            return result;

        }

        public class BPoint
        {
            double id;
            public BPoint(double id)
            {
                this.id = id;
            }
            public string GetData { get { return id.ToString() + "\n"; } }
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