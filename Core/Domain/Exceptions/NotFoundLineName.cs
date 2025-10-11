using Core.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class NotFoundLineName(string message) : NotFoundException($"Line name not found: {message}")
    {
    }
}
