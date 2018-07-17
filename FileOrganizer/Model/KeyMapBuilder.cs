using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileOrganizer.Model
{
    /**
     * Provides the KeyMap for the application to use. Also writes KeyMaps to file.
     */ 
    class KeyMap
    {
        private Dictionary<Char, DirectoryInfo> keyMap;

        public KeyMap()
        {

        }

        public Dictionary<Char, DirectoryInfo> GetKeyMap()
        {
            return keyMap;
        }

        private void ReadMapping(string mapText)
        {
            var entries = mapText.Split('\n');
            foreach (var entry in entries)
            {
                entry.Trim('[', ']');
                var pair = entry.Split(',');
                keyMap.Add(char.Parse(pair[0]), new DirectoryInfo(pair[1]));
            }
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            foreach (var pair in keyMap)
            {
                output.AppendFormat("[{0},{1}]\n", pair.Key.ToString(), pair.Value.FullName);
            }
            return output.ToString();
        }

        private void WriteMapping()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, "KeyMappings.txt")))
            {
                outputFile.Write(ToString());
            }
        }
    }
}
