using System;
// ReSharper disable InconsistentNaming

namespace BmFontLib.blocks
{
    public struct Info
    {
        public short fontSize;
        public byte bitField;
        public byte charSet;
        public ushort stretchH;
        public byte aa;
        public byte paddingUp;
        public byte paddingRight;
        public byte paddingDown;
        public byte paddingLeft;
        public byte spacingHoriz;
        public byte spacingVert;
        public byte outline;
        public string fontName;

        public Info(byte[] values)
        {
            int i = 0;
            fontSize = BitConverter.ToInt16(values, i);
            i += 2;
            bitField = values[ i++ ];
            charSet = values[ i++ ];
            stretchH = BitConverter.ToUInt16(values, i);
            i += 2;
            aa = values[i++];
            paddingUp = values[i++];
            paddingRight = values[i++];
            paddingDown = values[i++];
            paddingLeft = values[i++];
            spacingHoriz = values[i++];
            spacingVert = values[i++];
            outline = values[i++];
            fontName = GetString(values, i);
        }

        private static string GetString(byte[] bytes, int offset)
        {
            byte[] chars = new byte[bytes.Length - offset - 1];
            Buffer.BlockCopy(bytes, offset, chars, 0, bytes.Length - offset - 1);
            return System.Text.Encoding.Default.GetString(chars);
        }

    }
}
