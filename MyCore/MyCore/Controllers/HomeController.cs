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
            foreach (var item in list)
            {
                var table = _testRepository.GetTableName(item.Name);

                var tt = new List<DataModel>();
                foreach (DataRow dr in table.Rows)
                {
                    tt.Add(new DataModel { Name = dr["Name"].ToString() });
                }

                item.TableList = tt;
            }

            return list;
        }

        public List<TableModel> Get(string tableName)
        {
            var result = _testRepository.Get(tableName);

            var list = new List<TableModel>();
            foreach (DataRow dr in result.Rows)
            {
                list.Add(new TableModel { TableName = dr["TableName"].ToString() });
                list.Add(new TableModel { FieldName = dr["FieldName"].ToString() });
                list.Add(new TableModel { Type = dr["Type"].ToString() });
                list.Add(new TableModel { Length = dr["Length"].ToString() });
                list.Add(new TableModel { DefaultValue = dr["DefaultValue"].ToString() });
                list.Add(new TableModel { FieldDescription = dr["FieldDescription"].ToString() });
            }

            return list;
        }
    }
}
