namespace Common.Models
{
    public enum FileType
    {
        Unknown = 0,
        Text = 1,
        Media = 2,
        Browsable = 4,
        Application = 8,
        Image = 16, // 0x00000010
        Folder = 36, // 0x00000024
        Zip = 68, // 0x00000044
    }
}
