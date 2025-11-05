// File: FileStorage/FileReaders/JsonFileStore.cs

using System.Collections.Concurrent;
using System.Text.Json;
using FileStorage.FileReaders;

namespace FileStorage.WritersReaders
{
    public sealed class JsonFileStore : IFileReader, IWriter, IDisposable
    {
        private static readonly ConcurrentDictionary<string, SemaphoreSlim> Locks = new();
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private readonly string _filePath;
        private readonly SemaphoreSlim _semaphore;

        public JsonFileStore(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path must not be null or empty.", nameof(filePath));

            _filePath = Path.GetFullPath(filePath);
            _semaphore = Locks.GetOrAdd(_filePath, _ => new SemaphoreSlim(1, 1));
        }

        public async Task<Data> ReadFileAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                await using var fs = new FileStream(
                    _filePath,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.Read);

                var data = await JsonSerializer.DeserializeAsync<Data>(fs, JsonOptions);
                return data ?? throw new ArgumentException($"Problem with file {_filePath}. Content is not valid.");
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task Write(Data data)
        {
            if (data is null)
                throw new ArgumentNullException(nameof(data));

            await _semaphore.WaitAsync();
            try
            {
                var dir = Path.GetDirectoryName(_filePath);
                if (!string.IsNullOrEmpty(dir))
                    Directory.CreateDirectory(dir);

                using var fs = new FileStream(
                    _filePath,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None,
                    bufferSize: 4096,
                    useAsync: true);

                await JsonSerializer.SerializeAsync(fs, data, JsonOptions).ConfigureAwait(false);
                await fs.FlushAsync().ConfigureAwait(false);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public void Dispose()
        {
            _semaphore.Dispose();
        }
    }
}