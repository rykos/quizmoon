using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.AspNetCore.Http;

namespace quizmoon.Helpers
{
    public static class SysHelper
    {
        public static byte[] FileToByteArray(IFormFile file)
        {
            if (file != default && file.Length > 0)
            {
                using (MemoryStream ms = new())
                {
                    file.CopyTo(ms);
                    return ms.ToArray();
                }
            }
            else
            {
                return null;
            }
        }
    }
}