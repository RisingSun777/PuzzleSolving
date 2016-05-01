using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ProblemSolvingTest
{
    [TestClass]
    public class TrivialTest
    {
        class ClassA { public int Property1 { get; set; } }
        struct StructA { public int Property1 { get; set; } }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Test_reference_key_dictionary()
        {
            Dictionary<ClassA, int> testDict = new Dictionary<ClassA, int>();

            ClassA a1 = new ClassA { Property1 = 6 };
            ClassA a2 = new ClassA { Property1 = 6 };

            Assert.AreNotSame(a1, a2);

            testDict.Add(a1, 10);
            Assert.AreEqual(10, testDict[a1]);

            Assert.AreEqual(10, testDict[a2]);
        }

        [TestMethod]
        public void Test_value_key_dictionary()
        {
            Dictionary<StructA, int> testDict = new Dictionary<StructA, int>();

            StructA a1 = new StructA { Property1 = 6 };
            StructA a2 = new StructA { Property1 = 6 };

            Assert.AreNotSame(a1, a2);

            testDict.Add(a1, 10);
            Assert.AreEqual(10, testDict[a1]);

            Assert.AreEqual(10, testDict[a2]);
        }
    }
}
