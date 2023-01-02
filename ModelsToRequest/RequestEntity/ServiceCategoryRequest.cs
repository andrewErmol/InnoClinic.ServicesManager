using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsToRequest.RequestEntity
{
    public class ServiceCategoryRequest
    {
        public string Name { get; set; }
        public int TimeSlotSizeInMinutes { get; set; }
    }
}
