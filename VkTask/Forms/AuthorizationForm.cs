using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace VkTask.Forms
{
    class AuthorizationForm : Form
    {
        public ITextBox InputTextBox(string elementId, string elementName) => ElementFactory.GetTextBox(By.Id($"{elementId}"), elementName);
        public IButton SignInBtn => ElementFactory.GetButton(By.Id("index_login_button"), "SignIn button");

        public AuthorizationForm()
            : base(By.XPath("//form[@id='index_login_form']"), "Authorization form")
        {
        }

        public void Authorize(string email, string password)
        {
            InputTextBox("index_email", "Email").Type(email);
            InputTextBox("index_pass", "Password").Type(password);
            SignInBtn.Click();
        }
    }
}
