namespace WebApi136.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using POCO;

    using Repository;

    using Service;

    public class CapeController : ApiController
    {
        private readonly CapeService service = new CapeService(new CapeRepository());

        private List<string> errors = new List<string>();

        [HttpPost]
        public string InsertCape(Cape cape)
        {
            this.service.InsertCape(cape, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpPost]
        public string UpdateCape(Cape cape)
        {
            this.service.UpdateCape(cape, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpPost]
        public string DeleteCape(int id)
        {
            this.service.DeleteCape(id, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpGet]
        public Cape ViewCape(string id1, int id2)
        {
            return this.service.ViewCape(id1, id2, ref this.errors);
        }

        [HttpGet]
        public List<Cape> ViewStudentCape(int id)
        {
            return this.service.ViewStudentCape(id, ref this.errors);
        }
    }
}