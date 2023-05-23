using System;
using Gpm.Common.ThirdParty.MessagePack;
using Gpm.Common.ThirdParty.MessagePack.Formatters;

namespace Gpm.CacheStorage.Formatters
{
    public class StringToValueIntFormatter : IMessagePackFormatter<StringToValue<int>>
    {
        public int Serialize(ref byte[] bytes, int offset, StringToValue<int> value, IFormatterResolver formatterResolver)
        {
            return MessagePackBinary.WriteInt32(ref bytes, offset, value.GetValue());
        }

        public StringToValue<int> Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            int value = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            return new StringToValue<int>(value);
        }
    }
    public class StringToValueDateTimeFormatter : IMessagePackFormatter<StringToValue<DateTime>>
    {
        public int Serialize(ref byte[] bytes, int offset, StringToValue<DateTime> value, IFormatterResolver formatterResolver)
        {
            return MessagePackBinary.WriteInt64(ref bytes, offset, value.GetValue().Ticks);
        }

        public StringToValue<DateTime> Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            long ticks = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
            return new StringToValue<DateTime>(new DateTime(ticks));
        }
    }
}