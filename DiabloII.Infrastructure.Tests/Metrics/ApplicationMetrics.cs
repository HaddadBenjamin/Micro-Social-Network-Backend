using System.IO;
using System.Linq;
using NUnit.Framework;

namespace DiabloII.Infrastructure.Tests.Metrics
{
    [Ignore("Those information are not relative to tests, however I need them for a personal uses.")]
    [TestFixture]
    public class ApplicationMetrics
    {
        [Test]
        public void CountCSharpFilesVolumetry()
        {
            var csharpFiles = Directory.EnumerateFiles(@"C:\Users\Benjamin Haddad\Desktop\Projets\Diablo-II-Items", "*.cs", SearchOption.AllDirectories);
            var csharpFilesCount = csharpFiles.Count();
            var csharpLinesCount = csharpFiles.Sum(file => File.ReadAllLines(file).Length);
            var csharpCharactersCount = csharpFiles
                .Sum(file => File.ReadAllLines(file).Select(f => f.Length).Sum());
        }
    }
}