using System.Text.RegularExpressions;

namespace webNET_2024_aspnet_1.AdditionalServices.Validators
{
    public class PasswordValidator
    {
        private static readonly string Pattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            return Regex.IsMatch(password, Pattern);
        }
    }
}
