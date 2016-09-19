using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.dto
{
    [Serializable]
    public class MessageDto
    {
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
