using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberGuard
{
    public class CyberUser
    {
        public string username { get; set; } = "User";
        public string Section { get; set; } = "getname";
        public string lastTopic { get; set; } = "";
        public string favTopic { get; set; } = "";

        // Engagement Analytics
        public int QuestionCount { get; set; } = 0;
        public DateTime TopicStartTime { get; set; } = DateTime.Now;
        public Dictionary<string, TimeSpan> TopicDurations { get; set; } = new Dictionary<string, TimeSpan>();

        public void TrackTopic(string newTopic)
        {
            // 1. Calculate and save elapsed time for the previous topic before changing state
            if (!string.IsNullOrEmpty(lastTopic))
            {
                TimeSpan duration = DateTime.Now - TopicStartTime;
                if (TopicDurations.ContainsKey(lastTopic))
                {
                    TopicDurations[lastTopic] += duration;
                }
                else
                {
                    TopicDurations[lastTopic] = duration;
                }
            }

            lastTopic = newTopic;
            // 2. Transition state configurations to the new active topic
            if (TopicDurations.Count > 0)
            {
                favTopic = TopicDurations.OrderByDescending(x => x.Value).First().Key;
            }
            else if (!string.IsNullOrEmpty(newTopic))
            {
                favTopic = newTopic;
            }

            TopicStartTime = DateTime.Now; // Reset track clock timer
        }
    }
}