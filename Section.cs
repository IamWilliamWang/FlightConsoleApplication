using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightConsoleApplication
{
    class Section
    {
        public String mode { get; set; }
        public List<Item> itemList = new List<Item>();
        public Boolean hasCI = false;
    }
}
