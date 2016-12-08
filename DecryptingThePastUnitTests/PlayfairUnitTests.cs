using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecryptingThePastUnitTests
{
    [TestClass]
    public class PlayfairUnitTests
    {
        [TestMethod]
        public void TestKeyEvenLength()
        {
            PlayfairCipher cipher = new PlayfairCipher("copy");
            string formattedKey = cipher.formatKey("copy");
            Assert.AreEqual("copy", formattedKey);
        }
        [TestMethod]
        public void TestKeyOddLength()
        {
            PlayfairCipher cipher = new PlayfairCipher("cop");
            string formattedKey = cipher.formatKey("cop");
            Assert.AreEqual("cop", formattedKey);
        }
        [TestMethod]
        public void TestKeyRepeatCharacters()
        {
            PlayfairCipher cipher = new PlayfairCipher("hello");
            string formattedKey = cipher.formatKey("hello");
            Assert.AreEqual("helo", formattedKey);
        }
        [TestMethod]
        public void TestKeyWithJ()
        {
            PlayfairCipher cipher = new PlayfairCipher("hello");
            string formattedKey = cipher.formatKey("james");
            Assert.AreEqual("iames", formattedKey);
        }
        [TestMethod]
        public void TestFormatPlainTextOddLength()
        {
            PlayfairCipher cipher = new PlayfairCipher("copy");
            string formattedText = cipher.formatPlaintext("rob");
            Assert.AreEqual("robx", formattedText);
        }
        [TestMethod]
        public void TestFormatPlainTextEvenLength()
        {
            PlayfairCipher cipher = new PlayfairCipher("copy");
            string formattedText = cipher.formatPlaintext("phones");
            Assert.AreEqual("phones", formattedText);
        }
        [TestMethod]
        public void TestFormatPlainTextWithRepeats()
        {
            PlayfairCipher cipher = new PlayfairCipher("copy");
            string formattedText = cipher.formatPlaintext("bugatti");
            Assert.AreEqual("bugatxti", formattedText);
        }
        [TestMethod]
        public void TestFormatPlainTextWithJ()
        {
            PlayfairCipher cipher = new PlayfairCipher("copy");
            string formattedText = cipher.formatPlaintext("james");
            Assert.AreEqual("iamesx", formattedText);
        }
    }
}
