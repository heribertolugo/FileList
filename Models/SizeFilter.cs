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
        private int _value1;
        private int? _value2;
        private StorageSize _storageSize1;
        private StorageSize? _storageSize2;

        public SizeFilter(
          FilterType filterType,
          int value1,
          StorageSize storageSize1,
          int? value2,
          StorageSize? storageSize2)
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

        public int Value1
        {
            get
            {
                return this._value1;
            }
            private set
            {
            }
        }

        public StorageSize StorageSize1
        {
            get
            {
                return this._storageSize1;
            }
            private set
            {
            }
        }

        public int? Value2
        {
            get
            {
                return this._value2;
            }
            private set
            {
            }
        }

        public StorageSize? StorageSize2
        {
            get
            {
                return this._storageSize2;
            }
            private set
            {
            }
        }
    }
}
