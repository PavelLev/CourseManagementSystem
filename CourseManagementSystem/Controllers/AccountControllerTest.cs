using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseManagementSystem.Models;
using System.Web;
using Moq;
using System.Web.Routing;


namespace CourseManagementSystem.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void TestRegisterdActionAuthenticate()
        {
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.IsAuthenticated).Returns(true); // or false

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);

            var controller = new AccountController();
            controller.ControllerContext =
                   new ControllerContext(context.Object, new RouteData(), controller);

            // test

            RedirectToRouteResult routeResult = (RedirectToRouteResult)controller.Register();
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);

        }

        [TestMethod]
        public void TestRegisterActionNotAuthenticated()
        {
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.IsAuthenticated).Returns(false);

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);

            var controller = new AccountController();
            controller.ControllerContext =
                   new ControllerContext(context.Object, new RouteData(), controller);

            // test

            ViewResult viewResult = (ViewResult)controller.Register();
            Assert.AreEqual("Register", viewResult.ViewName);
        }

        [TestMethod]
        public void TestLoginActionAuthenticated()
        {
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.IsAuthenticated).Returns(true); // or false

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);

            var controller = new AccountController();
            controller.ControllerContext =
                   new ControllerContext(context.Object, new RouteData(), controller);

            // test

            RedirectToRouteResult routeResult = (RedirectToRouteResult)controller.LogIn("testURL");
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);

        }
        [TestMethod]
        public void TestLoginActionNotAuthenticated()
        {
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.IsAuthenticated).Returns(false); // or false

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);

            var controller = new AccountController();
            controller.ControllerContext =
                   new ControllerContext(context.Object, new RouteData(), controller);

            // test
            ViewResult viewResult = (ViewResult)controller.LogIn("tesrURL");
            Assert.AreEqual("LogIn", viewResult.ViewName);
     
        }

        [TestMethod]
        public void TestRegisterActionModelNotValidView()
        {
            var controller = new AccountController();
            controller.ModelState.AddModelError("test", "test");
            var registrationViewModel = new RegistrationViewModel { Name = "TestName", Email = "testemail@test.com", Password = "testpassword" };
            var actionResultTask = controller.Register(registrationViewModel);
            actionResultTask.Wait();
            var viewResult = actionResultTask.Result as ViewResult;

            //test
            
            Assert.AreEqual("Register", viewResult.ViewName);
        }

        [TestMethod]
        public void TestRegisterActionModelNotValidData()
        {
            var controller = new AccountController();
            controller.ModelState.AddModelError("test", "test");
            var registrationViewModel = new RegistrationViewModel { Name = "TestName", Email = "testemail@test.com", Password = "testpassword" };
            var actionResultTask = controller.Register(registrationViewModel);
            actionResultTask.Wait();
            var viewResult = actionResultTask.Result as ViewResult;

            var redisterViewModelData = (RegistrationViewModel)viewResult.ViewData.Model;
            //test
            Assert.AreEqual("TestName", redisterViewModelData.Name);
            Assert.AreEqual("testemail@test.com", redisterViewModelData.Email);
            Assert.AreEqual("testpassword", redisterViewModelData.Password);
        }
    }
}