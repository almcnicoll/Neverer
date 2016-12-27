using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
    [Serializable]
    public class FlexStack<T> : List<T>
    {
        public void Push(T item)
        {
            this.Insert(0, item);
        }
        public T Pop()
        {
            if (base.Count > 0)
            {
                T temp = this[0];
                this.RemoveAt(0);
                return temp;
            }
            else
            {
                return default(T);
            }
        }
    }
}
