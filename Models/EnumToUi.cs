using System;
using System.Text;

namespace FileList.Models
{
    public struct EnumToUi<T> where T : struct, IConvertible
    {
        private T _value;
        private string _friendly;

        public EnumToUi(T enm)
        {
            this._value = enm;
            this._friendly = string.Empty;
            this._friendly = this.GetFriendly(enm);
        }

        public T Value
        {
            get
            {
                return this._value;
            }
            private set
            {
            }
        }

        public string Friendly
        {
            get
            {
                return this._friendly;
            }
            private set
            {
            }
        }

        public override string ToString()
        {
            return this._friendly;
        }

        private string GetFriendly(T enm)
        {
            string name = Enum.GetName(typeof(T), enm);
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < name.Length; ++index)
            {
                if (index - 1 > -1 && char.IsUpper(name[index]) && char.IsLower(name[index - 1]))
                    stringBuilder.Append(" ");
                stringBuilder.Append(name[index]);
            }
            return stringBuilder.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType().Equals(typeof(EnumToUi<T>)))
                return (ValueType)this.Value == (ValueType)((EnumToUi<T>)obj).Value;
            if (obj.GetType().Equals(typeof(T)))
                return (ValueType)this.Value == (ValueType)(T)obj;
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
