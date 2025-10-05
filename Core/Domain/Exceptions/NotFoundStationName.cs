using Core.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exception
{
    public class NotFoundStationName(string message):NotFoundException($"Station name not found: {message}")
    {
    }
}
