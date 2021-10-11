using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        //Burası bir passwordun hashini oluşturmaya yarar
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        //bir password vereceğiz dışarıya out ile iki değeri çıkarak bir yapı tasarlayacağız
        {
            //burdaki hmac kriptografi sınıfında kullandığımız classa karşılık geliyor.
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key; //burdaki key her kullanıcı için bir key oluşturur oldukça güvenlidir.. o yüzden db de salt değeri de tutacağımızı söyledik bu sayede  istediğimiz zaman değiştirebiliriz
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                //passwordun string değerini yollayamıyoruz byte değeri gerekiyor, encoding ile bu çeviriyi sağlayabiliyoruz...
            }
        }

        //Burası sonradan sisteme girmek isteyen kişinin verdiği password un bizim db'deki hash ile ilgili salt a göre eşleşip eşleşmediğine bakıyor
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        //burada outlar olmamalı çünkü hash ı ve saltı biz vereceğiz verify yapıyoruz.
        //kullanıcının verdiği passwordunu aynı algoritma ile hashleseydin karşına böyle birşey çıkar mıydı çıkmaz mıydı ona bakıyor
        //eğer iki hash değeri eşit ise true değilse false.
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) //kullanılan keyi ister, passwordSaltı vermeliyiz.
            {
                //sisteme kullanıcı tekrar girmeye çalışıyor, tekrardan girerken verdiği parolayı yukarıda yaptığımız gibi tekrardan oluşturacağız
                var computedHash= hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                //oluşacak değeri karşılaştırmalıyız yukarıda compute edilen hash yukarıdaki salt kullanılarak yapılıyor.
                //oluşan değer bir byte array'dir değerleri aynı mı diye bakalım
                for (int i = 0; i < computedHash.Length; i++) //hesaplanan hash in bütün değerlerini tek tek dolaş
                {
                    if (computedHash[i]!=passwordHash[i]) // eğer computed hash in İ'ninci değeri db'den gelen hash İ'ye eşit değilse
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
