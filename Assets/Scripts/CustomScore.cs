using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace UnityEngine.SocialPlatforms
{
    public class CustomScore : IScore
    {

        public System.DateTime date { get; }
        //
        // Summary:
        //     ///
        //     The correctly formatted value of the score, like X points or X kills.
        //     ///
        public string formattedValue { get; }
        //
        // Summary:
        //     ///
        //     The ID of the leaderboard this score belongs to.
        //     ///
        public string leaderboardID { get; set; }
        //
        // Summary:
        //     ///
        //     The rank or position of the score in the leaderboard.
        //     ///
        public int rank { get; }
        //
        // Summary:
        //     ///
        //     The user who owns this score.
        //     ///
        public string userID { get; }
        //
        // Summary:
        //     ///
        //     The score value achieved.
        //     ///
        public long value { get; set; }

        public void ReportScore(System.Action<bool> callback)
        {
            throw new System.NotImplementedException();
        }
    }
}
