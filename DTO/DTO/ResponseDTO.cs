using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InoviDataTransferObject.DTO
{
    public class ResponseDTO
    {
        public dynamic Response { get; set; }
        public bool HasError { get; set; } = false;
        public string ErrorMessage { get; set; } 


    }
}
