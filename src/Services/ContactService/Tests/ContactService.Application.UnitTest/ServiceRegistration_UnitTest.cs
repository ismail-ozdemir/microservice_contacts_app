﻿using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation;
using ContactService.Application.Dto.PersonDto;
using ContactService.Application.Validators.Person;

namespace ContactService.Application.UnitTest
{
    public class ServiceRegistration_UnitTest
    {
        IServiceCollection _services;

        [SetUp]
        public void Setup()
        {
            _services = new ServiceCollection();
            _services.RegisterApplication();
        }


        #region Mapper Resolve
        [Test]
        public void ServiceRegistration_ResolveMapper_IsValid()
        {
            var provider = _services.BuildServiceProvider();
            var mapper = provider.GetRequiredService<IMapper>();
            Assert.Pass("Mapper resolve edildi");
        }
        #endregion

        #region Validator Resolve
        [Test]
        public void ServiceRegistration_ResolveCreatePersonRequestValidator_IsValid()
        {
            var provider = _services.BuildServiceProvider();
            var validator = provider.GetRequiredService<IValidator<CreatePersonRequest>>();
            Assert.IsTrue(validator.GetType() == typeof(CreatePersonValidator));
        }
        #endregion
    }
}
