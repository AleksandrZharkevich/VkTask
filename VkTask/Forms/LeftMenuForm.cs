using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace VkTask.Forms
{
    public class LeftMenuForm : Form
    {
        public ILink ProfileLink => ElementFactory.GetLink(By.Id("l_pr"), "Profile link");

        public LeftMenuForm() : base(By.Id("side_bar_inner"), "Left menu")
        { }

        public void OpenProfilePage()
        {
            ProfileLink.ClickAndWait();
        }
    }
}
