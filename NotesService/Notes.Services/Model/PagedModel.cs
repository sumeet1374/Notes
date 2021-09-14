using System.Collections.Generic;

namespace Notes.Services.Model
{
    /// <summary>
    ///  Model to store paged data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedModel<T>
    {
        public IList<T> Result { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
    }
}
