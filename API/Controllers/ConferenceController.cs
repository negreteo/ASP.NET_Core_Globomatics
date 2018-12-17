using System.Collections.Generic;
using System.Linq;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace API.Controllers
{
   // Attribute routing, v1 is used to version the web api
   [Route ("v1/[controller]")]
   public class ConferenceController : ControllerBase // ControlBase for API Controllers
   {
      private readonly IConferenceRepo repo;

      public ConferenceController (IConferenceRepo repo)
      {
         this.repo = repo;
      }

      public IActionResult GetAll ()
      {
         var conferences = repo.GetAll ();
         if (!conferences.Any ())
            return new NoContentResult (); // Returns 204 - No Content

         return new ObjectResult (conferences); // Returns 202 - Accepted
      }

      [HttpPost]
      public ConferenceModel Add ([FromBody] ConferenceModel conference)
      {
         return repo.Add (conference); // Return 200 - OK
      }
   }
}
