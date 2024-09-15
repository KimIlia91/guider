using ErrorOr;
using Guider.Application.Common;
using Guider.Domain.Categories;
using Guider.Domain.Categories.ValueObjects;
using Guider.Domain.Common.Errors;
using MediatR;

namespace Guider.Application.Features.Categories.Commands.Delete;

public sealed record DeleteCategoryCommand(Guid Id) : IRequest<ErrorOr<CategoryId>>;

internal sealed class DeleteCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteCategoryCommand, ErrorOr<CategoryId>>
{
    public async Task<ErrorOr<CategoryId>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(CategoryId.Convert(request.Id), cancellationToken);

        if (category is null)
            return Errors.Category.NotFoundById(request.Id);
        
        category.Delete();
        categoryRepository.Update(category);
        await unitOfWork.SaveAsync(cancellationToken);
        return category.Id;
    }
}