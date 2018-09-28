using System;
using System.Collections.Generic;
using System.Text;

namespace BmFontLib
{
    public struct Char
    {
        public uint id;
        public ushort x;
        public ushort y;
        public ushort width;
        public ushort height;
        public short xoffset;
        public short yoffset;
        public short xadvance;
        public byte page;
        public byte chnl;


        public Dictionary<uint, short> kernings;

        public Char(byte[] values, int offset)
        {
            kernings = new Dictionary<uint, short>();
            int i = 0;
            id = BitConverter.ToUInt32(values, offset + i);
            i += 4;
            x = BitConverter.ToUInt16(values, offset + i);
            i += 2;
            y = BitConverter.ToUInt16(values, offset + i);
            i += 2;
            width = BitConverter.ToUInt16(values, offset + i);
            i += 2;
            height = BitConverter.ToUInt16(values, offset + i);
            i += 2;
            xoffset = BitConverter.ToInt16(values, offset + i);
            i += 2;
            yoffset = BitConverter.ToInt16(values, offset + i);
            i += 2;
            xadvance = BitConverter.ToInt16(values, offset + i);
            i += 2;
            page = values[offset + i];
            i++;
            chnl = values[offset + i];
        }
    }


}
