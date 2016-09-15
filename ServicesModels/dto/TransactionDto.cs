using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesModels.dto
{
    [Serializable]
    public class TransactionDto
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public DateTime? TransactionDate { get; set; }
        public double Price { get; set; }

        public int ReservationId { get; set; }
    }
}
