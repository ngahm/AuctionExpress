using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Models
{
    public class DateValidator : IValidatableObject
    {
        public DateValidator (DateTimeOffset dateOne, DateTimeOffset dateTwo)
            {
            DateOne = dateOne;
            DateTwo = dateTwo;
            ErrorMessage = "Auction Start Time must be before Auction Close Time";
            }

        public DateValidator(DateTimeOffset dateTwo)
        {
            DateOne = DateTimeOffset.Now;
            DateTwo = dateTwo;
            ErrorMessage = "Auction Close Time must be set later than current time.";
        }
        public DateTimeOffset DateOne { get; set; }
        public DateTimeOffset DateTwo { get; set; }
        public string ErrorMessage { get; set; }


       IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            List<ValidationResult> res = new List<ValidationResult>();
            if (DateTwo < DateOne)
            {
                ValidationResult mss = new ValidationResult(ErrorMessage);
                res.Add(mss);
            }
            return res;
        }
    }
}
