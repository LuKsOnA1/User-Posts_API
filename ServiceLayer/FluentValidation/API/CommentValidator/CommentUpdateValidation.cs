﻿using EntityLayer.DTOs.API.Comment;
using FluentValidation;
using ServiceLayer.Messages;

namespace ServiceLayer.FluentValidation.API.CommentValidator
{
    public class CommentUpdateValidation : AbstractValidator<CommentUpdateDTO>
    {
        public CommentUpdateValidation()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Content"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Content"))
                .MaximumLength(250).WithMessage(ValidationMessages.MaximumCharacterAllowence("Content", 250));
        }
    }
}
