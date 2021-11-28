using AutoFixture;
using AutoFixture.AutoMoq;
using NUnit.Framework;
using SmsService.Definition.Core.Exceptions;
using SmsService.Domain.Requests;

namespace SmsService.Domain.Tests
{
    public class SendSmsApplicationRequestTests
    {
        private IFixture _fixture;
        
        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization {ConfigureMembers = true});
        }
        
        [Test]
        public void Validate_Null_Message_Throws_Exception()
        {
            //Arrange
            var phoneNumber = "+31648429403";
            var request = _fixture.Build<SendSmsApplicationRequest>()
                .With(c => c.Recipient, phoneNumber)
                .Without(c => c.Message)
                .WithAutoProperties()
                .Create();
            
            // Act
            var exception = Assert.Throws<ValidationException>(() => request.Validate());
            
            // Assert
            Assert.True(exception.Message.Contains($"Message must contain value"));
        }
        
        [Test]
        [TestCase("x310631575396")]
        [TestCase("+31080")]
        public void Validate_Invalid_PhoneNumber_Throws_Exception(string phoneNumber)
        {
            //Arrange
            var request = _fixture.Build<SendSmsApplicationRequest>()
                .With(c => c.Recipient, phoneNumber)
                .WithAutoProperties()
                .Create();
            
            // Act
            var exception = Assert.Throws<ValidationException>(() => request.Validate());
            
            // Assert
            Assert.True(exception.Message.Contains($"{phoneNumber} is not a valid phone number"));
        }
        
        [Test]
        public void Validate_Valid_Request_Passes()
        {
            //Arrange
            var phoneNumber = "+31648429403";
            var request = _fixture.Build<SendSmsApplicationRequest>()
                .With(c => c.Recipient, phoneNumber)
                .WithAutoProperties()
                .Create();
            
            // Act
            request.Validate();
            
            // Assert
            Assert.Pass();
        }
    }
}