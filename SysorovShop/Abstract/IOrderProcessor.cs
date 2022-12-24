using SysorovShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysorovShop.Abstract
{
    public interface IOrderProcessor //будет обрабатывать заказы, отправляя их по электронной почте администратору сайта
        //имея реализацию интерфейса IOrderProcessor и средства для её конфигурирования можно воспользоваться Ninject для создания экземпляров этой реализации 
    {
        void ProcessOrder(Cart cart, ShoppingDetails shoppingDetails);
    }
}