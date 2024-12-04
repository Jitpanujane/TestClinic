using Project.Features.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Project.Features.Models.ClinicBranchModels;

namespace Project.Api.Controllers.Api
{
    [RoutePrefix("api/ClinicBranch")]
    public class ClinicBranchController : _BaseController
    {

        private readonly IClinicBranch clinicBranchService;

        public ClinicBranchController(IClinicBranch ClinicBranch)
        {
            clinicBranchService = ClinicBranch;
        }

        /// <summary>
        /// แสดงข้อมูลสาขาทั้งหมด
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet]
        [Route]
        public IHttpActionResult getClinicBranchAll(string status)
        {
            if (status == "แสดงทั้งหมด")
            {
                return Ok(clinicBranchService.getClinicBranchAll());
            }
            else
            {
                return Ok(clinicBranchService.getClinicBranchStatus(status));
            }
        }

        
        /// <summary>
        /// แสดงข้อมูลสาขาที่เลือกจาก id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult getClinicBranch(int id)
        {

            return Ok(clinicBranchService.getClinicBranch(id));
        }

        /// <summary>
        /// แก้ไขข้อมูลสาขา
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult editClinicBranch(int id, EditClinicBranchModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model invalid.");
            }
            var result = clinicBranchService.editClinicBranch(id, value);

            return Ok(result);
        }


        /// <summary>
        /// แก้ไขสถานะของสาขา
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("status/{id}")]
        public IHttpActionResult editStatusClinicBranch(int id, EditStatusClinicBranchModel value)
        {
            if (value.status == "เปิดใช้งาน" || value.status != "ปิดใช้งาน")
            {
                var result = clinicBranchService.editStatusClinicBranch(id, value);
                return Ok(result);
            }
            else
            {
                return BadRequest("Model invalid.");
            }
        }
    }
}