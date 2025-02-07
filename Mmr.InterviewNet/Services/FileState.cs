namespace Mmr.InterviewNet.Services
{
    public class FileState
    {
        public FileState(string path, int version, ChangeType changeType)
        {
            Path = path;
            ChangeType = changeType;
            Version = version;
        }

        public string Path { get; set; } = null!;
        public ChangeType ChangeType { get; set; } = ChangeType.None;
        public int Version { get; set; }
    }

    public enum ChangeType
    {
        None = 0,

        Added = 1,
        Modified = 2, 
        Deleted = 3,
    }
}
