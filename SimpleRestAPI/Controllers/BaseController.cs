using Microsoft.AspNetCore.Mvc;
using SimpleRestAPI.Models;
using SimpleRestAPI.Repositories.Interfaces;
using SimpleRestAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace SimpleRestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        private IRepairService RepairService { get; set; }
        private IBaseRepository<Document> Documents { get; set; }

        public BaseController(IRepairService repairService, IBaseRepository<Document> document)
        {
            RepairService = repairService;
            Documents = document;
        }

        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(Documents.GetAll());
        }

        [HttpPost]
        public JsonResult Post()
        {
            RepairService.Work();
            return new JsonResult("Work was successfully done");
        }

        [HttpPut]
        public JsonResult Put(Document doc)
        {
            bool success = true;
            var document = Documents.Get(doc.Id);
            try
            {
                if (document != null)
                {
                    document = Documents.Update(doc);
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception)
            {
                success = false;
            }

            return success ? new JsonResult($"Update successful {document.Id}") : new JsonResult("Update was not successful");
        }

        [HttpDelete]
        public JsonResult Delete(Guid id)
        {
            bool success = true;
            var document = Documents.Get(id);

            try
            {
                if (document != null)
                {
                    Documents.Delete(document.Id);
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception)
            {
                success = false;
            }

            return success ? new JsonResult("Delete successful") : new JsonResult("Delete was not successful");
        }
    }
}
