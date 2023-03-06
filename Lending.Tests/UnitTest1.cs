using Lending.Domain;

namespace Lending.Tests
{
    /// <summary>
    /// Some basic unit tests
    /// </summary>
    public class Tests
    {
        Lender lender = new Lender();
        
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Application_Fails_When_Loan_Is_Less_Than_One_Hundred_Thousand()
        {
            var applicant = Applicant.Create("Test", 990000, 2000000, 500, "GBP");

            var loanResult = lender.Apply(applicant);

            Assert.IsFalse(loanResult.result);
            Assert.That(loanResult.message, Is.EqualTo("Loan amount must be between £100,000 and £1,500,000."));
        }

        [Test]
        public void Application_Fails_When_Loan_Is_More_Than_OneAndHalfMillion()
        {
            var applicant = Applicant.Create("Test", 1600000, 2000000, 500, "GBP");

            var loanResult = lender.Apply(applicant);

            Assert.IsFalse(loanResult.result);
            Assert.That(loanResult.message, Is.EqualTo("Loan amount must be between £100,000 and £1,500,000."));
        }

        [Test]
        public void Application_Fails_When_Loan_Is_More_Than_OneMillion_And_LTV_More_Than_60_percentage()
        {
            var applicant = Applicant.Create("Test", 1000000, 1400000, 955, "GBP");

            var loanResult = lender.Apply(applicant);

            Assert.IsFalse(loanResult.result);
            Assert.That(loanResult.message, Is.EqualTo("If Loan amount more than £1,000,000 then the LTV must be 60% or less and credit score must be 950 or more."));
        }

        [Test]
        public void Application_Fails_When_Loan_Is_More_Than_OneMillion_And_CreditScore_Less_Than_950()
        {
            var applicant = Applicant.Create("Test", 1000000, 1800000, 949, "GBP");

            var loanResult = lender.Apply(applicant);

            Assert.IsFalse(loanResult.result);
            Assert.That(loanResult.message, Is.EqualTo("If Loan amount more than £1,000,000 then the LTV must be 60% or less and credit score must be 950 or more."));
        }

        [Test]
        public void Application_Fails_When_Loan_Is_Less_Than_OneMillion_And_LTV_LessThan_60_PerCentage_And_CreditScore_Less_Than_750()
        {
            var applicant = Applicant.Create("Test", 900000, 1800000, 745, "GBP");

            var loanResult = lender.Apply(applicant);

            Assert.IsFalse(loanResult.result);
            Assert.That(loanResult.message, Is.EqualTo("If Loan less than £1million and LTV less than 60% then credit score must be 750 or more"));
        }
    }
}