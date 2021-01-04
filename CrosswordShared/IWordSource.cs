using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neverer.UtilityClass
{
    public interface IWordSource
    {
        String fileName { get; set; }
        bool enabled { get; set; }
        SerializableDictionary<String, List<String>> entries { get; }
        IEnumerable<String> keys { get; }

        String dictionaryName { get; set; }

        DateTime lastUpdated { get; set; }

        void Save();
    }
}
