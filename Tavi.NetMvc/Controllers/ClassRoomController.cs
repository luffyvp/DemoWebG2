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
    public class ClassRoomController : Controller
    {
        #region Danh Sách ClassRoom
        [HttpGet]
        public ActionResult Index(int? ClassRoomID,int? DepartmentID,string ClassName,int? PageCurrent)
        {
            ClassRoomService classRoom = new ClassRoomService();
            int Pagenumber = PageCurrent ?? 1;
            IPagedList<ClassRoom> classroom = classRoom.GetClassRooms(ClassRoomID, DepartmentID, ClassName, Pagenumber, 15);

            ViewBag.ClassRoomID = ClassRoomID;
            ViewBag.ClassName = ClassName;
            ViewBag.PageCurrent = PageCurrent;

            return View(classroom);
        }
        [HttpPost]
        public ActionResult Index(int? ClassRoomID, int? DepartmentID, string ClassName)
        {
            ClassRoomService classRoom = new ClassRoomService();
            int Pagenumber = 1;
            IPagedList<ClassRoom> classroom = classRoom.GetClassRooms(ClassRoomID, DepartmentID, ClassName, Pagenumber, 15);

            ViewBag.ClassRoomID = ClassRoomID;
            ViewBag.ClassName = ClassName;
            ViewBag.PageCurrent = Pagenumber;

            return View(classroom);
        }
        #endregion
        #region Thêm Mới Lớp Học
        [HttpGet]
        public ActionResult Add(int? Id)
        {
            ClassRoomService classroomservice = new ClassRoomService();
            ClassRoom classroom = classroomservice.FindByKey(Id);

            return View(classroom);
        }

        public PartialViewResult ListDepartment(int? DepartmentID)
        {
            GetDepartmentService getdepartmentservice = new GetDepartmentService();
            ViewBag.ListDepartmentId = new SelectList(getdepartmentservice.GetDepartments(), "DepartmentID", "DepartmentName", DepartmentID);

            return PartialView("_Department");
        }
        [HttpPost]
        public ActionResult Add(int?Id,
            string ClassName,
            string ListDepartmentId,
            string Description,
            bool Status)
        {
            ClassRoomService classroomservice = new ClassRoomService();
            ClassRoom classroom = classroomservice.FindByKey(Id);
            classroom.ClassName = ClassName;
            if (!string.IsNullOrEmpty(ListDepartmentId))
            {
                classroom.DepartmentID = Convert.ToInt32(ListDepartmentId);
            }
            classroom.Description = Description;
            classroom.Status = Status;
            classroom.IsDelete = false;
            if (Id.HasValue)
            {
                classroomservice.Update(classroom);
                TempData["StatusMessage"] = "Cập Nhật Lớp Học Thành Công";
            }
            else
            {
                classroomservice.Insert(classroom);
                TempData["StatusMessage"] = "Thêm Mới Lớp Học Thành Công";
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Xóa Lớp Học
        [HttpPost]
        public ActionResult Delete(int [] cbxItem)
        {
            if (cbxItem.Count() > 0)
            {
                foreach  (int item in cbxItem)
                {
                    ClassRoomService classroomservice = new ClassRoomService();
                    classroomservice.Delete(item);
                    TempData["StatusMessage"] = "Xóa Lớp Học Thành Công";
                }
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}
