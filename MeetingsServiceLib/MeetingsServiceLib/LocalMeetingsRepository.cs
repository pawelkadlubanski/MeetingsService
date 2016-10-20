using System;
using System.Collections.Generic;
using System.Linq;

namespace MeetingsServiceLib
{
    public class LocalMeetingsRepository : IMeetingsRepository
    {
        private List<Meeting> meetingList;

        public LocalMeetingsRepository()
        {
            meetingList = new List<Meeting>();
        }

        public bool addMetting(Meeting meeting)
        {
            if (!meetingList.Contains(meeting))
            {
                meetingList.Add(meeting);
            }
            return true;
        }

        public List<Meeting> getAllMeetings()
        {
            return meetingList;
        }

        public Meeting getMeetingsByName(string name)
        {
            return meetingList.Where(m => m.Name.Equals(name)).DefaultIfEmpty(null).Single();
        }

        public List<Meeting> getMeetingByDate(DateTime? starDate, DateTime? endDate)
        {
            var meetings = meetingList.Where(m => (m.Time.CompareTo(starDate) > 0));

            if (endDate.HasValue)
            {
                meetings = meetings.Where(m => (m.Time.CompareTo(endDate) < 0));
            }

            return meetings.ToList();
        }
    }
}
