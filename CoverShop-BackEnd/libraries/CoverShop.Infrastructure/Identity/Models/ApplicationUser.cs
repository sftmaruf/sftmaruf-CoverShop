using Microsoft.AspNetCore.Identity;

namespace CoverShop.Infrastructure.Identity.Models;

public class ApplicationUser : IdentityUser<Guid>
{
    public string Name { get; set; } = default!;
    public string? Image { get; set; }
    public string Timezone { get; set; } = string.Empty;
}
