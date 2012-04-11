using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TextTransformer
{
    public class CustomTransformerFactory
    {
        private string _folderPath;

        public CustomTransformerFactory(string path)
        {
            _folderPath = path;
        }

        public List<TransformerBase> GetTransformers()
        {
            var ts = new List<TransformerBase>();

            var files = Directory.GetFiles(_folderPath);
            foreach (var file in files)
            {
                ts.Add(new TransformerFromFile(file) { Name = Path.GetFileNameWithoutExtension(file) });
            }

            return ts;
        }
    }
}