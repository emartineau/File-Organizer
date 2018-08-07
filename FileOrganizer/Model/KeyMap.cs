using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Input;

namespace FileOrganizer.Model
{
    using KeyMapping = Dictionary<Key, (DirectoryInfo, CommandType)>;
    /// <summary>
    /// Provides the KeyMap for the application to use. Also writes KeyMaps to file.
    /// </summary>    
    static class KeyMap
    {
        public static readonly string DefaultBinding = "[Space,,move]";
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
                if (!Enum.TryParse(pair[0], out Key key) || Enum.TryParse(pair[2], out CommandType commandType))
                    continue;

                var dir = string.IsNullOrWhiteSpace(pair[1]) ? DefaultSkipName : pair[1];
                keyMap.Add(key, (new DirectoryInfo(dir), commandType));
            }

            return keyMap;
        }

        // Converts a KeyMapping to a readable text format.
        public static string MapToString(KeyMapping keyMap)
        {
            var output = new StringBuilder();
            foreach (var pair in keyMap)
            {
                output.AppendFormat("[{0},{1},{2}]\n",
                    pair.Key.ToString(),
                    pair.Value.Item1.FullName,
                    pair.Value.Item2.ToString());
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
