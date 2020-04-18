namespace DiabloII.Domain.Extensions
{
    public static class StringExtensions
    {
        public static bool IAsValidEmail(this string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}