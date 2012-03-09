using System;
using System.IO;
using System.Linq;
using System.Reflection;
using TextTransformer;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class writing_text
    {
        [Test]
        public void can_generate_long_string_from_short_seed()
        {
            var text = "First is the last word here";
            _textGenerator.Source = text;
            //_textGenerator.ReadText(text);

            foreach (var i in Enumerable.Range(0, 100))
            {
                var writtenText = _textGenerator.Write(100, 150);
                Console.WriteLine(writtenText);
                Assert.GreaterOrEqual(writtenText.Length, 100, writtenText);
                Assert.LessOrEqual(writtenText.Length, 200, writtenText);
            }
        }

        [Test]
        public void can_generate_from_long_file()
        {
            TextReader reader = new StreamReader(new StreamReader(string.Format(@"{0}\AllsWellThatEndsWell.txt", Assembly.GetExecutingAssembly().Directory())).BaseStream);
            var text = reader.ReadToEnd();
            reader.Close();

            _textGenerator.Source = text;

            //_textGenerator.ReadTextFile(string.Format(@"{0}\AllsWellThatEndsWell.txt", Assembly.GetExecutingAssembly().Directory()));

            var writtenText = _textGenerator.Write(250, 500);
            Console.WriteLine(writtenText);
        }

        private MarkovGenerator _textGenerator;

        [SetUp]
        public void SetUp()
        {
            _textGenerator = new MarkovGenerator();
        }
    }
}