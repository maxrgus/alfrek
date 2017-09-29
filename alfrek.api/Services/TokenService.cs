using System;
using System.Collections.Generic;
using alfrek.api.Configuration;
using alfrek.api.Interfaces;
using alfrek.api.Models;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.Extensions.Options;

namespace alfrek.api.Services
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<TokenConfiguration> _configuration;

        public TokenService(IOptions<TokenConfiguration> configuration)
        {
            _configuration = configuration;
        }
        
        public string GetIdToken(ApplicationUser user)
        {
            var payload = new Dictionary<string, object>
            {
                {"id", user.Id},
                {"sub", user.Email},
                {"email", user.Email},
                {"emailConfirmed", user.EmailConfirmed},
            };

            return GetToken(payload);
        }

        public string GetAccessToken(string email)
        {
            var payload = new Dictionary<string, object>
            {
                {"sub", email},
                {"email", email}
            };

            return GetToken(payload);
        }

        private string GetToken(Dictionary<string, object> payload)
        {
            var secret = _configuration.Value.Secret;
            
            payload.Add("iss", _configuration.Value.Issuer);
            payload.Add("aud", _configuration.Value.Audience);
            payload.Add("nbf", ConvertToUnixTimestamp(DateTime.Now));
            payload.Add("iat", ConvertToUnixTimestamp(DateTime.Now));
            payload.Add("exp", ConvertToUnixTimestamp(DateTime.Now.AddMinutes(_configuration.Value.Expiry)));
            
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            return encoder.Encode(payload, secret);
            
        }
        
        private static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }
}