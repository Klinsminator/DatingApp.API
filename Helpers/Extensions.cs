using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Helpers
{
    //Extension method for any other methos
    //Would be static so no instances should be created
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            
            //Next two headers allow the first one to be displayed
            //Cors header so Angular doesn't kdeal with it, given that doesn't count with appropiate access control...
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static int CalculateAge(this DateTime theDateTime)
        {
            //This would be an extension method to the datetime class of C# so can calculate the age of a user

            var age = DateTime.Today.Year - theDateTime.Year;

            //dapending on day of birth may not be correct so will check birthday
            if (theDateTime.AddYears(age) > DateTime.Today)
                age--;

            return age;
        }
    }
}
