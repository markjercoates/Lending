using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lending.Domain
{
    /// <summary>
    /// Provide a contract to handle a list of loan rules
    /// </summary>
    public interface LoanSpecification
    {
        bool IsSpecifiedBy(Applicant applicant);
        string RejectReason { get; }
    }

    public class LoanAmountRangeSpecification : LoanSpecification
    {
        public bool IsSpecifiedBy(Applicant applicant)
        {
            if (applicant.loanAmount < 100000 || applicant.loanAmount > 1500000)
            {
                return false;
            }

            return true;
        }

        public string RejectReason
        {
            get { return "Loan amount must be between £100,000 and £1,500,000."; }
        }
    }

    public class LoanAmountMoreThanEqual1Million : LoanSpecification
    {
        public bool IsSpecifiedBy(Applicant applicant)
        {
            if (applicant.loanAmount >= 1000000)
            {
                if (applicant.LTV > 60 || applicant.creditScore < 950)
                {
                    return false;
                }
            }

            return true;
        }

        public string RejectReason
        {
            get { return "If Loan amount more than £1,000,000 then the LTV must be 60% or less and " +
                    "credit score must be 950 or more."; }
        }
    }

    public class LoanAmountLessThan1Million : LoanSpecification
    {
        string rejectMessage = string.Empty;
        public bool IsSpecifiedBy(Applicant applicant)
        {            
            if (applicant.loanAmount < 1000000)
            {
                if (applicant.LTV < 60 && applicant.creditScore < 750)
                {
                    rejectMessage = "If Loan less than £1million and LTV less than 60% then credit score must be 750 or more";
                    return false;
                }
                else if (applicant.LTV < 80 && applicant.creditScore < 800)
                {
                    rejectMessage = "If Loan less than £1million and LTV less than 80% then credit score must be 800 or more";
                    return false;
                }
                else if (applicant.LTV < 90 && applicant.creditScore < 900)
                {
                    rejectMessage = "If Loan less than £1million and LTV less than 90% then credit score must be 900 or more";
                    return false;
                }
                else if(applicant.LTV > 90)
                {
                    rejectMessage = "If Loan less than £1million then LTV must be less than 90%.";
                    return false;
                }

                return true;
            }

            return true;
        }

        public string RejectReason
        {
            get
            {
                return rejectMessage;
            }
        }
    }
}
