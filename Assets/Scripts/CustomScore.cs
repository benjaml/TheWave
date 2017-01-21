using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CustomScore : IScore {
    public DateTime date
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public string formattedValue
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public string leaderboardID
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public int rank
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public string userID
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public long value
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public void ReportScore(System.Action<bool> callback)
    {
        throw new System.NotImplementedException();
    }
}
