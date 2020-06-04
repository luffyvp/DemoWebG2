using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tavi.NetMvc.Models;
using Tavi.NetMvc.Helper;
using PagedList;
using Tavi.NetMvc.Service;
namespace Tavi.NetMvc.Controllers
{
    public class StudentController : Controller
    {
        #region danh sach sinh vien
        [HttpGet]
        public ActionResult Index(string StudentCode, string FullName, int? DepartmentID, int? PageCurrent)
        {
            StudentService studentService = new StudentService();
            int pageNumber = PageCurrent ?? 1;
            IPagedList<Student> students = studentService.GetStudent(StudentCode
                , FullName
                , DepartmentID
                , pageNumber
                , 10);
            ViewBag.StudentCode = StudentCode;
            ViewBag.FullName = FullName;
            ViewBag.PageCurrent = PageCurrent;
            return View(students);
        }
        [HttpPost]
        public ActionResult Index(string StudentCode, string FullName, int? DepartmentID)
        {

            StudentService studentService = new StudentService();
            int pageNumber = 1;
            IPagedList<Student> students = studentService.GetStudent(StudentCode
                , FullName
                , DepartmentID
                , pageNumber
                , 10);
            ViewBag.StudentCode = StudentCode;
            ViewBag.FullName = FullName;
            ViewBag.PageCurrent = pageNumber;
            return View(students);
        }
        #endregion
        #region them moi sinh vien
        [HttpGet]
        public ActionResult Add(int? Id)
        {
            StudentService studentService = new StudentService();
            Student student = studentService.FindByKey(Id);

            
            return View(student);
        }
    
        public PartialViewResult ListDepartment(int? DepartmentID)
        {
            GetDepartmentService getdepartmentservice = new GetDepartmentService();
            ViewBag.ListDepartmentId = new SelectList(getdepartmentservice.GetDepartments(), "DepartmentID", "DepartmentName", DepartmentID);
            return PartialView("_Department");
        }
        public PartialViewResult LisClassRoom(int? DepartmentID,int? ClassRoomID)
        {
            GetClassRoomService getclassroomservice = new GetClassRoomService();
            ViewBag.ListLisClassRoomId = new SelectList(getclassroomservice.GetClassRooms(DepartmentID), "ClassRoomID", "ClassName", ClassRoomID);
            return PartialView("_ClassRoom");
        }
        [HttpPost]
        public ActionResult Add(int? Id
            , string StudentCode
            , string FullName
            , string Birthday
            , string Address
            , string Phone
            , string Email
            , string ListDepartmentId
            , string ListLisClassRoomId
            , string Description
            , bool Status)
        {
            StudentService studentService = new StudentService();
            Student student = studentService.FindByKey(Id);
            student.StudentCode = StudentCode;
            student.FullName = FullName;
            if (!string.IsNullOrEmpty(Birthday))
                student.Birthday = ConvertEx.ToDate(Birthday);
            student.Address = Address;
            student.Phone = Phone;
            student.Email = Email;
            if (!string.IsNullOrEmpty(ListDepartmentId))
                student.DepartmentID = Convert.ToInt32(ListDepartmentId);
            if (!string.IsNullOrEmpty(ListLisClassRoomId))
                student.ClassRoomID = Convert.ToInt32(ListLisClassRoomId);
            student.Description = Description;
            student.Status = Status;
            student.IsDelete = false;
            if (Id.HasValue)
            {
                studentService.Update(student);
                TempData["StatusMessage"] = "Cập Nhật Sinh Viên Thành Công";
            }
            else
            {
                studentService.Insert(student);
                TempData["StatusMessage"] = "Thêm Mới Sinh Viên Thành Công";
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region xoa sinh vien
        [HttpPost]
        public ActionResult Delete(int [] cbxItem)
        {
            if(cbxItem.Count()>0)
            {
                foreach(int item in cbxItem)
                {
                    StudentService studentService = new StudentService();
                    studentService.Delete(item);
                }
            }
            TempData["StatusMessage"] = "Xóa Sinh Viên Thành Công";
            return RedirectToAction("Index");
        }
        #endregion
    }
}
