using System;


namespace ReservationService
{
    class Program
    {
        static void Main(string[] args)
        {
			TransactionService tranService = new TransactionService("Transaction Service");
			ReservationService service = new ReservationService(tranService,"Reservation Service");
			service.listen();
        }
    }
}
