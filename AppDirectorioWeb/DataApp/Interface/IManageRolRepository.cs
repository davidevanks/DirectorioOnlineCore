using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelApp.Models;

namespace DataApp.Interface
{
   public interface IManageRolRepository
    {
         Task<List<PlanViewModel>> GetListPlan();
    }
}
