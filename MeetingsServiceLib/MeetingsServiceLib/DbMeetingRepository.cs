using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsServiceLib
{
    public class DbMeetingRepository : IMeetingsRepository
    {
        public void addMetting(Meeting meeting)
        {
            throw new NotImplementedException();
        }

        public List<Meeting> getAllMeetings()
        {
            throw new NotImplementedException();
        }

        public List<Meeting> getMeetingByDate(DateTime? starDate, DateTime? endDate)
        {
            throw new NotImplementedException();
        }

        public Meeting getMeetingsByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
