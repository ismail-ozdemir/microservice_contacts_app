using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Application.Exceptions
{
    internal class RecordNotFoundException : Exception
    {
        // TODO : burası detaylandırılacak
        public RecordNotFoundException(string message) : base(message)
        {

        }
    }
}
