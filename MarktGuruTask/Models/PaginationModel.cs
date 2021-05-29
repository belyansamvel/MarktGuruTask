using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarktGuruTask.Models
{
    public class PaginationModel
    {
        public int Offset { get; set; } = 0;
        public int Count { get; set; } = 100;
    }
}
