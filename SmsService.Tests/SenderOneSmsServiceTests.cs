using System.Threading;
using AutoFixture;
using AutoFixture.AutoMoq;
using MassTransit;
using Moq;
using NUnit.Framework;
using SmsService.Definition.Core;
using SmsService.Definition.Events;
using SmsService.Domain.Requests;
using SmsService.SenderOne;

namespace SmsService.Domain.Tests
{
    public class SenderOneSmsServiceTests
    {
        private IFixture _fixture;
        private Mock<IBus> _serviceBus;
        private SenderOneSmsService _smsServiceTests;
        
        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization {ConfigureMembers = true});
            
            _serviceBus = _fixture.Freeze<Mock<IBus>>();
            _smsServiceTests = _fixture.Create<SenderOneSmsService>();
        }
        
        [Test]
        public void SendSms_With_Valid_CountryCode_Returns_True()
        {
            //Arrange && Act && Assert
            Assert.True(_smsServiceTests.CanSend(CountryCode.FR));
        }
        
        [Test]
        public void SendSms_With_Valid_Request_Publishes_Event()
        {
            //Arrange
            var phoneNumber = "+31648429403";
            var request = _fixture.Build<SendSmsApplicationRequest>()
                .With(c => c.Recipient, phoneNumber)
                .WithAutoProperties()
                .Create();
            
            // Act
            _smsServiceTests.SendSms(request);
            
            //Assert
            _serviceBus.Verify(c => c.Publish(It.IsAny<SmsSentEvent>(), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}