using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class EasyUIBLL
    {
        DAL.TreeDAL newTree = new DAL.TreeDAL();
        public DataTable GetTable()
        {
            DataTable da = newTree.GetTree();
            return da;
        }

        TreeDAL treedal = new TreeDAL();

        public JsonTree GetModel(int id)
        {
            return treedal.GetModel(id);
        }

        public bool Updata(JsonTree jsontree)
        {
            return treedal.Update(jsontree) > 0;
        }

        public bool Add(JsonTree jsontree) {
            return treedal.Add(jsontree) > 0;
        }
        public bool Delete(int id) {
            return treedal.Delete(id) > 0;
        }

        public bool Father(int id) {
            return treedal.Father(id) > 0;
        }
    }
}
