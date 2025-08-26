using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IntroAPI.Controllers
{
    [RoutePrefix("api/student")]
    public class StudentController : ApiController
    {
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage AllStudents() { 
            return Request.CreateResponse(HttpStatusCode.OK,"All Students");
        }
        [HttpGet]
        [Route("{s_id}")]
        public HttpResponseMessage AllStudents(int s_id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Student "+s_id);
        }
        [HttpGet]
        [Route("scholarship")]
        public HttpResponseMessage ScStudents() {
            return Request.CreateResponse(HttpStatusCode.OK, "Scholarship Students");
        }
        [HttpGet]
        [Route("probation")]
        public HttpResponseMessage ProbStudents()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Problation Students");
        }
    }
}
