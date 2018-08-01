using System;
using System.Collections.Generic;
using System.IO;

namespace FileOrganizer.ViewModel
{
    /// <summary>
    /// May or may not get implemented. Contains all the possible actions one can take (that are keybound).
    /// </summary>
    static class Commands
    {
        private delegate void Del(string file, string parameter);

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

        public static IDictionary<string, Delegate> Delegates { get; } = new Dictionary<string, Delegate>
        {
            { "Copy", copy },
            { "Move", move },
            { "Delete", delete }
        };
    }
}
