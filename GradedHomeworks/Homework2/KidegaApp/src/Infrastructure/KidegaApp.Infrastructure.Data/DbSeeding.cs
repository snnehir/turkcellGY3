using KidegaApp.Entities;

namespace KidegaApp.Infrastructure.Data
{
    public static class DbSeeding
    {
        public static void SeedDatabase(KidegaDbContext context)
        {
            seedCategoryIfNotExist(context);
            seedAuthorIfNotExist(context);
            seedBookIfNotExist(context);
            seedBookCategoryIfNotExist(context);
            
        }

        private static void seedCategoryIfNotExist(KidegaDbContext context)
        {
            if(!context.Categories.Any())
            {
                var categories = new List<Category>() 
                {
                    new Category(){CategoryName="Classics"},
                    new Category(){CategoryName="Sci-Fi"},
                    new Category(){CategoryName="Philosophy"},

                };
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }
        }

        private static void seedBookCategoryIfNotExist(KidegaDbContext context)
        {
            if (!context.BookCategories.Any())
            {
                var bookCategories = new List<BookCategory>()
                {
                    new BookCategory(){BookId=1, CategoryId=1},
                    new BookCategory(){BookId=2, CategoryId=1},
                    new BookCategory(){BookId=3, CategoryId=1},
                    new BookCategory(){BookId=4, CategoryId=2},
                };
                context.BookCategories.AddRange(bookCategories);
                context.SaveChanges();
            }
        }

        private static void seedBookIfNotExist(KidegaDbContext context)
        {
            if (!context.Books.Any())
            {
                var books = new List<Book>()
                {
                    new Book(){Title="Satranç", Description="Satranç, Zweig’ın novella türünde eriştiği ustalığı gözler önüne seren en son yapıtı; aynı zamanda bir edebi veda mektubudur. İkinci Dünya Savaşı yüzünden ülkesini terk eden yazarın, çok sevdiği Avrupa’nın içler acısı durumunun ve zorunlu göçün yarattığı derin umutsuzluk sonucu yaşamına son vermesinden bir yıl önce, 1941’de yayımlanmıştır. Buenos Aires’e gitmek üzere New York’tan hareket eden büyük bir geminin yolcuları arasında dünya satranç şampiyonu Mirko Czentovic de vardır. Satranç meraklısı bir grup yolcu kendisiyle oynadıkları ilk partiyi kaybeder. Czentovic ikinci partiyi de kazanmak üzereyken, aniden zuhur eden Dr. B. adlı şahsın müdahale ve yönlendirmesiyle oyun berabere biter. Bunun üzerine Dr. B., Czentovic’le tek başına oynamaya ikna edilir. Ancak bir eğlence olarak başlayan satranç oyunu, Nazilerin yarattığı korku ve dehşet ikliminden payına düşeni alan bu adamda bazı travmatik anıları canlandıracaktır.",
                               AuthorId=1, ImageUrl="https://img1-kidega.mncdn.com/mnresize/260/399/UPLOAD/urunler/9786254293894.jpg", UnitPrice=14},
                    new Book(){Title="80 Günde Devri Alem", Description="Hiç seyahat etmiş miydi? Büyük olasılıkla, çünkü dünya haritasına kimse onun kadar hâkim değildi. Paylaşacak detaylı bilgisinin olmadığı hiçbir yer yoktu. Kulüpte, yolunu kaybetmiş ya da şaşırmış gezginlerle ilgili ortaya atılan binlerce görüşü kimi zaman çok az sözcükle, kısaca ve açık bir şekilde düzeltirdi... Her yeri gezmiş biri olmalıydı, en azından zihninde... Bununla birlikte kesin olan bir şey vardı, o da Phileas Fogg’un senelerdir Londra’dan ayrılmadığıydı. Phileas Fogg, kimsenin hakkında bir şey bilmediği zengin ve kibar bir İngiliz beyefendisidir. Son derece düzenli bir hayat sürmesi, titiz ve dakik olmasıyla ünlüdür. Bir gün üyesi olduğu Londra Reform Kulübü’nde gerçekleştirmesi imkânsız gibi görünen bir konuda iddiaya girer: Dünyanın çevresini 80 günde dolaşacaktır. Fogg tek bir aksilikte her şeyini kaybedebileceği bu seyahate yardımcısıyla çıkar. Onlarca farklı ülkede başına geleceklerden ise habersizdir. 1872’de yayımlandığı günden beri her yaştan okurun hayal gücünü zenginleştiren 80 Günde Devri Âlem, on dokuzuncu yüzyıldaki teknolojik gelişmeler sayesinde uzakların yakınlaşmaya başladığı ve böylece modern kültürün de adım adım değiştiği bir dönemin panoramasını çiziyor.",
                               AuthorId=2, ImageUrl="https://img1-kidega.mncdn.com/mnresize/260/399/UPLOAD/urunler/9786052651698.jpg", UnitPrice=30},
                    new Book(){Title="Dünyanın Merkezine Yolculuk", Description="Jeoloji profesörü Lidenbrock, Arne Saknussemm adında bir kâşifin dünyanın merkezine giden bir yolu tarif ettiği elyazmasına rastlar. Yeğeni Axel’in elyazmasındaki bilmeceyi deşifre etmesiyle profesörün ve Axel’in hayatı sonsuza dek değişir. Sessiz Hans'ın rehberliğinde, yeraltı dünyasındaki tehlikeli, şaşırtıcı ve ürkütücü yolculuklarında tarihöncesi canavarlarla ve türlü tuhaflıklarla karşılaşırlar. Bir Jules Verne klasiği olan Dünyanın Merkezine Yolculuk, harikalar diyarına yapılan hayalî bir yolculuğun tasviriyle okurlarını büyülemeye devam ediyor.",
                               AuthorId=2, ImageUrl="https://img1-kidega.mncdn.com/mnresize/260/399/UPLOAD/urunler/9786257491655.jpg", UnitPrice=24},
                    new Book(){Title="John Carter 8: Mars Kılıçları", Description="Macera dolu serileriyle tüm dünyayı kasıp kavuran Edgar Rice Burroughs, bilimkurgu yazınına yön veren “John Carter” Barsoom dizisiyle karşınızda!\r\n8. kitap “Mars Kılıçları” ile aksiyon dolu hikâye devam ediyor!\r\n\r\n\r\n20. yüzyılın ilk “çok ama çok satan” bilimkurgu klasiği; sonraki kuşak tüm bilimkurgu yazarlarının anılarında büyük yeri olan, etkisi Star Wars dünyasından Avatar filmine kadar her bilimkurgu yapıtında hissedilen, eşsiz bir gezegenlerarası macera dizisi...\r\n\r\n\r\nMars Kılıçları’nda John Carter tekrardan baş kahraman olarak karşımıza çıkıyor ve aksiyon dolu bu maceraya yön veriyor. Ur Jan liderliğindeki bir suikastçı loncasına karşı verdiği mücadele ve kılıç düelloları bizi esere bağlarken, iki rakip bilimadamı Fal Sivas ve Gar Nal’ın rekabetinin ortasında kalıyoruz; kendimizi “yapay beyin” üretme çalışmaları içerisinde buluyoruz.\r\n\r\n\r\nJohn Carter yine aksiyon sınırlarını zorlarken, büyük aşkla bağlı olduğu karısı Dejah Thoris’i kurtarmanın yollarını arıyor ve kılıçlar hiç susmuyor!\r\n\r\n\r\nEdgar Rice Burroughs’un Barsoom dünyası, şanına yaraşır aksiyon dolu macerasına bizleri davet ediyor.\r\n\r\n\r\nHadi, bu sonsuz evrene siz de katılın!",
                               AuthorId=3, ImageUrl="https://img1-kidega.mncdn.com/mnresize/260/399/UPLOAD/urunler/9786256438262.jpg", UnitPrice=87},

                };
                context.Books.AddRange(books);
                context.SaveChanges();
            }
        }

