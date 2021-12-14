using XUnit.Framework;
using OOP_Eksamen;

namespace OOP_Eksamen_Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
        [Theory]
        public void TestIfUserExists(string email)
        {
            Assert.Throws<EmailNotLegalExecption>(()=>new User("test", "test", "test", email, 0, 1));
        }
    }
}