using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Expenses_Tracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using Microsoft.Extensions.Options;
using Expenses_Tracker.Providers;

namespace Expenses_Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly AppSettings _connectionString;
        private TransactionProvider _transactionProvider;

        public TransactionController(IOptions<AppSettings> appSettings, TransactionProvider transactionProvider)
        {
            _connectionString = appSettings.Value;
            _transactionProvider = transactionProvider;
        }

        [HttpGet("/{userId}/transactions")]
        public async Task<IEnumerable<TransactionModel>> AllUserTransactions(string userId)
        {
            return await _transactionProvider.GetTransactions(userId);
        }

        [HttpPost("/{userId}/add")]
        public void AddTransaction(string userId, [FromBody]TransactionModel transaction)
        {
            _transactionProvider.AddTransaction(userId, transaction);
        }

        [HttpDelete("/{userId}/{transactionId}/delete")]
        public void DeleteTransaction(string userId, string transactionId)
        {
            _transactionProvider.DeleteTransaction(userId, transactionId);
        }

        [HttpPut("/{userId}/{transactionId}/update")]
        public void UpdateTransaction(string userId, string transactionId, [FromBody]TransactionModel transaction)
        {
            _transactionProvider.UpdateTransaction(userId, transactionId, transaction);
        }
    }
}