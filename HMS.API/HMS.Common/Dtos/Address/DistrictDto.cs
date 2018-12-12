using System;
using System.Collections.Generic;
using System.Text;

namespace HMS.Common.Dtos.Address
{
    public class DistrictDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int ProvinceId { get; set; }
    }
}
