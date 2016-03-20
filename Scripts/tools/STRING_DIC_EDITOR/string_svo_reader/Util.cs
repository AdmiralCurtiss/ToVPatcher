using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace string_svo_reader
{
    static class Util
    {
        public static Encoding ShiftJISEncoding = Encoding.GetEncoding("shift-jis");

        public static UInt32 SwapEndian(UInt32 x)
        {
            return x = (x >> 24) |
                      ((x << 8) & 0x00FF0000) |
                      ((x >> 8) & 0x0000FF00) |
                       (x << 24);
        }

        

        public static byte[] StringToBytes(String s)
        {
            //byte[] bytes = ShiftJISEncoding.GetBytes(s);
            //return bytes.TakeWhile(subject => subject != 0x00).ToArray();
            return ShiftJISEncoding.GetBytes(s);
        }

        public static void DisplayException(Exception e)
        {
            MessageBox.Show("Exception occurred:\n" + e.Message);
        }
    }
}
