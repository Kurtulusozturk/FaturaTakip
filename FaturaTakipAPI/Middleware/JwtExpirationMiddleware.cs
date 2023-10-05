namespace FaturaTakipAPI.Middleware
{
    public class JwtExpirationMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        public JwtExpirationMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }
        public async Task Invoke(HttpContext context)
        {
            // Burada JWT token süresi kontrolünü gerçekleştirin
            // Token süresi dolmuşsa isteği işlemeyi durdurabilirsiniz
            // Aksi takdirde, isteği devam ettirin           
        }
    }
}
