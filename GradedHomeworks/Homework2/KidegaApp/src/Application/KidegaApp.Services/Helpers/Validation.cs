namespace KidegaApp.Services.Helpers
{
    public static class Validation
    {
        // from microsoft
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    if (!domainName.Equals("gmail.com"))
                    {
                        throw new ArgumentException("Invalid domain.");
                    }
                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static bool IsValidPassword(string password)
        {
            return true; //Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        }


        private static readonly Dictionary<string, byte[]> _allowedMimeTypes = new Dictionary<string, byte[]>
        {
            {"image/jpeg", new byte[] {255, 216, 255}},
            {"image/jpg", new byte[] {255, 216, 255}},
            {"image/pjpeg", new byte[] {255, 216, 255}},
            {"image/png", new byte[] {137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82}},
        };
        // returns MimeType of a file if its type is valid
        public static string? ValidateMimeType(byte[] file, string contentType)
        {
            var imageType = _allowedMimeTypes.SingleOrDefault(x => x.Key.Equals(contentType));
            // type is not allowed
            if (imageType.Equals(new KeyValuePair<string, byte[]>()))
            {
                return null;
            }

            if (file.Take(imageType.Value.Length).SequenceEqual(imageType.Value))
            {
                return imageType.Key;
            }
            // wrong bytes
            return null;
        }
    }
}
