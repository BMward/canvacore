using System;
using System.Collections.Generic;
using System.Text;

namespace CanvacoreLib
{
    public class EncoderMethods
    {
        Random randint = new Random();

        public static Dictionary<int, char> SetupCipher(string charset)
        {
            Dictionary<int, char> cipher = new Dictionary<int, char>();
            for (int i = 0; i < charset.Length; i ++)
            {
                cipher.Add(i * 9, charset[i]);
            }
            return cipher;
        }

        public static List<Pixel> convertValuesToPixels(List<int> ints)
        {
            List<Pixel> pix = new List<Pixel>();
            for(int i = 0; i < ints.Count -1; i += 4)
            {
                pix.Add(new Pixel() { R = ints[i], G = ints[i + 1], B = ints[i + 2], A = ints[i + 3] });
            }
            return pix;
        }

        public Canvas EncodeObject(string input, string alphabet)
        {
            //setup
            List<int> values = new List<int>();

            //prepare toEncode object for processing
            var stringToEncode = input.ToLower();

            var cipher = SetupCipher(alphabet);
            //string must be evenly divisible by 4 to ensure it translates to a pixel list correctly
            while (stringToEncode.Length % 4 != 0)
            {
                //add some noise to the end
                stringToEncode += alphabet[randint.Next(alphabet.Length)];
            }

            Canvas encodedCanvas = new Canvas();
            
            foreach (var character in stringToEncode)
            {
                foreach (KeyValuePair<int, char> kvp in cipher)
                {
                    if (character == kvp.Value)
                    {
                        values.Add(randint.Next(kvp.Key, kvp.Key + 8));
                    }
                }
            }

            encodedCanvas.Pixels = convertValuesToPixels(values);
            return encodedCanvas;
        }
    }
}
