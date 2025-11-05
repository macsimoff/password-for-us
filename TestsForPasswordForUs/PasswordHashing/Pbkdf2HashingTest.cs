using System.Text;
using PasswordHashing;

namespace TestsForPasswordForUs.PasswordHashing
{
    [TestFixture]
    public class Pbkdf2HashingTests
    {
        private readonly Pbkdf2Hashing _sut = new();

        [Test]
        public void GetHash_256_Returns32Bytes()
        {
            var master = Encoding.UTF8.GetBytes("password");
            var salt = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            var hash = _sut.GetHash_256(master, salt, 10000);

            Assert.AreEqual(32, hash.Length);
        }

        [Test]
        public void GetHash_256_IsDeterministic_ForSameInputs()
        {
            var master = Encoding.UTF8.GetBytes("password");
            var salt = new byte[] { 10, 20, 30, 40 };
            var iterations = 15000;

            var h1 = _sut.GetHash_256(master, salt, iterations);
            var h2 = _sut.GetHash_256(master, salt, iterations);

            CollectionAssert.AreEqual(h1, h2);
        }

        [Test]
        public void GetHash_256_DifferentSalt_ProducesDifferentHash()
        {
            var master = Encoding.UTF8.GetBytes("password");
            var salt1 = new byte[] { 1, 2, 3, 4 };
            var salt2 = new byte[] { 4, 3, 2, 1 };

            var h1 = _sut.GetHash_256(master, salt1, 10000);
            var h2 = _sut.GetHash_256(master, salt2, 10000);

            CollectionAssert.AreNotEqual(h1, h2);
        }

        [Test]
        public void GetHash_256_DifferentMasterKey_ProducesDifferentHash()
        {
            var master1 = Encoding.UTF8.GetBytes("password1");
            var master2 = Encoding.UTF8.GetBytes("password2");
            var salt = new byte[] { 1, 2, 3, 4 };

            var h1 = _sut.GetHash_256(master1, salt, 10000);
            var h2 = _sut.GetHash_256(master2, salt, 10000);

            CollectionAssert.AreNotEqual(h1, h2);
        }

        [Test]
        public void GetHash_256_DifferentIterations_ProducesDifferentHash()
        {
            var master = Encoding.UTF8.GetBytes("password");
            var salt = new byte[] { 1, 2, 3, 4 };

            var h1 = _sut.GetHash_256(master, salt, 1000);
            var h2 = _sut.GetHash_256(master, salt, 2000);

            CollectionAssert.AreNotEqual(h1, h2);
        }

        [Test]
        public void GetHash_256_Throws_OnNullMasterKey()
        {
            var salt = new byte[] { 1, 2, 3, 4 };
            Assert.Throws<ArgumentNullException>(() => _sut.GetHash_256(null!, salt, 1000));
        }

        [Test]
        public void GetHash_256_Throws_OnNullSalt()
        {
            var master = Encoding.UTF8.GetBytes("password");
            Assert.Throws<ArgumentNullException>(() => _sut.GetHash_256(master, null!, 1000));
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void GetHash_256_Throws_OnInvalidIterations(int iterations)
        {
            var master = Encoding.UTF8.GetBytes("password");
            var salt = new byte[] { 1, 2, 3, 4 };

            Assert.Throws<ArgumentOutOfRangeException>(() => _sut.GetHash_256(master, salt, iterations));
        }
    }
}