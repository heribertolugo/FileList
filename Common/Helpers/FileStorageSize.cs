﻿using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Helpers
{
    public class FileStorageSize
    {
        private static ICollection<string> ByteVariants = new string[] { "byte", "bytes", "bt", "bts" };
        private static ICollection<string> KilobyteVariants = new string[] { "kilobyte", "kilobytes", "kb", "kbs" };
        private static ICollection<string> MegabyteVariants = new string[] { "megabyte", "megabytes", "mb", "mbs" };
        private static ICollection<string> GigabyteVariants = new string[] { "gigabyte", "gigabytes", "gb", "gbs" };
        private static ICollection<string> TerabyteVariants = new string[] { "terabyte", "terabytes", "tb", "tbs" };

        public static float ConvertStorageValueToKb(float value, StorageSizeType storageSize)
        {
            int multiplier = 1024;
            int sizeFactor;
            switch (storageSize)
            {
                case StorageSizeType.Kb:
                    sizeFactor = 0;
                    break;
                case StorageSizeType.Mb:
                    sizeFactor = 1;
                    break;
                case StorageSizeType.Gb:
                    sizeFactor = 2;
                    break;
                default:
                    return 0.0f;
            }
            return value * (float)Math.Pow((double)multiplier, (double)sizeFactor);
        }

        public static float ConvertStorageValueToKb(string value)
        {
            int multiplier = 1024;
            // split on spaces.. we are expecting a value similar to 20.3 mb
            // we take into account possibility of decimals or not.
            // but we will also handle values without spaces, such as 20.3mb, with or without decimals
            // storage type may or may not be abbreviated
            string[] values = value.Split();

            // handle values that did not include a space // this area has not been tested
            if (values.Length < 2)
            {
                bool hasDecimal = false;
                StringBuilder storageSizeBuilder = new StringBuilder();
                StringBuilder storageTypeBuilder = new StringBuilder();

                foreach (char karacter in value)
                {
                    // if we havent started building our storage type, we must be working on the size value itself
                    // just make sure we have acceptable values for the value of size, compatible with a float - excluding "e" and such scientific notations
                    if (storageTypeBuilder.Length == 0 && (char.IsDigit(karacter) || karacter.Equals('.') || karacter.Equals(',')))
                    {
                        if (hasDecimal && karacter.Equals('.'))
                            return 0.0f;

                        // grab 
                        if (!karacter.Equals(','))
                            storageSizeBuilder.Append(karacter);

                        if (karacter.Equals('.'))
                            hasDecimal = true;
                    }
                    else
                    {
                        // we started building our storage type, but we encountered a invalid character or unknown storage type
                        if (storageTypeBuilder.Length != 0 && char.IsNumber(karacter))
                            return 0.0f;
                        // continue building our storage type
                        if (storageSizeBuilder.Length != 0 && char.IsLetter(karacter))
                            storageTypeBuilder.Append(karacter);
                        // invalid data, or format does not meet our expectations
                        else if (storageSizeBuilder.Length == 0 && char.IsLetter(karacter))
                            return 0.0f;
                    }
                }

                // now that we have a friendly format, we can parse it
                values = new string[2]
                {
                    storageSizeBuilder.ToString(),
                    storageTypeBuilder.ToString()
                };
            }

            float storageSize;
            if (!float.TryParse(values[0], out storageSize))
                return 0.0f;

            StorageSizeType storageType;

            // we ran into plural.. make singular and try to parse
            if (values[1].Length == 3)
                values[1] = values[1].Substring(0, 2);
            if (Common.Helpers.EnumHelpers.TryParseEnum<StorageSizeType>(values[1], true, out storageType))
                return FileStorageSize.ConvertStorageValueToKb(storageSize, storageType);
            // if our parse was not successful, try other means
            if (FileStorageSize.KilobyteVariants.Contains(values[1].ToLowerInvariant()))
                storageType = StorageSizeType.Kb;
            else if (FileStorageSize.MegabyteVariants.Contains(values[1].ToLowerInvariant()))
                storageType = StorageSizeType.Mb;
            else if (FileStorageSize.GigabyteVariants.Contains(values[1].ToLowerInvariant()))
                storageType = StorageSizeType.Gb;
            else if (FileStorageSize.TerabyteVariants.Contains(values[1].ToLowerInvariant()))
            {
                storageSize /= (float)multiplier;
                storageType = StorageSizeType.Gb;
            }
            else
            {
                if (!FileStorageSize.ByteVariants.Contains(values[1].ToLowerInvariant()))
                    return 0.0f;
                storageSize /= (float)multiplier;
                storageType = StorageSizeType.Kb;
            }
            try
            {
                return FileStorageSize.ConvertStorageValueToKb(storageSize, storageType);
            }
            catch (Exception ex)
            {
                return 0.0f;
            }
        }
    }
}
