using EyesSave.Domain.Entities;
using EyesSave.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EyesSave.Presentation.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public List<Agent> Agents { get; set; } = null!;

        public MainWindowViewModel()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Agents = context.Agents
                    .Include(at => at.AgentType)
                    .Include(ps => ps.ProductSales)
                    .ThenInclude(P => P.Product)
                    .ToList();
            }
        }
    }
}
