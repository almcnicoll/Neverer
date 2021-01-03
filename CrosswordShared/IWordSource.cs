using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neverer.UtilityClass
{
    public interface IWordSource
    {
        String FileName { get; set; }
        bool Enabled { get; set; }
        SerializableDictionary<String, List<String>> Entries { get; }
        IEnumerable<String> Keys { get; }

        String DictionaryName { get; set; }

        DateTime LastUpdated { get; set; }

        void Save();
    }
}
