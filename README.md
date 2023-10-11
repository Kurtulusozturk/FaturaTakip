# FaturaTakip

Project Video: https://drive.google.com/file/d/1NhJhR8GYlRyL5uG7NtExpXV5f56mZ1V0/view?usp=drive_link

TR: Bu projede .NET Core kullanarak, şirketlerin fatura kaydı yapıp faturalarını kontrol ettiği bir web sitesi geliştirdim.Ayrıca şirketlerin müşterilerini de kaydedilebileceği bir kısmı da var. Böylelikle faturaları ve müşterileri eşleştirebiliyorsunuz.
Biraz da projenin yazılım kısmından bahsetmek istiyorum; Front-end kısmında Html, CSS ve Bootstrap kullandım. Back-End kısmında ise her yapmam gereken işlem için yazmış olduğum apiye istek atıp dönen cevaba göre işlemlerimi gerçekleştirdim.

EN: In this project, using .NET Core, I developed a website where companies register and check their invoices.There is also a section where companies can register their customers. This way, you can match invoices and customers.
I would like to talk a little about the software part of the project; I used Html, CSS and Bootstrap in the front-end. In the Back-End section, I sent a request to the API I wrote for each operation I needed to perform and carried out my operations according to the response.
# JWT(JSON Web Token):
TR:Web projelerimizi geliştirirken kullanıcı kimliklendirme/yetkilendirme işlemi oldukça önemlidir. Uygulamamızı yetkisiz kişilerden korumak ve yalnızca yetkili kullanıcıların erişimi için çeşitli yöntemler kullanırız. Bende bunun için JWT token kullandım. JWT token içerisine kullanıcı ile ilgili bilgiler saklayabildiğiniz, geçerlilik süresi belirtebildiğiniz bir teklonojidir. API tarafında oluşturduğum bu JWT tokenim bana Client tarafından istek atarken isteği atan kullanıcı ile ilgili bilgiler sağlamaktadır. Siz de projenizde bu teknolojiyi kullanmak istiyorsanız benim de yardım aldığım bu videodan yardım alabilirsiniz.

EN:User identification/authorization is very important when developing our web projects. We use various methods to protect our application from unauthorized persons and ensure access only to authorized users.I used JWT token for this. JWT is a technology where you can store user-related information in the token and specify the validity period. This JWT token, which I created on the API side, sends me a request from the Client side. It provides information about the user who posted it. If you want to use this technology in your project, you can get help from this video, which I also got help from.

https://www.youtube.com/watch?v=062BBfvMB7s&ab_channel=FatihBaytar
# HTTP cookies:
TR:Cookie size kullanıcı ile ilgili bilgileri login olurken elinizde tutabileceğiniz ve bunu projenin her kısmından çağırabileceğiniz bir olanak sağlar. Yani projenizde size bir bilgi projenin birçok kısmında lazım olacaksa cookie kullanmak çok mantıklı olacaktır. Ben bu projemde kullanıcı giriş yaparken onunla ilgili JWT token, User Id ve User Email bilgilerini tuttum çünkü bunlar bana apiye istek atarken lazım olacak değerlerdir.

EN:The cookie allows you to keep information about the user while logging in and call it from any part of the project. So, if you will need information in many parts of your project, it would be very logical to use cookies. In this project, I kept the JWT token, User Id and User Email information about the user while logging in because these are the values ​​I will need when sending a request to the API.
# BCrypt in .NET:
TR:BCrypt kütüphanesi kullanıcının şifresini Hash lemeye yarayan bir kütüphanedir. Bu tarz algoritmalar kullanmak güvenlik açısından çok önemlidir hem kullanıcının şifresi açık bir şekilde db de gözükmez hem de API request ve response larında size ve kullanıcılarınıza güvenlik sağlar.

EN:BCrypt library is a library used to Hash the user's password. Using such algorithms is very important in terms of security, as the user's password is not clearly visible in the db and it provides security to you and your users in API requests and responses.

<img width="692" alt="m8opZ" src="https://github.com/Kurtulusozturk/FaturaTakip/assets/92689191/9ec01dee-511d-4c32-b088-41e961244eb1">

# Fatura Takip API's:
TR:Bu kısımda ise resimde de gördüğünüz gibi benim isteklerime göre bana cevaplar döndürecek API ler yazdım. Entity framework kullanarak, MSSQL bağlantılarımı oluşturup Data Base oluşturduktan sonra API işlemlerimi gerçekleştirdim.

EN:In this section, as you can see in the picture, I wrote APIs that will return me answers according to my requests. Using the Entity framework, I created my MSSQL connections and created the Data Base and then performed my API operations.

![NET Api](https://github.com/Kurtulusozturk/FaturaTakip/assets/92689191/6549d7b6-3557-467f-a6ae-1fc10e8980c0)
