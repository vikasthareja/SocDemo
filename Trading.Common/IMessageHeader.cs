﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Common
{
    public interface IMessageHeader
    {
        long MessageKey { get; set; }

    }
}
