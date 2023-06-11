## Kidega Projesi

### Ekranlar

Katmanl� mimari yap�s� ile ASP.NET Core kullanarak geli�tirdi�im Kidega klon proje ekranlar� �u �ekildedir:

1. Ana sayfa, kategoriler, yazar sayfas�

![index](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/kidega-app/index.gif)


2. Sepet i�lemleri sayfas�

![cart](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/kidega-app/cart.gif)


3. Yeni �ye kaydolma sayfas�

![cart](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/kidega-app/signup.gif)


4. Giri� yapma sayfas�

![cart](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/kidega-app/login.gif)


### Veri taban� diagram�

![cart](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/kidega-app/db-diagram.png)
Projede kullan�lan veri taban� diagram�na [drawSql](https://drawsql.app/teams/me-438/diagrams/kidega-clone) �zerinden de ula�abilirsiniz.


### Mapping

Mapping k�t�phanesi olarak di�er alternatiflerden daha performansl� oldu�u ve kullanma kolayl���ndan �t�r� [Mapster](https://github.com/MapsterMapper/Mapster) k�t�phanesini tercih ettim.

Mapster kullanmak i�in �ncelikle gerekli Nuget paketi indirilir.

![mapster](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/kidega-app/mapster.png)

Sonras�nda bir extension metodu arac�l���yla d�n���mler eklenebilir:

```
public static void MapsterConfigurations(this IServiceCollection services)
{
    TypeAdapterConfig<Book, BookDisplayResponse>
                .NewConfig().Map(dest => dest.Author, src => $"{src.Author.FirstName} {src.Author.LastName}")
                            .Map(dest => dest.AuthorId, src => src.AuthorId);
    TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
}
```

`Program.cs` dosyas�na extension eklenir:

``` 
builder.Services.MapsterConfigurations(); 
```


D�n��t�rme i�lemi `Scan` metodu sayesinde projenin her k�sm�nda `Adapt` metodu ile ger�ekle�tirilir:

 ```
public async Task<IEnumerable<BookDisplayResponse>> GetBooksByCategoryAsync(int categoryId)
{
    var books = await _bookRepository.GetBooksByCategoryAsync(categoryId);
    return books.Adapt<IEnumerable<BookDisplayResponse>>();
}
 
 ```
