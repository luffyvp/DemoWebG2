using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tavi.NetMvc.Models;
using Tavi.NetMvc.Service;

namespace Tavi.NetMvc.Controllers
{
    public class DepartmentController : Controller
    {
        #region Danh Sách Khoa Ngành
        [HttpGet]
        public ActionResult Index(int? DepartmentID,String DepartmentName,int? PageCurrent)
        {
            DepartmentService dms = new DepartmentService();
            int Pagenumber = PageCurrent ?? 1;
            IPagedList<Department> department = dms.GetDepartments(DepartmentID, DepartmentName, Pagenumber, 10);

            ViewBag.DepartmentID = DepartmentID;
            ViewBag.DepartmentName = DepartmentName;
            ViewBag.PageCurrent = PageCurrent;
            return View(department);
        }
        [HttpPost]
        public ActionResult Index(int? DepartmentID, String DepartmentName)
        {
            DepartmentService dms = new DepartmentService();
            int PageNumber = 1;
            IPagedList<Department> department = dms.GetDepartments(DepartmentID, DepartmentName, PageNumber, 10);

            ViewBag.DepartmentID = DepartmentID;
            ViewBag.DepartmentName = DepartmentName;
            ViewBag.PageCurrent = PageNumber;
            return View(department);
        }
        #endregion
        #region Thêm Mới Khoa Ngành
        [HttpGet]
        public ActionResult Add(int? Id)
        {
            DepartmentService dms = new DepartmentService();
            Department department = dms.FindByKey(Id);

            return View(department);
        }
        
        [HttpPost]
        public ActionResult Add(int?Id,
            string DepartmentName,
            string Description,
            bool Status)
        {
            DepartmentService dms = new DepartmentService();
            Department department = dms.FindByKey(Id);
            department.DepartmentName = DepartmentName;
            department.Description = Description;
            department.Status = Status;
            department.IsDelete = false;
            if (Id.HasValue)
            {
                dms.Update(department);
                TempData["StatusMessage"] = "Cập Nhật Khoa Ngành Thành Công";
            }
            else
            {
                dms.Insert(department);
                TempData["StatusMessage"] = "Thêm Mới Khoa Ngành Thành Công";
            }
            return RedirectToAction("Index");
        }
      
        #endregion
        #region Xóa Khoa Ngành
        [HttpPost]
        public ActionResult Delete(int[] cbxItem)
        {
            if (cbxItem.Count()>0)
            {
                foreach (int item in cbxItem)
                {
                    DepartmentService dms = new DepartmentService();
                    dms.Delete(item);
                }
            }
            TempData["StatusMessage"] = "Xóa Khoa Ngành Thành Công";
            return RedirectToAction("Index");
        }
        #endregion
    }
}
