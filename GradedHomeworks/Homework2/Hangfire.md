## Hangfire Nedir?
Hangfire, NET ve .NET Core uygulamalar�nda arka planda i�lem (background task) yapman�n  kolay bir yoludur.
Hangfire kullanabilmek i�in gerekli Nuget paketini indirmek ve bir RDBMS ya da NoSQL kullanmak yeterlidir. 
![hangfire-packages](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/hangfire/hangfire-packages.png)

Hangfire; t�rler, y�ntem adlar�, arg�manlar vb. gibi t�m ayr�nt�lar� veri taban�nda tutar. Bu y�zden veri taban� ba�lant�s�na ihtiyac�m�z vard�r.
![hangfire-db](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/hangfire/hangfire-db.png)

Hangfire ve Hangfire Dashboard'u kullanabilmemiz i�in Program.cs'te yap�lmas� gereken konfig�rasyonlar �u �ekildedir:
```c#
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add database
var connectionString = builder.Configuration.GetConnectionString("HangfireConnection");
builder.Services.AddHangfire(configuration => configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_180).UseSimpleAssemblyNameTypeSerializer().UseRecommendedSerializerSettings().UseSqlServerStorage(connectionString));

builder.Services.AddHangfireServer();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseHangfireDashboard();

app.UseRouting();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHangfireDashboard();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

```

Konfig�rasyon hakk�nda daha detayl� bilgi i�in: [Hangfire Documentation](https://docs.hangfire.io/en/latest/getting-started/aspnet-core-applications.html)

## Hangfire Job T�rleri

### 1. Fire-and-Forget Jobs (Tek Seferlik G�revler)
Bu t�r job, g�revi bir kez �al��t�r�r ve sonucunu takip etmez. Sadece g�revi ba�lat�r ve geri d�ner.

**�rnek:** Sipari� onayland�ktan sonra e-fatura �retmek istedi�imiz varsayal�m. 

![fire-and-forget-jobs--1](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/hangfire/continuation-jobs-1.png)

Bu job'� �retmek i�in `Enqueue` metodu kullan�l�r:
```cs
var jobId = BackgroundJob.Enqueue(() => CreateInvoice());
```

`Confirm` butonuna bas�ld���nda job olu�turulur:

![fire-and-forget-jobs-2](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/hangfire/continuation-jobs-2.png)

### 2. Continuation / Dependent Jobs  (Devam Eden G�revler)
Bu �zellik, bir g�revin tamamlanmas�ndan sonra otomatik olarak ba�ka bir g�revin ba�lat�lmas�n� sa�lar. 

**�rnek:** Yukar�daki senaryodan devam edecek olursak, fatura olu�turulduktan sonra faturan�n kullan�c�n�n mailine g�nderilmesini isteyebiliriz. Bu durumda mail g�nderme i�lemi fatura olu�turma i�ine ba�l� olmu� olur.

Bu t�r job'� �retmek i�in ba��ml� olunan `jobId` ile `ContinueJobWith` metodu kullan�l�r:
```cs
var jobId = BackgroundJob.Enqueue(() => CreateInvoice());
BackgroundJob.ContinueJobWith(jobId, () => SendOrderInvoiceMail());
```

![continuation-jobs](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/hangfire/continuation-jobs-3.png)

### 3. Recurring Jobs  (Tekrarlanan G�revler)
Belirli aral�klarla tekrarlanan bir g�revi olu�turmak i�in bu t�r job kullan�l�r. Zamanlama format� `Cron` ifadesiyle belirtilir.

**�rnek:** Kullan�c�lara d�zenli olarak her hafta kampanyalar ve promosyonlar hakk�nda bilgilendirici mail at�lmas�n� isteyebiliriz.

Bu t�r job'� a�a��daki gibi �retebiliriz:
```cs
RecurringJob.AddOrUpdate(() => SendPromotionMail(), recurrencePattern);
```

![recurring-jobs-2](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/hangfire/recurring-job-1.png)

Kullan�c� `Subscribe` butonuna bast���nda tekrarlanan g�rev olu�ur:

![recurring-jobs-2](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/hangfire/recurring-job-2.png)

### 4. Delayed / Scheduled Jobs  (Gecikmeli / Planl� G�revler)
Bu t�r job, belirli bir s�re sonra g�revi �al��t�r�r. Gecikme s�resi `TimeSpan` olarak belirtilir. 

**�rnek:** Kullan�c�n�n ba�ka bir kullan�c�ya zaman� planlanm�� mesajlar atmas�n� sa�layabiliriz.

Bu t�r job'� a�a��daki gibi �retebiliriz:
```cs
TimeSpan timeDifference = DateTime.Now - model.DateTime;
int jobDelayInSeconds = (int)timeDifference.TotalSeconds;
var jobId = BackgroundJob.Schedule(() => SendMail(model.Message, model.To), TimeSpan.FromSeconds(jobDelayInSeconds));
```

![scheduled-jobs-1](https://github.com/snnehir/turkcellGY3/blob/master/GradedHomeworks/Homework2/images/hangfire/delayed-scheduled-jobs.png)

Kullan�c� `Send` butonuna bast���nda g�rev olu�ur:

![scheduled-jobs-1](https://github.com/snnehir/turkcellGY3/blob/master/GradedHomeworks/Homework2/images/hangfire/delayed-scheduled-jobs-2.png)
