using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expenses_Tracker.Controllers
{
    interface ITransactionsController
    {
        public void AllUserTransactions(Guid userId);
        public void TransactionDetails(Guid transactionId);
        public void VendorTransactions(Guid userId, string vendor); 
    }
}
