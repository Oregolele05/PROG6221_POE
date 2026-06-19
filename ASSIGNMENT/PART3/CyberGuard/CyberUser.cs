using System;
using System.Collections.Generic;

namespace CyberGuard
{
    // ══════════════════════════════════════════════════════════════════════
    // CyberUser — stores all session memory and engagement analytics
    // Satisfies Memory and Recall + generic collection requirements
    // ══════════════════════════════════════════════════════════════════════
    public class CyberUser
    {
        // User's validated name
        public string username { get; set; } = "User";

        // Current screen — read by MainWindow to route input
        public string Section { get; set; } = "getname";

        // Last topic visited — used for follow-up tips
        public string lastTopic { get; set; } = "";

        // Memory Profile Data
        public string declaredFavTopic { get; set; } = "";
        private string calculatedFavTopic { get; set; } = "";

        // Gets the user's favorite topic, prioritizing the explicitly declared one,
        // falling back to the calculated one based on engagement analytics
        public string favTopic
        {
            get { return !string.IsNullOrEmpty(declaredFavTopic) ? declaredFavTopic : calculatedFavTopic; }
            set { calculatedFavTopic = value; }
        }

        // Engagement Analytics
        public int QuestionCount { get; set; } = 0;
        public DateTime TopicStartTime { get; set; } = DateTime.Now;

        // Generic Dictionary<string,TimeSpan> — tracks time spent per topic
        public Dictionary<string, TimeSpan> TopicDurations { get; set; } = new Dictionary<string, TimeSpan>();

        // ── TrackTopic ────────────────────────────────────────────────────
        // Called every time a topic is opened or left
        // Logs time on current topic, sets new topic, recalculates favourite
        public void TrackTopic(string newTopic)
        {
            // Log time spent on the topic being left
            if (!string.IsNullOrEmpty(lastTopic))
            {
                TimeSpan duration = DateTime.Now - TopicStartTime;
                if (TopicDurations.ContainsKey(lastTopic))
                    TopicDurations[lastTopic] += duration;
                else
                    TopicDurations[lastTopic] = duration;
            }

            // Set the new incoming topic
            if (!string.IsNullOrEmpty(newTopic))
                lastTopic = newTopic;

            // Recalculate which topic has the most accumulated time
            DetermineFavoriteTopic();

            // Reset timer for the new active topic
            TopicStartTime = DateTime.Now;
        }

        // Determines the favourite topic based on total time spent
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
                calculatedFavTopic = bestTopic;
        }
    }
}