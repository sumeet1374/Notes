using System;

namespace Notes.Services.Model
{
    /// <summary>
    ///  Specific exception considerting data validation
    /// </summary>
    public class ValidationException:Exception
    {
        public ValidationException(string message):base(message)
        {

        }
    }
}
