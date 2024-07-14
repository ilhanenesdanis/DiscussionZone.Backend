using DiscussionZone.Application.Constants;
using DiscussionZone.Application.DTOs;
using DiscussionZone.Application.Repository;
using MediatR;

namespace DiscussionZone.Application.Features.Command.Category.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest, BaseResponse<CreateCategoryCommandResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<BaseResponse<CreateCategoryCommandResponse>> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.AddAsync(new Domain.Category
            {
                Name = request.Name,
                ParentId = request.ParentGUID,
            });

            if (result > 0)
                return BaseResponse<CreateCategoryCommandResponse>.Success(new CreateCategoryCommandResponse(), ResponseMessage.SuccessMessage);
            return BaseResponse<CreateCategoryCommandResponse>.Error(ResponseMessage.FailMessage);

        }
    }
}
