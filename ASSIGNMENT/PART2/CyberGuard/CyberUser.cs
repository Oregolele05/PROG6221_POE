using System;
using System.Collections.Generic;


public class CyberUser
{
    public string username = string.Empty;
    public string lastTopic = string.Empty;
    public string favTopic = string.Empty;

    public string Section { get; set; } = "getname";

    public List<string> topicsViewed = new List<string>();

    public Dictionary<string, int> topicCount = new Dictionary<string, int>();

    public void TrackTopic(string topic)
    {
        if (!topicsViewed.Contains(topic))
        {
            topicsViewed.Add(topic);
        }
        if (topicCount.ContainsKey(topic))
        {
            topicCount[topic]++;
        }
        else
        {
            topicCount[topic] = 1;
        }
        int max = 0;
        foreach (var count in topicCount)
        {
            if (count.Value > max)
            {
                max = count.Value;
                favTopic = count.Key;

            }
            lastTopic = topic;
        }   
    }
}


