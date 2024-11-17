using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountTransactionService.Application.DTOs.Inputs
{
    public class CreateTransactionInputDto
    {
        public DateTime Date { get; set; }
        public char TransactionType { get; set; } // 'I' para ingreso, 'O' para salida
        public decimal Amount { get; set; }
        public int AccountId { get; set; }
    }
}
