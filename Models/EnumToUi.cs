using System;

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
            this._friendly = Common.Helpers.EnumHelpers.GetFriendly<T>(enm);
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
