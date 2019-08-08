
using FileList.Views;
using System;
using System.Text;

namespace FileList.Logic
{
    public static class Misc
    {
        public static float ConvertStorageValueToKb(float value, FileFilterForm.StorageSize storageSize)
        {
            int num1 = 1024;
            int num2;
            switch (storageSize)
            {
                case FileFilterForm.StorageSize.Kb:
                    num2 = 0;
                    break;
                case FileFilterForm.StorageSize.Mb:
                    num2 = 1;
                    break;
                case FileFilterForm.StorageSize.Gb:
                    num2 = 2;
                    break;
                default:
                    return 0.0f;
            }
            return value * (float)Math.Pow((double)num1, (double)num2);
        }

        public static float ConvertStorageValueToKb(string value)
        {
            int num = 1024;
            string[] strArray = value.Split();
            if (strArray.Length < 2)
            {
                StringBuilder stringBuilder1 = new StringBuilder();
                StringBuilder stringBuilder2 = new StringBuilder();
                foreach (char c in value)
                {
                    if (stringBuilder2.Length == 0 && (char.IsDigit(c) || c.Equals('.') || c.Equals(',')))
                    {
                        if (!c.Equals(','))
                            stringBuilder2.Append(c);
                    }
                    else
                    {
                        if (stringBuilder2.Length != 0 && char.IsNumber(c))
                            return 0.0f;
                        if (stringBuilder1.Length != 0 && char.IsLetter(c))
                            stringBuilder2.Append(c);
                        else if (stringBuilder1.Length == 0 && char.IsLetter(c))
                            return 0.0f;
                    }
                }
                strArray = new string[2]
                {
          stringBuilder1.ToString(),
          stringBuilder2.ToString()
                };
            }
            float result1;
            if (!float.TryParse(strArray[0], out result1))
                return 0.0f;
            FileFilterForm.StorageSize result2;
            if (Misc.TryParseEnum<FileFilterForm.StorageSize>(strArray[1], true, out result2) || strArray[1].Length == 3 && strArray[1].ToLowerInvariant().Equals("s") && Misc.TryParseEnum<FileFilterForm.StorageSize>(strArray[1].Substring(0, 2), true, out result2))
                return Misc.ConvertStorageValueToKb(result1, result2);
            if (strArray[1].ToLowerInvariant().Equals("kilobyte") || strArray[1].ToLowerInvariant().Equals("kilobytes"))
                result2 = FileFilterForm.StorageSize.Kb;
            else if (strArray[1].ToLowerInvariant().Equals("megabyte") || strArray[1].ToLowerInvariant().Equals("megabytes"))
                result2 = FileFilterForm.StorageSize.Mb;
            else if (strArray[1].ToLowerInvariant().Equals("gigabyte") || strArray[1].ToLowerInvariant().Equals("gigabytes"))
                result2 = FileFilterForm.StorageSize.Gb;
            else if (strArray[1].ToLowerInvariant().Equals("terabyte") || strArray[1].ToLowerInvariant().Equals("terabytes") || strArray[1].ToLowerInvariant().Equals("tb") || strArray[1].ToLowerInvariant().Equals("tbs"))
            {
                result1 /= (float)num;
                result2 = FileFilterForm.StorageSize.Gb;
            }
            else
            {
                if (!strArray[1].ToLowerInvariant().Equals("byte") && !strArray[1].ToLowerInvariant().Equals("bytes") && !strArray[1].ToLowerInvariant().Equals("bt") && !strArray[1].ToLowerInvariant().Equals("bts"))
                    return 0.0f;
                result1 /= (float)num;
                result2 = FileFilterForm.StorageSize.Kb;
            }
            try
            {
                return Misc.ConvertStorageValueToKb(result1, result2);
            }
            catch (Exception ex)
            {
                return 0.0f;
            }
        }

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
    }
}

