using System;
using System.Collections.Generic;
using System.Text;
using Armut.CaseStudy.Operation.UserServices;
using Moq;
using Xunit;

namespace Armut.CaseStudy.UnitTests.Services
{

    public class UserServiceTests 
    {
        private readonly IMock<IUserService> mockService;
        public UserServiceTests(IMock<IUserService> mock)
        {
            mockService = mock;
        }

        [Fact]
        public void Login_NullParameter_ReturnErrorMessage()
        {

        }

    }
}
