using System.Collections.Generic;

namespace MeetingsServiceLib
{
    public interface IParticipationRepository
    {
        void addParticipations(Participation participation);
        List<Participation> getUserMeetings(string userLogin);
        List<Participation> getMeetingParticipants(string meetingName);
    }
}