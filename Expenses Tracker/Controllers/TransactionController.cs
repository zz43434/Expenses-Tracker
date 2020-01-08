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

namespace Expenses_Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private string _connectionString = "Server=localhost\\SQLEXPRESS;Database=TransactionDB;Trusted_Connection=True;";

        [HttpGet("/{userId}/transactions")]
        public async Task<IEnumerable<TransactionModel>> AllUserTransactions(string userId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var query = @"SELECT * FROM Transactions WHERE user_id = @UserId";
                return await db.QueryAsync<TransactionModel>(query, new { UserId = userId });
            }

        }

        [HttpPost("/{userId}/add")]
        public void AddTransaction(string userId, [FromBody]TransactionModel transaction)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO Transactions([id], [amount], [date], [type], [vendor], [category], [user_id]) values (@id, @amount, @date, @type, @vendor, @category, @user_id)";
                var result = db.Execute(query, new TransactionModel
                {
                    Id = Guid.NewGuid(),
                    Amount = transaction.Amount,
                    Date = transaction.Date,
                    Type = transaction.Type,
                    Vendor = transaction.Vendor,
                    Category = transaction.Category,
                    User_Id = transaction.User_Id
                });
            }
        }

        [HttpDelete("/{userId}/{transactionId}/delete")]
        public void DeleteTransaction(string userId, string transactionId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                string query = @"DELETE FROM Transactions WHERE id = @id AND user_id = @userId";
                var result = db.Query(query, new { transactionId, userId });
            }
        }

        [HttpPut("/{userId}/{transactionId}/update")]
        public void UpdateTransaction(string userId, string transactionId, [FromBody]TransactionModel transaction)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE Transactions SET amount = @amount, date = @date, type = @type, vendor = @vendor, category = @category WHERE id = @id AND user_id = @userId";
                var result = db.Execute(query, new
                {
                    amount = transaction.Amount,
                    date = transaction.Date,
                    type = transaction.Type,
                    vendor = transaction.Vendor,
                    category = transaction.Category,
                    id = transactionId,
                    userId
                });
            }
        }
    }
}