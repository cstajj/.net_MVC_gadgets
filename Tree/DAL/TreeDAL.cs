
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public class TreeDAL
    {
        public DataTable GetTree()
        {
            string sql = "select * from Tree";
            // string sql = @"INSERT INTO Tree ([CityName]
            //,[ParentID]
            //,[State]
            //,[Url]) VALUES('s',0,'0','url')";
            SqlParameter[] pars = { };
            //int i = (int)SqlHelper.ExecuteScalare(sql, CommandType.Text, null);
            //return null;
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, null);
            return da;
        }

        private void LoadEntity(DataRow row, JsonTree jsontree)
        {
            Dictionary<string, string> test = new Dictionary<string, string>();
            jsontree.id = Convert.ToInt32(row["ID"]);
            jsontree.text = row["CityName"] != DBNull.Value ? row["CityName"].ToString() : string.Empty;
            //jsontree.score = Convert.ToInt32(row["Score"]);
            jsontree.score = row["Score"] != DBNull.Value ? Convert.ToInt32(row["Score"]) : 0;
            jsontree.state = "open";
            jsontree.attributes = test;
            jsontree.children = "null";
        }

        public JsonTree GetModel(int id)
        {
            string sql = "select * from Tree where id=@id";
            SqlParameter[] pars = {
                                 new SqlParameter("@id",SqlDbType.Int)
                                 };
            pars[0].Value = id;
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
            JsonTree jsontree = null;
            if (da.Rows.Count > 0)
            {
                jsontree = new JsonTree();
                LoadEntity(da.Rows[0], jsontree);
            }
            return jsontree;
        }

        public int Update(JsonTree jsontree)
        {
            string sql = "update Tree set CityName=@text,Score=@score where id=@id";
            SqlParameter[] pars = {
                                  new SqlParameter("@text",SqlDbType.NVarChar,50),
                                      new SqlParameter("@id",SqlDbType.Int),
                                      new SqlParameter("@score",SqlDbType.Int)
                                 };
            pars[0].Value = jsontree.text;
            pars[1].Value = jsontree.id;
            pars[2].Value= jsontree.score;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
        }

        public int Add(JsonTree jsontree)
        {
            string sql = "insert into Tree(CityName,ParentID,Score)VALUES (@text,@id,@score)";
            SqlParameter[] pars = {
                                  new SqlParameter("@text",SqlDbType.NVarChar,50),
                                      new SqlParameter("@id",SqlDbType.Int),
                                      new SqlParameter("@score",SqlDbType.Int)
                                 };
            pars[0].Value = jsontree.text;
            pars[1].Value = jsontree.id;
            pars[2].Value= jsontree.score;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
        }

        List<int> delectid = new List<int>();

        public int Delete(int id)
        {
            delectid.Add(id);
            List<int> list = new List<int>();
            string sql = "select id from Tree where ParentID=@id";
            SqlParameter[] pars = {
                                 new SqlParameter("@id",SqlDbType.Int)
                                 };
            pars[0].Value = id;
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
            for (int i = 0; i < da.Rows.Count; i++)
            {
                list.Add(int.Parse(da.Rows[i]["ID"].ToString()));
                delectid.Add(int.Parse(da.Rows[i]["ID"].ToString()));
            }
            List<int> delect = SelectChild(list);

            while (delect.Count>0) {
                delect = SelectChild(delect);
            }

            foreach (var item in delectid) {
                string dele = "DELETE FROM Tree WHERE ID = @id";
                SqlParameter[] parsdele = {
                                      new SqlParameter("@id",SqlDbType.Int)
                                 };
                parsdele[0].Value = item;
                SqlHelper.ExecuteNonquery(dele, CommandType.Text, parsdele);
            }
            return 1;
        }

        public List<int> SelectChild(List<int> child) {
            List<int> list = new List<int>();
            foreach (var item in child) {
                string sql = "select id from Tree where ParentID=@id";
                SqlParameter[] pars = {
                                 new SqlParameter("@id",SqlDbType.Int)
                                 };
                pars[0].Value = item;
                DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
                for (int i = 0; i < da.Rows.Count; i++)
                {
                    list.Add(int.Parse(da.Rows[i]["ID"].ToString()));
                    delectid.Add(int.Parse(da.Rows[i]["ID"].ToString()));
                }  
            } 
            return list;
        }

        public int Father(int id) {
            string sql = "insert into Tree(CityName,ParentID,Score)VALUES (@text,@id,@score)";
            SqlParameter[] pars = {
                                  new SqlParameter("@text",SqlDbType.NVarChar,50),
                                      new SqlParameter("@id",SqlDbType.Int),
                                      new SqlParameter("@score",SqlDbType.Int)
                                 };
            pars[0].Value = "祖节点";
            pars[1].Value = id;
            pars[2].Value = 0;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
        }


    }
}
