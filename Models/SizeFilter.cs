using Common.Models;
using FileList.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileList.Models
{
    public struct SizeFilter
    {
        private FilterType _filterType;
        private float _value1;
        private float? _value2;
        private StorageSizeType _storageSize1;
        private StorageSizeType? _storageSize2;

        public SizeFilter(
          FilterType filterType,
          float value1,
          StorageSizeType storageSize1,
          float? value2,
          StorageSizeType? storageSize2)
        {
            this._filterType = filterType;
            this._value1 = value1;
            this._value2 = value2;
            this._storageSize1 = storageSize1;
            this._storageSize2 = storageSize2;
        }

        public FilterType FilterType
        {
            get
            {
                return this._filterType;
            }
            private set
            {
            }
        }

        public float Value1
        {
            get
            {
                return this._value1;
            }
            private set
            {
            }
        }

        public StorageSizeType StorageSize1
        {
            get
            {
                return this._storageSize1;
            }
            private set
            {
            }
        }

        public float? Value2
        {
            get
            {
                return this._value2;
            }
            private set
            {
            }
        }

        public StorageSizeType? StorageSize2
        {
            get
            {
                return this._storageSize2;
            }
            private set
            {
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            if (this.FilterType != FilterType.None)
            {
                builder.Append(Common.Helpers.EnumHelpers.GetFriendly<FilterType>(this.FilterType, false));
                
                builder.Append($" {this.Value1} {Enum.GetName(typeof(StorageSizeType), this.StorageSize1)}");

                if (this.FilterType == FilterType.Between)
                    builder.Append(" and ");

                if (this.FilterType  == FilterType.Between && this.StorageSize2.HasValue)
                    builder.Append($"{this.Value2} {Enum.GetName(typeof(StorageSizeType), this.StorageSize2.Value)}");
            }

            return builder.ToString();
        }
    }
}
