using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Input;

namespace FileOrganizer.Model
{
    using KeyMapping = Dictionary<Key, DirectoryInfo>;
    /// <summary>
    /// Provides the KeyMap for the application to use. Also writes KeyMaps to file.
    /// </summary>    
    static class KeyMap
    {
        public static readonly string DefaultBinding = "[Space,]";
        public static readonly string DefaultSavePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static readonly string DefaultFileName = "KeyMappings.txt";
        public static readonly string DefaultSkipName = "FakeDirectory";

        // Generates a KeyMapping from text.
        public static KeyMapping ReadMapping(string mapText)
        {
            var keyMap = new KeyMapping();
            var entries = mapText.Split(new string[] {"\r\n"}, StringSplitOptions.None);
            foreach (var entry in entries)
            {
                var trimmedEntry = entry.Trim('[', ']', ' ');
                var pair = trimmedEntry.Split(',');
                if (string.IsNullOrEmpty(pair[0]))
                    continue;

                var dir = string.IsNullOrWhiteSpace(pair[1]) ? DefaultSkipName : pair[1];
                keyMap.Add((Key) new KeyConverter().ConvertFromString(pair[0]), new DirectoryInfo(dir));
            }

            return keyMap;
        }

        // Converts a KeyMapping to a readable text format.
        public static string MapToString(KeyMapping keyMap)
        {
            var output = new StringBuilder();
            foreach (var pair in keyMap)
            {
                output.AppendFormat("[{0},{1}]\n", pair.Key.ToString(), pair.Value.FullName);
            }
            return output.ToString();
        }

        // Writes a KeyMapping directly to file.
        public static void WriteMapping(KeyMapping keyMap)
        {
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(DefaultSavePath, DefaultFileName)))
            {
                outputFile.Write(MapToString(keyMap));
            }
        }
    }
}
