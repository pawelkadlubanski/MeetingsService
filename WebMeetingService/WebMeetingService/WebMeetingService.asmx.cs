using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MeetingsServiceLib;

namespace WebMeetingService
{
    /// <summary>
    /// Summary description for WebMeetingService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebMeetingService : System.Web.Services.WebService
    {
        private Service service;

        public WebMeetingService()
        {
            IMeetingsRepository meetingRepository = new LocalMeetingsRepository();
            IUserRepository userRepository = new LocalUserRepository();
            IParticipationRepository participationRepository = new LocalParticipationRepository();
            service = new Service(meetingRepository, userRepository, participationRepository);
        }

        [WebMethod]
        public void addMeeting(String name, String location, string date, int maxNumberOfParticipants)
        {
            service.addMeeting(name, location, date, maxNumberOfParticipants);
        }

        [WebMethod]
        public List<Meeting> getAllMeetingsList()
        {
            return service.getAllMeetingsList();
        }

        [WebMethod]
        public Meeting getMeetingsByName(string name)
        {
            return service.getMeetingsByName(name);
        }

        [WebMethod]
        public List<Meeting> getMeetingsByDate(DateTime? startDate = null, DateTime? endDate = null)
        {
            return service.getMeetingsByDate(startDate, endDate);
        }

        [WebMethod]
        public List<User> getAllUsers()
        {
            return service.getAllUsers();
        }

        [WebMethod]
        public void addUser(string name, string surname, string login, string email)
        {
            service.addUser(name, surname, login, email);
        }

        [WebMethod]
        public User getUserByLogin(string login)
        {
            return service.getUserByLogin(login);
        }

        [WebMethod]
        public void removeUser(string name)
        {
           service.removeUser(name);
        }

        [WebMethod]
        public void signUpUserToMeeting(string login, string name)
        {
            service.signUpUserToMeeting(login, name);
        }

        [WebMethod]
        public List<Meeting> getUserMeetings(string userLogin)
        {
            return service.getUserMeetings(userLogin);
        }

        [WebMethod]
        public List<User> getMeetingParticipants(string meetingName)
        {
            return service.getMeetingParticipants(meetingName);
        }
    }
}
