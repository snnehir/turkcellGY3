## Anket Uygulamas�

### Uygulama Hakk�nda

ASP.NET Core Web API ve ASP.NET Core MVC kullan�larak kay�tl� kullan�c�lar�n anket olu�turabilece�i, anketlerini ba�kalar�yla payla�abilece�i ve anket sonu�lar�n� g�r�nt�leyebilece�i bir uygulamad�r.

### Veri taban� diyagram�

![diagram](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/FinalHomework/images/db-diagram.png) 

Projede kullan�lan veri taban� diagram�na [drawSql](https://drawsql.app/teams/me-438/diagrams/survey-app) �zerinden de ula�abilirsiniz.


### Authentication ve Authorization

API taraf� yetkilendirme i�lemleri i�in JWT authentication mekanizmas�n� kulland�m.

MVC (frontend) taraf�nda ise Role-Based Cookie Authentication kulland�m.

�stemciden sunucu taraf�na yetkilendirme gereken bir istek ger�ekle�tirilecekse iste�e "Authorization header" eklenmelidir. Bunun i�in istemci taraf�nda `AuthHeaderHandler` isimli bir DelegateHandler tan�mlad�m. Ayr�ca bu handler sayesinde API taraf�ndan "unauthorize" hatas� al�nd���nda token yenileme i�lemi de ger�ekle�tirilir. Detayl� bilgi i�in Refit'in resmi d�k�mantasyonu inceleyebilirsiniz: [setting-request-headers](https://github.com/reactiveui/refit#setting-request-headers)


### Caching

Web API stateless bir yap�da oldu�undan kullan�c�larla ilgili bilgi tutmaz. Bundan dolay� bir kullan�c� giri� yapt��� zaman olu�turulan "access token" ve "refresh token" bilgilerini tutmak i�in �l�eklenebilirlik a��s�ndan InMemoryCache'e g�re daha avantajl� oldu�undan Redis'ten yararland�m.

Docker �zerinden lokal Redis'e ba�lanmak i�in bu komutu �al��t�rabilirsiniz:

```
docker run -d --name redis-server -p 6379:6379 redis/redis-stack-server:latest
```

Redis'in projeye eklenmesi i�in Microsoft'un d�k�mantasyonunu inceleyebilirsiniz: [distributed-redis-cache](https://learn.microsoft.com/en-us/aspnet/core/performance/caching/distributed?view=aspnetcore-7.0#distributed-redis-cache)


### API Consuming

MVC k�sm�nda yap�lan API isteklerinin daha kolay bir �ekilde y�netilmesini sa�lad���n� d���nd���m i�in Refit paketini kulland�m. Refit sayesinde API istekleri, tan�mlanan interface metotlar� arac�l���yla kolayca ger�ekle�tirilebilir. Daha detayl� bilgi i�in: [Refit](https://github.com/reactiveui/refit)


### Mapping

Mapping k�t�phanesi olarak di�er alternatiflerden daha performansl� oldu�u ve kullanma kolayl���ndan �t�r� [Mapster](https://github.com/MapsterMapper/Mapster) k�t�phanesini tercih ettim.

