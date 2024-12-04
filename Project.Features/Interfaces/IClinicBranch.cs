using Project.Domain.Entities;
using Project.Features.Models.ClinicBranchModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Features.Interfaces
{
    public interface IClinicBranch
    {
        branches getClinicBranch(int id);
        IEnumerable<branches> getClinicBranchAll();
        IEnumerable<branches> getClinicBranchStatus(string status);
        branches editClinicBranch(int id, EditClinicBranchModel value);
        branches editStatusClinicBranch(int id, EditStatusClinicBranchModel value);

    }
}
