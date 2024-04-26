using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TokenInformation
    {
        public string Token  { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Message { get; set; }
        public LoggedUser LoggedUser { get; set; }
    }
}
