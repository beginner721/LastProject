using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")] //route demek istek yapanlar bize nasıl ulaşsın demektir
    //internetsitesi.com/api/[controller] yazılarak buraya ulaşırlar, burada [controller] = [Products]Controller, parantez içindeki kısım yazılması yeterlidir.
    //alttaki class adından gelir ProductsContoller'ın products'ı kullanılır.
    // internetsitesi.com/api/products
    [ApiController] //burası ATTRIBUTE yani bir class ile ilgili bilgi verme/imzalama, controller oldugunu belli ediyor
    public class ProductsController : ControllerBase
    {
        //Loosely coupled
        //IoC Container -- Inversion of Control 
        IProductService _productService;

        public ProductsController(IProductService productService) //soyuta bağımlı, injection yapıldı
        {
            _productService = productService;
        }

        //internetsitesi.com/api/products/getall oldu çünkü HttpGet'leri birbirinden ayırmak için alias verdik
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            //Dependency chain-- bağımlılık zinciri
           
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);
        }

        //parantez içine string verdikten sonra bu metoda ulaşma yolumuz değişecektir site.com/api/products/getbyid?id=1
        [HttpGet("getbyid")] //tek bir ürün id si ile get yapalım
        //parantez açarak birbirinden ayırmış oluyoruz alias vererek, çünkü ikisi de IActionResult Get, karışabilirler, parantez verdikten sonra Get kısmını değiştirebiliriz ...
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        //internetsitesi.com/api/products/add
        [HttpPost("add")] //Http Post, Post= ben data vereceğim demek, gönderi demektir...
        public IActionResult Add(Product product) //controllerın bildiği yer burası clientten gelen Product neyse onu ekleriz
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        //silme ve güncelleme için de HttpPost kullanılır %99 ölçekte, ancak istenirse güncelleme için HttpPut, silme için HttpDelete kullanılabilir
        
    }
}
