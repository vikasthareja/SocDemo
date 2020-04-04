using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Common
{
    public class MPSMessage
    {
        public IMessageHeader Header { get; set; }
        public object Payload { get; set; }
    }
}
