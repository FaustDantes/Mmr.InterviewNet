using System.Security.Cryptography;
using System.Text.Json;

namespace Mmr.InterviewNet.Services
{
    public interface IDirectoryScanner
    {
        IEnumerable<FileState> Scan(string directoryPath);
    }

    public class DirectoryScanner : IDirectoryScanner
    {
        private readonly string _stateFile = "directoryState.json";
        private readonly int _initialVersion = 1;

        public IEnumerable<FileState> Scan(string directoryPath)
        {
            var currentStates = GetDirectoryState(directoryPath);
            var previousStates = LoadPreviousState();
            var changes = CompareStates(previousStates, currentStates);
            SaveState(currentStates);
            
            return changes;
        }

        private Dictionary<string, FileMetadata> GetDirectoryState(string path)
        {
            return Directory.GetFiles(path, "*", SearchOption.AllDirectories)
                .ToDictionary(file => file, file => new FileMetadata { FileHash = ComputeFileHash(file), Version = _initialVersion }); 
        }

        private List<FileState> CompareStates(Dictionary<string, FileMetadata> previousStates, Dictionary<string, FileMetadata> currentStates)
        {
            var changes = new List<FileState>();

            foreach (var currentState in currentStates)
            {
                if (!previousStates.ContainsKey(currentState.Key))
                {
                    changes.Add(new FileState(currentState.Key, _initialVersion, ChangeType.Added));
                    continue;
                }

                if (previousStates[currentState.Key].FileHash != currentState.Value.FileHash)
                {
                    int newVersion = previousStates[currentState.Key].Version++;
                    changes.Add(new FileState(currentState.Key, newVersion, ChangeType.Modified));
                    currentStates[currentState.Key] = new FileMetadata { FileHash = currentState.Value.FileHash, Version = newVersion };
                }
            }

            changes.AddRange(previousStates.Where(state => !currentStates.ContainsKey(state.Key)).Select(state => new FileState(state.Key, state.Value.Version, ChangeType.Deleted)));

            return changes;
        }

        private void SaveState(Dictionary<string, FileMetadata> state)
        {
            var serialized = JsonSerializer.Serialize(state);
            File.WriteAllText(_stateFile, serialized);
        }

        private Dictionary<string, FileMetadata> LoadPreviousState()
        {
            if (!File.Exists(_stateFile)) return [];

            var content = File.ReadAllText(_stateFile);
            return JsonSerializer.Deserialize<Dictionary<string, FileMetadata>>(content) ?? [];
        }

        private static string ComputeFileHash(string filePath)
        {
            using var md5 = MD5.Create();
            using var stream = File.OpenRead(filePath);
            return BitConverter.ToString(md5.ComputeHash(stream));
        }
    }
}
