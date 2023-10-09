namespace CoverShop.Application.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
internal class AuthorizeAttribute : Attribute
{
    public string Claims = String.Empty; 
    public string Roles = String.Empty;
    public string Policy = String.Empty;
}
