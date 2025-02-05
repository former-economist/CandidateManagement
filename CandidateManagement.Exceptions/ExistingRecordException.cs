using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateManagement.Exceptions
{
    public class ExistingRecordException : Exception
    {
        public ExistingRecordException()
        {
        }

        public ExistingRecordException(string message)
            : base(message)
        {
        }

        public ExistingRecordException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
