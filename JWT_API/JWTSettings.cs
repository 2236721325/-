namespace JWT_API
{
    public class JWTSettings
    {
        public string SecretKey { get; set; }
        public int ExpirationHour { get; set; }//����ʱ�� �������24Сʱ
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}