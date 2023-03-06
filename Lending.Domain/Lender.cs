namespace Lending.Domain
{
    /// <summary>
    /// Engine to evaluate loans against a list of specifications.
    /// Applicants are just stored in a static list in memory.
    /// Not using any DI.
    /// </summary>
    public class Lender
    {
        private static IList<Applicant> applicants = new List<Applicant>();

        private readonly IList<LoanSpecification> loanSpecifications; 
                               
        public Lender()
        {
            loanSpecifications = new List<LoanSpecification>()
            {
                new LoanAmountRangeSpecification(),
                new LoanAmountMoreThanEqual1Million(),
                new LoanAmountLessThan1Million()
            };
        }

        public (bool result, string message) Apply(Applicant applicant)
        {
            applicant.Applied = DateTime.Now;
           
            var loanResult = EvaluateLoan(applicant);

            applicant.LoanResult = loanResult.result;
            applicants.Add(applicant);

            return loanResult;
        }

        public List<Tuple<string,int>> GetNumberApplicants()
        {
            var groupedApplicants = applicants.GroupBy(app => app.LoanResult)
                        .Select(group => Tuple.Create(group.Key ? "Succeeded" : "Failed", group.Count()))
                        .ToList();

            return groupedApplicants;
        }

        public decimal GetTotalValueOfLoans()
        {
            var totalValue = applicants
                            .Where(a => a.LoanResult == true)
                            .Sum(x => x.loanAmount);

            return totalValue;
        }

        public decimal GetAverageLTV()
        {
            var averageLTV = applicants
                             .Average(a => a.LTV);

            return averageLTV;
        }

        /// <summary>
        /// Checks applicant against list of loan specifications
        /// </summary>
        /// <param name="applicant"></param>
        /// <returns>result of application and any failed message</returns>
        private (bool result, string message) EvaluateLoan(Applicant applicant)
        {
            foreach(var spec in loanSpecifications)
            {
                if (!spec.IsSpecifiedBy(applicant))
                {
                    return (false, spec.RejectReason);
                }
            }

            return (true, string.Empty);
        }
    }
}