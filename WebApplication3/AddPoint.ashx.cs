using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;

namespace WebApplication3
{
    /// <summary>
    /// Summary description for AddPoint
    /// </summary>
    public class AddPoint : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string result = "";
            result = AddPts(context, result);
            context.Response.Write(result);
        }

        virtual protected string AddPts(HttpContext context, string result)
        {
            string lat = context.Request.QueryString["lat"];
            string lon = context.Request.QueryString["lon"];
            string cityname = context.Request.QueryString["cityname"];
            string citypinyin = context.Request.QueryString["citypinyin"];
            string cityclass = context.Request.QueryString["cityclass"];

            //建立数据库连接
            NpgsqlConnection connection = new NpgsqlConnection("Server = localhost;Port = 5432;UserId = postgres;" +
            "Password = postgres;Database = postgis_llw");

            connection.Open();

            //构建SQL语句进行查询

            string sqlCommand = string.Format("INSERT INTO res2_4m(geom,name,pinyin,adclass) VALUES(ST_GeomFromText('POINT({0} {1})',4326),'{2}','{3}',{4});", lat, lon,cityname,citypinyin,cityclass);
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