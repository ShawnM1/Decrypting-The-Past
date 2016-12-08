using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DecryptingThePastUnitTests
{
    [TestClass]
    public class CaesarCipherTest
    {
        [TestMethod]
        public void TestEncryption()
        {
            CaesarCipher cipher = new CaesarCipher();
            string encryptedMessage = cipher.encryptCipher(1, "zana");
            Assert.AreEqual("abob", encryptedMessage);
        }
        [TestMethod]
        public void TestDecryption()
        {
            CaesarCipher cipher = new CaesarCipher();
            string decryptedMessage = cipher.decrypt(-1, "abob");
            Assert.AreEqual("zana", decryptedMessage);
        }
    }
}
