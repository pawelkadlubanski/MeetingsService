using System;
using System.Collections.Generic;

namespace MeetingsServiceLib
{
    public interface IMeetingsRepository
    {
        void addMetting(Meeting meeting);
        List<Meeting> getAllMeetings();
        Meeting getMeetingsByName(string name);
        List<Meeting> getMeetingByDate(DateTime? starDate, DateTime? endDate);
    }
}