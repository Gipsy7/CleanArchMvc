using CleanArchMvc.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace CleanArchMvc.Domain.Tests
{
    public class ProductUnitTest1
    {
        [Fact(DisplayName = "Create Product With Valid State")]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "product image");
            action.Should()
                .NotThrow<CleanArchMvc.Domain.Validations.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Product With Negative Id Value")]
        public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Product(-1, "Product Name", "Product Description", 9.99m, 99, "product image");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validations.DomainExceptionValidation>().WithMessage("Invalid Id Value");
        }

        [Fact(DisplayName = "Create Product With Short Name Value")]
        public void CreateProduct_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Product(1, "Pr", "Product Description", 9.99m, 99, "product image");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validations.DomainExceptionValidation>().WithMessage("Invalid name.Name, too short, minimum 3 characters");
        }

        [Fact(DisplayName = "Create Product With Missing Name Value")]
        public void CreateProduct_MissingNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new Product(1, "", "Product Description", 9.99m, 99, "product image");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validations.DomainExceptionValidation>().WithMessage("Invalid name.Name. Name is required");
        }

        [Fact(DisplayName = "Create Product With Short Description Value")]
        public void CreateProduct_ShortDescriptionValue_DomainExceptionShortDescription()
        {
            Action action = () => new Product(1, "Product Name", "Pr", 9.99m, 99, "product image");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validations.DomainExceptionValidation>().WithMessage("Invalid description.Description, too short, minimum 3 characters");
        }

        [Fact(DisplayName = "Create Product With Missing Description Value")]
        public void CreateProduct_MissingDescriptionValue_DomainExceptionRequiredDescription()
        {
            Action action = () => new Product(1, "Product Name", "", 9.99m, 99, "product image");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validations.DomainExceptionValidation>().WithMessage("Invalid description.Description. Description is required");
        }

        [Fact(DisplayName = "Create Product With Invalid Price Value")]
        public void CreateProduct_InvalidPriceValue_DomainExceptionInvalidPriceValue()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", -9.99m, 99, "product image");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validations.DomainExceptionValidation>().WithMessage("Invalid price value");
        }

        [Fact(DisplayName = "Create Product With Invalid Stock Value")]
        public void CreateProduct_InvalidStockValue_DomainExceptionInvalidStockValue()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, -99, "product image");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validations.DomainExceptionValidation>().WithMessage("Invalid stock value");
        }

        [Fact(DisplayName = "Create Product With Long Image Name")]
        public void CreateProduct_LongImageName_DomainExceptionLongImageName()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "5R0BSYFSVSwOEfvPGXzQ4Fk49W6y7MoTMOdD8PTgkErBzsb7Hp82JD6IPvbEpqTBL2En7iYwZ8daCBgVqHJ0r1EwK24RKIiwK6HcAC1ShP1bz44cduoAWvQKBDZfg4CAJU9TB5OTi5SINqMbxIiwyUff6EWFDx4EhrjldfEeO2nB51ULVFAZDkmEERNy1ovoQ5g1L6iYE6HyTgdhoTv1v4szqiMOVCui13nvXo00lk083u07gpvDm5PExDC");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validations.DomainExceptionValidation>().WithMessage("Invalid Image name, too long, maximum 250 characters");
        }

        [Fact(DisplayName = "Create Product With Empty Image Name")]
        public void CreateProduct_EmptyImageName_DomainExceptionLongImageName()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "");
            action.Should()
                .NotThrow<CleanArchMvc.Domain.Validations.DomainExceptionValidation>();
        }



    }
}
