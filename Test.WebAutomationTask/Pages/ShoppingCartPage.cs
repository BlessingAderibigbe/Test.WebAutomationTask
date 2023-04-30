using OpenQA.Selenium;
using System;
using System.Linq;
using System.Threading;
using Test.WebAutomationTask.SetUp;

namespace Test.WebAutomationTask.Pages
{
    public class ShoppingCartPage
    {
        private IWebDriver _driver;

        public ShoppingCartPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void AddFourRandomItems()
        {

            try
            {
                var interferingElement = _driver.FindElement(By.CssSelector("[class='site-branding']"));
                var secondInterferingElement = _driver.FindElement(By.CssSelector("[class='site-header']"));
                if (interferingElement != null)
                {
                    ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].remove();", interferingElement);
                    ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].remove();", secondInterferingElement);
                }


                var items = _driver.FindElements(addToCartButton);

                var random = new Random();
                var indexes = Enumerable.Range(0, items.Count).OrderBy(x => random.Next()).Take(4);

                foreach (var index in indexes)
                {
                    items[index].Click();
                    Thread.Sleep(500);
                }
            }
            catch (Exception)
            {

            }
        }

        public void ViewCart()
        {
            try
            {
                _driver.Click(viewCartButton);
            }
            catch (Exception)
            {
                ((IJavaScriptExecutor)_driver).ExecuteScript("document.querySelector('[class='site-branding']').remove();");
                ((IJavaScriptExecutor)_driver).ExecuteScript("document.querySelector('[class='site-header']').remove();");

                _driver.Click(viewCartButton);
            }
        }

        public int VerifyCartItems()
        {
            return _driver.FindElements(cartItems).Count();
        }

        public decimal GetLowestPrice()
        {
            var elements = _driver.FindElements(itemPrice);

            var prices = elements.Select(e => decimal.Parse(e.Text.Replace("$", "")));
            decimal lowestPrice = prices.Min();

            return lowestPrice;
        }

        public void RemoveLowestPriceItem()
        {
            var priceElements = _driver.FindElements(itemPrice);
            var removeButtons = _driver.FindElements(removeItemButton);

            var prices = priceElements.Select(e => decimal.Parse(e.Text.Replace("$", "")));
            int lowestPriceIndex = prices.ToList().IndexOf(prices.Min());

            removeButtons[lowestPriceIndex].Click();
        }















        private By addToCartButton = By.CssSelector("[class*='add_to_cart_button ajax_add_to_cart']");
        private By viewCartButton = By.XPath("(//*[@title='View cart'])[1]");
        private By cartItems = By.CssSelector("[class*='cart_item']");
        private By itemPrice = By.CssSelector("[data-title='Price']");
        private By removeItemButton = By.CssSelector("[aria-label='Remove this item']");
    }
}
