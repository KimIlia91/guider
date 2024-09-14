using Guider.Domain.Common.Specifications;
using Guider.Domain.Tags.ValueObjects;

namespace Guider.Domain.Tags;

public interface ITagRepository
{
   Task<Tag?> GetByIdAsync(TagId id, CancellationToken cancellationToken);

   Task<bool> ExistByNameAsync(string tagName, CancellationToken cancellationToken);
   
   Task CreateAsync(Tag tag, CancellationToken cancellationToken);

   void Update(Tag tag);

   Task<List<Tag>> GetAllAsync(
      Specification<Tag, TagId>? specification = null, 
      CancellationToken cancellationToken = default);
}