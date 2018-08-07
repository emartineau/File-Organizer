using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOrganizer.Model
{
    static class AppCommands
    {
        public delegate void AppDel(ref Organizer organizer);

        private static void ToParentDirectory(ref Organizer organizer)
        {
            organizer.WorkingDirectory = organizer.WorkingDirectory.Parent;
        }

        private static readonly AppDel toParentDirectory = ToParentDirectory;

        public static readonly IList<AppDel> Delegates = new List<AppDel>()
        {
            toParentDirectory
        };
    }
}
