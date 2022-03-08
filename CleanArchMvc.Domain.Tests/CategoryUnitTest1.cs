using CleanArchMvc.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace CleanArchMvc.Domain.Tests
{
    public class CategoryUnitTest1
    {
        [Fact(DisplayName = "Create Category With Valid State")]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Category(1, "Category Name");
            action.Should()
                .NotThrow<CleanArchMvc.Domain.Validations.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Category With Negative Id Value")]
        public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Category(-1, "Category Name");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validations.DomainExceptionValidation>().WithMessage("Invalid Id Value");
        }

        [Fact(DisplayName = "Create Category With Short Name Value")]
        public void CreateCategory_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Category(1, "Ca");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validations.DomainExceptionValidation>().WithMessage("Invalid name.Name, too short, minimum 3 characters");
        }

        [Fact(DisplayName = "Create Category With Missing Name Value")]
        public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new Category(1, "");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validations.DomainExceptionValidation>().WithMessage("Invalid name.Name. Name is required");
        }
    }
}
