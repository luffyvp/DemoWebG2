using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavi.NetMvc.Models;

namespace Tavi.NetMvc.Service
{
    class GetDepartmentService
    {
        public IEnumerable<Department> GetDepartments()
        {
            NetMvcDb db = new NetMvcDb();
            var list = db.Departments.Where(m => m.IsDelete == false).AsQueryable();
            return list.OrderByDescending(m => m.DepartmentID);
        }
    }
}
