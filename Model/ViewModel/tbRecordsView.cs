using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class tbRecordsView
    {
        public class tbRecord
        {
            public string OpenId { get; set; } = string.Empty;
            public string ProjectIds { get; set; } = string.Empty;
            public string RecordDate { get; set; } = string.Empty;
        }
    }
}
