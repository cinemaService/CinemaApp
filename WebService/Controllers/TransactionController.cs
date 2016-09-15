using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicesModels.db;
using WebService.Logic;

namespace WebService.Controllers
{
    public class TransactionController : Controller
    {
        DatabaseContext db = new DatabaseContext();

        // GET: Transaction
        public ActionResult Index(int id)
        {
            Transaction transaction;

            try
            {
                transaction = db.Transactions.Single(t => t.Id == id);

                if (transaction.TransactionDate != null)
                {
                    return RedirectToAction("PaymentDone");
                }
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }

            return View(transaction);
        }

        [HttpPost]
        public ActionResult Pay(int id)
        {
            var transactionSender = new TransactionSender("WebService");

            transactionSender.send(id);

            return RedirectToAction("PaymentDone");
        }

        public ActionResult PaymentDone()
        {
            return View();
        }
    }
}