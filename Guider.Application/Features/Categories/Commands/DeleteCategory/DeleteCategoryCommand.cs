using Guider.Application.Common.Repositories;
using Guider.Domain.Categories;
using Guider.Domain.Categories.ValueObjects;
using MediatR;

namespace Guider.Application.Features.Categories.Commands.DeleteCategory;

public sealed record DeleteCategoryCommand(Guid Id) : IRequest;

internal sealed class DeleteCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteCategoryCommand>
{
    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(CategoryId.Convert(request.Id), cancellationToken);

        if (category is null)
            throw new ArgumentException(nameof(request.Id));
        
        category.Delete();
        await categoryRepository.UpdateAsync(category, cancellationToken);
        await unitOfWork.SaveAsync(cancellationToken);
    }
}