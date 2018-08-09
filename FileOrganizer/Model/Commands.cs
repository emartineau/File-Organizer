using System.Collections.Generic;
using System.IO;

namespace FileOrganizer.Model
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

        private static Del Move = MoveDelMethod;
        private static Del Delete = DeleteDelMethod;
        private static Del Copy = CopyDelMethod;

        public static IDictionary<CommandType, Del> Delegates { get; } = new Dictionary<CommandType, Del>
        {
            { CommandType.MOVE, Move },
            { CommandType.DELETE, Delete },
            { CommandType.COPY, Copy }
        };
    }
}
