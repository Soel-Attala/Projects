namespace UniversityAPI.Models.DataModels
{
    public class JwtSettings
    {
        public bool ValidateIssuerSinginKey { get; set; }
        public string? IssuerSinginKey { get; set; }
        public bool ValidateIssuers { get; set; } = true;
        public string? ValidIssuer { get; set; }
        public bool ValidateAudience { get; set; } = true;
        public string? ValidAudience { get; set; }
        public bool RequireExpirationTime { get; set; }

        public bool ValidateLifeTime { get; set; } = true;

    }
}
