using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ContactApi.Models;
using ContactApi.Repositories;

namespace ContactApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        [Route("get")]
        public ActionResult GetOption(int id)
        {
            ContactRepository Repo = new ContactRepository();
            var result = Repo.Get(id);
            return Ok(result);
        }

        [Route("list")]
        public ActionResult ListOption()
        {
            ContactRepository Repo = new ContactRepository();
            var result = Repo.List();
            return Ok(result);
        }
        [HttpPost]
        [Route("create")]
        public ActionResult CreateOption(Contact Model)
        {
            ContactRepository Repo = new ContactRepository();
            Repo.Create(Model);
            return Ok();
        }
        [Route("delete")]
        public ActionResult DeleteOption(int id)
        {
            ContactRepository Repo = new ContactRepository();
            Repo.Delete(id);
            return Ok();
        }
        [Route("update")]
        public ActionResult UpdateOption(Contact Model, int id)
        {
            ContactRepository Repo = new ContactRepository();
            Repo.Update(id, Model);
            return Ok();
        }
    }
}
