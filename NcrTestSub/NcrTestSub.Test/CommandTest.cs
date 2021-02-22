using NUnit.Framework;
using NcrTestSub.Bo.Factory;
using System;
using NcrTestSub.Dto;

namespace Tests
{
    [TestFixture]
    public class CommandTest
    {

        FactoryBo factory = FactoryBo.GetInstance;

        [Test]
        public void CommandShouldReturnObject()
        {
            

            string json = "{ \"sender\": \"pc\", \"destination\":\"kb_test\", \"command\":\"test\" }";

            Assert.AreNotEqual(null, factory.CreateInstanceCommandBo.processMessage(json).destination);
        }

        [Test]
        public void ShouldThrowsException()
        {
            var ex = Assert.Throws<Exception>(() => factory.CreateInstanceCommandBo.processCommand(null));

            Assert.That(ex.Message, Is.EqualTo("Configguration settings must be initialized"));
        }

        [Test]
        public void ShouldReturnTrue()
        {
            CommandDto dto = new CommandDto("pc", "client", "test");
            CommandDto dtoToCompare = new CommandDto("pc", "client", "test");

            Assert.AreEqual(dto, dtoToCompare);
        }

        [Test]
        public void ShouldReturnEqualInstance()
        {
            FactoryBo factoryToCompare = FactoryBo.GetInstance;

            Assert.AreEqual(factory, factoryToCompare);
        }
    }
}