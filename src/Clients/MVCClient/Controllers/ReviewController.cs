using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MVCClient.Controllers
{
    [Controller]
    public class ReviewController : Controller
    {
        [HttpGet("/v1/Review/{id}")]
        public async Task<string> GetAllReviewsByProductId(Guid id)
        {
            var content = new[]
            {
                new { stars = 3, comment = "Lorem Ipsum is simply dummy text of the pr make but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", userName = "Kevin", date = new DateTime(2020,1,10).ToShortDateString()},
                new { stars = 1, comment = "Lorem Ipsum is simply dummy text of the pr make but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", userName = "Deyber", date = new DateTime(2020,11,1).ToShortDateString()},
                new { stars = 5, comment = "Lorem Ipsum is simply dummy text of the pr make but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", userName = "German", date = new DateTime(2019,4,11).ToShortDateString()},
                new { stars = 4, comment = "Lorem Ipsum is simply dummy text of the pr make but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", userName = "Marlene", date = new DateTime(2018,5,14).ToShortDateString()},
                new { stars = 1, comment = "Lorem Ipsum is simply dummy text of the pr make but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", userName = "Kiara", date = new DateTime(2021,6,20).ToShortDateString()},
                new { stars = 3, comment = "Lorem Ipsum is simply dummy text of the pr make but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", userName = "Katherine", date = new DateTime(2020,1,5).ToShortDateString()},
            };

            var response = new { content = content, statusCode = 200, title = "All reviews have been reached", succeeded = true};

            return JsonSerializer.Serialize(response);
        }
    }
}
