using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace MeetingsServiceLib
{
    public class LocalUserRepository : IUserRepository
    {
        private List<User> listOfUsers;

        public LocalUserRepository()
        {
            listOfUsers = new List<User>();
        }

        public void addUser(User user)
        {
            listOfUsers.Add(user);
        }

        public List<User> getAllUsers()
        {
            return listOfUsers;
        }

        public User getUserByLogin(string login)
        {
            return listOfUsers.Where(m => m.Login.Equals(login)).DefaultIfEmpty(null).Single();
        }

        public User getUserByEmail(string email)
        {
            return listOfUsers.Where(m => m.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase)).DefaultIfEmpty(null).Single();
        }

        public void removeUser(string name)
        {
            listOfUsers.RemoveAll(m => m.Name.Equals(name));
        }
    }
}
