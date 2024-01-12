using Labb2.Model;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Labb2.DTOs;

public class AuthorDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public List<string> BookTitles { get; } = new();
}

