using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SiteServer.Plugin
{
    /// <summary>
    /// ϵͳ֧�ֵ�ģ�����͡�
    /// </summary>
    [JsonConverter(typeof(TemplateTypeConverter))]
    public class TemplateType : IEquatable<TemplateType>, IComparable<TemplateType>
    {
        /// <summary>
        /// ��ҳģ�塣
        /// </summary>
        public static readonly TemplateType IndexPageTemplate = new TemplateType(nameof(IndexPageTemplate));

        /// <summary>
        /// ��Ŀģ�塣
        /// </summary>
        public static readonly TemplateType ChannelTemplate = new TemplateType(nameof(ChannelTemplate));

        /// <summary>
        /// ����ģ�塣
        /// </summary>
        public static readonly TemplateType ContentTemplate = new TemplateType(nameof(ContentTemplate));

        /// <summary>
        /// ��ҳģ�塣
        /// </summary>
        public static readonly TemplateType FileTemplate = new TemplateType(nameof(FileTemplate));

        internal TemplateType(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            Value = value;
        }

        /// <summary>
        /// �������͵�ֵ��
        /// </summary>
        public string Value { get; }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return Equals(obj as TemplateType);
        }

        /// <summary>
        /// �Ƚ��������������Ƿ�һ�¡�
        /// </summary>
        /// <param name="a">��Ҫ�Ƚϵ��������͡�</param>
        /// <param name="b">��Ҫ�Ƚϵ��������͡�</param>
        /// <returns>���һ�£���Ϊtrue������Ϊfalse��</returns>
        public static bool operator ==(TemplateType a, TemplateType b)
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
        /// �Ƚ��������������Ƿ�һ�¡�
        /// </summary>
        /// <param name="a">��Ҫ�Ƚϵ��������͡�</param>
        /// <param name="b">��Ҫ�Ƚϵ��������͡�</param>
        /// <returns>�����һ�£���Ϊtrue������Ϊfalse��</returns>
        public static bool operator !=(TemplateType a, TemplateType b)
        {
            return !(a == b);
        }

        /// <summary>
        /// �Ƚ��������������Ƿ�һ�¡�
        /// </summary>
        /// <param name="other">��Ҫ�Ƚϵ��������͡�</param>
        /// <returns>���һ�£���Ϊtrue������Ϊfalse��</returns>
        public bool Equals(TemplateType other)
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
        /// �Ƚ��������������Ƿ�һ�¡�
        /// </summary>
        /// <param name="other">��Ҫ�Ƚϵ��������͡�</param>
        /// <returns>���һ�£���Ϊ0������Ϊ1��</returns>
        public int CompareTo(TemplateType other)
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
    /// �ַ�����TemplateTypeת���ࡣ
    /// </summary>
    public class TemplateTypeConverter : JsonConverter
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
            return objectType == typeof(TemplateType);
        }

        /// <summary>
        /// ��д�����JSON��ʾ��
        /// </summary>
        /// <param name="writer">JsonWriter</param>
        /// <param name="value">ֵ</param>
        /// <param name="serializer">���л���</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var templateType = value as TemplateType;
            serializer.Serialize(writer, templateType != null ? templateType.Value : null);
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
            return string.IsNullOrEmpty(value) ? null : new TemplateType(value);
        }
    }
}