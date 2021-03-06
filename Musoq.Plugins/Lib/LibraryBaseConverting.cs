﻿using System;
using System.Globalization;
using System.Text;
using Musoq.Plugins.Attributes;

namespace Musoq.Plugins
{
    public partial class LibraryBase
    {
        [BindableMethod]
        public string ToHex(byte[] bytes, string delimiter = "")
        {
            if (bytes == null)
                return null;

            var hexBuilder = new StringBuilder();

            if (bytes.Length > 0)
            {
                for (int i = 0; i < bytes.Length - 1; i++)
                {
                    var byteValue = bytes[i];
                    hexBuilder.Append(byteValue.ToString("X2"));
                    hexBuilder.Append(delimiter);
                }

                hexBuilder.Append(bytes[bytes.Length - 1].ToString("X2"));
            }

            return hexBuilder.ToString();
        }

        [BindableMethod]
        public string ToBin(byte[] bytes, string delimiter = "")
        {
            if (bytes == null)
                return null;

            var builder = new StringBuilder();

            for (int i = 0; i < bytes.Length; ++i)
            {
                var binaryRepresentation = Convert
                    .ToString(bytes[i], 2)
                    .PadLeft(8, '0');

                builder.Append(binaryRepresentation);
                builder.Append(delimiter);
            }

            return builder.ToString();
        }

        [BindableMethod]
        public string ToHex<T>(T value) where T : IConvertible
        {
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Boolean:
                    return ToHex(GetBytes(value.ToBoolean(CultureInfo.CurrentCulture)));
                case TypeCode.Byte:
                    return ToHex(GetBytes(value.ToByte(CultureInfo.CurrentCulture)));
                case TypeCode.Char:
                    return ToHex(GetBytes(value.ToChar(CultureInfo.CurrentCulture)));
                case TypeCode.DateTime:
                    return "CONVERSION_NOT_SUPPORTED";
                case TypeCode.DBNull:
                    return "CONVERSION_NOT_SUPPORTED";
                case TypeCode.Decimal:
                    return ToHex(GetBytes(value.ToDecimal(CultureInfo.CurrentCulture)));
                case TypeCode.Double:
                    return ToHex(GetBytes(value.ToDouble(CultureInfo.CurrentCulture)));
                case TypeCode.Empty:
                    return "CONVERSION_NOT_SUPPORTED";
                case TypeCode.Int16:
                    return ToHex(GetBytes(value.ToInt16(CultureInfo.CurrentCulture)));
                case TypeCode.Int32:
                    return ToHex(GetBytes(value.ToInt32(CultureInfo.CurrentCulture)));
                case TypeCode.Int64:
                    return ToHex(GetBytes(value.ToInt64(CultureInfo.CurrentCulture)));
                case TypeCode.Object:
                    return "CONVERSION_NOT_SUPPORTED";
                case TypeCode.SByte:
                    return ToHex(GetBytes(value.ToSByte(CultureInfo.CurrentCulture)));
                case TypeCode.Single:
                    return ToHex(GetBytes(value.ToSingle(CultureInfo.CurrentCulture)));
                case TypeCode.String:
                    return ToHex(GetBytes(value.ToString(CultureInfo.CurrentCulture)));
                case TypeCode.UInt16:
                    return ToHex(GetBytes(value.ToUInt16(CultureInfo.CurrentCulture)));
                case TypeCode.UInt32:
                    return ToHex(GetBytes(value.ToUInt32(CultureInfo.CurrentCulture)));
                case TypeCode.UInt64:
                    return ToHex(GetBytes(value.ToUInt64(CultureInfo.CurrentCulture)));
            }

            return "CONVERSION_NOT_SUPPORTED";
        }

        [BindableMethod]
        public string ToBin<T>(T value) where T : IConvertible
        {
            return ToBase(value, 2);
        }

        [BindableMethod]
        public string ToOcta<T>(T value) where T : IConvertible
        {
            return ToBase(value, 8);
        }

        [BindableMethod]
        public string ToDec<T>(T value) where T : IConvertible
        {
            return ToBase(value, 10);
        }

