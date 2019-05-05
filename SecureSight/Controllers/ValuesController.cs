using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;

//System.Diagnostics.Trace.TraceInformation("Information"); 
//System.Diagnostics.Trace.TraceWarning("Warning"); 
//System.Diagnostics.Trace.TraceError("Error");

namespace SecureSight.Controllers
{
    public class SecureSightController : ApiController
    {
        // GET api/securesight
        [SwaggerOperation("GetAll")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/securesight/5
        [SwaggerOperation("GetById")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public string Get(int id)
        {
            return "value";
        }

        // GET api/securesight/deviceId
        [SwaggerOperation("Register")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public string Register(int deviceId)
        {
            //Create Resources
            //Return Connection Key
            return "value";
        }

        // PUT api/securesight/deviceId
        [SwaggerOperation("Heartbeat")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public bool Heartbeat(int deviceId, [FromBody]string value)
        {
            return true;
        }

        // POST api/securesight
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/securesight/5
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/securesight/5
        [SwaggerOperation("Delete")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Delete(int id)
        {
        }
    }
}
