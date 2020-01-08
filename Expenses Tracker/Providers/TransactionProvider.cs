using Expenses_Tracker.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Expenses_Tracker.Providers
{
    public class TransactionProvider
    {
        private readonly AppSettings _settings;
        public TransactionProvider(IOptions<AppSettings> options)
        {
            _settings = options.Value;
        }

        public async Task<IEnumerable<TransactionModel>> GetTransactions(string userId)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                var query = @"SELECT * FROM Transactions WHERE user_id = @UserId";
                return await db.QueryAsync<TransactionModel>(query, new { UserId = userId });
            }

        }

        public void AddTransaction(string userId, TransactionModel transaction)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
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

        public void DeleteTransaction(string userId, string transactionId)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
            {
                string query = @"DELETE FROM Transactions WHERE id = @id AND user_id = @userId";
                var result = db.Query(query, new { transactionId, userId });
            }
        }

        public void UpdateTransaction(string userId, string transactionId, TransactionModel transaction)
        {
            using (var db = new SqlConnection(_settings.ConnectionString))
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
