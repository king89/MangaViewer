using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace Windows.Security.Cryptography
{
    public static class SecurityExtention
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="orignalString"></param>
        /// <returns></returns>
        public static string MD5(string securityString)
        {
            String strAlgName = "MD5";
            HashAlgorithmProvider objAlgProv = HashAlgorithmProvider.OpenAlgorithm(strAlgName);
            IBuffer buffMsg = CryptographicBuffer.ConvertStringToBinary(securityString, BinaryStringEncoding.Utf8);
            IBuffer buffHash = objAlgProv.HashData(buffMsg);
            string sign = CryptographicBuffer.EncodeToHexString(buffHash);
            return sign;
        }

        /// <summary>
        /// HMAC_SHA1加密
        /// </summary>
        /// <param name="securityString"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string HMAC_SHA1(string securityString, string key)
        {
            String strMsg = securityString;
            String strAlgName = "HMAC_SHA1";
            IBuffer buffMsg;
            CryptographicKey hmacKey;
            IBuffer buffHMAC;

            // Create a MacAlgorithmProvider object for the specified algorithm.
            MacAlgorithmProvider objMacProv = MacAlgorithmProvider.OpenAlgorithm(strAlgName);

            // Demonstrate how to retrieve the name of the algorithm used.
            String strNameUsed = objMacProv.AlgorithmName;

            // Create a buffer that contains the the message to be signed.
            //BinaryStringEncoding encoding = BinaryStringEncoding.Utf8;
            //buffMsg = CryptographicBuffer.ConvertStringToBinary(strMsg, encoding);
            buffMsg = StringToAscii(strMsg);

            // Create a key to be signed with the message.
            IBuffer buffKeyMaterial = StringToAscii(key);
            hmacKey = objMacProv.CreateKey(buffKeyMaterial);

            // Sign the key and message together.
            buffHMAC = CryptographicEngine.Sign(hmacKey, buffMsg);

            // Verify that the HMAC length is correct for the selected algorithm
            if (buffHMAC.Length != objMacProv.MacLength)
            {
                throw new Exception("Error computing digest");
            }
            Boolean IsAuthenticated = CryptographicEngine.VerifySignature(hmacKey, buffMsg, buffHMAC);
            if (!IsAuthenticated)
            {
                throw new Exception("The message cannot be verified.");
            }
            String strHashBase64 = CryptographicBuffer.EncodeToBase64String(buffHMAC);

            return strHashBase64;
        }
        private static IBuffer StringToAscii(string s)
        {
            BinaryStringEncoding encoding = BinaryStringEncoding.Utf8;
            return CryptographicBuffer.ConvertStringToBinary(s, encoding); ;
        }

        /// <summary>
        /// 加密字符串
        /// </summary>
        public static IBuffer SampleCipherEncryption(String strMsg, string keyStr, BinaryStringEncoding encoding)
        {
            IBuffer buffMsg = CryptographicBuffer.ConvertStringToBinary(strMsg, encoding);
            return SampleCipherEncryption(buffMsg, keyStr, encoding);
        }
        /// <summary>
        /// 加密buffer内容
        /// </summary>
        /// <param name="original"></param>
        /// <param name="keyStr"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static IBuffer SampleCipherEncryption(IBuffer original, string keyStr, BinaryStringEncoding encoding)
        {
            // Initialize the initialization vector.
            // Initialize the binary encoding value.
            //var encoding = BinaryStringEncoding.Utf8;
            // Create a buffer that contains the encoded message to be encrypted. 
            String strAlgName = SymmetricAlgorithmNames.Rc2EcbPkcs7;
            // Open a symmetric algorithm provider for the specified algorithm. 
            SymmetricKeyAlgorithmProvider objAlg = SymmetricKeyAlgorithmProvider.OpenAlgorithm(strAlgName);

            // Determine whether the message length is a multiple of the block length.
            // This is not necessary for PKCS #7 algorithms which automatically pad the
            // message to an appropriate length.
            if (!strAlgName.Contains("PKCS7"))
            {
                if ((original.Length % objAlg.BlockLength) != 0)
                {
                    throw new Exception("Message buffer length must be multiple of block length.");
                }
            }
            // Create a symmetric key.
            var keyMaterial = CryptographicBuffer.ConvertStringToBinary(keyStr, encoding);
            var key = objAlg.CreateSymmetricKey(keyMaterial);

            // CBC algorithms require an initialization vector. Here, a random
            // number is used for the vector.
            //if (strAlgName.Contains("CBC"))
            //{
            //    iv = CryptographicBuffer.GenerateRandom(objAlg.BlockLength);
            //}
            // Encrypt the data and return.
            IBuffer buffEncrypt = CryptographicEngine.Encrypt(key, original, null);
            return buffEncrypt;
        }


        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="strAlgName"></param>
        /// <param name="buffEncrypt"></param>
        /// <param name="iv"></param>
        /// <param name="encoding"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IBuffer SampleCipherDecryption(IBuffer buffEncrypt, string keyStr, BinaryStringEncoding encoding)
        {
            // Declare a buffer to contain the decrypted data.
            IBuffer buffDecrypted;
            String strAlgName = SymmetricAlgorithmNames.Rc2EcbPkcs7;
            // Open an symmetric algorithm provider for the specified algorithm. 
            SymmetricKeyAlgorithmProvider objAlg = SymmetricKeyAlgorithmProvider.OpenAlgorithm(strAlgName);
            var keyMaterial = CryptographicBuffer.ConvertStringToBinary(keyStr, encoding);
            var key = objAlg.CreateSymmetricKey(keyMaterial);
            // The input key must be securely shared between the sender of the encrypted message
            // and the recipient. The initialization vector must also be shared but does not
            // need to be shared in a secure manner. If the sender encodes a message string 
            // to a buffer, the binary encoding method must also be shared with the recipient.
            buffDecrypted = CryptographicEngine.Decrypt(key, buffEncrypt, null);

            // Convert the decrypted buffer to a string (for display). If the sender created the
            // original message buffer from a string, the sender must tell the recipient what 
            // BinaryStringEncoding value was used. Here, BinaryStringEncoding.Utf8 is used to
            // convert the message to a buffer before encryption and to convert the decrypted
            // buffer back to the original plaintext.
            //String strDecrypted = CryptographicBuffer.ConvertBinaryToString(encoding, buffDecrypted);
            return buffDecrypted;
        }





    }
}
