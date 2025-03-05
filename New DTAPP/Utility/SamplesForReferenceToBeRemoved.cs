using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations;
namespace New_DTAPP.Utility
{
    public class SamplesForReferenceToBeRemoved : ValidationAttribute
    {

        private readonly string _requestCreatedAtPropertyName;
        private readonly string _sentTimePropertyName;
        private readonly string _reviewedAtPropertyName;
        private readonly string _completedAtPropertyName;

        public SamplesForReferenceToBeRemoved(string requestCreatedAtPropertyName, string sentTimePropertyName = "", string reviewedAtPropertyName = "", string completedAtPropertyName = "")
        {
            _requestCreatedAtPropertyName = requestCreatedAtPropertyName;
            _sentTimePropertyName = sentTimePropertyName;
            _reviewedAtPropertyName = reviewedAtPropertyName;
            _completedAtPropertyName = completedAtPropertyName;
        }


        public override bool IsValid(object? value)
        {
            return base.IsValid(value);
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {

            ErrorMessage = ErrorMessageString;

            DateTime currentDate;

            var requestCreatedAtProperty = validationContext.ObjectType.GetProperty(_requestCreatedAtPropertyName);
            if (requestCreatedAtProperty == null)
                throw new ArgumentException("Property with this name not found");

            var sentTimeProperty = validationContext.ObjectType.GetProperty(_sentTimePropertyName);
            if (sentTimeProperty == null)
                throw new ArgumentException("Property with this name not found");

            var reviewedAtProperty = validationContext.ObjectType.GetProperty(_reviewedAtPropertyName);
            if (reviewedAtProperty == null)
                throw new ArgumentException("Property with this name not found");

            var completedAtProperty = validationContext.ObjectType.GetProperty(_completedAtPropertyName);
            if (completedAtProperty == null)
                throw new ArgumentException("Property with this name not found");

            if (value != null && validationContext.ObjectInstance != null)
            {
                //This can be whatever, this is just an exmaple attribute that works with a number of properties in the TransferModel
                currentDate = (DateTime)value;
                var requestCreatedAtDate = (DateTime)requestCreatedAtProperty.GetValue(validationContext.ObjectInstance)!;
                var sentTimePropertyDate = (DateTime)sentTimeProperty.GetValue(validationContext.ObjectInstance)!;
                var reviewedAtPropertyValue = (DateTime)reviewedAtProperty.GetValue(validationContext.ObjectInstance)!;
                var completedAtPropertyValue = (DateTime)completedAtProperty.GetValue(validationContext.ObjectInstance)!;


                if (requestCreatedAtDate.Date <= currentDate.Date)
                    return new ValidationResult($"Request Created At Date must be equal or less than today's date.");

                //Sample... //This can be whatever, this is just an exmaple attribute that works with a number of properties in the TransferModel
                if (sentTimePropertyDate > currentDate.Date)
                    return new ValidationResult($"Sent Date is too far into the future - To be changed - just a test.");

            }

            return ValidationResult.Success!;

        }
    }
}
