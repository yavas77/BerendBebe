using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.MvcHelpers.Enums
{
    public enum Status
    {
        Ok,
        Error,
        BadRequest,
        NotFound
    }
    public class JResult
    {
        public Status Status { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
    }
}
