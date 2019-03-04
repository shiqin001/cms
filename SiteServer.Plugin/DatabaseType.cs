using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SiteServer.Plugin
{
    /// <summary>
    /// ϵͳ֧�ֵ����ݿ�����
    /// </summary>
    [JsonConverter(typeof(DatabaseTypeConverter))]
    public class DatabaseType : IEquatable<DatabaseType>, IComparable<DatabaseType>
    {
        /// <summary>
        /// MySql ���ݿ�
        /// </summary>
        public static readonly DatabaseType MySql = new DatabaseType(nameof(MySql));

        /// <summary>
        /// SqlServer ���ݿ�
        /// </summary>
        public static readonly DatabaseType SqlServer = new DatabaseType(nameof(SqlServer));

        /// <summary>
        /// PostgreSql ���ݿ�
        /// </summary>
        public static readonly DatabaseType PostgreSql = new DatabaseType(nameof(PostgreSql));

        /// <summary>
        /// Oracle ���ݿ�
        /// </summary>
        public static readonly DatabaseType Oracle = new DatabaseType(nameof(Oracle));

        internal DatabaseType(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            Value = value;
        }

        /// <summary>
        /// ���ݿ����͵�ֵ��
        /// </summary>
        public string Value { get; }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return Equals(obj as DatabaseType);
        }

        /// <summary>
        /// �Ƚ��������ݿ������Ƿ�һ�¡�
        /// </summary>
        /// <param name="a">��Ҫ�Ƚϵ����ݿ����͡�</param>
        /// <param name="b">��Ҫ�Ƚϵ����ݿ����͡�</param>
        /// <returns>���һ�£���Ϊtrue������Ϊfalse��</returns>
        public static bool operator ==(DatabaseType a, DatabaseType b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if ((object) a == null || (object) b == null)
            {
                return false;
            }

            return a.Equals(b);
        }

        /// <summary>
        /// �Ƚ��������ݿ������Ƿ�һ�¡�
        /// </summary>
        /// <param name="a">��Ҫ�Ƚϵ����ݿ����͡�</param>
        /// <param name="b">��Ҫ�Ƚϵ����ݿ����͡�</param>
        /// <returns>�����һ�£���Ϊtrue������Ϊfalse��</returns>
        public static bool operator !=(DatabaseType a, DatabaseType b)
        {
            return !(a == b);
        }

        /// <summary>
        /// �Ƚ��������ݿ������Ƿ�һ�¡�
        /// </summary>
        /// <param name="other">��Ҫ�Ƚϵ����ݿ����͡�</param>
        /// <returns>���һ�£���Ϊtrue������Ϊfalse��</returns>
        public bool Equals(DatabaseType other)
        {
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return
                Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// �Ƚ��������ݿ������Ƿ�һ�¡�
        /// </summary>
        /// <param name="other">��Ҫ�Ƚϵ����ݿ����͡�</param>
        /// <returns>���һ�£���Ϊ0������Ϊ1��</returns>
        public int CompareTo(DatabaseType other)
        {
            if (other == null)
            {
                return 1;
            }

            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            return StringComparer.OrdinalIgnoreCase.Compare(Value, other.Value);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return EqualityComparer<string>.Default.GetHashCode(Value);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Value;
        }
    }

    /// <summary>
    /// �ַ�����DatabaseTypeת���ࡣ
    /// </summary>
    public class DatabaseTypeConverter : JsonConverter
    {
        /// <summary>
        /// ȷ����ʵ���Ƿ����ת��ָ���Ķ������͡�
        /// </summary>
        /// <param name="objectType">����ʵ��</param>
        /// <returns>
        /// <c>true</c> ������ʵ������ת��ָ���Ķ�������; ����, <c>false</c>��
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DatabaseType);
        }

        /// <summary>
        /// ��д�����JSON��ʾ��
        /// </summary>
        /// <param name="writer">JsonWriter</param>
        /// <param name="value">ֵ</param>
        /// <param name="serializer">���л���</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var databaseType = value as DatabaseType;
            serializer.Serialize(writer, databaseType != null ? databaseType.Value : null);
        }

        /// <summary>
        /// ��ȡ�����JSON��ʾ��
        /// </summary>
        /// <param name="reader">JsonReader</param>
        /// <param name="objectType">��������</param>
        /// <param name="existingValue">���ڶ�ȡ�Ķ��������ֵ</param>
        /// <param name="serializer">���л���</param>
        /// <returns>���ض���</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var value = (string)reader.Value;
            return string.IsNullOrEmpty(value) ? null : new DatabaseType(value);
        }
    }
}