        [BindableMethod]
        public decimal? ToDecimal(string value)
        {
            if (value == null)
                return null;

            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out decimal result))
                return result;

            return null;
        }

        [BindableMethod]
        public decimal? ToDecimal(string value, string culture)
        {
            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo(culture), out decimal result))
                return result;

            return null;
        }

        [BindableMethod]
        public TimeSpan? ToTimeSpan(string value) => ToTimeSpan(value, CultureInfo.CurrentCulture.Name);

        [BindableMethod]
        public TimeSpan? ToTimeSpan(string value, string culture)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            if (!TimeSpan.TryParse(value, CultureInfo.GetCultureInfo(culture), out var result))
                return null;

            return result;
        }

        [BindableMethod]
        public DateTime? ToDateTime(string value) => ToDateTime(value, CultureInfo.CurrentCulture.Name);

        [BindableMethod]
        public DateTime? ToDateTime(string value, string culture)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            if (!DateTime.TryParse(value, CultureInfo.GetCultureInfo(culture), DateTimeStyles.None, out var result))
                return null;

            return result;
        }

        [BindableMethod]
        public decimal? ToDecimal(long? value)
        {
            return value;
        }

        [BindableMethod]
        public decimal? ToDecimal(double? value)
        {
            if (value == null)
                return null;

            return Convert.ToDecimal(value.Value);
        }

        [BindableMethod]
        public long? ToInt64(string value)
        {
            if (long.TryParse(value, out var number))
                return number;

            return null;
        }

        [BindableMethod]
        public long? ToInt64(long? value)
        {
            return value;
        }

        [BindableMethod]
        public long? ToInt64(decimal? value)
        {
            if (value == null)
                return null;

            return Convert.ToInt64(value.Value);
        }

        [BindableMethod]
        public int? ToInt32(string value)
        {
            if (int.TryParse(value, out var number))
                return number;

            return null;
        }

        [BindableMethod]
        public int? ToInt32(long? value)
        {
            if (value == null)
                return null;

            return Convert.ToInt32(value.Value);
        }

        [BindableMethod]
        public int? ToInt32(decimal? value)
        {
            if (value == null)
                return null;

            return Convert.ToInt32(value.Value);
        }

        [BindableMethod]
        public char? ToChar(string value)
        {
            if (value == null)
                return null;

            if (value == string.Empty)
                return null;

            return value[0];
        }

        [BindableMethod]
        public char? ToChar(int? value)
        {
            if (value == null)
                return null;

            return (char)value;
        }

        [BindableMethod]
        public char? ToChar(short? value)
        {
            if (value == null)
                return null;

            return (char)value;
        }

        [BindableMethod]
        public char? ToChar(byte? value)
        {
            if (value == null)
                return null;

            return (char)value;
        }

        [BindableMethod]
        public string ToString(char? character)
        {
            if (character == null)
                return null;

            return character.ToString();
        }

        [BindableMethod]
        public string ToString(DateTimeOffset? date)
        {
            return date?.ToString(CultureInfo.CurrentCulture);
        }

        [BindableMethod]
        public string ToString(decimal? value)
        {
            return value?.ToString(CultureInfo.CurrentCulture);
        }

        [BindableMethod]
        public string ToString(long? value)
        {
            return value?.ToString(CultureInfo.CurrentCulture);
        }

        [BindableMethod]
        public string ToString(object obj)
        {
            if (obj == null)
                return null;

            return obj?.ToString();
        }

        [BindableMethod]
        public string ToString<T>(T obj)
            where T : class
        {
            if (obj == default(T))
                return null;

            return obj.ToString();
        }

        [BindableMethod]
        public string ToString(string[] obj)
        {
            var builder = new StringBuilder();

            for (int i = 0; i < obj.Length - 1; ++i)
            {
                builder.Append(obj[i]);
                builder.Append(',');
            }

            if (obj.Length > 0)
            {
                builder.Append(obj[obj.Length - 1]);
            }

            return builder.ToString();
        }

        [BindableMethod]
        public string ToString<T>(T[] obj)
        {
            var builder = new StringBuilder();

            for (int i = 0; i < obj.Length - 1; ++i)
            {
                builder.Append(obj[i].ToString());
                builder.Append(',');
            }

            if (obj.Length > 0)
            {
                builder.Append(obj[obj.Length - 1].ToString());
            }

            return builder.ToString();
        }

        [BindableMethod]
        public string ToBase64(byte[] array)
        {
            return Convert.ToBase64String(array, Base64FormattingOptions.None);
        }

        [BindableMethod]
        public string ToBase64(byte[] array, int offset, int length)
        {
            return Convert.ToBase64String(array, offset, length, Base64FormattingOptions.None);
        }

        [BindableMethod]
        public byte[] FromBase64(string base64String)
        {
            return Convert.FromBase64String(base64String);
        }

        private string ToBase<T>(T value, int baseNumber) where T : IConvertible
        {
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Boolean:
                    return Convert.ToString(value.ToBoolean(CultureInfo.CurrentCulture) ? 1 : 0, baseNumber);
                case TypeCode.Byte:
                    return Convert.ToString(value.ToByte(CultureInfo.CurrentCulture), baseNumber);
                case TypeCode.Char:
                    return Convert.ToString(value.ToChar(CultureInfo.CurrentCulture), baseNumber);
                case TypeCode.DateTime:
                    return "CONVERSION_NOT_SUPPORTED";
                case TypeCode.DBNull:
                    return "CONVERSION_NOT_SUPPORTED";
                case TypeCode.Decimal:
                    return "CONVERSION_NOT_SUPPORTED";
                case TypeCode.Double:
                    return Convert.ToString(BitConverter.DoubleToInt64Bits(value.ToDouble(CultureInfo.CurrentCulture)), baseNumber);
                case TypeCode.Empty:
                    return "CONVERSION_NOT_SUPPORTED";
                case TypeCode.Int16:
                    return Convert.ToString(value.ToInt16(CultureInfo.CurrentCulture), baseNumber);
                case TypeCode.Int32:
                    return Convert.ToString(value.ToInt32(CultureInfo.CurrentCulture), baseNumber);
                case TypeCode.Int64:
                    return Convert.ToString(value.ToInt64(CultureInfo.CurrentCulture), baseNumber);
                case TypeCode.Object:
                    return "CONVERSION_NOT_SUPPORTED";
                case TypeCode.SByte:
                    return Convert.ToString(value.ToSByte(CultureInfo.CurrentCulture), baseNumber);
                case TypeCode.Single:
                    return "CONVERSION_NOT_SUPPORTED";
                case TypeCode.String:
                    return "CONVERSION_NOT_SUPPORTED";
                case TypeCode.UInt16:
                    return Convert.ToString(value.ToUInt16(CultureInfo.CurrentCulture), baseNumber);
                case TypeCode.UInt32:
                    return Convert.ToString(value.ToUInt32(CultureInfo.CurrentCulture), baseNumber);
                case TypeCode.UInt64:
                    return "CONVERSION_NOT_SUPPORTED";
            }

            return "CONVERSION_NOT_SUPPORTED";
        }
    }
}
