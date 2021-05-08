using System;
using System.IO;

namespace Core
{
    public class LocalContentService
    {
        private readonly string contentPath;
        private readonly string contentFile;

        public LocalContentService()
        {
            this.contentPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "MiniNotes");
            this.contentFile = Path.Combine(contentPath, "content.txt");
        }

        public string GetContent()
        {
            if (!File.Exists(this.contentFile))
                return string.Empty;

            return File.ReadAllText(this.contentFile);
        }

        public void StoreContent(string content)
        {
            if (!Directory.Exists(this.contentPath))
                Directory.CreateDirectory(this.contentPath);

            File.WriteAllText(this.contentFile, content);
        }
    }
}