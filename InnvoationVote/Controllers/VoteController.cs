using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.ViewModel;
using SqlHelper;
using BLL;
using Common;
using Model.DBModel;

namespace InnvoationVote.Controllers
{
    public class VoteController : Controller
    {
        private readonly tbRecordsBll _tbRecordsBll = new tbRecordsBll();
        private readonly tbProjectBll _tbProjectBll = new tbProjectBll();
        // GET: Vote
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Intro()
        {
            return View();
        }

        public ActionResult GetProjectList()
        {
            var sh = new SqlHelper<tbProjectView.tbProjectPage>("tbProject");
            var list = sh.Select().Select(x => new tbProjectView.tbProjectPage
            {
                ProjectId = x.ProjectId,
                TypeId = x.TypeId,
                TypeName = x.TypeName,
                OrganName = x.OrganName,
                ProjectName = x.ProjectName,
                ProjectImages = x.ProjectImages,
                VoteNumber = x.VoteNumber
            });
            var type1 = (from project in list
                         where project.TypeId.Equals("1")
                         select project);

            var type2 = (from project in list
                         where project.TypeId.Equals("2")
                         select project);

            var type3 = (from project in list
                         where project.TypeId.Equals("3")
                         select project);

            return Json(new
            {
                type1,
                type2,
                type3
            });
        }

        public ActionResult GetVote(string openid)
        {
            var records = _tbRecordsBll.GetModelList($" AND OpenId='{openid}' AND Convert(varchar(10),RecordDate,23)='{DateTime.Now.ToString("yyyy-MM-dd")}'");
            return Json(new
            {
                data = records.Select(x => new
                {
                    x.OpenId,
                    x.Prodjects
                })
            });
        }
        [HttpPost]
        public ActionResult SaveRecord(tbHavote thavote)
        {
            var recordsList = _tbRecordsBll.GetModelList($" AND OpenId='{thavote.openid}' AND Convert(varchar(10),RecordDate,23)='{DateTime.Now.ToString("yyyy-MM-dd")}'");
            ResStatue code;
            if (recordsList.Count > 0) //已经存在该记录，执行修改
            {
                tbRecords records = recordsList[0];
                records.Prodjects = thavote.havote;
                code = _tbRecordsBll.Update(records) ? ResStatue.Yes : ResStatue.Warn;

            }
            else //不存在该记录，需要添加
            {
                tbRecords records = new tbRecords();
                records.OpenId = thavote.openid;
                records.Prodjects = thavote.havote;
                records.RecordDate = DateTime.Now;
                code = _tbRecordsBll.Add(records) > 0 ? ResStatue.Yes : ResStatue.Warn;
            }
            var project = _tbProjectBll.GetModel(int.Parse(thavote.projectid));
            project.VoteNumber++;
            Dictionary<tbProjectEnum, object> up = new Dictionary<tbProjectEnum, object>();
            up.Add(tbProjectEnum.VoteNumber, project.VoteNumber);
            _tbProjectBll.Update(up, " and ProjectId=" + project.ProjectId);
            return Json(new
            {
                ResStatue = code,
                data = code == ResStatue.Yes ? "投票成功" : "今日已投票，请明日再来！"
            });
        }

        public ActionResult GetProject(string pid)
        {
            var sh = new SqlHelper<tbProject>("tbProject");
            sh.AddWhere("ProjectId", int.Parse(pid));
            return Json(new
            {
                data = sh.Select().Select(x => new tbProject
                {
                    ProjectId = x.ProjectId,
                    TypeId = x.TypeId,
                    TypeName = x.TypeName,
                    OrganName =  x.OrganName,
                    ProjectName = x.ProjectName,
                    ProjectIntro = x.ProjectIntro,
                    ProjectImages = x.ProjectImages

                })
            });
        }

    }
}