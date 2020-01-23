using DatingApp.API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Data
{
    public class Seed
    {
        //Class to serialize json seeding data

        public static void SeedUsers(DataContext context)
        {
            //Check if there is any data on the DB
            if (!context.Users.Any())
            {
                //Read from the json file on this same directory
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                //Deserialize json to a list of users objects
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                foreach (var user in users)
                {
                    //need to create a password for the user with hash/salt
                    byte[] passworHash, passwordSalt;
                    CreatePasswordHash("password", out passworHash, out passwordSalt);
                    user.PasswordHash = passworHash;
                    user.PasswordSalt = passwordSalt;
                    user.Username = user.Username.ToLower();
                    context.Users.Add(user);
                }
                //No need to make it async, would call at start and just once.. no differencce made of it
                context.SaveChanges();
                //Normally will add this to startup file, but microsoft chaged best practices and now it is called on program class
            }
        }

        //Copied from AuthRepository because it is a private method that won't be turned public or else.. added the static to run here
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
