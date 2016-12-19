using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

namespace CommonController
{
    public class KLoggerCollection : System.Collections.IEnumerable
    {
        private Dictionary<long,KLogger> dictionary = new Dictionary<long,KLogger>();

        public void LoadLoggersFromConfigFiles(string path)
        {
            if (!Directory.Exists(path)) return;
            string[] files = Directory.GetFiles(path, @"*.config");
            foreach (string file in files)
            {
                KLogger k = KLogger.Deserialize(file);
                dictionary.Add(k.stationID, k);
            }   
        }

        public void LoadLoggersFromDataBase()
        {
            foreach (KLogger k in db.Signal.GetAllFromDatabase())
                dictionary.Add(k.stationID, k);
        }

        public void Start(int stationID)
        {
            if (this[stationID].enabled)
                this[stationID].Start();
        }

        public void Stop(int stationID)
        {
            this[stationID].Stop();
        }

        public void StartAll()
        {
            foreach (KeyValuePair<long, KLogger> kvp in dictionary)
            {
                KLogger k = kvp.Value;
                if (k.enabled) k.Start();
            }
        }

        public void StopAll()
        {
            foreach (KeyValuePair<long, KLogger> kvp in dictionary)
                kvp.Value.Stop();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        public KLogger this [int stationId]
        {
            get 
            {
                KLogger k;
                dictionary.TryGetValue(stationId, out k);
                return k;
            }
        }

        public int Count
        {
            get { return dictionary.Count; } 
        }

        public KLogger[] ToArray()
        {
            KLogger[] loggers = new KLogger[Count];
            int i = 0;
            foreach (KeyValuePair<long, KLogger> kvp in dictionary)
                loggers[i++] = kvp.Value;
            return loggers;
        }
    }
}
