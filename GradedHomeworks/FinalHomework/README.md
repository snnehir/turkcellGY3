## Anket Uygulaması

### Uygulama Hakkında

ASP.NET Core Web API ve ASP.NET Core MVC kullanılarak kayıtlı kullanıcıların anket oluşturabileceği, anketlerini başkalarıyla paylaşabileceği ve anket sonuçlarını görüntüleyebileceği bir uygulamadır.

### Veri tabanı diyagramı

![diagram](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/FinalHomework/images/db_diagram.png) 

Projede kullanılan veri tabanı diagramına [drawSql](https://drawsql.app/teams/me-438/diagrams/survey-app) üzerinden de ulaşabilirsiniz.


### Authentication ve Authorization

API tarafı yetkilendirme işlemleri için JWT authentication mekanizmasını kullandım.

MVC (frontend) tarafında ise Role-Based Cookie Authentication kullandım.

İstemciden sunucu tarafına yetkilendirme gereken bir istek gerçekleştirilecekse isteğe "Authorization header" eklenmelidir. Bunun için istemci tarafında `AuthHeaderHandler` isimli bir DelegateHandler tanımladım. Ayrıca bu handler sayesinde API tarafından "unauthorize" hatası alındığında token yenileme işlemi de gerçekleştirilir. Detaylı bilgi için Refit'in resmi dökümantasyonu inceleyebilirsiniz: [setting-request-headers](https://github.com/reactiveui/refit#setting-request-headers)


### Caching

Web API stateless bir yapıda olduğundan kullanıcılarla ilgili bilgi tutmaz. Bundan dolayı bir kullanıcı giriş yaptığı zaman oluşturulan "access token" ve "refresh token" bilgilerini tutmak için ölçeklenebilirlik açısından InMemoryCache'e göre daha avantajlı olduğundan Redis'ten yararlandım.

Docker üzerinden lokal Redis'e bağlanmak için bu komutu çalıştırabilirsiniz:

```
docker run -d --name redis-server -p 6379:6379 redis/redis-stack-server:latest
```

Redis'in projeye eklenmesi için Microsoft'un dökümantasyonunu inceleyebilirsiniz: [distributed-redis-cache](https://learn.microsoft.com/en-us/aspnet/core/performance/caching/distributed?view=aspnetcore-7.0#distributed-redis-cache)


### API Consuming

MVC kısmında yapılan API isteklerinin daha kolay bir şekilde yönetilmesini sağladığını düşündüğüm için Refit paketini kullandım. Refit sayesinde API istekleri, tanımlanan interface metotları aracılığıyla kolayca gerçekleştirilebilir. Daha detaylı bilgi için: [Refit](https://github.com/reactiveui/refit)


### Mapping

Mapping kütüphanesi olarak diğer alternatiflerden daha performanslı olduğu ve kullanma kolaylığından ötürü [Mapster](https://github.com/MapsterMapper/Mapster) kütüphanesini tercih ettim.

