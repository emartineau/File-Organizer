using System;
using System.Collections.Generic;
using System.IO;

namespace FileOrganizer.ViewModel
{
    /// <summary>
    /// Contains all the possible actions one can take that can be keybound.
    /// </summary>
    public static class Commands
    {
        public delegate void Del(string file, string parameter);

        private static void MoveDelMethod(string file, string parameter)
        {
            File.Move(file, parameter);
        }
        private static void DeleteDelMethod(string file, string parameter)
        {
            File.Delete(file);
        }
        private static void CopyDelMethod(string file, string parameter)
        {
            File.Copy(file, parameter);
        }

        private static Del move = MoveDelMethod;
        private static Del delete = DeleteDelMethod;
        private static Del copy = CopyDelMethod;

        public static IDictionary<string, Del> Delegates { get; } = new Dictionary<string, Del>
        {
            { "move", move },
            { "delete", delete },
            { "copy", copy }
        };
    }
}
