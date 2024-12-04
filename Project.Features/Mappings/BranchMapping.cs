using AutoMapper;
using Project.Core.Security;
using Project.Domain.Entities;
using Project.Features.Models.ClinicBranchModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Features.Mappings
{
    public class BranchMapping : Profile
    {
        public BranchMapping()
        {
            CreateMap<EditClinicBranchModel, branches>(MemberList.None)
            .ForMember(s => s.logo, s => s.Ignore())
            .ForMember(s => s.image, s => s.Ignore());
        }
    }

}
