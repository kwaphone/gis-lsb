using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISLSB
{
    public class Convertor
    {
        public static string StringToHexString(string s, Encoding encode)
        {
            byte[] b = encode.GetBytes(s);//encode string to byte array
            string result = string.Empty;
            for (int i = 0; i < b.Length; i++)//convert to hex and separates with " "
            {
                if (i > 0)
                {
                    result += " ";
                }
                result += Convert.ToString(b[i], 16);
            }
            return result;
        }

        /// <summary>
        /// convert hex to decimal number string
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static int HexToDecString(string hex)
        {
            return Convert.ToInt32(hex, 16);
        }

        /// <summary>
        /// convert decimal to hex
        /// </summary>
        /// <param name="decNum"></param>
        /// <returns></returns>
        public static string DecToHexString(int decNum)
        {
            return Convert.ToString(decNum, 16);
        }

        /// <summary>
        /// convert hex to string
        /// </summary>
        /// <param name="hex"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string UnHex(string hex, string charset)
        {
            if (hex == null)
                throw new ArgumentNullException("hex");
            hex = hex.Replace(",", "");
            hex = hex.Replace("\n", "");
            hex = hex.Replace("\\", "");
            hex = hex.Replace(" ", "");
            if (hex.Length % 2 != 0)
            {
                hex += "20";//empty space
            }
            // convert hex to array
            byte[] bytes = new byte[hex.Length / 2];

            for (int i = 0; i < bytes.Length; i++)
            {
                try
                {
                    // compile to element into byte 
                    bytes[i] = byte.Parse(hex.Substring(i * 2, 2),
                    System.Globalization.NumberStyles.HexNumber);
                }
                catch
                {
                    // Rethrow an exception with custom message. 
                    throw new ArgumentException("Invalid hex number!", "hex");
                }
            }
            System.Text.Encoding chs = System.Text.Encoding.GetEncoding(charset);
            return chs.GetString(bytes);
        }
    }
}
