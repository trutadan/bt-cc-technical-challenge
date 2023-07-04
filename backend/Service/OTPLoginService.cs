using System.Security.Cryptography;
using System.Text;

namespace technical_challenge.Service
{
    public class OTPLoginService
    {
        // Number of digits in the TOTP code
        private const int DigitLength = 6; 

        // Time step duration in seconds
        private const int TimeStepSeconds = 30; 

        // Initial counter value
        private const int T0 = 0; 

        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1);

        private readonly IConfiguration _configuration;

        public OTPLoginService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateTotp(string userId, DateTime dateTime)
        {
            string secretKey = _configuration.GetValue<string>("OTPLogin:SecretKey");
            long counter = GetCounter(dateTime);
            return GenerateTotp(userId, secretKey, counter);
        }

        public bool IsTotpCodeExpired(string userId, DateTime dateTime, string totpCode)
        {
            string secretKey = _configuration.GetValue<string>("OTPLogin:SecretKey");
            long counter = GetCounter(dateTime);
            string generatedCode = GenerateTotp(userId, secretKey, counter);

            return totpCode != generatedCode;
        }

        private string GenerateTotp(string userId, string secretKey, long counter)
        {
            byte[] keyBytes = Encoding.ASCII.GetBytes(secretKey);
            byte[] counterBytes = BitConverter.GetBytes(counter);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(counterBytes);
            }

            byte[] hmac = new HMACSHA1(keyBytes).ComputeHash(counterBytes);

            int offset = hmac[^1] & 0x0F;
            int code = ((hmac[offset] & 0x7F) << 24) |
                       ((hmac[offset + 1] & 0xFF) << 16) |
                       ((hmac[offset + 2] & 0xFF) << 8) |
                       (hmac[offset + 3] & 0xFF);

            int digits = code % (int)Math.Pow(10, DigitLength);
            return digits.ToString().PadLeft(DigitLength, '0');
        }

        private long GetCounter(DateTime dateTime)
        {
            TimeSpan time = dateTime - UnixEpoch;
            long counter = (long)time.TotalSeconds / TimeStepSeconds;
            return counter + T0;
        }
    }
}
