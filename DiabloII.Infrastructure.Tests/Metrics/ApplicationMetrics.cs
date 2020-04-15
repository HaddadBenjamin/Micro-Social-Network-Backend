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
            var csharpFiles = Directory.EnumerateFiles(@"C:\Users\hadda\OneDrive\Bureau\Travaux\Diablo-II-Items", "*.cs", SearchOption.AllDirectories);
            var csharpFilesCount = csharpFiles.Count();
            var csharpLinesCount = csharpFiles.Select(file => File.ReadAllLines(file).Length).Sum();
        }
    }
}