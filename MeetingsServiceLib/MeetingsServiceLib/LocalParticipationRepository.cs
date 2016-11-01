using System;
using System.Collections.Generic;
using System.Linq;

namespace MeetingsServiceLib
{
    public class LocalParticipationRepository : IParticipationRepository
    {
        private List<Participation> participationList;

        public LocalParticipationRepository()
        {
            participationList = new List<Participation>();
        }

        public void addParticipations(Participation participation)
        {
            if (!participationList.Contains(participation))
                participationList.Add(participation);
        }

        public List<Participation> getMeetingParticipants(string meetingName)
        {
            return participationList.Where(m => m.MeetingName.Equals(meetingName)).ToList();
        }

        public List<Participation> getUserMeetings(string userLogin)
        {
            return participationList.Where(m => m.UserLogin.Equals(userLogin)).ToList();
        }
    }
}