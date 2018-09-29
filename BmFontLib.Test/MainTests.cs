using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BmFontLib.Test
{
    [TestClass]
    public class MainTests
    {
        
        [DeploymentItem("sampleData")]
        [TestMethod]
        public void TestAll()
        {
            using (var stream = new FileStream("sampleData/arial.fnt", FileMode.Open))
            {
                var bmFont = new BmFont(stream);

                Assert.AreEqual(bmFont.pages.Count, 2);

                Assert.AreEqual(bmFont.info.fontName, "Arial");

                Assert.AreEqual(bmFont.characters.Count, 319);

                Assert.AreEqual(bmFont.pages.Count, 2);

                Assert.AreEqual(stream.Position, stream.Length);
            }
        }

        [DeploymentItem("sampleData")]
        [TestMethod]
        public void TestKernings()
        {
            using (var stream = new FileStream("sampleData/arial.fnt", FileMode.Open))
            {
                var bmFont = new BmFont(stream);

                foreach (var bmFontCharacter in bmFont.characters)
                {
                    foreach (var valueKerning in bmFontCharacter.Value.kernings)
                    {
                        Assert.AreNotEqual(valueKerning.Value, 0);
                    }
                }
            }
        }

        [DeploymentItem("sampleData")]
        [TestMethod]
        public void TestTextWidth()
        {
            using (var stream = new FileStream("sampleData/arial.fnt", FileMode.Open))
            {
                var bmFont = new BmFont(stream);

                Assert.AreEqual(bmFont.characters['A'].kernings['v'], -1);

                Assert.AreEqual(bmFont.GetTextWidth("Avatar", false), 78);

                Assert.AreEqual(bmFont.GetTextWidth("Avatar", true), 77);
            }
        }
    }
}
