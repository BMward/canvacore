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

            //prepare input string for processing
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
            PrepareForImagifying(encodedCanvas);
            return encodedCanvas;
        }

        public static Canvas PrepareForImagifying(Canvas encoded)
        {
            if (encoded.Pixels.Count <= 1 )
            {
                encoded.Height = 1;
                encoded.Width = 1;
                encoded.PixelCount = 1;
            }
            else
            {
                //need the pixel count to be a sqrt so that we can produce a square image.
                //Add additional pixel noise to the end to square it off.
                //TODO: randomize these values so it's not as obvious.
                while(Math.Sqrt(encoded.Pixels.Count) % 1 != 0)
                {
                    //add pixels until we know that the sqrt of the pixel count can produce a square image.
                    encoded.Pixels.Add(new Pixel() { R = 0, G = 0, B = 0, A = 0 });
                }
                encoded.Height = Math.Sqrt(encoded.Pixels.Count);
                encoded.Width = Math.Sqrt(encoded.Pixels.Count);
                encoded.PixelCount += encoded.Pixels.Count;
            }

            return encoded;
        }

        public static string DecodeCanvas(Canvas encodedCanvas, string charset)
        {
            var cipher = SetupCipher(charset);
            string output = "";



            return output;
        }

    }
}
