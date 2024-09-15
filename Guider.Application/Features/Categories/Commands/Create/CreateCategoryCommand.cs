using ErrorOr;
using Guider.Application.Common;
using Guider.Application.Features.Categories.Models;
using Guider.Domain.Categories;
using Guider.Domain.Common.Errors;
using MediatR;

namespace Guider.Application.Features.Categories.Commands.Create;

public sealed record CreateCategoryCommand(string Name, string Description) 
    : IRequest<ErrorOr<CategoryResult>>;

internal sealed class CreateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateCategoryCommand, ErrorOr<CategoryResult>>
{
    public async Task<ErrorOr<CategoryResult>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        if (await categoryRepository.ExistByNameAsync(request.Name, cancellationToken))
            return Errors.Category.NameConflict(request.Name);
        
        var newCategory = Category.Create(request.Name, request.Description);
        await categoryRepository.CreateAsync(newCategory, cancellationToken);
        await unitOfWork.SaveAsync(cancellationToken);
        return new CategoryResult(newCategory.Id.Value, newCategory.Name, newCategory.Description);
    }
}