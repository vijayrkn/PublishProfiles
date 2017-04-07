using System;
using System.Text;

namespace PublishProfileManager
{
    public class DataProtection
    {
        public static Byte[] GetProtectedPassword(string password)
        {
            if (password == null)
            {
                return null;
            }
            byte[] bArray = Encoding.Unicode.GetBytes(password);
            byte[] crypted = System.Security.Cryptography.ProtectedData.Protect(bArray, null, System.Security.Cryptography.DataProtectionScope.CurrentUser);
            return crypted;
        }

        public static string GetUnProtectedPassword(Byte[] cryptedPassword)
        {
            if (cryptedPassword == null)
            {
                return "";
            }

            string plainPWD = "";
            try
            {
                Byte[] uncrypted = System.Security.Cryptography.ProtectedData.Unprotect(cryptedPassword, null, System.Security.Cryptography.DataProtectionScope.CurrentUser);
                plainPWD = Encoding.Unicode.GetString(uncrypted);
            }
            catch
            {
            }
            return plainPWD;
        }
    }
}
