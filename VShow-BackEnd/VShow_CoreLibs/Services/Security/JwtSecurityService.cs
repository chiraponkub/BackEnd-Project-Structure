using Jose;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VShow_BackEnd.Services.Abstracts;
using VShow_BackEnd.Services.Models;

namespace VShow_BackEnd.Services.Security
{
    /// <summary>
    /// ส่วนสำหรับใช้งาน JWT (Json Web Token)
    /// </summary>
    public class JwtSecurityService : IJwtSecurityService
    {
        /// <summary>
        /// กำหนด Security Key เพื่อเข้ารหัสกับ Json Web Token
        /// </summary>
        /// SECRET_KEY => VShow เอาไป Hash ได้ bvR4AXHG1nbkjfLKgbnQq0xkmXQE5tqt:b3mDQ/b+nm/tlYaMJJYsYQ==
        /// SECRET_KEY => bvR4AXHG1nbkjfLKgbnQq0xkmXQE5tqt:b3mDQ/b+nm/tlYaMJJYsYQ== เอาไป Hash ได้ SECRET_KEY
        public const string SECRET_KEY = "u36EGNbQKGYHj4GYBMADopUgkgt2WNsx:ijjq3R/M4DEAlezyN1nXXA==";
        public const string ISSUER = "https://VShow.com/";
        public const string AUDIENCE = "https://VShow.com/";

        public string JWTEncode<T>(T data, int minute)
        {
            try
            {
                var payload = new JwtPayload<T>
                {
                    Data = data,
                    Expire = DateTime.UtcNow.AddMinutes(minute).Ticks
                };
                return JWT.Encode(payload, Encoding.ASCII.GetBytes(SECRET_KEY), JwsAlgorithm.HS256);
            }
            catch
            {
                return null;
            }
        }

        public T JWTDecode<T>(string token)
        {
            try
            {
                var payload = JWT.Decode<JwtPayload<T>>(token, Encoding.ASCII.GetBytes(SECRET_KEY), JwsAlgorithm.HS256);
                if (payload.Expire < DateTime.UtcNow.Ticks) throw new Exception("Token is exprise");
                return payload.Data;
            }
            catch
            {
                return default;
            }
        }

        public string GenerateJWTAuthentication(string id, string role, int minute = 20)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Sid, id),
                new Claim(ClaimTypes.Role, role)
            };
            var token = new JwtSecurityToken(
                issuer: ISSUER,
                audience: AUDIENCE,
                claims: claims,
                expires: DateTime.Now.AddMinutes(minute),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
