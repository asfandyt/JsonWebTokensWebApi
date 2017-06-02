using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using AuthorizationServer.Api.Models;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace AuthorizationServer.Api.Models
{
    public class AudiencesStore
    {
        public static ConcurrentDictionary<string, Models.AudienceModel> AudiencesList = new ConcurrentDictionary<string, Models.AudienceModel>();

        static AudiencesStore()
        {
            AudiencesList.TryAdd("099153c2625149bc8ecb3e85e03f0022",
                new Models.AudienceModel
                {
                    ClientId = "099153c2625149bc8ecb3e85e03f0022",
                    Base64Secret = "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw",
                    Name = "ResourceServer.Api 1"
                });
        }

        public static AudienceModel AddAudience(string name)
        {
            var clientId = Guid.NewGuid().ToString("N");

            var key = new byte[32];
            RNGCryptoServiceProvider.Create().GetBytes(key);
            var base64Secret = TextEncodings.Base64Url.Encode(key);

            Models.AudienceModel newAudience = new Models.AudienceModel { ClientId = clientId, Base64Secret = base64Secret, Name = name};
            AudiencesList.TryAdd(clientId, newAudience);
            return newAudience;
        }

        public static AudienceModel FindAudience(string clientId)
        {
            Models.AudienceModel audience = null;
            if (AudiencesList.TryGetValue(clientId, out audience))
            {
                return audience;
            }
            return null;
        }
    }
}