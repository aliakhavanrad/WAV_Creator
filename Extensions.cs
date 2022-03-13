using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAV_Creator
{
    public static class Extensions
    {
        public static List<byte> AddByte(this List<byte> _list, byte _byte)
        {
            _list.Add(_byte);
            return _list;
        }

        public static List<byte> AddBytes(this List<byte> _list, IEnumerable<byte> _bytes)
        {
            _list.AddRange(_bytes);
            return _list;
        }

        public static IEnumerable<byte> ToByteArray(this IEnumerable<char> _list)
        {
            return _list.Select(x => Convert.ToByte(x)).ToList();
        }

        public static IEnumerable<byte> ToByteArray(this IEnumerable<int> _list)
        {
            return _list.Select(x => Convert.ToByte(x));
        }
    }
}
