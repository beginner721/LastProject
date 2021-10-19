using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
            //hangi rolleri bulmak istiyorsam ? string claimType
        {
            //jwt den gelen claimleri okumak için .NET de olan class: ClaimsPrincipal 
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList(); //istediğimiz claimtype a göre getirecek bir listeye atacak
            return result;
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
            //çoğunlukla roller lazım ise claimroles dediğim zaman bana direkt rolleri döndür...
        {
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }
    }
}
