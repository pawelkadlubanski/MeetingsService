using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsServiceLibFunctionalTests
{
    class UserData
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }

        public UserData(string name, string surname, string login, string email)
        {
            this.Name = name;
            this.Surname = surname;
            this.Login = login;
            this.Email = email;
        }

        public MeetingsServiceLib.User convertToUser()
        {
            var user = new MeetingsServiceLib.User();
            user.Name = this.Name;
            user.Surname = this.Surname;
            user.Login = this.Login;
            user.Email = this.Email;
            return user;
        }
    }
}
