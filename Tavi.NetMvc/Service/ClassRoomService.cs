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
   public class ClassRoomService
    {
       /// <summary>
       /// danh sach lop hoc
       /// </summary>
       /// <returns></returns>
        public IPagedList<ClassRoom> GetClassRooms(int? ClassRoomID,int? DepartmentID,string ClassName, int PageCurrent, int PageSize)
        {
            NetMvcDb db = new NetMvcDb();
            var list = db.ClassRooms.Where(m => m.IsDelete == false).AsQueryable();
            if (ClassRoomID.HasValue)
            {
                list = list.Where(m => m.ClassRoomID == ClassRoomID).AsQueryable();
            }
            if(DepartmentID.HasValue)
            {
                list = list.Where(m => m.DepartmentID == DepartmentID).AsQueryable();
            }
            if (!string.IsNullOrEmpty(ClassName))
            {
                list = list.Where(m => m.ClassName.Contains(ClassName)).AsQueryable();
            }
            return list.OrderByDescending(m => m.ClassRoomID).ToPagedList(PageCurrent, PageSize);
        }
        public ClassRoom FindByKey(int? ClassRoomID)
        {
            NetMvcDb db = new NetMvcDb();
            ClassRoom classroom = new ClassRoom();
            if (ClassRoomID.HasValue)
                classroom = db.ClassRooms.Find(ClassRoomID);
            return classroom;
        }
        public void Insert(ClassRoom classroom)
        {
            NetMvcDb db = new NetMvcDb();
            db.ClassRooms.Add(classroom);
            db.SaveChanges();
        }
        public void Update(ClassRoom classroom)
        {
            NetMvcDb db = new NetMvcDb();
            db.ClassRooms.AddOrUpdate(classroom);
            db.SaveChanges();
        }
        public void Delete(int? ClassRoomID)
        {
            NetMvcDb db = new NetMvcDb();
            ClassRoom classroom = new ClassRoom();
            if (ClassRoomID.HasValue)
            {
                classroom = db.ClassRooms.Find(ClassRoomID);
                db.ClassRooms.Remove(classroom);
            }

            db.SaveChanges();
        }

    }
}
