using Mmr.InterviewNet.Services;

namespace Mmr.InterviewNet.Models
{
    public class ScanDirectoryModel
    {
        public string DirectoryPath { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; }
        public IEnumerable<FileState>? Changes { get; set; }
    }
}
