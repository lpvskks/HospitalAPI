using System.Text.RegularExpressions;

namespace webNET_2024_aspnet_1.AdditionalServices.Validators
{
    public class PhoneNumberVlidator
    {
        private static readonly string Pattern = @"^(\+7|8)\s?\(?\d{3}\)?\s?\d{3}[-\s]?\d{2}[-\s]?\d{2}$";
        public static bool IsValidePhoneNumber(string phoneNumber)
        {

            if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length < 11)
            {
                return false;
            }

            return Regex.IsMatch(phoneNumber, Pattern);
        }
    }
}
