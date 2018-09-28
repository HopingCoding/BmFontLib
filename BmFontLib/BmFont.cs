using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BmFontLib
{
    /// <summary>
    ///
    /// http://www.angelcode.com/products/bmfont/doc/file_format.html
    /// </summary>
    public class BmFont
    {


        
        public Info info;
        public Common common;
        public List<Page> pages = new List<Page>();
        public IDictionary<uint, Char> characters = new Dictionary<uint, Char>();



        public BmFont(Stream stream)
        {
            byte [] fourbyte = new byte[4];
            stream.Read(fourbyte, 0, fourbyte.Length); //BMF3
            if (fourbyte[0] != 66 || fourbyte[1] != 77 || fourbyte[2] != 70 || fourbyte[3] != 3)
            {
                Console.WriteLine("[BMFont] FourCC incorrect: " + fourbyte);
            }

            int blockId;
            while ((blockId = stream.ReadByte()) >= 0)
            {
                stream.Read(fourbyte, 0, fourbyte.Length);
                int length = BitConverter.ToInt32(fourbyte, 0);
              

                byte [] data = new byte[length];
                stream.Read(data, 0, length);

                switch (blockId)
                {
                    case 1:
                        {
                            info = new Info(data);
                        }
                        break;
                    case 2:
                        {
                            common = new Common(data);
                        }
                        break;
                    case 3:
                        {
                            string name = "";
                            int i = 0;
                            while (i < data.Length)
                            {
                                while (data[i] != '\0')
                                {
                                    name += (char)data[i];
                                    i++;
                                }
                                pages.Add(new Page {file = name});
                                i++;
                            }
                        }
                        break;
                    case 4:
                        {
                            int numChars = length/20;
                            for (int i = 0; i < numChars; i++)
                            {
                                Char c = new Char(data, i*20);
                                characters.Add(c.id, c);
                            }
                        }
                        break;
                    case 5:
                        {
                            int numPairs = length/10;
                            for (int i = 0; i < numPairs; i++)
                            {
                                int offset = i * 10;
                                uint first = BitConverter.ToUInt32(data, offset);
                                uint second = BitConverter.ToUInt32(data, offset + 4);
                                short amount = BitConverter.ToInt16(data, offset + 8);
                                if (!characters[first].kernings.ContainsKey(second))
                                {
                                    characters[first].kernings.Add(second, amount);
                                }
                            }
                        }
                        break;
                }
            }
        }

        // TODO: add kerning?
        public int GetTextWidth(string text)
        {
            int w = 0;
            foreach (char c in text)
            {
                if (characters.TryGetValue(c, out var glyph))
                {
                    w += glyph.xadvance;
                }
            }
            return w;
        }
    }
}
