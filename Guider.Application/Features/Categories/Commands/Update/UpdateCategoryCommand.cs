using Guider.Application.Common;
using Guider.Application.Common.Models;
using Guider.Application.Features.Categories.Commands.Create;
using Guider.Application.Features.Categories.Models;
using Guider.Domain.Categories;
using Guider.Domain.Categories.ValueObjects;
using MediatR;

namespace Guider.Application.Features.Categories.Commands.Update;

public sealed record UpdateCategoryCommand(
    Guid Id, string Name, string Description) : IRequest<CategoryResult>;

internal sealed class UpdateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateCategoryCommand, CategoryResult>
{
    public async Task<CategoryResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryToUpdate = await categoryRepository.GetByIdAsync(CategoryId.Convert(request.Id), cancellationToken);

        if (categoryToUpdate is null)
        {
            throw new ArgumentException(nameof(request.Id));
        }
        
        categoryToUpdate.Update(request.Name, request.Description);
        await unitOfWork.SaveAsync(cancellationToken);
        return new CategoryResult(categoryToUpdate.Id.Value, categoryToUpdate.Name, categoryToUpdate.Description);
    }
}