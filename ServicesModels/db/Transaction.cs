using System;

namespace ServicesModels.db
{
    [Serializable]
    public class Transaction
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public DateTime? TransactionDate { get; set; }
        public double Price { get; set; }

        public int ReservationId { get; set; }

        public virtual Reservation Reservation { get; set; }
    }
}