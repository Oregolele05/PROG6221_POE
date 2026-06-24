using System;
using System.Collections.ObjectModel;

namespace CyberGuard
{
    public static class CyberLogger
    {
        private static ObservableCollection<string> _log = new ObservableCollection<string>();
        public static ObservableCollection<string> Log => _log;

        public static void Add(string entry)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _log.Add($"[{timestamp}] {entry}");
            if (_log.Count > 20)
                _log.RemoveAt(0);
        }
    }
}