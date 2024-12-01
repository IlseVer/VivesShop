using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VivesShop.UI.Mvc.Core;
using VivesShop.UI.Mvc.Models;

namespace VivesShop.UI.Mvc.Controllers
{
    public class ShopController : Controller
    {
        private readonly VivesShopDbContext _vivesShopDbContext;

        public ShopController(VivesShopDbContext vivesShopDbContext)
        {
            _vivesShopDbContext = vivesShopDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new ShopViewModel
            {
                ShopItems = _vivesShopDbContext.Shop.ToList(),
                OrderItems = _vivesShopDbContext.Orders.Include(o => o.ShopItem).ToList()
            };

            return View(viewModel);
        }

        //REMOVE FROM CART
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int shopItemId)
        {
            var orderItem = _vivesShopDbContext.Orders.FirstOrDefault(o => o.ShopItemId == shopItemId);
            if (orderItem == null)
            {
                orderItem = new OrderItem { ShopItemId = shopItemId, Quantity = 1 };
                _vivesShopDbContext.Orders.Add(orderItem);
            }
            else
            {
                orderItem.Quantity++;
                _vivesShopDbContext.Orders.Update(orderItem);
            }
            _vivesShopDbContext.SaveChanges();
            return RedirectToAction("Index");
        }


        //REMOVE FROM CART
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int shopItemId)
        {
            var orderItem = _vivesShopDbContext.Orders.FirstOrDefault(o => o.ShopItemId == shopItemId);

            if (orderItem != null)
            {
                // Als de hoeveelheid groter is dan 1, verlaag de hoeveelheid
                if (orderItem.Quantity > 1)
                {
                    orderItem.Quantity--;
                    _vivesShopDbContext.Orders.Update(orderItem);
                }
                // Anders, verwijder het item uit de winkelmand
                else
                {
                    _vivesShopDbContext.Orders.Remove(orderItem);
                }

                _vivesShopDbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        //CHECKOUT
        [HttpGet]
        public IActionResult Checkout()
        {
            var orderItems = _vivesShopDbContext.Orders.Include(o => o.ShopItem).ToList();
            if (orderItems is null)
            {
                return RedirectToAction("Manage");
            }
            return View(orderItems);
        }

        //MANAGE
        [HttpGet]
        public IActionResult Manage()
        {
            var shopItems = _vivesShopDbContext.Shop.ToList();
            return View(shopItems);
        }

        //CREATE
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product item)
        {
            _vivesShopDbContext.Shop.Add(item);
            _vivesShopDbContext.SaveChanges();
            return RedirectToAction("Manage");
        }

        //EDIT
        [HttpGet]
        public IActionResult Edit([FromRoute] int id)
        {
            var item = _vivesShopDbContext.Shop.FirstOrDefault(i => i.Id == id);
            if (item is null)
            {
                return RedirectToAction("Manage");
            }
            return View(item);
            //return RedirectToAction("Manage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product item)
        {
            _vivesShopDbContext.Shop.Update(item);
            _vivesShopDbContext.SaveChanges();
            return RedirectToAction("Manage");
        }

        //DELETE PRODUCT
        [HttpGet]
        public IActionResult Delete([FromRoute]int id)
        {
            var item = _vivesShopDbContext.Shop.FirstOrDefault(i => i.Id == id);
            if (item is null)
            {
                return RedirectToAction("Manage");
            }
            return View(item);
        }


        [HttpPost]
        [Route("Shop/delete/{id:int?}")] 
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = new Product
            {
                Id = id,
                Name = string.Empty,
                Price = 0
            };

            _vivesShopDbContext.Shop.Attach(product);
            _vivesShopDbContext.Shop.Remove(product); 
            _vivesShopDbContext.SaveChanges();  //het effectief verwijderen

            return RedirectToAction("Manage");
        }

    }
}


