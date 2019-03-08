using BLL;
using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace TreeView.Controllers
{
    public class TreeGoController : Controller
    {
        // GET: TreeGo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult JsonTreeTest()
        {
            EasyUIBLL bll = new EasyUIBLL();
            EasyUITree EUItree = new EasyUITree();
            DataTable dt = bll.GetTable();
            //int c = dt.Rows.Count;
            List<JsonTree> list = EUItree.initTree(dt);
            var json = JsonConvert.SerializeObject(list); //把对象集合转换成Json
            return Content(json);
        }

        EasyUIBLL easyuibll = new EasyUIBLL();

        public ActionResult GetTree()
        {
            int id = int.Parse(Request["id"]);
            JsonTree jsontree = easyuibll.GetModel(id);
            return Json(jsontree, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Updata(JsonTree jsontree)
        {
            Dictionary<string, string> test = new Dictionary<string, string>();
            jsontree.children = "";
            jsontree.attributes = test;
            jsontree.state = "open";
            if (easyuibll.Updata(jsontree))
            {
                //return Content("ok");
                //return Content("<script>alert('修改成功！')</script>");

                return Json(new { message = "yes", haserror = "dfdf" }, JsonRequestBehavior.AllowGet);
            }
            //return Content("no");
            //return Content("<script>alert('修改失败！')</script>");
            return Json(new { message = "no" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Add(JsonTree jsontree)
        {
            Dictionary<string, string> test = new Dictionary<string, string>();
            jsontree.children = "";
            jsontree.attributes = test;
            jsontree.state = "open";
            if (easyuibll.Add(jsontree))
            {
                //return Content("ok");
                //return Content("<script>alert('修改成功！')</script>");

                return Json(new { message = "yes"}, JsonRequestBehavior.AllowGet);
            }
            //return Content("no");
            //return Content("<script>alert('修改失败！')</script>");
            return Json(new { message = "no" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id) {
            if (easyuibll.Delete(id))
            {
                return Json(new { message = "yes" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { message = "no" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Father(int id)
        {
            if (easyuibll.Father(id))
            {
                return Json(new { message = "yes" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { message = "no" }, JsonRequestBehavior.AllowGet);
        }

    }
}