using Common;
using Common.Database;
using Common.Framework;
using Dapper;
using Dapper.FastCrud;
using Entity;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace API.Models
{
    public class GetNewsAction : CommandBase<IEnumerable<dynamic>>
    {
        public int? current_page { get; set; }
        public int? page_size { get; set; }
        public long? total_page { get; set; }
        public long? news_group_id { get; set; }
        protected override async Task<Result<IEnumerable<dynamic>>> ExecuteCore(ObjectContext context)
        {
            var data = await context.Connection
                .FindAsync<News>(s => s.Skip((this.current_page - 1) * this.page_size).Top(this.page_size));
            var total = await this.GetTotal(context);
            return await Success(data.Select(x=> {
                return new
                {
                    title = x.Title,
                    id = x.Id,
                    created_time = x.CreatedDate.Value.ToString("dd-MM-yyyy")
                };
            }),new Paging {
                current_page = this.current_page,
                page_size = this.page_size,
                count = total
            });
        }
        protected override Task ValidateCore(ObjectContext context)
        {
            this.current_page = this.current_page ?? 1;
            this.page_size = this.page_size ?? 16;
            return Task.CompletedTask;
        }
        private Task<int> GetTotal(ObjectContext context)
        {
            return context.Connection.CountAsync<News>(s => s.Where($"NewsGroupId = @id").WithParameters(new { id = this.news_group_id }));
        }
        
    }
}