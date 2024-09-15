using ErrorOr;
using Guider.Application.Common;
using Guider.Application.Features.Categories.Models;
using Guider.Domain.Categories;
using Guider.Domain.Categories.ValueObjects;
using Guider.Domain.Common.Errors;
using MediatR;

namespace Guider.Application.Features.Categories.Commands.Update;

public sealed record UpdateCategoryCommand(
    Guid Id, string Name, string Description) : IRequest<ErrorOr<CategoryResult>>;

internal sealed class UpdateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateCategoryCommand, ErrorOr<CategoryResult>>
{
    public async Task<ErrorOr<CategoryResult>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(CategoryId.Convert(request.Id), cancellationToken);

        if (category is null) return Errors.Category.NotFoundById(request.Id); 
                
        category.Update(request.Name, request.Description);
        await unitOfWork.SaveAsync(cancellationToken);
        return new CategoryResult(category.Id.Value, category.Name, category.Description);
    }
}