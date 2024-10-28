namespace webNET_2024_aspnet_1.AdditionalServices.Validators
{
    public class NameValidator
    {
        private static readonly string NamePattern = @"^([A-Za-zА-Яа-яЁё]+[-]?[A-Za-zА-Яа-яЁё]+)(\s[A-Za-zА-Яа-яЁё]+[-]?[A-Za-zА-Яа-яЁё]+){1,2}$";

        public static bool IsValidName(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;

            return System.Text.RegularExpressions.Regex.IsMatch(name, NamePattern);
        }
    }
}
