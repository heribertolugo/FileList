using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileList.Models
{
    public interface IMultiComparer<T> : IComparer<T>
    {
        IEnumerable<IComparer<T>> Comparers { get; }
    }
}
