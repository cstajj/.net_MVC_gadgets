using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class EasyUITree
    {
        public List<JsonTree> initTree(DataTable dt)
        {
            DataRow[] drList = dt.Select("parentid=0");
            List<JsonTree> rootNode = new List<JsonTree>();
            foreach (DataRow dr in drList)
            {
                JsonTree jt = new JsonTree();
                jt.id = int.Parse(dr["id"].ToString());
                jt.text = dr["cityname"].ToString();
                jt.state = dr["state"].ToString();
                jt.attributes = CreateUrl(dt, jt);
                jt.children = CreateChildTree(dt, jt);
                rootNode.Add(jt);
            }
            return rootNode;
        }

        private List<JsonTree> CreateChildTree(DataTable dt, JsonTree jt)
        {
            int keyid = jt.id;                                        //根节点ID
            List<JsonTree> nodeList = new List<JsonTree>();
            DataRow[] children = dt.Select("Parentid='" + keyid + "'");
            foreach (DataRow dr in children)
            {
                JsonTree node = new JsonTree();
                node.id = int.Parse(dr["id"].ToString());
                node.text = dr["cityname"].ToString();
                node.state = dr["state"].ToString();
                node.attributes = CreateUrl(dt, node);
                node.children = CreateChildTree(dt, node);
                nodeList.Add(node);
            }
            return nodeList;
        }


        private Dictionary<string, string> CreateUrl(DataTable dt, JsonTree jt)    //把Url属性添加到attribute中，如果需要别的属性，也可以在这里添加
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            int keyid = jt.id;
            DataRow[] urlList = dt.Select("id='" + keyid + "'");
            string url = urlList[0]["Url"].ToString();
            string score = urlList[0]["Score"].ToString();
            dic.Add("url", url);
            dic.Add("score", score);
            return dic;
        }
    }
}