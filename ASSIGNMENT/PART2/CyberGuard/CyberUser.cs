using System;
using System.Collections.Generic;

namespace CyberGuard
{
    public class CyberUser
    {
        public string username { get; set; } = "User";
        public string Section { get; set; } = "getname";
        public string lastTopic { get; set; } = "";

        // Memory Profile Data
        public string declaredFavTopic { get; set; } = "";
        private string calculatedFavTopic { get; set; } = "";

        // gets the user's favorite topic, prioritizing the explicitly declared one, but falling back to the calculated one based on engagement analytics if no declaration exists
        public string favTopic
        {
            get
            {
                return !string.IsNullOrEmpty(declaredFavTopic) ? declaredFavTopic : calculatedFavTopic;
            }
            set
            {
                calculatedFavTopic = value;
            }
        }

        // Engagement Analytics
        public int QuestionCount { get; set; } = 0;
        public DateTime TopicStartTime { get; set; } = DateTime.Now;
        public Dictionary<string, TimeSpan> TopicDurations { get; set; } = new Dictionary<string, TimeSpan>();

        public void TrackTopic(string newTopic)
        {
            // 1. Log the time spent on the topic the user is currently leaving
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

            // 2. Set the new incoming topic as the active one
            if (!string.IsNullOrEmpty(newTopic))
            {
                lastTopic = newTopic;
            }

            // 3. Recalculate which topic has the most accumulated time
            DetermineFavoriteTopic();

            // 4. Reset the tracking timer for the new active state
            TopicStartTime = DateTime.Now;
        }

        private void DetermineFavoriteTopic()
        {
            TimeSpan longestDuration = TimeSpan.Zero;
            string bestTopic = "";

            foreach (var record in TopicDurations)
            {
                if (!string.IsNullOrEmpty(record.Key) && record.Value > longestDuration)
                {
                    longestDuration = record.Value;
                    bestTopic = record.Key;
                }
            }

            if (!string.IsNullOrEmpty(bestTopic))
            {
                calculatedFavTopic = bestTopic;
            }
        }
    }
}