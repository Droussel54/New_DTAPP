
using System.ComponentModel.DataAnnotations;
namespace New_DTAPP.Utility
{
    public class ValidateRequiredDropDownsAndTextFieldsAttribute : ValidationAttribute
    {

        public enum FieldValidationType
        {
            RequiredFieldString, //Probably not needed. But just for Special scenarios.
            RequiredFieldInt,
            OrigAndDestSystemCannotBeTheSame,
            ReviewedAndCompletedUsersCannotBeTheSame
        }


        private readonly FieldValidationType _fieldValidationType;
        private readonly string _fieldToCompare;
        public ValidateRequiredDropDownsAndTextFieldsAttribute(FieldValidationType fieldValidationType, string fieldToCompare= "")
        {
            _fieldValidationType = fieldValidationType;
            _fieldToCompare = fieldToCompare;
        }

        public override bool IsValid(object? value) 
        {
            return base.IsValid(value);
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext) 
        {
            switch (_fieldValidationType)
            {
                case FieldValidationType.RequiredFieldString:

                    return HasValue(value, validationContext);
                
                case FieldValidationType.RequiredFieldInt:

                    return HasValueInt(value, validationContext);

                case FieldValidationType.OrigAndDestSystemCannotBeTheSame:

                    return OrigAndDestSystemCannotBeTheSame(value, validationContext, _fieldToCompare);
                
                case FieldValidationType.ReviewedAndCompletedUsersCannotBeTheSame:

                    return ReviewedAndCompletedUsersCannotBeTheSame(value, validationContext, _fieldToCompare);

                default:
                    
                    return ValidationResult.Success!;
            }
        }

        private ValidationResult ReviewedAndCompletedUsersCannotBeTheSame(object? value, ValidationContext validationContext, string fieldToCompare)
        {

            int completedUser;
            int reviewedUser;

            if (value != null && validationContext.ObjectInstance != null)
            {

                completedUser = (int)value;

                var reviewedUserProperty = validationContext.ObjectType.GetProperty(fieldToCompare);

                if (reviewedUserProperty != null)
                {
                    if (reviewedUserProperty.GetValue(validationContext.ObjectInstance) != null)
                    {
                        reviewedUser = (int)reviewedUserProperty.GetValue(validationContext.ObjectInstance)!;

                        if (reviewedUser == completedUser)
                        {
                            if (fieldToCompare == "ReviewedUserId")
                            {
                                return new ValidationResult($"Completed User cannot be the same as Reviewed User.");
                            }
                            else
                            {
                                return new ValidationResult($"Reviewed User cannot be the same as Completed User.");
                            }
                        }
                    }
                }
                else
                {
                    if (reviewedUserProperty == null)
                        throw new ArgumentException("Property name not found");
                }

            }

            return ValidationResult.Success!;

        }

        private ValidationResult OrigAndDestSystemCannotBeTheSame(object? value, ValidationContext validationContext, string fieldToCompare)
        {
            int completedUser;
            int reviewedUser;

            if (value != null && validationContext.ObjectInstance != null)
            {

                completedUser = (int)value;

                var reviewedUserProperty = validationContext.ObjectType.GetProperty(fieldToCompare);

                if (reviewedUserProperty != null)
                {
                    if (reviewedUserProperty.GetValue(validationContext.ObjectInstance) != null)
                    {
                        reviewedUser = (int)reviewedUserProperty.GetValue(validationContext.ObjectInstance)!;

                        if (reviewedUser == completedUser)
                        {
                            if (fieldToCompare == "DestSystemId")
                            {
                                return new ValidationResult($"Originating System cannot be the same as the Destination System.");
                            }
                            else
                            {
                                return new ValidationResult($"Destination System cannot be the same as the Originating System.");
                            }
                        }
                    }
                }
                else
                {
                    if (reviewedUserProperty == null)
                        throw new ArgumentException("Property name not found");
                }

            }

            return ValidationResult.Success!;
        }

        private ValidationResult HasValue(object? value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;

            if (value != null && validationContext.ObjectInstance != null)
            {
                var prop = value;

                if (prop != null)
                {
                    return ValidationResult.Success!;
                }
            }
            return new ValidationResult(ErrorMessage);
        }

        private ValidationResult HasValueInt(object? value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;

            if (value != null && validationContext.ObjectInstance != null)
            {
                int prop = (int)value;

                if (prop != 0)
                {
                    return ValidationResult.Success!;
                }
            }
            return new ValidationResult(ErrorMessage);
        }


    }
}
