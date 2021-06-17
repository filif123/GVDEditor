using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using GVDEditor.Tools;

namespace GVDEditorTests.Tools
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class UtilsTests
    {
        [TestMethod]
        public void ANSItoUTF_Bytes_Null()
        {
            try
            {
                // ReSharper disable once InvokeAsExtensionMethod
                Utils.ANSItoUTF((byte[])null);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentNullException);
            }
        }

        [TestMethod]
        public void ANSItoUTF_Bytes_NotNull()
        {
            byte[] bytes = {0x20,0x40,0x60};

            string res = bytes.ANSItoUTF();
            
            Assert.AreEqual("\x0020\x0040\x0060", res);
        }

        [TestMethod]
        public void ANSItoUTF_String_Null()
        {
            try
            {
                // ReSharper disable once InvokeAsExtensionMethod
                Utils.ANSItoUTF((string)null);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentNullException);
            }
        }

        [TestMethod]
        public void ANSItoUTF_String_NotNull()
        {
            const string input = "č š ř";
            string res = input.ANSItoUTF();
            
            Assert.AreEqual("č š ř", res);
        }

        [TestMethod]
        public void UTFtoANSI_Bytes_Null()
        {
            try
            {
                // ReSharper disable once InvokeAsExtensionMethod
                Utils.UTFtoANSI((byte[])null);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentNullException);
            }
        }

        [TestMethod]
        public void UTFtoANSI_Bytes_NotNull()
        {
            byte[] bytes = {0x20,0x40,0x60};

            string res = bytes.UTFtoANSI();
            
            Assert.AreEqual("\x0020\x0040\x0060", res);
        }

        [TestMethod]
        public void UTFtoANSI_String_Null()
        {
            try
            {
                // ReSharper disable once InvokeAsExtensionMethod
                Utils.UTFtoANSI((string)null);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentNullException);
            }
        }

        [TestMethod]
        public void UTFtoANSI_String_NotNull()
        {
            const string input = "č š ř";

            string res = input.UTFtoANSI();
            
            Assert.AreEqual("č š ř", res);
        }

        [TestMethod]
        public void PadZeros()
        {
            const int input = 4;

            string res = input.PadZeros(5);
            
            Assert.AreEqual("00004", res);
        }

        [TestMethod]
        public void PadZerosZeroDigit()
        {
            const int input = 4;

            string res = input.PadZeros(0);

            Assert.AreEqual("4", res);
        }

        [TestMethod]
        public void IsInt_Word()
        {
            const string input = "Test";

            var res = Utils.IsInt(input);

            Assert.IsFalse(res);
        }

        [TestMethod]
        public void IsInt_Num()
        {
            const string input = "07841";

            var res = Utils.IsInt(input);

            Assert.IsTrue(res);
        }

        [TestMethod]
        public void ParseIntOrDefault_Word()
        {
            const string input = "Test";

            var res = Utils.ParseIntOrDefault(input, 9);

            Assert.AreEqual(9, res);
        }

        [TestMethod]
        public void ParseIntOrDefault_Num()
        {
            const string input = "07841";

            var res = Utils.ParseIntOrDefault(input, 9);

            Assert.AreEqual(7841, res);
        }

        [TestMethod]
        public void ParseIntOrNull_Word()
        {
            const string input = "Test";

            var res = Utils.ParseIntOrNull(input);

            Assert.AreEqual(null, res);
        }

        [TestMethod]
        public void ParseIntOrNull_Num()
        {
            const string input = "07841";

            var res = Utils.ParseIntOrNull(input);

            Assert.AreEqual(7841, res);
        }

        [TestMethod]
        public void ParseStringOrDefault_Null()
        {
            const string input = null;

            var res = Utils.ParseStringOrDefault(input);

            Assert.AreEqual(null, res);
        }

        [TestMethod]
        public void ParseStringOrDefault_NotNull()
        {
            const string input = "null";

            var res = Utils.ParseStringOrDefault(input);

            Assert.AreEqual("null", res);
        }

        [TestMethod]
        public void BitArrayToString()
        {
            var arr = new BitArray(new[] {true, false, true, false, true});

            string res = Utils.BitArrayToString(arr);
            
            Assert.AreEqual("10101", res);
        }

        [TestMethod]
        public void StringToBitArray()
        {
            const string arr = "10101";

            BitArray res = Utils.StringToBitArray(arr);

            var compare = new BitArray(new[] {true, false, true, false, true});
            bool equals = compare.Xor(res).OfType<bool>().All(e => !e);

            Assert.IsTrue(equals);
        }

        [TestMethod]
        public void ToQuotedString()
        {
            const string arr = "Bratislava- N.Mesto";

            string res = arr.ToQuotedString();

            Assert.AreEqual("\"Bratislava- N.Mesto\"", res);
        }

        [TestMethod]
        public void BoolToNumber()
        {
            const bool input = true;

            var res = input.ToNumber();

            Assert.AreEqual(1, res);
        }

        [TestMethod]
        public void NumToBool()
        {
            const int input = 1;

            var res = input.ToBool();

            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void ToHEX()
        {
            Color c = Color.BlueViolet;

            string res = c.ToHEX();

            Assert.AreEqual("0xE22B8A", res);
        }

        [TestMethod]
        public void ParseHEX()
        {
            const string arr = "0xE22B8A";

            Color res = Utils.ParseHEX(arr);

            Assert.AreEqual(Color.FromArgb(255,138,43,226), res);
        }

        [TestMethod]
        public void TryParseHEX_CorrectInput()
        {
            const string arr = "0xE22B8A";

            Color? res = Utils.TryParseHEX(arr);

            Assert.AreEqual(Color.FromArgb(255, 138, 43, 226), res);
        }

        [TestMethod]
        public void TryParseHEX_IncorrectInput()
        {
            const string arr = "Test";

            Color? res = Utils.TryParseHEX(arr);

            Assert.AreEqual(null, res);
        }

        [TestMethod]
        public void CombinePath_ZeroPaths()
        {
            string res = Utils.CombinePath();

            Assert.AreEqual(null, res);
        }

        [TestMethod]
        public void CombinePath_NonZeroPaths_FirstEmpty()
        {
            string res = Utils.CombinePath("", "Test", "Microsoft");

            Assert.AreEqual("", res);
        }

        [TestMethod]
        public void CombinePath_NonZeroPaths()
        {
            string res = Utils.CombinePath("\\Users", "Test", "Microsoft");

            Assert.AreEqual("\\Users\\Test\\Microsoft", res);
        }

        [TestMethod]
        public void GetSystemFontNames()
        {
            var res = Utils.GetSystemFontNames();
            using var fonts = new InstalledFontCollection();
            var expected = fonts.Families.Length;

            Assert.AreEqual(expected, res.Count);
        }

        [TestMethod]
        public void IsFontMonospaced_Arial()
        {
            using var g = Graphics.FromImage(new Bitmap(100, 100));
            using var family = new FontFamily("Arial");
            using var font = new Font(family, 20);

            var res = Utils.IsFontMonospaced(g, font);

            Assert.AreEqual(false, res);
        }

        [TestMethod]
        public void IsFontMonospaced_Consolas()
        {
            using var g = Graphics.FromImage(new Bitmap(100, 100));
            using var family = new FontFamily("Consolas");
            using var font = new Font(family, 20);

            var res = Utils.IsFontMonospaced(g, font);

            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void ValidateShortcut_OK()
        {
            bool res = Utils.ValidateShortcut(Keys.Control | Keys.Shift | Keys.A);

            Assert.IsTrue(res);
        }

        [TestMethod]
        public void ValidateShortcut_Wrong()
        {
            bool res = Utils.ValidateShortcut(Keys.A | Keys.B);

            Assert.IsFalse(res);
        }

        [TestMethod]
        public void RemoveDiacritics()
        {
            const string arr = "Účel: Seznam spojů pro jednotlivé vzory";

            string res = Utils.RemoveDiacritics(arr);

            Assert.AreEqual("Ucel: Seznam spoju pro jednotlive vzory", res);
        }

        [TestMethod]
        public void ParseDate_OK()
        {
            const string arr = "17.12.2021";

            var res = Utils.ParseDate(arr);
            Assert.AreEqual(new DateTime(2021,12,17), res);
        }
        
        [TestMethod]
        public void ParseDate_Wrong()
        {
            const string arr = "7.4.2021";

            try
            {
                Utils.ParseDate(arr);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is FormatException);
            }
        }

        [TestMethod]
        public void ParseDateAlts_OK()
        {
            const string arr = "17.2.2021";

            var res = Utils.ParseDateAlts(arr);
            Assert.AreEqual(new DateTime(2021,2,17), res);
        }
        
        [TestMethod]
        public void ParseDateAlts_Wrong()
        {
            const string arr = "71.4.2021";

            try
            {
                Utils.ParseDateAlts(arr);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is FormatException);
            }
        }

        [TestMethod]
        public void ParseTime_OK()
        {
            const string arr = "18:00";

            var res = Utils.ParseTime(arr);
            var today = DateTime.Today;

            var expected = new DateTime(today.Year, today.Month, today.Day, 18, 0, 0);
            Assert.AreEqual(expected, res);
        }
        
        [TestMethod]
        public void ParseTime_Wrong()
        {
            const string arr = "2:0";

            try
            {
                Utils.ParseTime(arr);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is FormatException);
            }
        }

        [TestMethod]
        public void ExReplace()
        {
            const string arr = ";d:\\newgvd\\Novem\\data\\Vlaky.txt\r\n;Účel: Seznam spojů pro jednotlivé vzory\r\n;Vygenerované programem Export3, verze 3.01 dne 26.11.2007 v 21:26\r\n;Druh dopravy: Vlaky - kategorie=1, podkategorie=0\r\n;Název stanice: Bratislava-N.Mesto\r\n;Platnost grafikonu: 9.12.2007 - 13.12.2008\r\n;Platnost dat: 9.12.2007 - 13.12.2008\r\n;Data vytvořená: 26.11.2007 (ID=1196108785)\r\n;=====================================================";

            var res = arr.ExReplace("p", "Q", StringComparison.OrdinalIgnoreCase);

            const string expected = ";d:\\newgvd\\Novem\\data\\Vlaky.txt\r\n;Účel: Seznam sQojů Qro jednotlivé vzory\r\n;Vygenerované Qrogramem ExQort3, verze 3.01 dne 26.11.2007 v 21:26\r\n;Druh doQravy: Vlaky - kategorie=1, Qodkategorie=0\r\n;Název stanice: Bratislava-N.Mesto\r\n;Qlatnost grafikonu: 9.12.2007 - 13.12.2008\r\n;Qlatnost dat: 9.12.2007 - 13.12.2008\r\n;Data vytvořená: 26.11.2007 (ID=1196108785)\r\n;=====================================================";
            Assert.AreEqual(expected, res);
        }
    }
}
