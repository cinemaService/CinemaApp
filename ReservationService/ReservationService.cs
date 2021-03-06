﻿using System;
using System.Linq;
using ServicesModels.db;
using ServicesModels.dto;
using AbstractService;
using LoggingServiceModel;
using Config = ReservationServiceModels.Config;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace ReservationService
{
    public class ReservationService : AService<Reservation>
    {
        private TransactionService transactionService;
        private EmailService emailService;
        private MessageService messageService;

		public ReservationService(TransactionService transactionService, EmailService emailService, MessageService messageService, string name) : base(name)
		{
			this.transactionService = transactionService;
		    this.emailService = emailService;
		    this.messageService = messageService;
		}

		public void listen()
		{
			ReservationListener listener = new ReservationListener(this);
			base.listen(listener, Config.Url, Config.ReservQueueName);
		}

		public void Consume(Reservation reservationDto)
        {
			Console.WriteLine("Reservation request received.");
			writeToLog("Reservation request received.");

			var success = false;
            using (var db = new DatabaseContext())
            {
				var reservedSpots = db.Reservations
                    .Where(r => r.SeanceId == reservationDto.SeanceId)
                    .SelectMany(r => r.Spots)
                    .ToList();

                var reservation = reservedSpots.Any(s => reservationDto.Spots.Any(sDto => s.Id == sDto.Id));

                if (!reservation)
                {
                    var spotsId = reservationDto.Spots.Select(s => s.Id).ToArray();
                    reservationDto.Spots = db.Spots.Where(s => spotsId.Contains(s.Id)).ToList();
                    reservationDto.ReservationDate = DateTime.Now;
                    db.Reservations.Add(reservationDto);
                    success = true;
                }
                else
                {
                    emailService.SendRejection(reservationDto);
                    Console.WriteLine("At least one spot is already engaged.");
					writeToLog("At least one spot is already engaged.", LogMessage.LogType.WARNING);
				}

				try {
					db.SaveChanges();
				}
				catch(DbUpdateException e)
				{
					Console.WriteLine(e.InnerException.InnerException.Message);
					writeToLog(e.InnerException.InnerException.Message,LogMessage.LogType.ERROR);
					return;
                }

				Console.WriteLine("Reservation succeeded.");
				writeToLog("Reservation succeeded.");
			}

		    if (success)
		    {
		        transactionService.Send(reservationDto);
		        emailService.Send(reservationDto);
		        messageService.Send(reservationDto.UserEmail, "Reservation performed successfully");
		        Console.WriteLine("Reservation performed correctly");
		        writeToLog("Reservation performed correctly");
		    }
		    else
		    {
		        messageService.Send(reservationDto.UserEmail, "At least one spot was already engaged");
		    }
        }

		static void Main(string[] args)
		{
			TransactionService tranService = new TransactionService("Transaction Service - RS");
            EmailService emailService = new EmailService("Email Service - RS");
            MessageService messageService = new MessageService("Message Service - RS");
			ReservationService service = new ReservationService(tranService, emailService, messageService, "Reservation Service - RS");
			service.listen();
		}
	}
}