using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Model.ViewModel;
using Model.DBModel;
using SqlHelper;

namespace InnvoationVote.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetProjectVotePage(tbProjectView.tbProjectWhere para, tbProjectView.tbProjectOrder order)
        {
            var orderSql = string.Empty;
            if (order.VoteNumber != -1)
            {
                orderSql += $" VoteNumber   "+(order.VoteNumber==0?"DESC":"ASC")+",";
            }
            var sh = new SqlHelper<tbProjectView.tbProjectPage>("tbProject")
            {
                PageConfig = new PageConfig
                {
                    PageSize = PageSize,
                    PageSortSql = orderSql.Trim(',').IsNullOrEmpty() ? " ProjectId ASC " : orderSql.Trim(','),
                    PageIndex = para.CurrentPage
                }
            };
            if (para.ProjectId > 0)
            {
                sh.AddWhere("ProjectId",para.ProjectId);
            }
            if (!para.ProjectName.IsNullOrEmpty())
            {
                sh.AddWhere("ProjectName", para.ProjectName);
            }
            if (para.TypeId > 0)
            {
                sh.AddWhere("TypeId",para.TypeId);
            }
            return Json(new
            {
                data = sh.Select().Select(x => new tbProjectView.tbProjectPage
                {
                    ProjectId = x.ProjectId,
                    TypeId = x.TypeId,
                    TypeName = x.TypeName,
                    OrganName = x.OrganName,
                    ProjectName = x.ProjectName,
                    VoteNumber = x.VoteNumber
                }),
                sql = sh.SqlString.ToString(),
                total = sh.Total
            });
        }
    }
}