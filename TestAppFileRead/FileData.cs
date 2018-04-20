using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAppFileRead
{
    class FileData
    {

        public DateTime AccessTime { get; set; }
        public string ProcessName { get; set; }
        public int PID { get; set; }
        public string Path { get; set; }
        public int LengthOfMemory { get; set; }

    }
}
