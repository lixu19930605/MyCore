using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCore.Models;
using MyCore.Repository;
using System.Data;
using MyCore.Model;

namespace MyCore.Controllers
{
    public class HomeController : Controller
    {
        public TestRepository _testRepository = new TestRepository();

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取库名+表名
        /// </summary>
        /// <returns></returns>
        public List<DataModel> GetData()
        {
            var result = _testRepository.Test();

            var list = new List<DataModel>();
            foreach (DataRow dr in result.Rows)
            {
                list.Add(new DataModel { Name = dr["name"].ToString() });
            }

            return list;
        }
    }
}
