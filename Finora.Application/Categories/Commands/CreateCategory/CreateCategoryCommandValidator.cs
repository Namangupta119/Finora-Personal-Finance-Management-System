using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator(ICategoryRepository categoryRepository, ICurrentUserService currentUserService)
        {
            RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Category name is required.")
            .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters.")
            .MustAsync(async (name, CancellationToken) =>
            {
                return !await categoryRepository.ExistsAsync(name, currentUserService.UserId);
            }).WithMessage("Category name already exist");

            RuleFor(x => x.Description)
            .MaximumLength(250).WithMessage("Description cannot exceed 250 characters.");

            RuleFor(x => x.IconKey)
            .NotEmpty().WithMessage("Icon Key is required.");

            RuleFor(x => x.ColorKey)
            .NotEmpty().WithMessage("Color is required");
        }
    }
}
