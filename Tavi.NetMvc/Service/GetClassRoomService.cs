using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavi.NetMvc.Models;

namespace Tavi.NetMvc.Service
{
    public class GetClassRoomService
    {
        public IEnumerable<ClassRoom> GetClassRooms(int? DepartmentID)
        {
            NetMvcDb db = new NetMvcDb();
            var list = db.ClassRooms.Where(m => m.IsDelete == false).AsQueryable();
            if (DepartmentID.HasValue)
            {
                list = list.Where(m => m.DepartmentID == DepartmentID).AsQueryable();
            }
            return list.OrderBy(m => m.SortOrder);
        }
    }
}
