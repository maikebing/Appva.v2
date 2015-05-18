// <copyright file="AdminModelBinder.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Infrastructure
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Web.Mvc;
    using Appva.Core;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// http://stackoverflow.com/questions/2186969/custom-model-binder-for-a-property?lq=1
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class AdminModelBinder : DefaultModelBinder
    {
        protected override Object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            if (modelType.Equals(typeof(IPersonalIdentityNumber)))
            {
                string isLol = "";
                /*Type instantiationType = typeof(MyDerievedClass);
                var obj = Activator.CreateInstance(instantiationType);
                bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, instantiationType);
                bindingContext.ModelMetadata.Model = obj;
                return obj;*/
            }
            return base.CreateModel(controllerContext, bindingContext, modelType);
        }

        protected override Object GetPropertyValue(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor, IModelBinder propertyBinder)
        {
            if (propertyDescriptor.PropertyType == typeof(PersonalIdentityNumber))
            {
                var Value = bindingContext.ValueProvider.GetValue("PersonalIdentityNumber");
                return new PersonalIdentityNumber(Value.AttemptedValue);
            }
            return base.GetPropertyValue(controllerContext, bindingContext, propertyDescriptor, propertyBinder);
        }

        protected override void SetProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor, Object value)
        {
            base.SetProperty(controllerContext, bindingContext, propertyDescriptor, value);
        }

        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor)
        {
            if (propertyDescriptor.PropertyType == typeof(PersonalIdentityNumber))
            {
                //propertyDescriptor.Name
                var Value = bindingContext.ValueProvider.GetValue("PersonalIdentityNumber");
                
                var item = new PersonalIdentityNumber(Value.AttemptedValue);
                propertyDescriptor.SetValue(bindingContext.Model, item);
                this.SetProperty(controllerContext, bindingContext, propertyDescriptor, item);
                return;
            }
            base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        }
    }
    public class CustomValueProviderAttribute : Attribute, System.Web.ModelBinding.IValueProviderSource
    {

        #region IValueProviderSource Members

        System.Web.ModelBinding.IValueProvider System.Web.ModelBinding.IValueProviderSource.GetValueProvider(System.Web.ModelBinding.ModelBindingExecutionContext modelBindingExecutionContext)
        {
            return new CustomValueProvider(modelBindingExecutionContext);
        }

        #endregion
    }
    public class CustomValueProvider : System.Web.ModelBinding.IValueProvider
    {
        System.Web.ModelBinding.ModelBindingExecutionContext _modelBindingExecutionContext;

        public CustomValueProvider(System.Web.ModelBinding.ModelBindingExecutionContext modelBindingExecutionContext)
        {
            this._modelBindingExecutionContext = modelBindingExecutionContext;
        }

        public bool ContainsPrefix(string prefix)
        {
            // validate if requested key is exist or not
            return false;
        }

        #region IValueProvider Members


        System.Web.ModelBinding.ValueProviderResult System.Web.ModelBinding.IValueProvider.GetValue(string key)
        {
            return null;
        }

        #endregion
    }
}