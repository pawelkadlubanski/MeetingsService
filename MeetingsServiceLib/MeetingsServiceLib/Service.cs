using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsServiceLib
{
    public class Service
    {
        private MeetingFactory meetingFactory;
        private UserFactory userFactory;

        private IMeetingsRepository meetingsRepository;
        private IUserRepository userRepository;

        public Service(IMeetingsRepository meetingsRepository, IUserRepository userRepository)
        {
            meetingFactory = new MeetingFactory();
            userFactory = new UserFactory();

            this.meetingsRepository = meetingsRepository;
            this.userRepository = userRepository;
        }

        public void addMeeting(String name, String location, string date, int maxNumberOfParticipants)
        {
            if (meetingsRepository.getMeetingsByName(name) != null)
                return;

            var meeting = meetingFactory.create(name, location, date, maxNumberOfParticipants);
            if (meeting != null)
            {
                meetingsRepository.addMetting(meeting);
            }
        }

        public List<Meeting> getAllMeetingsList()
        {
            return meetingsRepository.getAllMeetings();
        }

        public Meeting getMeetingsByName(string name)
        {
            return meetingsRepository.getMeetingsByName(name);
        }

        public List<Meeting> getMeetingsByDate(DateTime? startDate = null, DateTime? endDate = null)
        {
            return meetingsRepository.getMeetingByDate(startDate, endDate);
        }

        public List<User> getAllUsers()
        {
            return userRepository.getAllUsers();
        }

        public void addUser(string name, string surname, string login, string email)
        {
            if (userRepository.getUserByLogin(login) == null && userRepository.getUserByEmail(email) == null)
            {
                var user = userFactory.create(name, surname, login, email);
                userRepository.addUser(user);
            }
        }

        public User getUserByLogin(string login)
        {
            return userRepository.getUserByLogin(login);
        }

        public void removeUser(string name)
        {
            userRepository.removeUser(name);
        }
    }
}
