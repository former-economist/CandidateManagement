using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateManagement.Exceptions
{
    public class RecordNotDeletedException : Exception
    {
        public RecordNotDeletedException()
        {
        }

        public RecordNotDeletedException(string message)
            : base(message)
        {
        }

        public RecordNotDeletedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
