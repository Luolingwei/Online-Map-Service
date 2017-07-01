using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;

namespace WebApplication3
{
    /// <summary>
    /// Summary description for ModifyPoint
    /// </summary>
    public class ModifyPoint : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string result = "";
            result = ModifyPts(context, result);
            context.Response.Write(result);
        }

        virtual protected string ModifyPts(HttpContext context, string result)
        {
            string cityname = context.Request.QueryString["cityname"];
            string citypinyin = context.Request.QueryString["citypinyin"];
            string cityclass = context.Request.QueryString["cityclass"];
            string city_first = context.Request.QueryString["city_first"];

            //建立数据库连接
            NpgsqlConnection connection = new NpgsqlConnection("Server = localhost;Port = 5432;UserId = postgres;" +
            "Password = postgres;Database = postgis_llw");

            connection.Open();

            //构建SQL语句进行查询
            string sqlCommand = string.Format("UPDATE res2_4m SET name='{0}',pinyin='{1}',adclass='{2}' WHERE name='{3}';", cityname, citypinyin, cityclass,city_first);
            //delete where id=
            NpgsqlCommand command = new NpgsqlCommand(sqlCommand, connection);
            command.ExecuteNonQuery();

            return true.ToString();
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