using Common.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Interfaces
{
    public interface ICripto
    {
        string ComputeHashMd5(string value, string salt);
        string Encrypt(string text, string keyString);
        string Decrypt(string cipherText, string keyString);
    }
}
