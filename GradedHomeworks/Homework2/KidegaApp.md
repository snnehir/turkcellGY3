## Kidega Projesi

### Ekranlar

Katmanlı mimari yapısı ile ASP.NET Core kullanarak geliştirdiğim Kidega klon proje ekranları şu şekildedir:

1. Ana sayfa, kategoriler, yazar sayfası

![index](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/kidega-app/index.gif)


2. Sepet işlemleri sayfası

![cart](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/kidega-app/cart.gif)


3. Yeni üye kaydolma sayfası

![cart](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/kidega-app/signup.gif)


4. Giriş yapma sayfası

![cart](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/kidega-app/login.gif)


### Veri tabanı diagramı

![cart](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/kidega-app/db-diagram.png) 

Projede kullanılan veri tabanı diagramına [drawSql](https://drawsql.app/teams/me-438/diagrams/kidega-clone) üzerinden de ulaşabilirsiniz.


### Mapping

Mapping kütüphanesi olarak diğer alternatiflerden daha performanslı olduğu ve kullanma kolaylığından ötürü [Mapster](https://github.com/MapsterMapper/Mapster) kütüphanesini tercih ettim.

Mapster kullanmak için öncelikle gerekli Nuget paketi indirilir.

![mapster](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/kidega-app/mapster.png)

Sonrasında bir extension metodu aracılığıyla dönüşümler eklenebilir:

```
public static void MapsterConfigurations(this IServiceCollection services)
{
    TypeAdapterConfig<Book, BookDisplayResponse>
                .NewConfig().Map(dest => dest.Author, src => $"{src.Author.FirstName} {src.Author.LastName}")
                            .Map(dest => dest.AuthorId, src => src.AuthorId);
    TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
}
```

`Program.cs` dosyasına extension eklenir:

``` 
builder.Services.MapsterConfigurations(); 
```


Tanımladığımız konfigürasyona `Scan` metodu sayesinde projenin her kısmından erişilebilir ve dönüştüme işlemi `Adapt` metodu ile yapılır:

```
public async Task<IEnumerable<BookDisplayResponse>> GetBooksByCategoryAsync(int categoryId)
{
    var books = await _bookRepository.GetBooksByCategoryAsync(categoryId);
    return books.Adapt<IEnumerable<BookDisplayResponse>>();
}
```
