using BusinessLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class HashService : IHashService
    {
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null || String.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Hasło nie może być puste lub zawierać tylko białe znaki");
            }

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null || String.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Hasło nie może być puste lub zawierać tylko białe znaki");
            }
            if (storedHash.Length != 64)
            {
                throw new ArgumentException("Nieodpowiednia długość Hash", "passwordHash");
            }
            if (storedSalt.Length != 128)
            {
                throw new ArgumentException("Nieodpowiednia długość Salt.", "passwordHash");
            }

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
