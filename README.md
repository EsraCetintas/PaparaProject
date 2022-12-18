# PaparaProject

Patika ve Papara ortaklığında düzenlenen .NET Core bootcamp'i için bitirme projesi olarak fatura yönetim sistemi geliştirilmiştir. Bu projede :

- .NET 5 kullanılmıştır.
- Onion mimari tercih edilmiştir.
- Veritabanı olarak MSSQL kullanılmıştır.
- ORM olarak entity framework tercih edilmiştir. Code first yaklaşımıyla geliştirilmiştir.
- Generic repository pattern kullanılmıştır.
- Ödeme servisi mongo kullanılarak geliştirilmiştir ve ayrı bir servis olarak yazılmıştır.
- Mongo, cloud üzerinden kullanılarak veritabanı işlemleri gerçekleştirilmiştir. Cloud üzerinden giriş yaparak projede belirtilen kullanıcı bilgileri ile veritabanına erişilebilmektedir. 
- Rol bazlı yetkilendirilme yapılmıştır. Kimlik doğrulama için JWT tercih edilmiştir.
- Validasyon için fluent validation kütüphanesi kullanılmıştır.
- Cacheleme için InMemory Cache tercih edilmiştir.
- Caching, validation, authorization ve authetication Aspect Oriented Programming yaklaşımıyla sağlanmıştır. 
- Aspect Oriented Progmming için Autofac kütüphanesi kullanılmıştır.
- Job management için Hangfire tercih edilmiştir. Bu sayede fatura ödemeyen kişilere günlük mail atılmaktadır.
- Kart hareketlerinin dökümente edilebilmesi amacıyla Excel oluşturulmaktadır.


