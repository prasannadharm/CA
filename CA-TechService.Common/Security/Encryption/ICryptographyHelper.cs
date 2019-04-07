
#region ProgramInformation
/*------------------------------------------------------------------------------------------------------------------------------
	Module & SubModule Name	: Common
    Package Name            : TFO.COMMON.Security
	Description	            : Interface for Cryptography.
    Author(s)               : Krishna Mohan
    Date Created        	: Oct 11, 2010
------------------------------------------------------------------------------------------------------------------------------*/
#endregion

namespace CA_TechService.Common.Security.Encryption
{
    internal interface ICryptographyHelper
    {
        string Encrypt(byte[] plainBytes);
        string Encrypt(string plainText);

        string Decrypt(byte[] cipherBytes);
        string Decrypt(string cipherText);

        string CreateHash(byte[] plainBytes);
        string CreateHash(string plainText);

        string EncryptQueryString(string plainText);

        bool CompareHash(string plainText, string HashText);
    }
}
