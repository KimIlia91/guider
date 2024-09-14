using Guider.Application.Common;
using Guider.Application.Features.Categories.Models;
using Guider.Domain.Categories;
using MediatR;

namespace Guider.Application.Features.Categories.Commands.Create;

public sealed record CreateCategoryCommand(string Name, string Description) : IRequest<CategoryResult>;

internal sealed class CreateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateCategoryCommand, CategoryResult>
{
    public async Task<CategoryResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        if (await categoryRepository.ExistByNameAsync(request.Name, cancellationToken))
        {
            throw new ArgumentException(nameof(request.Name));
        }

        var newCategory = Category.Create(request.Name, request.Description);
        await categoryRepository.CreateAsync(newCategory, cancellationToken);
        await unitOfWork.SaveAsync(cancellationToken);
        return new CategoryResult(newCategory.Id.Value, newCategory.Name, newCategory.Description);
    }
}