using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestEx
{
    public class File
    {
        private string extension;
        private string filename;
        private string content;
        private double size;
         
        public File(String filename, String content)
        {
            this.filename = filename;
            this.content = content;
            this.size = content.Length / 2.0; // исправил деление на double

            // исправил проверку на наличие точки
            if (filename.Contains("."))
            {
                this.extension = filename.Split('.')[filename.Split('.').Length - 1];
            }
            else
            {
                this.extension = "";
            }
        }

        public double GetSize()
        {
            return size; // исправил возвращает double, не приводим к int
        }

        public string GetFilename()
        {
            return filename;
        }
    }
}
