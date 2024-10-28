using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace RuKiSo.DataAccess
{
    public class RuKiSoDataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public RuKiSoDataContext(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public RuKiSoDataContext(DbContextOptions<RuKiSoDataContext> options) : base(options)
        {

        }

    }
}
