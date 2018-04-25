using System;

namespace FullLegitCode.Util
{
    public interface IByteArrayWrapper
    {
        Byte[] Bytes { get; set; }
    }


    public sealed class ByteArrayWrapper : IByteArrayWrapper
    {
        public Byte[] Bytes { get; set; }
    }
}
