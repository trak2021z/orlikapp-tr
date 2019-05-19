using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services.Interfaces
{
    public interface IHashService
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
    }
}
