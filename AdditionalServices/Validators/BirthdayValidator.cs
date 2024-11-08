namespace webNET_2024_aspnet_1.AdditionalServices.Validators
{
    public class BirthdayValidator
    {
        public static bool ValidateBirthday(DateTime birthday)
        {
            DateTime minDate = new DateTime(1900, 1, 1, 0, 0, 0);
            DateTime maxDate = new DateTime(9999, 12, 31, 0, 0, 0);
            DateTime currentDate = DateTime.Now;

            return birthday >= minDate && birthday <= maxDate && birthday <= currentDate;
        }
    }
}
