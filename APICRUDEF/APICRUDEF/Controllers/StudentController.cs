using APICRUDEF.DTOs;
using APICRUDEF.EF;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APICRUDEF.Controllers
{
    [RoutePrefix("api/student")]
    public class StudentController : ApiController
    {

        Sum25_BEntities db = new Sum25_BEntities();
        public static Mapper GetMapper() {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Student, StudentDTO>().ReverseMap();
                cfg.CreateMap<Student, StudentDeptDTO>().ReverseMap();
                cfg.CreateMap<Department, DepartmentDTO>().ReverseMap();
                cfg.CreateMap<Department, DepartmentStudentDTO>().ReverseMap();
            });
            return new Mapper(config);
        }
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage Get() {
            try
            {
                var data = GetMapper().Map<List<StudentDTO>>(db.Students.ToList());
            
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex) { 
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage Get(int id) {
            try {
                var data = GetMapper().Map<StudentDTO>(db.Students.Find(id));
                if (data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);

                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Data Not Found");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(StudentDTO s) {
            try {
                var data = GetMapper().Map<Student>(s);
                db.Students.Add(data);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

    }
}
