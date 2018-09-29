using System;
// ReSharper disable InconsistentNaming

namespace BmFontLib.blocks
{
    public struct Common
    {
        public ushort lineHeight;
        public ushort _base;
        public ushort scaleW;
        public ushort scaleH;
        public ushort pages;
        public byte bitField;
        public byte alphaChnl;
        public byte redChnl;
        public byte greenChnl;
        public byte blueChnl;

        public Common(byte[] values)
        {
            int i = 0;
            lineHeight = BitConverter.ToUInt16(values, i);
            i += 2;
            _base = BitConverter.ToUInt16(values, i);
            i += 2;
            scaleW = BitConverter.ToUInt16(values, i);
            i += 2;
            scaleH = BitConverter.ToUInt16(values, i);
            i += 2;
            pages = BitConverter.ToUInt16(values, i);
            i += 2;
            bitField = values[i++];
            alphaChnl = values[i++];
            redChnl = values[i++];
            greenChnl = values[i++];
            blueChnl = values[i];
        }
    }
}
