using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Services
{
    public class NotesServiceMessages
    {
        public const string REQUIRED = "{0} is required.";
        public const string INVALID = "Invalid {0}.";
        public const string MATCH = "{0} and {1} does not match.";
        public const string MIN_LENGTH = "{0} should alteast contain {1} characters.";
        public const string USER_ALREADY_EXISTS = "User already exists.";
    }
}
