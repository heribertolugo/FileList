using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileList.Models
{
    public struct DateFilter
    {
        private FilterType _FilterType;
        private DateTime? _dateTime1;
        private DateTime? _dateTime2;

        public DateFilter(FilterType filterType, DateTime? dateTime1, DateTime? dateTime2)
        {
            this._FilterType = filterType;
            this._dateTime1 = dateTime1;
            this._dateTime2 = dateTime2;
        }

        public FilterType FilterType
        {
            get
            {
                return this._FilterType;
            }
            private set
            {
            }
        }

        public DateTime? DateTime1
        {
            get
            {
                return this._dateTime1;
            }
            private set
            {
            }
        }

        public DateTime? DateTime2
        {
            get
            {
                return this._dateTime2;
            }
            private set
            {
            }
        }
    }
}
