using System.Collections.Generic;

namespace Neverer.UtilityClass
{
    public class Matrix<T>
    {
        private Dictionary<int, Dictionary<int, T>> __data = new Dictionary<int, Dictionary<int, T>>();

        public bool returnValueOnKeyNotFound { get; set; }
        public T defaultValue { get; set; }
        private int __maxCol = 0;
        private int __maxRow = 0;

        public T this[int col, int row]
        {
            get
            {
                if (__data.ContainsKey(col))
                {
                    if (__data[col] == null) { __data[col] = new Dictionary<int, T>(); }
                    if (__data[col].ContainsKey(row))
                    {
                        return __data[col][row];
                    }
                    else
                    {
                        if (returnValueOnKeyNotFound)
                        {
                            return defaultValue;
                        }
                        else
                        {
                            throw new KeyNotFoundException(string.Format("Column {0} does not contain row {1}", col, row));
                        }
                    }
                }
                else
                {
                    if (returnValueOnKeyNotFound)
                    {
                        return defaultValue;
                    }
                    else
                    {
                        throw new KeyNotFoundException(string.Format("Column {0} does not exist", col));
                    }
                }
            }
            set
            {
                if (col > __maxCol) { __maxCol = col; }
                if (row > __maxRow) { __maxRow = row; }

                if (!__data.ContainsKey(col)) { __data.Add(col, new Dictionary<int, T>()); }
                if (!__data[col].ContainsKey(row))
                {
                    __data[col].Add(row, value);
                }
                else
                {
                    __data[col][row] = value;
                }
            }
        }

        public int cols { get { return __maxCol + 1; } }
        public int rows { get { return __maxRow + 1; } }
    }
}
