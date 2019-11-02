using System;
using System.Text;

namespace Common.Helpers
{
    public static class EnumHelpers
    {

        public static bool TryParseEnum<TEnum>(string value, bool ignoreCase, out TEnum result) where TEnum : struct, IConvertible
        {
            try
            {
                result = (TEnum)Enum.Parse(typeof(TEnum), value, ignoreCase);
                return true;
            }
            catch (Exception ex)
            {
                result = default(TEnum);
                return false;
            }
        }

        /// <summary>
        /// Turns a camel or pascal enum name to a human readable space seperated string 
        /// </summary>
        /// <param name="enm"></param>
        /// <param name="titleCase">whether to keep every first letter in every word capitalized or not</param>
        /// <returns></returns>
        public static string GetFriendly<TEnum>(TEnum enm, bool titleCase = true) where TEnum : struct, IConvertible
        {
            if (!enm.GetType().IsEnum)
                return null;
            string name = Enum.GetName(typeof(TEnum), enm);
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
    }
}
