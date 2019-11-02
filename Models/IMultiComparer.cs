using System.Collections.Generic;

namespace FileList.Models
{
    public interface IMultiComparer<T> : IComparer<T>
    {
        IEnumerable<IComparer<T>> Comparers { get; }
    }
}
