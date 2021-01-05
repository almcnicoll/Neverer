using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neverer.UtilityClass
{
    /// <summary>
    /// Describes a statistic about a <see cref="Crossword"/> or a <see cref="PlacedClue"/>
    /// </summary>
    public class CrosswordStatistic
    {
        /// <summary>
        /// The name of the statistic
        /// </summary>
        public String Statistic { get; set; }

        /// <summary>
        /// The Object value of the statistic
        /// </summary>
        private Object __ObjectValue { get; set; }

        /// <summary>
        /// The string value of the statistic
        /// </summary>
        public String Value
        {
            get
            {
                return __ObjectValue.ToString();
            }
        }

        public CrosswordStatistic(String statisticName, Object value)
        {
            Statistic = statisticName;
            __ObjectValue = value;
        }

        public void setValue(Object value)
        {
            __ObjectValue = value;
        }
    }
}