        private static void seedAuthorIfNotExist(KidegaDbContext context)
        {
            if (!context.BookAuthors.Any())
            {
                var authors = new List<Author>()
                {
                    new Author(){FirstName="Stefen", LastName="Zweig", Biography="Stefan Zweig, Avusturya-Macaristanlı roman, oyun, biyografi yazarı ve gazeteciydi. 1920'ler ile 1930'lar arasında edebiyat kariyerinin zirvesinde olmuş Zweig, dönemin dünyasının en çok tercüme edilen ve en popüler yazarlarından biriydi."},
                    new Author(){FirstName="Jules", LastName="Verne", Biography="Jules Gabriel Verne 1828 yılında Fransa’nın Nantes kasabasında doğmuştur. Ailesinin çiftliğinde çocukluğunu geçirmiş olan Verne’nin babası avukat annesi de çok katı kurallı bir İskoç'tu."},
                    new Author(){FirstName="Edgar Rice", LastName="Burroughs", Biography="Edgar Rice Burroughs, Bilmece ve Bulmacalar, Çocuk & Gençlik, Çocuk Öykü & Roman kategorilerinde eserler yazmış bir yazardır.\r\n\r\nBaşlıca kitapları alfabetik sırayla; A Princess of Mars, At the Earth's Core, Tarzan olarak sayılabilir.\r\n\r\nEdgar Rice Burroughs kitapları; Evans Yayınları, Gece Kitaplığı aracılığıyla kitapseverlerle buluşmuştur.\r\nEdgar Rice Burroughs tarafından yazılan son kitap \"A Princess of Mars\", Gece Kitaplığı tarafından okurların beğenisine sunulmuştur."},

                };
                context.BookAuthors.AddRange(authors);
                context.SaveChanges();
            }
        }
    }
}
