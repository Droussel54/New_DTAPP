using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations;
namespace New_DTAPP.Utility
{
    public class ValidateTranferUsesCodeValueRecordCannotBeDeleted : ValidationAttribute
    {

        public enum CodeValueType
        {
            Operation,
            Unit,
            OrgSystem,
            DestSystem
        }

        private readonly CodeValueType _codeValueType;
        
        public ValidateTranferUsesCodeValueRecordCannotBeDeleted(CodeValueType codeValueType, string fieldToCompare= "")
        {
            _codeValueType = codeValueType;
        }

        public override bool IsValid(object? value)
        {
            return base.IsValid(value);
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            switch (_codeValueType)
            {
                case CodeValueType.Operation:
                    
                    return ValidationResult.Success!;

                case CodeValueType.Unit:

                    return ValidationResult.Success!;

                case CodeValueType.OrgSystem:

                    return ValidationResult.Success!;

                case CodeValueType.DestSystem:

                    return ValidationResult.Success!;

                default:
                    return ValidationResult.Success!;
            }
                

        }

        private ValidationResult ReviewedAndCompletedUsersCannotBeTheSame(object? value, ValidationContext validationContext, string fieldToCompare)
        {
            return ValidationResult.Success!;
        }

        private ValidationResult OrigAndDestSystemCannotBeTheSame(object? value, ValidationContext validationContext, string fieldToCompare)
        {
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

        
    }
}
