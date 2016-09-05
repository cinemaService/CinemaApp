using System;

namespace ServicesModels.dto
{
    [Serializable]
    public class ReservationDto
    {
        public int Id { get; set; }

        public int SeanceId { get; set; }

        public int[] Spots { get; set; }

        public string Email { get; set; }
    }
}