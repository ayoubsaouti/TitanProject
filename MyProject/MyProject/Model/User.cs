using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Model
{
    public class User
    {
        public string IdUser { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public ICollection<Titan> MyCollection { get; set; }


        public User()
        {
            
        }

        public User(string id, string username, string password)
        {
            IdUser = id;
            Username= username;
            Password= password;
            MyCollection = new List<Titan>();
        }
    }

}
