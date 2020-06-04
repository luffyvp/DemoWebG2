using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavi.NetMvc.Models;
using PagedList;
using System.Data.Entity.Migrations;

namespace Tavi.NetMvc.Service
{
  public class StudentService
    {
        /// <summary>
        /// tra ve danh sach sinh vien
        /// </summary>
        /// <param name="StudentCode">ma sinh vien</param>
        /// <param name="FullName">ten sinh vien</param>
        /// <param name="DepartmentID">id khoa</param>
        /// <param name="PageCurrent">trang hien tai</param>
        /// <param name="PageSize">so ban ghi tren trang</param>
        /// <returns></returns>
        public IPagedList<Student> GetStudent(string StudentCode,string FullName,int? DepartmentID, int PageCurrent, int PageSize)
        {
            NetMvcDb db = new NetMvcDb();
            var list = db.Students.Where(m => m.IsDelete == false).AsQueryable();
            if(!string.IsNullOrEmpty(StudentCode))
            {
                list = list.Where(m => m.StudentCode.Contains(StudentCode)).AsQueryable();
            }
            if (!string.IsNullOrEmpty(FullName))
            {
                list = list.Where(m => m.FullName.Contains(FullName)).AsQueryable();
            }
            if(DepartmentID.HasValue)
            {
                list = list.Where(m => m.DepartmentID == DepartmentID);
            }
            return list.OrderByDescending(m => m.StudentID).ToPagedList(PageCurrent, PageSize);
        }
        
        
        public Student FindByKey(int? StudentID)
        {
            NetMvcDb db = new NetMvcDb();
            Student student = new Student();
            if (StudentID.HasValue)
                student = db.Students.Find(StudentID);
            return student;

        }
        public void Insert(Student student)
        {
            NetMvcDb db = new NetMvcDb();
            db.Students.Add(student);
            db.SaveChanges();
        }
        public void Update(Student student)
        {
            NetMvcDb db = new NetMvcDb();
            db.Students.AddOrUpdate(student);
            db.SaveChanges();

        }
        public void Delete(int? StudentID)
        {
            NetMvcDb db = new NetMvcDb();
            Student student = new Student();
            if (StudentID.HasValue)
            {
                student = db.Students.Find(StudentID);
                db.Students.Remove(student);
            }
               
            db.SaveChanges();

        }
        
    }
}
