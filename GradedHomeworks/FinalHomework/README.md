## Anket Uygulamasý

### Uygulama Hakkýnda

ASP.NET Core Web API ve ASP.NET Core MVC kullanýlarak kayýtlý kullanýcýlarýn anket oluþturabileceði, anketlerini baþkalarýyla paylaþabileceði ve anket sonuçlarýný görüntüleyebileceði bir uygulamadýr.

### Veri tabaný diyagramý

![diagram](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/FinalHomework/images/db-diagram.png) 

Projede kullanýlan veri tabaný diagramýna [drawSql](https://drawsql.app/teams/me-438/diagrams/survey-app) üzerinden de ulaþabilirsiniz.


### Authentication ve Authorization

API tarafý yetkilendirme iþlemleri için JWT authentication mekanizmasýný kullandým.

MVC (frontend) tarafýnda ise Role-Based Cookie Authentication kullandým.

Ýstemciden sunucu tarafýna yetkilendirme gereken bir istek gerçekleþtirilecekse isteðe "Authorization header" eklenmelidir. Bunun için istemci tarafýnda `AuthHeaderHandler` isimli bir DelegateHandler tanýmladým. Ayrýca bu handler sayesinde API tarafýndan "unauthorize" hatasý alýndýðýnda token yenileme iþlemi de gerçekleþtirilir. Detaylý bilgi için Refit'in resmi dökümantasyonu inceleyebilirsiniz: [setting-request-headers](https://github.com/reactiveui/refit#setting-request-headers)


### Caching

Web API stateless bir yapýda olduðundan kullanýcýlarla ilgili bilgi tutmaz. Bundan dolayý bir kullanýcý giriþ yaptýðý zaman oluþturulan "access token" ve "refresh token" bilgilerini tutmak için ölçeklenebilirlik açýsýndan InMemoryCache'e göre daha avantajlý olduðundan Redis'ten yararlandým.

Docker üzerinden lokal Redis'e baðlanmak için bu komutu çalýþtýrabilirsiniz:

```
docker run -d --name redis-server -p 6379:6379 redis/redis-stack-server:latest
```

Redis'in projeye eklenmesi için Microsoft'un dökümantasyonunu inceleyebilirsiniz: [distributed-redis-cache](https://learn.microsoft.com/en-us/aspnet/core/performance/caching/distributed?view=aspnetcore-7.0#distributed-redis-cache)


### API Consuming

MVC kýsmýnda yapýlan API isteklerinin daha kolay bir þekilde yönetilmesini saðladýðýný düþündüðüm için Refit paketini kullandým. Refit sayesinde API istekleri, tanýmlanan interface metotlarý aracýlýðýyla kolayca gerçekleþtirilebilir. Daha detaylý bilgi için: [Refit](https://github.com/reactiveui/refit)


### Mapping

Mapping kütüphanesi olarak diðer alternatiflerden daha performanslý olduðu ve kullanma kolaylýðýndan ötürü [Mapster](https://github.com/MapsterMapper/Mapster) kütüphanesini tercih ettim.

