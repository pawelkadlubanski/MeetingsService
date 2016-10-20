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
        private IMeetingsRepository meetingsRepository;

        public Service(IMeetingsRepository meetingsRepository)
        {
            meetingFactory = new MeetingFactory();
            this.meetingsRepository = meetingsRepository;
        }

        public bool addMeeting(String name, String location, string date, int maxNumberOfParticipants)
        {
            var meeting = meetingFactory.create(name, location, date, maxNumberOfParticipants);
            if (meeting == null)
            {
                return false;
            }
            return meetingsRepository.addMetting(meeting);
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
    }
}
