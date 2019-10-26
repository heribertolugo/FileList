namespace FileList.Models
{
    public enum Filter
    {
        None,
        Name,
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
}
