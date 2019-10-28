using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace API.Models.Banner
{
    public class GetBannerAction : CommandBase<IEnumerable<dynamic>>
    {
        protected override async Task<Result<IEnumerable<dynamic>>> ExecuteCore(ObjectContext context)
        {
            return await Success(new List<dynamic>
            {
                new
                {
                    image = "http://jxbachu.com/include/web_skins/tisu/images/tai_game_cai_dat_img.png"
                },
                new
                {
                    image = "http://jxbachu.com/include/web_skins/tisu/images/trang.jpg"
                },
                new
                {
                    image = "http://jxbachu.com/include/web_skins/tisu/images/cong_thanh_dai_chien_img.png"
                }
            });
        }
    }
}