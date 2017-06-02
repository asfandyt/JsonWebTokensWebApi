using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AuthorizationServer.Api.Models;

namespace AuthorizationServer.Api.Controllers
{
    [RoutePrefix("api/audience")]
    public class AudienceController : ApiController
    {
        [Route("")]
        public IHttpActionResult Post(Models.AudienceModel audienceModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Models.AudienceModel newAudience = AudiencesStore.AddAudience(audienceModel.Name);

            return base.Ok(newAudience);
        }
    }
}