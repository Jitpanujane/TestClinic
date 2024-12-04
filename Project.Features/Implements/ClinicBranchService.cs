using AutoMapper;
using AutoMapper.QueryableExtensions;
using Project.Core;
using Project.Core.Exceptions;
using Project.Core.Extensions;
using Project.Core.Security;
using Project.Domain;
using Project.Domain.Entities;
using Project.Domain.Helpers.Pagination;
using Project.Features.Interfaces;
using Project.Features.Models.ClinicBranchModels;
using Project.Features.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Features.Implements
{
    public class ClinicBranchService : CoreService, IClinicBranch
    {
        private readonly IMapper mapper;
        private readonly IPasswordHasher hasher;

        public ClinicBranchService(AppDbContext dbContext, IMapper mapper, IPasswordHasher hasher) : base(dbContext)
        {
            this.mapper = mapper;
            this.hasher = hasher;
        }

        private ImageReSize _ImageReSize = new ImageReSize();
        private string SaveImage(string image, string folder_name = "", string name_extension = "jpg")
        {
            string[] split_imgage = image.Split(',');

            if (!split_imgage[0].Contains("data:image"))
            {
                split_imgage[0] = _ImageReSize.ImageToBase64(image).FirstOrDefault();
            }
            string file_name = _ImageReSize.saveImage(split_imgage.LastOrDefault(), name_extension, folder_name);

            return file_name;
        }

        private class images
        {
            public string image_name { get; set; }
        }

        private List<images> SaveClinicImages(string[] images, string folder_name = "", string name_extension = "jpg")
        {
            var newImages = new List<images>();

            foreach (var image in images)
            {
                if (image != null)
                {
                    string imageName = SaveImage(image, folder_name);
                    if (!String.IsNullOrEmpty(imageName))
                    {
                        newImages.Add(new images
                        {
                            image_name = imageName,
                        });
                    }
                }
            }
            return newImages;
        }

        public branches editClinicBranch(int id, EditClinicBranchModel value)
        {
            var branch = getClinicBranch(id);
            mapper.Map(value, branch);

            if (!String.IsNullOrEmpty(value.logo))
            {
                var logo = new string[1] { value.logo };
                var im = SaveClinicImages(logo, $"logo", "jpg");

                branch.logo = im.Count != 0 ? im.FirstOrDefault().image_name : null;
            }

            if (!String.IsNullOrEmpty(value.image))
            {
                var image = new string[1] { value.image };
                var im = SaveClinicImages(image, $"branch_image", "jpg");

                branch.image = im.Count != 0 ? im.FirstOrDefault().image_name : null;
            }

            var old_bank = Db.branch_bank.Where(s => s.branch_id == branch.branch_id).ToList();
            if (value.branch_banks.Count > 0)
            {

                List<branch_bank> banks = new List<branch_bank>();
                foreach (var b in value.branch_banks)
                {
                    if (b.branch_bank_id == 0)
                    {
                        banks.Add(new branch_bank
                        {
                            bank_code = b.bank_code,
                            bank_number = b.bank_number,
                            branch_id = b.branch_id,
                        });
                    }
                    else
                    {
                        var bank_edit = old_bank.FirstOrDefault(s => s.branch_bank_id == b.branch_bank_id);
                        bank_edit.bank_code = b.bank_code;
                        bank_edit.bank_number = b.bank_number;
                    }
                }

                old_bank = old_bank.Where(o => value.branch_banks.Any(b => b.branch_bank_id == o.branch_bank_id)).ToList();

                Db.branch_bank.RemoveRange(old_bank);
                Db.branch_bank.AddRange(banks);
            }

            branch.updateAt = DateTime.UtcNow;
            Db.SaveChanges();

            return branch;
        }

        public branches editStatusClinicBranch(int id, EditStatusClinicBranchModel value)
        {
            var branch = getClinicBranch(id);

            branch.updateAt = DateTime.UtcNow;
            branch.status = value.status;
            Db.SaveChanges();

            return branch;
        }

        public IEnumerable<branches> getClinicBranchAll()
        {
            return Db.branchs;
        }

        public IEnumerable<branches> getClinicBranchStatus(string status)
        {
            return Db.branchs.Where(s => s.status == status);
        }

        public branches getClinicBranch(int id)
        {
            return Db.branchs.FirstOrDefault(s => s.branch_id == id);
        }

        
    }
}
