using System.Collections.Concurrent;
using System.Text.Json;

namespace FileStorage.FileReaders;
    
    public class JsonFileReaderWriter : IFileReader, IWriter
    {
        private static readonly ConcurrentDictionary<string, SemaphoreSlim> Locks = new();
        private readonly string _fileName;
        private readonly SemaphoreSlim _semaphore;
    
        public JsonFileReaderWriter(string fileName)
        {
            _fileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            _semaphore = Locks.GetOrAdd(_fileName, _ => new SemaphoreSlim(1, 1));
        }
    
        public async Task<Data> ReadFileAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                using var fs = new FileStream(_fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var sr = new StreamReader(fs);
                var fileContent = await sr.ReadToEndAsync();
                var data = JsonSerializer.Deserialize<Data>(fileContent);
                return data ?? throw new ArgumentException($"Problem with file {_fileName}. Content is not valid.");
            }
            finally
            {
                _semaphore.Release();
            }
        }
    
        public async Task Write(Data data)
        {
            await _semaphore.WaitAsync();
            try
            {
                using var fs = new FileStream(_fileName, FileMode.Create, FileAccess.Write, FileShare.None);
                using var sw = new StreamWriter(fs);
                var textData = JsonSerializer.Serialize(data);
                await sw.WriteAsync(textData);
                await sw.FlushAsync();
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }