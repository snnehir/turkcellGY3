## Kidega Projesi

### Ekranlar

Katmanlý mimari yapýsý ile ASP.NET Core kullanarak geliþtirdiðim Kidega klon proje ekranlarý þu þekildedir:

1. Ana sayfa, kategoriler, yazar sayfasý

![index](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/kidega-app/index.gif)


2. Sepet iþlemleri sayfasý

![cart](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/kidega-app/cart.gif)


3. Yeni üye kaydolma sayfasý

![cart](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/kidega-app/signup.gif)


4. Giriþ yapma sayfasý

![cart](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/kidega-app/login.gif)


### Veri tabaný diagramý

![cart](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/kidega-app/db-diagram.png)
Projede kullanýlan veri tabaný diagramýna [drawSql](https://drawsql.app/teams/me-438/diagrams/kidega-clone) üzerinden de ulaþabilirsiniz.


### Mapping

Mapping kütüphanesi olarak diðer alternatiflerden daha performanslý olduðu ve kullanma kolaylýðýndan ötürü [Mapster](https://github.com/MapsterMapper/Mapster) kütüphanesini tercih ettim.

Mapster kullanmak için öncelikle gerekli Nuget paketi indirilir.

![mapster](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/kidega-app/mapster.png)

Sonrasýnda bir extension metodu aracýlýðýyla dönüþümler eklenebilir:

```
public static void MapsterConfigurations(this IServiceCollection services)
{
    TypeAdapterConfig<Book, BookDisplayResponse>
                .NewConfig().Map(dest => dest.Author, src => $"{src.Author.FirstName} {src.Author.LastName}")
                            .Map(dest => dest.AuthorId, src => src.AuthorId);
    TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
}
```

`Program.cs` dosyasýna extension eklenir:

``` 
builder.Services.MapsterConfigurations(); 
```


Dönüþtürme iþlemi `Scan` metodu sayesinde projenin her kýsmýnda `Adapt` metodu ile gerçekleþtirilir:

 ```
public async Task<IEnumerable<BookDisplayResponse>> GetBooksByCategoryAsync(int categoryId)
{
    var books = await _bookRepository.GetBooksByCategoryAsync(categoryId);
    return books.Adapt<IEnumerable<BookDisplayResponse>>();
}
 
 ```
