using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavi.NetMvc.Models;
namespace Tavi.NetMvc.Service
{
    public class DepartmentService
    {
        /// <summary>
        /// danh sach khoa
        /// </summary>
        /// <returns></returns>
        public IPagedList<Department> GetDepartments(int? DepartmentID, string DepartmentName, int PageCurrent, int PageSize)
        {
            NetMvcDb db = new NetMvcDb();
            var list = db.Departments.Where(m => m.IsDelete == false).AsQueryable();
            if (DepartmentID.HasValue)
            {
                list = list.Where(m => m.DepartmentID == DepartmentID);
            }
            if (!string.IsNullOrEmpty(DepartmentName))
            {
                list = list.Where(m => m.DepartmentName.Contains(DepartmentName)).AsQueryable();
            }
            return list.OrderByDescending(m => m.DepartmentID).ToPagedList(PageCurrent, PageSize);
        }
        public Department FindByKey(int? DepartmentID)
        {
            NetMvcDb db = new NetMvcDb();
            Department department = new Department();
            if (DepartmentID.HasValue)
                department = db.Departments.Find(DepartmentID);
            return department;
        }
        public void Insert(Department department)
        {
            NetMvcDb db = new NetMvcDb();
            db.Departments.Add(department);          
            db.SaveChanges();
        }
        public void Update(Department department)
        {
            NetMvcDb db = new NetMvcDb();
            db.Departments.AddOrUpdate(department);
            db.SaveChanges();
        }
        public void Delete(int? DepartmentID)
        {
            NetMvcDb db = new NetMvcDb();
            Department department = new Department();
            if (DepartmentID.HasValue)
            {
                department = db.Departments.Find(DepartmentID);
                db.Departments.Remove(department);
            }

            db.SaveChanges();
        }
    }
}
