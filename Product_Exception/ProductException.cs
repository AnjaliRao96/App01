using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Exception
{
    [Serializable]   /// creating the exception for the product
    public class ProductException:Exception
    {
        public ProductException() { }
        public ProductException(string message) : base(message) { }
        public ProductException(string message, Exception inner) : base(message, inner) { }
    }
}
