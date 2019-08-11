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
            if (!enm.GetType().IsEnum)
                throw new ArgumentException();
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

        /// <summary>
        /// Turns a camel or pascal enum name to a human readable space seperated string 
        /// </summary>
        /// <param name="enm"></param>
        /// <param name="titleCase">whether to keep every first letter in every word capitalized or not</param>
        /// <returns></returns>
        private string GetFriendly(T enm, bool titleCase = true)
        {
            if (!enm.GetType().IsEnum)
                return null;
            string name = Enum.GetName(typeof(T), enm);
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < name.Length; ++index)
            {
                if ((index - 1) > -1 && char.IsUpper(name[index]) && char.IsLower(name[index - 1]))
                    stringBuilder.Append(" ");
                char kar = name[index];
                if (!titleCase && index > 0)
                    kar = char.ToLower(kar);
                stringBuilder.Append(kar);
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
