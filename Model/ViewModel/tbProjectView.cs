using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
   public class tbProjectView
    {
        public class  tbProjectWhere
        {
            public int ProjectId { get; set; } = 0;

            public string ProjectName { get; set; } = string.Empty;

            public int TypeId { get; set; } = 0;
            public int CurrentPage { get; set; } = 0;
        }
        public class  tbProjectOrder
        {
            public int VoteNumber { get; set; } = 0;
        }

        public class tbProjectPage
        {
            public string ProjectId { get; set; } = string.Empty;
            public string TypeId { get; set; } = string.Empty;
            public string TypeName { get; set; } = string.Empty;
            public string OrganName { get; set; } = string.Empty;
            public string ProjectName { get; set; } = string.Empty;
            public string ProjectIntro { get; set; } = string.Empty;
            public string ProjectImages { get; set; } = string.Empty;
            public string VoteNumber { get; set; } = string.Empty;

            public string ProjectState { get; set; } = string.Empty;
            public string ProjectRemark { get; set; } = string.Empty;
        }
    }
}
