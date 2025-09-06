using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Exceptions
{
    public class OrderNotFoundException:ApplicationException
    {
        public OrderNotFoundException(string name , Object Key):base($"Entity {name} -{Key} is not found")
        {
            
        }
    }
}
