using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;

namespace WebApplication3
{
    /// <summary>
    /// Summary description for DeletePoint
    /// </summary>
    public class DeletePoint : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string result = "";
            result = DeletePts(context, result);
            context.Response.Write(result);
        }

        virtual protected string DeletePts(HttpContext context, string result)
        {
            string id = context.Request.QueryString["id"];
           
            //建立数据库连接
            NpgsqlConnection connection = new NpgsqlConnection("Server = localhost;Port = 5432;UserId = postgres;" +
            "Password = postgres;Database = postgis_llw");

            connection.Open();

            //构建SQL语句进行查询

            string sqlCommand = "Delete FROM res2_4m WHERE gid ='" + id + "'";
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