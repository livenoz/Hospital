using HMS.Common.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace HMS.Common.Filters
{
    public class Filter
    {
        public int PageSize { get; set; } = Constant.PAGE_SIZE_DEFAULT;
        public int PageIndex { get; set; } = Constant.PAGE_INDEX_DEFAULT;
    }
}
