using System.Collections;
using Jpp.Common;
using NUnit.Framework;

namespace Common.Tests
{

    [TestFixture]
    public class TestProjectCodeComparer
    {
        [TestCaseSource(typeof(ProjectCodeData), nameof(ProjectCodeData.CompareTestCases))]
        public int CompareTest(string x, string y)
        {
            var c = new ProjectCodeComparer();
            return c.Compare(x, y);
        }
    }

    public static class ProjectCodeData
    {
        public static IEnumerable CompareTestCases
        {
            get
            {
                yield return new TestCaseData("", "").Returns(0);
                yield return new TestCaseData(null, null).Returns(0);
                yield return new TestCaseData("", null).Returns(1);
                yield return new TestCaseData(null, "").Returns(-1);

                yield return new TestCaseData("", "999").Returns(1);
                yield return new TestCaseData("", "999T").Returns(1);
                yield return new TestCaseData("", "999TENQ").Returns(1);
                yield return new TestCaseData("", "999T ENQ").Returns(1);

                yield return new TestCaseData(null, "999").Returns(1);
                yield return new TestCaseData(null, "999T").Returns(1);
                yield return new TestCaseData(null, "999TENQ").Returns(1);
                yield return new TestCaseData(null, "999T ENQ").Returns(1);

                yield return new TestCaseData("999", "").Returns(-1);
                yield return new TestCaseData("999T", "").Returns(-1);
                yield return new TestCaseData("999TENQ", "").Returns(-1);
                yield return new TestCaseData("999T ENQ", "").Returns(-1);

                yield return new TestCaseData("999", null).Returns(-1);
                yield return new TestCaseData("999T", null).Returns(-1);
                yield return new TestCaseData("999TENQ", null).Returns(-1);
                yield return new TestCaseData("999T ENQ", null).Returns(-1);

                yield return new TestCaseData("A1234", "B1234").Returns(-1);
                yield return new TestCaseData("A1234", "B1234T").Returns(-1);
                yield return new TestCaseData("A1234", "B1234TENQ").Returns(-1);
                yield return new TestCaseData("A1234", "B1234T ENQ").Returns(-1);
                yield return new TestCaseData("A1234T", "B1234").Returns(-1);
                yield return new TestCaseData("A1234T", "B1234T").Returns(-1);
                yield return new TestCaseData("A1234T", "B1234TENQ").Returns(-1);
                yield return new TestCaseData("A1234T", "B1234T ENQ").Returns(-1);
                yield return new TestCaseData("A1234TENQ", "B1234").Returns(-1);
                yield return new TestCaseData("A1234TENQ", "B1234T").Returns(-1);
                yield return new TestCaseData("A1234TENQ", "B1234TENQ").Returns(-1);
                yield return new TestCaseData("A1234TENQ", "B1234T ENQ").Returns(-1);
                yield return new TestCaseData("A1234T ENQ", "B1234").Returns(-1);
                yield return new TestCaseData("A1234T ENQ", "B1234T").Returns(-1);
                yield return new TestCaseData("A1234T ENQ", "B1234TENQ").Returns(-1);
                yield return new TestCaseData("A1234T ENQ", "B1234T ENQ").Returns(-1);

                yield return new TestCaseData("B1234", "A1234").Returns(1);
                yield return new TestCaseData("B1234", "A1234T").Returns(1);
                yield return new TestCaseData("B1234", "A1234TENQ").Returns(1);
                yield return new TestCaseData("B1234", "A1234T ENQ").Returns(1);
                yield return new TestCaseData("B1234T", "A1234").Returns(1);
                yield return new TestCaseData("B1234T", "A1234T").Returns(1);
                yield return new TestCaseData("B1234T", "A1234TENQ").Returns(1);
                yield return new TestCaseData("B1234T", "A1234T ENQ").Returns(1);
                yield return new TestCaseData("B1234TENQ", "A1234").Returns(1);
                yield return new TestCaseData("B1234TENQ", "A1234T").Returns(1);
                yield return new TestCaseData("B1234TENQ", "A1234TENQ").Returns(1);
                yield return new TestCaseData("B1234TENQ", "A1234T ENQ").Returns(1);
                yield return new TestCaseData("B1234T ENQ", "A1234").Returns(1);
                yield return new TestCaseData("B1234T ENQ", "A1234T").Returns(1);
                yield return new TestCaseData("B1234T ENQ", "A1234TENQ").Returns(1);
                yield return new TestCaseData("B1234T ENQ", "A1234T ENQ").Returns(1);

                yield return new TestCaseData("A1234", "A1234").Returns(0);
                yield return new TestCaseData("A1234", "A1234T").Returns(0);
                yield return new TestCaseData("A1234", "A1234TENQ").Returns(0);
                yield return new TestCaseData("A1234", "A1234T ENQ").Returns(0);
                yield return new TestCaseData("A1234T", "A1234").Returns(0);
                yield return new TestCaseData("A1234T", "A1234T").Returns(0);
                yield return new TestCaseData("A1234T", "A1234TENQ").Returns(0);
                yield return new TestCaseData("A1234T", "A1234T ENQ").Returns(0);
                yield return new TestCaseData("A1234TENQ", "A1234").Returns(0);
                yield return new TestCaseData("A1234TENQ", "A1234T").Returns(0);
                yield return new TestCaseData("A1234TENQ", "A1234TENQ").Returns(0);
                yield return new TestCaseData("A1234TENQ", "A1234T ENQ").Returns(0);
                yield return new TestCaseData("A1234T ENQ", "A1234").Returns(0);
                yield return new TestCaseData("A1234T ENQ", "A1234T").Returns(0);
                yield return new TestCaseData("A1234T ENQ", "A1234TENQ").Returns(0);
                yield return new TestCaseData("A1234T ENQ", "A1234T ENQ").Returns(0);

                yield return new TestCaseData("1000", "999").Returns(1);
                yield return new TestCaseData("1000", "999T").Returns(1);
                yield return new TestCaseData("1000", "999TENQ").Returns(1);
                yield return new TestCaseData("1000", "999T ENQ").Returns(1);
                yield return new TestCaseData("1000T", "999").Returns(1);
                yield return new TestCaseData("1000T", "999T").Returns(1);
                yield return new TestCaseData("1000T", "999TENQ").Returns(1);
                yield return new TestCaseData("1000T", "999T ENQ").Returns(1);
                yield return new TestCaseData("1000TENQ", "999").Returns(1);
                yield return new TestCaseData("1000TENQ", "999T").Returns(1);
                yield return new TestCaseData("1000TENQ", "999TENQ").Returns(1);
                yield return new TestCaseData("1000TENQ", "999T ENQ").Returns(1);
                yield return new TestCaseData("1000T ENQ", "999").Returns(1);
                yield return new TestCaseData("1000T ENQ", "999T").Returns(1);
                yield return new TestCaseData("1000T ENQ", "999TENQ").Returns(1);
                yield return new TestCaseData("1000T ENQ", "999T ENQ").Returns(1);

                yield return new TestCaseData("999", "1000").Returns(-1);
                yield return new TestCaseData("999", "1000T").Returns(-1);
                yield return new TestCaseData("999", "1000TENQ").Returns(-1);
                yield return new TestCaseData("999", "1000T ENQ").Returns(-1);
                yield return new TestCaseData("999T", "1000").Returns(-1);
                yield return new TestCaseData("999T", "1000T").Returns(-1);
                yield return new TestCaseData("999T", "1000TENQ").Returns(-1);
                yield return new TestCaseData("999T", "1000T ENQ").Returns(-1);
                yield return new TestCaseData("999TENQ", "1000").Returns(-1);
                yield return new TestCaseData("999TENQ", "1000T").Returns(-1);
                yield return new TestCaseData("999TENQ", "1000TENQ").Returns(-1);
                yield return new TestCaseData("999TENQ", "1000T ENQ").Returns(-1);
                yield return new TestCaseData("999T ENQ", "1000").Returns(-1);
                yield return new TestCaseData("999T ENQ", "1000T").Returns(-1);
                yield return new TestCaseData("999T ENQ", "1000TENQ").Returns(-1);
                yield return new TestCaseData("999T ENQ", "1000T ENQ").Returns(-1);

                yield return new TestCaseData("999", "999").Returns(0);
                yield return new TestCaseData("999", "999T").Returns(0);
                yield return new TestCaseData("999", "999TENQ").Returns(0);
                yield return new TestCaseData("999", "999T ENQ").Returns(0);
                yield return new TestCaseData("999T", "999").Returns(0);
                yield return new TestCaseData("999T", "999T").Returns(0);
                yield return new TestCaseData("999T", "999TENQ").Returns(0);
                yield return new TestCaseData("999T", "999T ENQ").Returns(0);
                yield return new TestCaseData("999TENQ", "999").Returns(0);
                yield return new TestCaseData("999TENQ", "999T").Returns(0);
                yield return new TestCaseData("999TENQ", "999TENQ").Returns(0);
                yield return new TestCaseData("999TENQ", "999T ENQ").Returns(0);
                yield return new TestCaseData("999T ENQ", "999").Returns(0);
                yield return new TestCaseData("999T ENQ", "999T").Returns(0);
                yield return new TestCaseData("999T ENQ", "999TENQ").Returns(0);
                yield return new TestCaseData("999T ENQ", "999T ENQ").Returns(0);

                yield return new TestCaseData("A1000", "999").Returns(1);
                yield return new TestCaseData("A1000", "999T").Returns(1);
                yield return new TestCaseData("A1000", "999TENQ").Returns(1);
                yield return new TestCaseData("A1000", "999T ENQ").Returns(1);
                yield return new TestCaseData("A1000T", "999").Returns(1);
                yield return new TestCaseData("A1000T", "999T").Returns(1);
                yield return new TestCaseData("A1000T", "999TENQ").Returns(1);
                yield return new TestCaseData("A1000T", "999T ENQ").Returns(1);
                yield return new TestCaseData("A1000TENQ", "999").Returns(1);
                yield return new TestCaseData("A1000TENQ", "999T").Returns(1);
                yield return new TestCaseData("A1000TENQ", "999TENQ").Returns(1);
                yield return new TestCaseData("A1000TENQ", "999T ENQ").Returns(1);
                yield return new TestCaseData("A1000T ENQ", "999").Returns(1);
                yield return new TestCaseData("A1000T ENQ", "999T").Returns(1);
                yield return new TestCaseData("A1000T ENQ", "999TENQ").Returns(1);
                yield return new TestCaseData("A1000T ENQ", "999T ENQ").Returns(1);

                yield return new TestCaseData("1000", "A999").Returns(-1);
                yield return new TestCaseData("1000", "A999T").Returns(-1);
                yield return new TestCaseData("1000", "A999TENQ").Returns(-1);
                yield return new TestCaseData("1000", "A999T ENQ").Returns(-1);
                yield return new TestCaseData("1000T", "A999").Returns(-1);
                yield return new TestCaseData("1000T", "A999T").Returns(-1);
                yield return new TestCaseData("1000T", "A999TENQ").Returns(-1);
                yield return new TestCaseData("1000T", "A999T ENQ").Returns(-1);
                yield return new TestCaseData("1000TENQ", "A999").Returns(-1);
                yield return new TestCaseData("1000TENQ", "A999T").Returns(-1);
                yield return new TestCaseData("1000TENQ", "A999TENQ").Returns(-1);
                yield return new TestCaseData("1000TENQ", "A999T ENQ").Returns(-1);
                yield return new TestCaseData("1000T ENQ", "A999").Returns(-1);
                yield return new TestCaseData("1000T ENQ", "A999T").Returns(-1);
                yield return new TestCaseData("1000T ENQ", "A999TENQ").Returns(-1);
                yield return new TestCaseData("1000T ENQ", "A999T ENQ").Returns(-1);
            }
        }
    }
}
