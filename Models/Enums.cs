﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileList.Models
{
    public enum Filter
    {
        None,
        Size,
        DateModified,
        DateCreated,
    }

    public enum FilterType
    {
        None,
        Between,
        LessThan,
        GreaterThan,
        Equals,
    }

    public enum StorageSize
    {
        None,
        Kb,
        Mb,
        Gb,
    }
}
