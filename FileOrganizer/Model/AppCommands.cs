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
            organizer.WorkingDirectory
        }
    }
}
