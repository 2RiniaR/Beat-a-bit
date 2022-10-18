using System;
using System.Collections.Generic;

namespace RineaR.BeatABit.Environments.Data
{
    [Serializable]
    public class RecordTable
    {
        public List<Record> records;

        public bool IsCleared(string key)
        {
            return records.Exists(record => record.key == key);
        }
    }
}