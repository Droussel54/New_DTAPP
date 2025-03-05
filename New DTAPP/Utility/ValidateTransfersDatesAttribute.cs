using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
namespace New_DTAPP.Utility
{

    public enum DateValidationType
    {
        RequestCreatedAt,
        SentTime,
        ReviewedAtDate,
        CompletedAtDate
    }
    
    public class ValidateTransfersDatesAttribute : ValidationAttribute //,Add later IClientModelValidator
    {
        private readonly DateValidationType _dateValidationType;
        private readonly string _requestCreatedAtPropertyName;
        private readonly string _sentTimePropertyName;
        private readonly string _reviewedAtPropertyName;
        private readonly string _completedAtPropertyName;

        public ValidateTransfersDatesAttribute( 
                DateValidationType dateValidationType,  
                string requestCreatedAtPropertyName, 
                string sentTimePropertyName = "", 
                string reviewedAtPropertyName = "", 
                string completedAtPropertyName = ""
            )
        { 
            _dateValidationType = dateValidationType;
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

            switch (_dateValidationType)
            {
                case DateValidationType.RequestCreatedAt:

                    return IsRequestCreatedAtCanNotBetGreaterThanNow(value, validationContext);

                case DateValidationType.SentTime:

                   return  IsSentDateCannotBeAfterRequestCreatedAtDate(value, validationContext);

                case DateValidationType.ReviewedAtDate:

                    return IsReviewedAtDateCannotBeBeforeRequestCreatedAtDate(value, validationContext);

                case DateValidationType.CompletedAtDate:

                    return IsCompletedAtDateCannotBeBeforeRequestCreatedAtDate(value, validationContext);

                default:
                    
                    return ValidationResult.Success!;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private ValidationResult IsCompletedAtDateCannotBeBeforeRequestCreatedAtDate(object? value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;

            DateTime completedAtDate;
            DateTime requestCreatedAtPropertyDate;

            if (value != null && validationContext.ObjectInstance != null)
            {

                completedAtDate = (DateTime)value;

                var sentTimeProperty = validationContext.ObjectType.GetProperty(_requestCreatedAtPropertyName);

                if (sentTimeProperty != null)
                {
                    if (sentTimeProperty.GetValue(validationContext.ObjectInstance) != null)
                    {
                        requestCreatedAtPropertyDate = (DateTime)sentTimeProperty.GetValue(validationContext.ObjectInstance)!;
                        if (completedAtDate.Date <= requestCreatedAtPropertyDate.Date)
                            return new ValidationResult($"The completed date cannot be earlier than the Request Created At date.");
                    }

                }
                else
                {
                    if (sentTimeProperty == null)
                        throw new ArgumentException("Property name not found");
                }

            }

            return ValidationResult.Success!;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private ValidationResult IsReviewedAtDateCannotBeBeforeRequestCreatedAtDate(object? value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;

            DateTime reviewedAt;
            DateTime requestCreatedAtPropertyDate;

            if (value != null && validationContext.ObjectInstance != null)
            {

                reviewedAt = (DateTime)value;

                var sentTimeProperty = validationContext.ObjectType.GetProperty(_requestCreatedAtPropertyName);

                if (sentTimeProperty != null)
                {
                    if (sentTimeProperty.GetValue(validationContext.ObjectInstance) != null)
                    {
                        requestCreatedAtPropertyDate = (DateTime)sentTimeProperty.GetValue(validationContext.ObjectInstance)!;
                        if (reviewedAt.Date <= requestCreatedAtPropertyDate.Date)
                            return new ValidationResult($"The Reviewed date cannot be earlier than the Request Created At date.");
                    }

                }
                else
                {
                    if (sentTimeProperty == null)
                        throw new ArgumentException("Property name not found");
                }

            }

            return ValidationResult.Success!;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private ValidationResult IsSentDateCannotBeAfterRequestCreatedAtDate(object? value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;

            DateTime sentTime;
            DateTime requestCreatedAtPropertyDate;

            if (value != null && validationContext.ObjectInstance != null)
            {

                sentTime = (DateTime)value;

                var requestCreatedAtProperty = validationContext.ObjectType.GetProperty(_requestCreatedAtPropertyName);

                if (requestCreatedAtProperty != null)
                {
                    if (requestCreatedAtProperty.GetValue(validationContext.ObjectInstance) != null)
                    {
                        requestCreatedAtPropertyDate = (DateTime)requestCreatedAtProperty.GetValue(validationContext.ObjectInstance)!;

                        if (sentTime <= requestCreatedAtPropertyDate)
                            return new ValidationResult($"The Sent Time date must be later than the Request Created At date.");
                    }

                }
                else
                {
                    if (requestCreatedAtProperty == null)
                        throw new ArgumentException("Property name not found");
                }

            }

            return ValidationResult.Success!;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private ValidationResult IsRequestCreatedAtCanNotBetGreaterThanNow(object? value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;

            DateTime requestCreatedAt;
            DateTime sentTimePropertyDate;

            if (value != null && validationContext.ObjectInstance != null)
            {
                //This can be whatever, this is just an exmaple attribute that works with a number of properties in the TransferModel
                requestCreatedAt = (DateTime)value;

                var sentTimeProperty = validationContext.ObjectType.GetProperty(_sentTimePropertyName);

                if (sentTimeProperty != null)
                {
                    if (sentTimeProperty.GetValue(validationContext.ObjectInstance) != null)
                    {
                        sentTimePropertyDate = (DateTime)sentTimeProperty.GetValue(validationContext.ObjectInstance)!;
                        if (requestCreatedAt >= sentTimePropertyDate)
                            return new ValidationResult($"The Sent Time must be later than the Request Created At time.");
                    }

                }
                else
                {
                    if (sentTimeProperty == null)
                        throw new ArgumentException("Property name not found");
                }

                //if (requestCreatedAt.Date >= DateTime.Now.Date)
                //{
                //    return new ValidationResult(ErrorMessage);
                //}
            }

            return ValidationResult.Success!;
        }

        //Add this implementation later.
        /// <summary>
        /// See commented out interface ref up top. 
        /// </summary>
        /// <param name="context"></param>
        public void AddValidation(ClientModelValidationContext context)
        {
            var error = FormatErrorMessage(context.ModelMetadata.GetDisplayName());
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-error", error);
        }
    }
}
