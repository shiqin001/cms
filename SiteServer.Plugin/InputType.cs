using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SiteServer.Plugin
{
    /// <summary>
    /// �����������͡�
    /// </summary>
    [JsonConverter(typeof(InputTypeConverter))]
    public class InputType : IEquatable<InputType>, IComparable<InputType>
    {
        /// <summary>
        /// �ı������
        /// </summary>
        public static readonly InputType Text = new InputType(nameof(Text));

        /// <summary>
        /// �����ı������
        /// </summary>
        public static readonly InputType TextArea = new InputType(nameof(TextArea));

        /// <summary>
        /// ���ı��༭����
        /// </summary>
        public static readonly InputType TextEditor = new InputType(nameof(TextEditor));

        /// <summary>
        /// ��ѡ�
        /// </summary>
        public static readonly InputType CheckBox = new InputType(nameof(CheckBox));

        /// <summary>
        /// ��ѡ�
        /// </summary>
        public static readonly InputType Radio = new InputType(nameof(Radio));

        /// <summary>
        /// ��ѡ������
        /// </summary>
        public static readonly InputType SelectOne = new InputType(nameof(SelectOne));

        /// <summary>
        /// ��ѡ������
        /// </summary>
        public static readonly InputType SelectMultiple = new InputType(nameof(SelectMultiple));

        /// <summary>
        /// ����ѡ��������
        /// </summary>
        public static readonly InputType SelectCascading = new InputType(nameof(SelectCascading));

        /// <summary>
        /// ����ѡ���
        /// </summary>
        public static readonly InputType Date = new InputType(nameof(Date));

        /// <summary>
        /// ���ڼ�ʱ��ѡ���
        /// </summary>
        public static readonly InputType DateTime = new InputType(nameof(DateTime));

        /// <summary>
        /// ͼƬ�ϴ��ؼ���
        /// </summary>
        public static readonly InputType Image = new InputType(nameof(Image));

        /// <summary>
        /// ����Ƶ�ϴ��ؼ���
        /// </summary>
        public static readonly InputType Video = new InputType(nameof(Video));

        /// <summary>
        /// �ļ��ϴ��ؼ���
        /// </summary>
        public static readonly InputType File = new InputType(nameof(File));

        /// <summary>
        /// �Զ�������ؼ���
        /// </summary>
        public static readonly InputType Customize = new InputType(nameof(Customize));

        /// <summary>
        /// �����
        /// </summary>
        public static readonly InputType Hidden = new InputType(nameof(Hidden));

        internal InputType(string value)
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
            return Equals(obj as InputType);
        }

        /// <summary>
        /// �Ƚ��������������Ƿ�һ�¡�
        /// </summary>
        /// <param name="a">��Ҫ�Ƚϵ��������͡�</param>
        /// <param name="b">��Ҫ�Ƚϵ��������͡�</param>
        /// <returns>���һ�£���Ϊtrue������Ϊfalse��</returns>
        public static bool operator ==(InputType a, InputType b)
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
        public static bool operator !=(InputType a, InputType b)
        {
            return !(a == b);
        }

        /// <summary>
        /// �Ƚ��������������Ƿ�һ�¡�
        /// </summary>
        /// <param name="other">��Ҫ�Ƚϵ��������͡�</param>
        /// <returns>���һ�£���Ϊtrue������Ϊfalse��</returns>
        public bool Equals(InputType other)
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
        public int CompareTo(InputType other)
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
    /// �ַ�����InputTypeת���ࡣ
    /// </summary>
    public class InputTypeConverter : JsonConverter
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
            return objectType == typeof(InputType);
        }

        /// <summary>
        /// ��д�����JSON��ʾ��
        /// </summary>
        /// <param name="writer">JsonWriter</param>
        /// <param name="value">ֵ</param>
        /// <param name="serializer">���л���</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var inputType = value as InputType;
            serializer.Serialize(writer, inputType != null ? inputType.Value : null);
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
            return string.IsNullOrEmpty(value) ? null : new InputType(value);
        }
    }
}