using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAppFileRead
{
    public class ProcessData
    {
        public string ProcessName { get; set; }
        public long ProcessLength { get; set; }
        public long ProcessOffset { get; set; }
        public string ProcessPID { get; set; }
        public string ProcessPath { get; set; }
        public string ProcessKey { get; set; }
    }
}
