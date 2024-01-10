using Labb2.DTOs;
using Labb2.Model;

namespace Labb2.Extensions;
public static class Extensions
{
    public static Author ToAuthor(this CreateAuthorDTO createAuthorDTO)
    {
        return new Author
        {
            FirstName = createAuthorDTO.FirstName,
            LastName = createAuthorDTO.LastName
        };
    }    
}