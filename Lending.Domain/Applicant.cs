using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lending.Domain
{
    public record Applicant(string name, decimal loanAmount, decimal assetValue, int creditScore, string currency)
    {
        public static Applicant Create(string name, decimal loanAmount, decimal assetValue, int creditScore, string currency)
           => new(name ?? throw new ArgumentNullException("Name cannot be empty."),
                  loanAmount < 1.0M ? throw new ArgumentException("Loan Amount must be greater or equal to 1.") : loanAmount,
                  assetValue < 1.0M ? throw new ArgumentException("Asset Value must be greater or equal to 1.") : assetValue,
                  creditScore < 1 || creditScore > 999 ? throw new ArgumentException("Credit Score must be between 1 and 999.") : creditScore,
                  currency ?? throw new ArgumentNullException("Currency cannot be empty."));
                 
        public decimal LTV => (loanAmount / assetValue) * 100;

        public bool LoanResult { get; set; }

        public DateTime Applied { get; set; }
    }
}
         
