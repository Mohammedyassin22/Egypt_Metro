﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorModels
{
    public class ValidationError
    {
        public string field { get; set; }
        public IEnumerable<string> errors { get; set; }
    }
}
