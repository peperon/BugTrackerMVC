using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BugTracker.Domain.RepositoryInterfaces;
using BugTracker.Domain.Models;
using BugTracker.Tests.WebUI.Fakes;
using System.Web;
using System.Web.Mvc;
using BugTracker.Controllers;
using BugTracker.Domain.Utilities;
using System.IO;
using BugTracker.Models;
using BugTracker.Infrastructure;

namespace BugTracker.Tests.WebUI
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void Can_Show_Login_Page()
        {
            var mock = new Mock<IUserRepository>();
            mock.Setup(users => users.GetUsers()).Returns(new User[] { });

            var request = new FakeHttpRequest(null, null, null);
            request.SetIsAuthenticated(false);
            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(x => x.Session).Returns(new FakeHttpSessionState(new System.Web.SessionState.SessionStateItemCollection()));
            httpContext.Setup(x => x.Request).Returns(request);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(c => c.HttpContext).Returns(httpContext.Object);

            var accountController = new AccountController(mock.Object);
            accountController.ControllerContext = controllerContext.Object;

            var result = accountController.Login() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Dont_Show_Login_Page_When_User_Is_Authenticated()
        {
            Mock<IUserRepository> userMock = new Mock<IUserRepository>();
            userMock.Setup(users => users.GetUsers()).Returns(new User[] { });

            var request = new FakeHttpRequest(null, null, null);
            request.SetIsAuthenticated(true);
            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(x => x.Session).Returns(new FakeHttpSessionState(new System.Web.SessionState.SessionStateItemCollection()));
            httpContext.Setup(x => x.Request).Returns(request);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(c => c.HttpContext).Returns(httpContext.Object);

            var accController = new AccountController(userMock.Object);
            accController.ControllerContext = controllerContext.Object;

            var result = accController.Login() as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        //[TestMethod]
        //public void Can_Login_With_Valid_Credentials()
        //{
        //    var session = new FakeHttpSessionState(new System.Web.SessionState.SessionStateItemCollection());
        //    var identity = new FakeIdentity("Admin");
        //    var principal = new FakePrincipal(identity, new string[] { });
        //    var httpContext = new Mock<HttpContextBase>();
        //    var controllerContext = new Mock<ControllerContext>();
        //    var userMock = new Mock<IUserRepository>();
        //    var logger = new Mock<ActionLogger>();

        //    userMock.Setup(users => 
        //        users.GetUser("Admin", PasswordUtility.HashPassword("abv123"))).
        //        Returns(new User
        //        {
        //            UserName = "Admin",
        //            Password = PasswordUtility.HashPassword("abv123")
        //        });

        //    logger.Setup(log => log.

        //    session.Add("ChangeRole", "Adminin");
        //    session.Add("IsPersistent", true);
        //    httpContext.Setup(x => x.Session).Returns(session);
        //    httpContext.Setup(x => x.User).Returns(principal);

        //    controllerContext.Setup(c => c.HttpContext).Returns(httpContext.Object);

        //    var accController = new AccountController(userMock.Object);
        //    accController.ControllerContext = controllerContext.Object;
        //    HttpContext.Current = new HttpContext(
        //        new HttpRequest("", "http://tempuri.org", ""),
        //        new HttpResponse(new StringWriter())
        //    );

        //    var model = new LoginModel() 
        //    { 
        //        Password = "abv123", 
        //        UserName = "Admin", 
        //        RememberMe = false 
        //    };
        //    var result = accController.Login(model) as RedirectToRouteResult;

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual("Index", result.RouteValues["action"]);
        //    Assert.AreEqual("Home", result.RouteValues["controller"]);
        //}
    }
}
