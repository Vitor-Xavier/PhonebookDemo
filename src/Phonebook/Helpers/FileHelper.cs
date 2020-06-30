using System.IO;
using System.Threading.Tasks;

namespace Phonebook.Helpers
{
    public static class FileHelper
    {
        public static async Task WriteLogFile(string path, string text)
        {
            if (!File.Exists(path))
            {
                using var sw = File.CreateText(path);
                await sw.WriteLineAsync(text);
            }
            else
            {
                using var sw = File.AppendText(path);
                await sw.WriteLineAsync(text);
            }
        }
    }
}
