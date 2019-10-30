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
            var dem = 1;
            return await Success(new List<dynamic>
            {
                new
                {
                    id = ++dem,
                    image = "http://jxbachu.com/include/web_skins/tisu/images/tai_game_cai_dat_img.png"
                },
                new
                {
                    id = ++dem,
                    image = "http://jxbachu.com/include/web_skins/tisu/images/trang.jpg"
                },
                new
                {
                    id = ++dem,
                    image = "http://jxbachu.com/include/web_skins/tisu/images/cong_thanh_dai_chien_img.png"
                }
            });
        }
    }
}