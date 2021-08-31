using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages //basit bir mesaj olduğu için static yapıyoruz newlemeden kullanmak adına 
        //messages. tek instance olarak kullanılır.
    {
        //private field olabilirdi bu öyle olunca camelCase yazılacaktı, ancak public olduğu için PascalCase olarak yazıldı. ProductAdded
        public static string ProductAdded = "Ürün eklendi.";
        public static string ProductNameInvalid = "Ürün adı geçersiz.";
        public static string MaintenanceTime = "Sistem bakımda.";
        public static string ProductsListed = "Ürünler listelendi.";
    }
}
